using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.DTOs;
using Talabat.Helper;
using TalabatCore.OrderService;

namespace Talabat.Controllers
{

    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOreUpdate(string basketId)
        {
            var basket =await paymentService.CreateOrUpdatePayment(basketId);
            if (basket == null)
                return BadRequest(new ApiResponse(404, "Aproblem with your Basket"));
            return Ok(basket);
        }
    }
}
