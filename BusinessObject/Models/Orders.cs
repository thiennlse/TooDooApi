using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Orders : BaseEntity
    {
        public int UserId { get; set; }
        public virtual ApplicationUsers? User { get; set; }

        public int SubcriptionsId { get; set; }
        public virtual Subcriptions? Subcriptions { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime CloseDate { get; set; }
        public double? Price { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }

    }
}
