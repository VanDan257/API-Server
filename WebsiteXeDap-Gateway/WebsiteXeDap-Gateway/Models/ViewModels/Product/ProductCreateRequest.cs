using Microsoft.AspNetCore.Http;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels.Product
{
    public class ProductCreateRequest
    {
        public string Title { get; set; }
        public Nullable<long> CateID { get; set; }
        public string? Desscription { get; set; }
        public string? Image { get; set; }
        public decimal? Price { get; set; }
        public List<SpecificationVM> listjson_specifications { get; set; }
    }
}