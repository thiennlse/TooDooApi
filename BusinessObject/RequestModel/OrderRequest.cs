using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.RequestModel
{
    public class OrderRequest
    {
        public int UserId { get; set; }
        public int SubcriptionId { get; set; }
        public DateTime CloseDate { get; set; }
        public double? Price { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
    }
}
