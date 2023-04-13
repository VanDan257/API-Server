using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class DetailPromotion
    {
        public long PromotionalDetailID { get; set; }
        public long PromotionalID { get; set; }
        public long ProID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
    }
}