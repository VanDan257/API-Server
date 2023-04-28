using BLL.InterFace;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;
using Models.ViewModels.Order;

namespace WebsiteXeDapAPI_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness _orderBusiness;

        public OrderController(IOrderBusiness orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        [Route("create")]
        [HttpPost]
        public async Task<bool> Create(OrderVM model)
        {
            return await _orderBusiness.Create(model);
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<bool> Delete(long id)
        {
            return await _orderBusiness.Delete(id);
        }

        [Route("order-status-update")]
        [HttpPut]
        public async Task<bool> UpdateStatusOrder([FromBody]OrderStatusUpdateRequest model)
        {
            return await _orderBusiness.UpdateStatusOrder(model);
        }

        [Route("get-by-id")]
        [HttpGet]
        public async Task<OrderVM> GetByID(long id)
        {
            return await _orderBusiness.GetByID(id);
        }

        [Route("getall")]
        [HttpGet]
        public async Task<List<OrderVM>> GetAll()
        {
            return await _orderBusiness.GetAll();
        }

        [Route("search")]
        [HttpPost]
        public ResponseViewModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseViewModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string CusName = "";
                string Phone = "";
                string Email = "";
                if (formData.Keys.Contains("CusName") && !string.IsNullOrEmpty(Convert.ToString(formData["CusName"]))) { CusName = Convert.ToString(formData["CusName"]); }
                if (formData.Keys.Contains("Phone") && !string.IsNullOrEmpty(Convert.ToString(formData["Phone"]))) { Phone = Convert.ToString(formData["Phone"]); }
                if (formData.Keys.Contains("Email") && !string.IsNullOrEmpty(Convert.ToString(formData["Email"]))) { Email = Convert.ToString(formData["Email"]); }
                DateTime? fr_NgayDat = null;
                if (formData.Keys.Contains("fr_NgayDat") && formData["fr_NgayDat"] != null && formData["fr_NgayDat"].ToString() != "")
                {
                    var dt = Convert.ToDateTime(formData["fr_NgayDat"].ToString());
                    fr_NgayDat = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
                }
                DateTime? to_NgayDat = null;
                if (formData.Keys.Contains("to_NgayDat") && formData["to_NgayDat"] != null && formData["to_NgayDat"].ToString() != "")
                {
                    var dt = Convert.ToDateTime(formData["to_NgayDat"].ToString());
                    to_NgayDat = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
                }
                long total = 0;
                var data = _orderBusiness.Search(page, pageSize, out total, CusName, Phone, Email, fr_NgayDat, to_NgayDat);
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