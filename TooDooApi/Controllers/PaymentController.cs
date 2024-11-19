using BusinessObject.Models;
using BusinessObject.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Service.Interface;

namespace TooDooApi.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ISubcriptionService _subcriptionService;

        public PaymentController(IPaymentService paymentService, ISubcriptionService subcriptionService)
        {
            _paymentService = paymentService;
            _subcriptionService = subcriptionService;
        }

        [HttpPost("/create-payment-link")]
        public async Task<IActionResult> Checkout([FromBody] PaymentRequest model)
        {
            try
            {
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                Subcriptions plan = await _subcriptionService.GetById(model.SubcriptionId);
                ItemData item = new ItemData(plan.Name, 1, (int)plan.Price);
                List<ItemData> items = new List<ItemData>();
                items.Add(item);
                PaymentData paymentData = new PaymentData
                    (
                    orderCode,
                    (int)plan.Price,
                    "Thanh toan 2DOO Premium",
                    items,
                    model.cancelUrl,
                    model.returnUrl
                    );
                CreatePaymentResult createPayment = await _paymentService.createPaymentLink(paymentData);

                return Ok(createPayment.checkoutUrl);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
