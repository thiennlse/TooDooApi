using Microsoft.Extensions.Configuration;
using Net.payOS;
using Net.payOS.Types;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PaymentService : IPaymentService
    {
        public async Task<CreatePaymentResult> createPaymentLink(PaymentData paymentData)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            var client_id = configuration["Environment:PAYOS_CLIENT_ID"];
            var api_key = configuration["Environment:PAYOS_API_KEY"];
            var checkSum_key = configuration["Environment:PAYOS_CHECKSUM_KEY"];

            PayOS payOS = new PayOS(client_id, api_key, checkSum_key);

            PaymentData payment = new PaymentData(
                paymentData.orderCode,
                paymentData.amount,
                "Payment Order",
                paymentData.items,
                paymentData.cancelUrl,
                paymentData.returnUrl
                );

            CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);
            return createPayment;

        }
    }
}
