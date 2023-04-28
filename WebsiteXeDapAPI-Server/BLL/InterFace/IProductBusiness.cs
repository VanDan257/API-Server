using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;
using Models.Models;
using Models.ViewModels.Product;
using Models.ViewModels.ProductImage;
using Models.ViewModels.Promotion;

namespace BLL.InterFace
{
    public interface IProductBusiness
    {
        Task<List<ProductVM>> GetAll();

        Task<List<ProductVM>> GetBestSellerProducts();

        Task<List<ProductVM>> GetNewProducts();

        Task<ProductVM> GetByID(long id);

        Task<bool> Create(ProductCreateRequest model);

        Task<bool> CreatePromotion(PromotionCreateRequest model);

        Task<bool> Update(ProductUpdateRequest model);

        Task<bool> Delete(long id);

        Task<bool> UpdatePrice(ProductPriceCreateRequest request);

        Task<string> Upload(IFormFile file, string folder);

        Task<ProductImageViewModel> GetImageByImgID(long ImgID);

        Task<List<ProductImageViewModel>> GetImageByProductId(long imgID);

        Task<List<SpecificationVM>> GetSpecificationsByProductId(long imgID);

        List<ProductVM> Search(int pageIndex, int pageSize, out long total, string Title, long? CateID, decimal fromPrice, decimal toPrice, string option);

        Task<long> AddImage(ProductImageCreateRequest model);

        Task<bool> RemoveImage(long imageId);

        Task<bool> UpdateImage(ProductImageUpdateRequest request);
    }
}