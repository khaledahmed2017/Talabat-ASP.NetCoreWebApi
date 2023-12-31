using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities.OrderAggregate;

namespace TalabatCore.Specification
{
    public class OrderWithItemsAndDeliveryMethodSpecification:BaseSpecification<Order>
    {
        public OrderWithItemsAndDeliveryMethodSpecification(string buyerEmail):base(o=>o.BuyerEmail==buyerEmail)
        {
            Includes.Add(o => o.Items);
            Includes.Add(o => o.DeliveryMethod);
        }
    }
}
