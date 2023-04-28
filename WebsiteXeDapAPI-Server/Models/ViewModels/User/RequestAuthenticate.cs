using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels.User
{
    public class RequestAuthenticate
    {
        public long CusID { get; set; }
        public string CusName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string token { get; set; }
    }
}