using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;
using TalabatCore.Entities.OrderAggregate;
using TalabatCore.OrderService;
using TalabatCore.Repositories;
using TalabatRepository;
using TalabatRepository.GenericRepository;
using Product = TalabatCore.Entities.Product;

namespace TalabatService.Services
{

    public class PaymentService : IPaymentService

    {
        private readonly IUnitofwork unitofwork;
        private readonly IConfiguration configuration;
        private readonly IBusketRep busketRep;

        public PaymentService(IUnitofwork unitofwork, IConfiguration configuration, IBusketRep busketRep)
        {
            this.unitofwork = unitofwork;
            this.configuration = configuration;
            this.busketRep = busketRep;
        }

        public async Task<CustomerBasket> CreateOrUpdatePayment(string basketId)
        {
            StripeConfiguration.ApiKey = configuration["stripe:SecretKey"];
            var basket = await busketRep.GetBusketAsync(basketId);
            if (basket == null)
            {
                return null;
            }
            var ShippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod = await unitofwork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                ShippingPrice = DeliveryMethod.Cost;
                basket.ShippingPrice = DeliveryMethod.Cost;
            }
            foreach (var item in basket.Items)
            {
                var product = await unitofwork.Repository<Product>().GetByIdAsync(item.Id);
                if (item.Price != product.Price)
                {
                    item.Price = product.Price;
                }
            }
            var service = new PaymentIntentService();
            PaymentIntent intent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Quantity * item.Price * 100) + (long)ShippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" },
                };
                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;

            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Quantity * item.Price * 100) + (long)ShippingPrice * 100,
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }
            await busketRep.UpdateBusketAsync(basket);
            return basket;

        }

    }
}