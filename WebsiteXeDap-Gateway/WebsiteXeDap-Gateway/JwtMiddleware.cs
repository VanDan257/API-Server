using WebsiteXeDap_Gateway.Helpers;
using WebsiteXeDap_Gateway.Helpers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.ViewModels.User;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

using WebsiteXeDap_Gateway.Helpers;

namespace WebsiteXeDap_Gateway
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly IDatabaseHelper _context;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, IConfiguration configuration, IDatabaseHelper context)
        {
            _next = next;
            _appSettings = appSettings.Value;
            //db = new BanMayTinhContext(configuration);
            _context = context;
        }

        public Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Expose-Headers", "*");
            if (!context.Request.Path.Equals("/api/token", StringComparison.Ordinal))
            {
                return _next(context);
            }
            if (context.Request.Method.Equals("POST") && context.Request.HasFormContentType)
            {
                return GenerateToken(context);
            }
            context.Response.StatusCode = 400;
            return context.Response.WriteAsync("Bad request.");
        }

        public async Task GenerateToken(HttpContext context)
        {
            string msgError = "";
            var Taikhoan = context.Request.Form["Taikhoan"].ToString();
            var Matkhau = context.Request.Form["Matkhau"].ToString();
            var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_login_user",
                "@Username", Taikhoan,
                "@Password", Matkhau);

            var user = dt.ConvertTo<AppUser>().FirstOrDefault();
            // return null if user not found
            if (user == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var result = JsonConvert.SerializeObject(new { code = (int)HttpStatusCode.BadRequest, error = "Tài khoản hoặc mật khẩu không đúng" });
                await context.Response.WriteAsync(result);
                return;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
               {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.DenyOnlyWindowsDeviceGroup, user.Password)
               }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tmp = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(tmp);
            var response = new { MaNguoiDung = user.CusID, HoTen = user.CusName, TaiKhoan = user.UserName, Token = token };
            var serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, serializerSettings));
            return;
        }
    }
}