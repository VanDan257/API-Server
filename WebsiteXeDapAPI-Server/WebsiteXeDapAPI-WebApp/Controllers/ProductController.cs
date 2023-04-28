using BLL.InterFace;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;
using Models.ViewModels.Product;
using Models.ViewModels.ProductImage;

namespace WebsiteXeDapAPI_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductBusiness _productBusiness;

        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        [Route("getall")]
        [HttpGet]
        public async Task<List<ProductVM>> GetAll()
        {
            return await _productBusiness.GetAll();
        }

        [Route("get-best-seller-products")]
        [HttpGet]
        public async Task<List<ProductVM>> GetBestSellerProducts()
        {
            return await _productBusiness.GetBestSellerProducts();
        }

        [Route("get-new-products")]
        [HttpGet]
        public async Task<List<ProductVM>> GetNewProducts()
        {
            return await _productBusiness.GetNewProducts();
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public async Task<ProductVM> GetByID(long id)
        {
            return await _productBusiness.GetByID(id);
        }

        [Route("getlistimgbyproid")]
        [HttpGet()]
        public async Task<IActionResult> GetImageByProductId(long proId)
        {
            var image = await _productBusiness.GetImageByProductId(proId);
            if (image == null)
                return BadRequest("Cannot find Image");
            return Ok(image);
        }

        [Route("getlistspecificationsbyproid")]
        [HttpGet()]
        public async Task<IActionResult> GetSpecificationsByProductId(long proId)
        {
            var specifications = await _productBusiness.GetSpecificationsByProductId(proId);
            if (specifications == null)
                return BadRequest("Không có thông số kỹ thuật nào");
            return Ok(specifications);
        }

        [Route("search")]
        [HttpPost]
        public ResponseViewModel Search([FromBody] Dictionary<string, object> formData)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                int? CateID = null;
                decimal fromPrice = 0;
                decimal toPrice = 0;
                if (formData.Keys.Contains("CateID") && !string.IsNullOrEmpty(Convert.ToString(formData["CateID"]))) { CateID = Convert.ToInt32(formData["CateID"].ToString()); }
                string Title = "";
                if (formData.Keys.Contains("Title") && !string.IsNullOrEmpty(Convert.ToString(formData["Title"]))) { Title = Convert.ToString(formData["Title"].ToString()); }
                if (formData.Keys.Contains("FromPrice") && formData["FromPrice"] != null && formData["FromPrice"].ToString() != "")
                {
                    fromPrice = Convert.ToDecimal(formData["FromPrice"].ToString());
                }
                if (formData.Keys.Contains("ToPrice") && formData["ToPrice"] != null && formData["ToPrice"].ToString() != "")
                {
                    toPrice = Convert.ToDecimal(formData["ToPrice"].ToString());
                }
                string option = "";
                if (formData.Keys.Contains("option") && !string.IsNullOrEmpty(Convert.ToString(formData["option"]))) { option = Convert.ToString(formData["option"]); }
                long total = 0;
                var data = _productBusiness.Search(page, pageSize, out total, Title, CateID, fromPrice, toPrice, option);
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
    }
}