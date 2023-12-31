using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;

namespace TalabatCore.Repositories
{
    public interface IBusketRep
    {
        public Task<bool> DeletebasketAsync(string Id); 
        public Task<CustomerBasket> GetBusketAsync(string Id); 
        public Task<CustomerBasket> UpdateBusketAsync(CustomerBasket basket);
    }
}
