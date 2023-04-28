using BLL.InterFace;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;

namespace WebsiteXeDapAPI_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerBusiness _customerBusiness;

        public CustomerController(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        [Route("getall")]
        [HttpGet]
        public async Task<List<Customer>> GetAll()
        {
            return await _customerBusiness.GetAll();
        }

        [Route("create-customer")]
        [HttpPost]
        public async Task<Customer> Create([FromBody] Customer model)
        {
            await _customerBusiness.Create(model);
            return model;
        }

        [Route("update-customer")]
        [HttpPost]
        public async Task<Customer> Update([FromBody] Customer model)
        {
            await _customerBusiness.Update(model);
            return model;
        }

        [Route("delete-customer")]
        [HttpPost]
        public async Task<IActionResult> Delete(long CusID)
        {
            if (await _customerBusiness.Delete(CusID))
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public async Task<Customer> GetByID(long id)
        {
            return await _customerBusiness.GetByID(id);
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
                long? CusID = null;
                string CusName = "";
                string option = "";
                long total = 0;
                if (formData.Keys.Contains("CusID") && !string.IsNullOrEmpty(Convert.ToString(formData["CusID"])))
                {
                    CusID = Convert.ToInt32(formData["CusID"]);
                }
                if (formData.Keys.Contains("CusName") && !string.IsNullOrEmpty(Convert.ToString(formData["CusName"])))
                {
                    CusName = Convert.ToString(formData["CusName"]);
                }
                if (formData.Keys.Contains("option") && !string.IsNullOrEmpty(Convert.ToString(formData["option"]))) { option = Convert.ToString(formData["option"]); }
                var data = _customerBusiness.Search(page, pageSize, out total, CusID, CusName, option);
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