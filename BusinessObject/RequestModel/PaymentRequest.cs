using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.RequestModel
{
    public class PaymentRequest
    {
        public int SubcriptionId { get; set; }
        public string returnUrl { get; set; }
        public string cancelUrl {  get; set; }
    }
}
