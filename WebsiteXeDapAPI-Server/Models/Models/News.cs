using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class News
    {
        public long NewsID { get; set; }
        public string Title { get; set; }
        public long UserID { get; set; }
        public DateTime PuclicDate { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }

    }
}
