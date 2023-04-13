using BLL.InterFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;
using Models.ViewModels.Product;
using Models.ViewModels.ProductImage;
using Models.ViewModels.Promotion;

namespace WebsiteXeDapAPI_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductBusiness _productBusiness;

        public ProductsController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<List<ProductVM>> GetAll()
        {
            return await _productBusiness.GetAll();
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public async Task<ProductVM> GetByID(long id)
        {
            return await _productBusiness.GetByID(id);
        }

        [Route("create-product")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequest model)
        {
            await _productBusiness.Create(model);
            return Ok();
        }

        [Route("update-product")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest model)
        {
            await _productBusiness.Update(model);
            return Ok();
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            await _productBusiness.Delete(id);
            return Ok();
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            string folder = "images";
            try
            {
                if (file.Length > 0)
                {
                    string filePath = await _productBusiness.Upload(file, folder);
                    return Ok(new { filePath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể upload tệp");
            }
        }

        [Route("update-price")]
        [HttpPost]
        public async Task<IActionResult> UpdatePrice([FromBody] ProductPriceCreateRequest request)
        {
            if (await _productBusiness.UpdatePrice(request))
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("create-promotion")]
        [HttpPost]
        public async Task<IActionResult> CreatePromotion([FromBody] PromotionCreateRequest request)
        {
            if (await _productBusiness.CreatePromotion(request))
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("add-image")]
        [HttpPost]
        public async Task<IActionResult> AddImage([FromBody] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _productBusiness.AddImage(request);
            if (imageId == 0)
                return BadRequest();

            //var image = await _productBusiness.GetImageByID(imageId);

            return Ok();
        }

        [Route("get-img-by-imgid")]
        [HttpGet()]
        public async Task<IActionResult> GetImageByImgID(long ImgID)
        {
            var image = await _productBusiness.GetImageByImgID(ImgID);
            if (image == null)
                return BadRequest("Cannot find image");
            return Ok(image);
        }

        [Route("getlistimgbyproid")]
        [HttpGet()]
        public async Task<IActionResult> GetImageByProductId(long proId)
        {
            var image = await _productBusiness.GetImageByProductId(proId);
            if (image == null)
                return BadRequest("Cannot find images");
            return Ok(image);
        }

        [Route("delete-image")]
        [HttpDelete]
        public async Task<IActionResult> DeleteImage(long imgID)
        {
            if (await _productBusiness.RemoveImage(imgID))
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("update-image")]
        [HttpPost]
        public async Task<IActionResult> UpdateImage([FromBody] ProductImageUpdateRequest request)
        {
            if (await _productBusiness.UpdateImage(request))
            {
                return Ok();
            }
            return BadRequest();
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
                    var dt = Convert.ToDateTime(formData["FromPrice"].ToString());
                }
                if (formData.Keys.Contains("ToPrice") && formData["ToPrice"] != null && formData["ToPrice"].ToString() != "")
                {
                    var dt = Convert.ToDateTime(formData["ToPrice"].ToString());
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