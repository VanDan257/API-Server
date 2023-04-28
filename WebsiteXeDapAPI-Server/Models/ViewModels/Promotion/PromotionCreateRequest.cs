using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels.Promotion
{
    public class PromotionCreateRequest
    {
        public long ProID { get; set; }
        public string PromotionalName { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
    }
}