using BLL.InterFace;
using DAL.Interface;
using Microsoft.AspNetCore.Http;
using Models.Models;
using Models.ViewModels.Product;
using Models.ViewModels.ProductImage;
using Models.ViewModels.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductBusiness : IProductBusiness
    {
        private IProductRepository _res;

        public ProductBusiness(IProductRepository res)
        {
            _res = res;
        }

        public async Task<List<ProductVM>> GetAll()
        {
            return await _res.GetAll();
        }

        public async Task<bool> Create(ProductCreateRequest model)
        {
            return await _res.Create(model);
        }

        public async Task<bool> CreatePromotion(PromotionCreateRequest model)
        {
            return await _res.CreatePromotion(model);
        }

        public async Task<bool> Delete(long id)
        {
            return await _res.Delete(id);
        }

        public async Task<ProductVM> GetByID(long id)
        {
            return await _res.GetById(id);
        }

        public List<ProductVM> Search(int pageIndex, int pageSize, out long total, string Title, long? CateID, decimal fromPrice, decimal toPrice, string option)
        {
            return _res.Search(pageIndex, pageSize, out total, Title, CateID, fromPrice, toPrice, option);
        }

        public async Task<bool> Update(ProductUpdateRequest model)
        {
            return await _res.Update(model);
        }

        public async Task<List<ProductVM>> GetBestSellerProducts()
        {
            return await _res.GetBestSellerProducts();
        }

        public async Task<ProductImageViewModel> GetImageByImgID(long ImgID)
        {
            return await _res.GetImageByImgID(ImgID);
        }

        public async Task<List<ProductImageViewModel>> GetImageByProductId(long imgID)
        {
            return await _res.GetImageByProductId(imgID);
        }

        public async Task<List<SpecificationVM>> GetSpecificationsByProductId(long imgID)
        {
            var specifications = await _res.GetSpecificationsByProductId(imgID);
            return specifications;
        }

        public async Task<string> Upload(IFormFile file, string folder)
        {
            return await _res.Upload(file, folder);
        }

        public async Task<bool> UpdatePrice(ProductPriceCreateRequest request)
        {
            return await _res.UpdatePrice(request);
        }

        public async Task<long> AddImage(ProductImageCreateRequest model)
        {
            return await _res.AddImage(model);
        }

        public async Task<bool> RemoveImage(long imageId)
        {
            return await _res.RemoveImage(imageId);
        }

        public Task<bool> UpdateImage(ProductImageUpdateRequest request)
        {
            return _res.UpdateImage(request);
        }

        public Task<List<ProductVM>> GetNewProducts()
        {
            return _res.GetNewProducts();
        }
    }
}