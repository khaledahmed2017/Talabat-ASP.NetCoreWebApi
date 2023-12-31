using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;
using TalabatCore.Entities.OrderAggregate;
using TalabatCore.OrderService;
using TalabatCore.Repositories;
using TalabatCore.Specification;
using TalabatRepository;

namespace TalabatService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBusketRep busketRep;
        private readonly IUnitofwork unitofwork;
        private readonly IPaymentService paymentService;

        public OrderService(IBusketRep busketRep, IUnitofwork unitofwork,IPaymentService paymentService)
        {
            this.busketRep = busketRep;
            this.unitofwork = unitofwork;
            this.paymentService = paymentService;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string BasketId, int DelivaryMethodId, Address shippingAddress)
        {
            //1 Get Basket From Basket Repo
            var basket = await busketRep.GetBusketAsync(BasketId);

            //2 Get Selected Items at Basket From Products Repo
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                //var product= await productRep.GetByIdAsync(item.Id);
                var product = await unitofwork.Repository<Product>().GetByIdAsync(item.Id);
                var ProducItemOredered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(ProducItemOredered,item.Quantity,item.Price);
                orderItems.Add(orderItem);
            }
            //3 Calulate subTotal
            var Subtotal = orderItems.Sum(item => item.Price * item.Quantity);
            //4 Get Delivery Method
            //var DeliveryMethod = await deliveyMethodRep.GetByIdAsync(DelivaryMethodId);
            var DeliveryMethod = await unitofwork.Repository<DeliveryMethod>().GetByIdAsync(DelivaryMethodId);

            //5 Create Order
            var spec = new OrderByPaymentIdSepicification(basket.PaymentIntentId);
            var existingOrders = await unitofwork.Repository<Order>().GetByIdWithSpecificaionAsync(spec);

            if(existingOrders != null)
            {
                unitofwork.Repository<Order>().Delete(existingOrders);
                await paymentService.CreateOrUpdatePayment(basket.Id);
            }

            var order = new Order(buyerEmail, shippingAddress, DeliveryMethod, orderItems, Subtotal,basket.PaymentIntentId);
            //await orderRep.CreateAsync(order);
            // 
            await unitofwork.Repository<Order>().CreateAsync(order);

            var Result= await unitofwork.Complete();
            if (Result <= 0)
            {
                return null;
            }
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
           var deliveryMethods= await unitofwork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethods;
        }

        public Task<Order> GetOrderByIdForUserAsync(int OrderId,string BuyerEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Order>> GetOrderByuserAsync(string BuyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecification(BuyerEmail);
            var orders = await unitofwork.Repository<Order>().GetAllWithSpecificationAsync(spec);

            return orders;


        }
    }
}
