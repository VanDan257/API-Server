using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels.Product
{
    public class ProductVM
    {
        public long ProID { get; set; }
        public string Title { get; set; }
        public long? CateID { get; set; }
        public string Desscription { get; set; }
        public string ImagePath { get; set; }
        public float Price { get; set; }
        public List<SpecificationVM> listjson_specifications { get; set; }
    }
}