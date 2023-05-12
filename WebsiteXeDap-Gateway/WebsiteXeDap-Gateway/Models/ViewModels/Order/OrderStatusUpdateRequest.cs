using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels.Order
{
    public class OrderStatusUpdateRequest
    {
        public long OrdID { get; set; }
        public string Status { get; set; }
    }
}