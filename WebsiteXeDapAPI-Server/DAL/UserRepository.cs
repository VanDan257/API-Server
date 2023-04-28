using DAL.Helper.Interfaces;
using DAL.Interface;
using Microsoft.IdentityModel.Tokens;
using Models.Helpers;
using Models.Models;
using Models.ViewModels.Common;
using Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Data;
using Microsoft.Extensions.Options;
using DAL.Helper;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseHelper _context;
        private readonly AppSettings _appSettings;

        public UserRepository(IDatabaseHelper context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<string> Login(UserModel model)
        {
            string msgError = "";
            var dt = _context.ExecuteSProcedureReturnDataTable(out msgError, "sp_login_user",
                "@Username", model.UserName,
                "@Password", model.Password);
            if (dt == null) return ("Tài khoản hoặc mật khẩu không đúng");

            var user = dt.ConvertTo<AppUser>().FirstOrDefault();

            // authentication successful so generate jwt token
            var tokenHanlder = new JwtSecurityTokenHandler();
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
            var tmp = tokenHanlder.CreateToken(tokenDescriptor);
            var token = tokenHanlder.WriteToken(tmp);
            return token;
        }
    }
}