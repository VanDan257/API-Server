using BLL.InterFace;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;
using Models.ViewModels.Order;

namespace WebsiteXeDapAPI_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderBusiness _orderBusiness;

        public OrderController(IOrderBusiness orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        [Route("create-donhang")]
        [HttpPost]
        public async Task<OrderVM> Create([FromBody] OrderVM model)
        {
            await _orderBusiness.Create(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public async Task<OrderVM> GetDatabyID(long id)
        {
            return await _orderBusiness.GetByID(id);
        }
    }
}