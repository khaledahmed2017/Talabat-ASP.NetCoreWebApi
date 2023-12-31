using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;

namespace TalabatCore.OrderService
{

    public interface IPaymentService
    {
        public Task<CustomerBasket> CreateOrUpdatePayment(string basketId);
     
    }
}
