using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalabatCore.Entities.OrderAggregate
{
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
            
        }

        public OrderItem(ProductItemOrdered productItemOrder, int quantity, decimal price)
        {
            ProductItemOrder = productItemOrder;
            Quantity = quantity;
            Price = price;
        }

        public ProductItemOrdered ProductItemOrder { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
