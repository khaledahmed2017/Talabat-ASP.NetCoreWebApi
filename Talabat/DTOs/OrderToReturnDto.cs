using System.Collections.Generic;
using System;
using TalabatCore.Entities.OrderAggregate;

namespace Talabat.DTOs
{
    public class OrderToReturnDto
    {
        public string BuyerEmail { get; set; }
       // public DateTimeOffset OrderDate { get; set; }
        public string status { get; set; }
        public Address ShipingAddress { get; set; }
        //public DeliveryMethod DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public string DeliveryMethodShortName { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal SubTotal { get; set; }
        //public decimal GetTotal => SubTotal + DeliveryMethodCost;
        public decimal Total { get;set; }
    }
}
