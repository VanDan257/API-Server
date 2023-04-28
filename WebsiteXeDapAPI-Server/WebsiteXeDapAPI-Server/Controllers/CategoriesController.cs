using BLL.InterFace;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace WebsiteXeDapAPI_Server.Controllers
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

        [Route("GetAll")]
        [HttpGet]
        public async Task<List<Category>> GetAll()
        {
            return await _productBusiness.GetAll();
        }

        [Route("GetByID")]
        [HttpGet]
        public async Task<Category> GetByID(long CateID)
        {
            return await _productBusiness.GetById(CateID);
        }

        [Route("get-all-parent-categories")]
        [HttpGet]
        public async Task<List<Category>> GetAllParentCategories()
        {
            return await _productBusiness.GetAllParentCategories();
        }
    }
}