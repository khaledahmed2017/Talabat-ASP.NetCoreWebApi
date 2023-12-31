using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.DTOs;
using TalabatCore.Entities;
using TalabatCore.Repositories;

namespace Talabat.Controllers
{
   
    public class BasketController : BaseApiController
    {
        private readonly IBusketRep busket;
        private readonly IMapper mapper;

        public BasketController(IBusketRep busket,IMapper mapper) 
        {
            this.busket = busket;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id) 
        {
            var basket = await busket.GetBusketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket) 
        {
            var ConvertedBasket = mapper.Map<CustomerBasketDto,CustomerBasket>(basket);
            var CreatedOrUpdated =await busket.UpdateBusketAsync(ConvertedBasket);
            return Ok(CreatedOrUpdated);
        }
        [HttpDelete]
        public async Task DeleteBasket(CustomerBasket basket)
        {
             await busket.DeletebasketAsync(basket.Id);
        }
    }
}
