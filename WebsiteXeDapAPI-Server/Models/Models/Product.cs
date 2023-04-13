using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Product
    {
        public long ProID { get; set; }
        public string Title { get; set; }
        public Nullable<long> CateID { get; set; }
        public string Desscription { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}