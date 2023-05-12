using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Order
    {
        public long OrdID { get; set; }
        public Nullable<long> CusID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        public string Status { get; set; }
        public Nullable<long> TotalPay { get; set; }
        public string Note { get; set; }
        public string Phone { get; set; }
        public string Recipient { get; set; }
    }
}