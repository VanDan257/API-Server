using BLL.InterFace;
using DAL.Interface;
using Models.ViewModels.Common;
using Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Login(UserModel model)
        {
            return await _userRepository.Login(model);
        }
    }
}