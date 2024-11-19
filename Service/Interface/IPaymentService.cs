using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IPaymentService
    {
        public Task<CreatePaymentResult> createPaymentLink(PaymentData paymentData);
    }
}
