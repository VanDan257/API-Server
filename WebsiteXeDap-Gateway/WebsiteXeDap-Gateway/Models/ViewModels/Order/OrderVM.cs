using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels.Order
{
    public class OrderVM
    {
        public long OrdID { get; set; }
        //public Nullable<long> CusID { get; set; }
        //public DateTime OrderDate { get; set; }

        public string? Address { get; set; }
        public string? Email { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Status { get; set; }
        public long? TotalPay { get; set; }
        public string? Note { get; set; }
        public string? Phone { get; set; }
        public string? CusName { get; set; }
        public bool? Paid { get; set; }

        //public CustomerOrderCreateRequest objectjson_customer { get; set; }
        public List<OrderDetailCreateRequest> listjson_detail { get; set; }
    }
}