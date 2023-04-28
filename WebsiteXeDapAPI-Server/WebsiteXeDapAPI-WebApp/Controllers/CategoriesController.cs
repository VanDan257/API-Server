using BLL.InterFace;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace WebsiteXeDapAPI_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryBusiness _productBusiness;

        public CategoriesController(ICategoryBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        [Route("getall")]
        [HttpGet]
        public async Task<List<Category>> GetAll()
        {
            return await _productBusiness.GetAll();
        }

        [Route("get-all-parent-categories")]
        [HttpGet]
        public async Task<List<Category>> GetAllParentCategories()
        {
            return await _productBusiness.GetAllParentCategories();
        }
    }
}