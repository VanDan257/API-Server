using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class OrderDetail
    {
        public long ODID { get; set; }
        public long OrdID { get; set; }
        public Nullable<long> ProID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> Price { get; set; }
    }
}
