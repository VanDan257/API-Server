using DAL.Helper;
using DAL.Helper.Interfaces;
using DAL.Interface;
using Microsoft.AspNetCore.Http;
using Models.Code;
using Models.Models;
using Models.ViewModels.Product;
using Models.ViewModels.ProductImage;
using Models.ViewModels.Promotion;
using System.Data;

namespace DAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDatabaseHelper _context;
        private readonly ITools _tools;

        public ProductRepository(IDatabaseHelper context, ITools tools)
        {
            _context = context;
            _tools = tools;
        }

        public async Task<bool> Create(ProductCreateRequest model)
        {
            string msgError = "";
            DateTime DateCreated = DateTime.Now;
            try
            {
                //string imgPath = await Upload(model.Image, folder);
                var result = _context.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_product_create",
                    "@Title", model.Title,
                    "@CateID", model.CateID,
                    "@Desscription", model.Desscription,
                    "@Image", model.Image,
                    "@DateCreated", DateCreated,
                    "@EndDate", Convert.ToDateTime("26/06/2023"),
                    "@Price", model.Price,
                    "@listjson_specifications", model.listjson_specifications != null ? MessageConvert.SerializeObject(model.listjson_specifications) : null);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(long id)
        {
            string msgError = "";
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_product_delete",
                    "@ProID", id);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductVM>> GetAll()
        {
            string msgError = "";
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_product_getall");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ProductVM>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductVM>> GetBestSellerProducts()
        {
            string msgError = "";
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetBestSellerProducts",
                    "@num", 8);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ProductVM>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductVM> GetById(long id)
        {
            string msgError = "";
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_product_get_by_id", "@ProID", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ProductVM>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductVM> Search(int pageIndex, int pageSize, out long total, string Title, long? CateID, decimal fromPrice, decimal toPrice, string option)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_product_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@Title", Title,
                    "@CateID", CateID,
                    "@from_Price", fromPrice,
                    "@to_Price", toPrice,
                    "@option", option);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<ProductVM>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(ProductUpdateRequest model)
        {
            string msgError = "";
            try
            {
                var result = _context.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_product_update",
                    "@ProID", model.ProID,
                    "@Title", model.Title,
                    "@CateID", model.CateID,
                    "@Desscription", model.Desscription);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SpecificationVM>> GetSpecificationsByProductId(long ProId)
        {
            string msgError = "";
            try
            {
                try
                {
                    var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_getlistspecifications",
                        "@proId", ProId);
                    if (!string.IsNullOrEmpty(msgError))
                        throw new Exception(msgError);
                    return dt.ConvertTo<SpecificationVM>().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductImageViewModel> GetImageByImgID(long ImgID)
        {
            string msgError = "";
            try
            {
                try
                {
                    var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "[sp_imageproduct_get]",
                        "@ImgID", ImgID);
                    if (!string.IsNullOrEmpty(msgError))
                        throw new Exception(msgError);
                    return dt.ConvertTo<ProductImageViewModel>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductImageViewModel>> GetImageByProductId(long proId)
        {
            string msgError = "";
            try
            {
                try
                {
                    var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_listimgbyidproduct",
                        "@proId", proId);
                    if (!string.IsNullOrEmpty(msgError))
                        throw new Exception(msgError);
                    return dt.ConvertTo<ProductImageViewModel>().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> Upload(IFormFile file, string folder)
        {
            string filePath = $"{folder}/{file.FileName.Replace("-", "_").Replace("%", "")}";
            var fullPath = _tools.CreatePathFile(filePath);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fullPath;
            //throw new Exception();
        }

        public async Task<bool> UpdatePrice(ProductPriceCreateRequest request)
        {
            string msgError = "";
            try
            {
                var result = _context.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_priceproduct_update",
                    "@ProID", request.ProID,
                    "@newPrice", request.Price,
                    "@CreateDate", DateTime.Now,
                    "@EndDate", request.EndDate);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<long> AddImage(ProductImageCreateRequest model)
        {
            string msgError = "";
            string msgError2 = "";
            try
            {
                //string ImagePath = await Upload(model.ImageFile, folder);
                var result = _context.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_addimageproduct",
                    "@ProID", model.ProID,
                    "@ImagePath", model.ImageFile,
                    "@Caption", model.Caption,
                    "@IsDefault", model.IsDefault,
                    "@DateCreated", DateTime.UtcNow,
                    "@SortOrder", model.SortOrder);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                var query = "Select TOP(1) ImgID AS ID from DetailImageProduct Order By ImgID DESC";
                DataTable dt = _context.ExecuteQueryToDataTable(query, out msgError2);
                if (!string.IsNullOrEmpty(msgError2))
                {
                    throw new Exception(Convert.ToString(msgError2));
                }
                List<long> ImgID = new List<long>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    ImgID.Add((long)dataRow[0]);
                }
                return ImgID[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveImage(long imageId)
        {
            string msgError = "";
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_productimage_delete",
                    "@ImgID", imageId);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateImage(ProductImageUpdateRequest request)
        {
            string msgError = "";
            try
            {
                var result = _context.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_productimage_update",
                    "@ImgID", request.ImgID,
                    "@ImagePath", request.ImageFile,
                    "@Caption", request.Caption,
                    "@IsDefault", request.IsDefault,
                    "@DateCreated", DateTime.UtcNow,
                    "@SortOrder", request.SortOrder,
                    "@FileSize", request.ImageFile.Length);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductVM>> GetNewProducts()
        {
            string msgError = "";
            try
            {
                var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetNewProducts",
                    "@num", 8);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ProductVM>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreatePromotion(PromotionCreateRequest model)
        {
            string msgError = "";
            DateTime StartDate = DateTime.Now;
            try
            {
                //string imgPath = await Upload(model.Image, folder);
                var result = _context.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_promotion_create",
                    "@ProID", model.ProID,
                    "@ProName", model.PromotionalName,
                    "@Description", model.Description,
                    "@EndDate", model.EndDate);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}