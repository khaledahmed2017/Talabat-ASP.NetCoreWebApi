using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities.OrderAggregate;

namespace TalabatCore.Specification
{
    public class OrderByPaymentIdSepicification:BaseSpecification<Order>
    {
        public OrderByPaymentIdSepicification(string paymentId):base(O=>O.PaymentIntentId==paymentId)
        {
            
        }
    }
}
