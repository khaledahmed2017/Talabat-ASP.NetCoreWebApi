using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Helper;
using TalabatRepository.Context;

namespace Talabat.Controllers
{

    public class BuggController : BaseApiController
    {
        private readonly StoreContext context;

        public BuggController(StoreContext context)
        {
            this.context = context;
        }
        [HttpGet("ServerError")]
        //{{BaseUrl}}/api/Bugg/ServerError
        public ActionResult GetServerError()
        {
            var products = context.Products.Find(100);
            var ProductToReturn = products.ToString();
            return Ok(ProductToReturn);
        }
        [HttpGet("NotFoundError")]
        public ActionResult NotFoundError()
        {
            var products = context.Products.Find(100);
            if (products == null)
            {
                return NotFound(new ApiResponse(404));
            }
            else
                return Ok(products);
        }
        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("BadRequest/{id}")]
        //{{BaseUrl}}/Bugg/BadRequest/1
        public ActionResult GetBadRequest(int id)
        {
            var products = context.Products.Find(id);
            return Ok(products);
        }
    }
}
