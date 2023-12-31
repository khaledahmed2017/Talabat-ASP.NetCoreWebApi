using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalabatCore.Entities.OrderAggregate
{
    public class Order:BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shipingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal,string PaymentIntent)
        {
            BuyerEmail = buyerEmail;
            ShipingAddress = shipingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = PaymentIntent;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus status { get; set; } = OrderStatus.Pending;
        public Address ShipingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GetTotal => SubTotal + DeliveryMethod.Cost;
    }
}
