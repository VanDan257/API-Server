using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels.Product;
using Models.ViewModels.ProductImage;
using Models.ViewModels.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IProductRepository
    {
        Task<List<ProductVM>> GetAll();

        Task<List<ProductVM>> GetBestSellerProducts();

        Task<List<ProductVM>> GetNewProducts();

        List<ProductVM> Search(int pageIndex, int pageSize, out long total, string Title, long? CateID, decimal fromPrice, decimal toPrice, string option);

        Task<bool> Create(ProductCreateRequest model);

        Task<bool> CreatePromotion(PromotionCreateRequest model);

        Task<bool> Update(ProductUpdateRequest model);

        Task<bool> UpdatePrice(ProductPriceCreateRequest request);

        Task<bool> Delete(long id);

        Task<ProductVM> GetById(long id);

        Task<long> AddImage(ProductImageCreateRequest model);

        Task<bool> RemoveImage(long imageId);

        Task<bool> UpdateImage(ProductImageUpdateRequest request);

        Task<string> Upload(IFormFile file, string folder);

        Task<ProductImageViewModel> GetImageByImgID(long ImgID);

        Task<List<ProductImageViewModel>> GetImageByProductId(long proId);

        Task<List<SpecificationVM>> GetSpecificationsByProductId(long proId);
    }
}