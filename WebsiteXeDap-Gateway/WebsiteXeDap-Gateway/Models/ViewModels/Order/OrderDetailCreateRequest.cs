using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels.Order
{
    public class OrderDetailCreateRequest
    {
        public long ProID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}