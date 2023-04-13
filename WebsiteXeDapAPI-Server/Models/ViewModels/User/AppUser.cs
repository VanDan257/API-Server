using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels.User
{
    public class AppUser
    {
        public long CusID { get; set; }

        public string CusName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Image { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}