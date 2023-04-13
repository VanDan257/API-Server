using BLL.InterFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels.Common;
using Models.ViewModels.User;

namespace WebsiteXeDapAPI_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserModel model)
        {
            try
            {
                string token = await _userBusiness.Login(model);
                if (token == null)
                    return BadRequest();
                else
                    return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}