using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities.OrderAggregate;

namespace TalabatCore.OrderService
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, string BasketId, int DelivaryMethodId, Address shippingAddress);
        Task<IReadOnlyList<Order>> GetOrderByuserAsync(string BuyerEmail);
        Task<Order> GetOrderByIdForUserAsync(int OrderId,string BuyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();
    }
}
