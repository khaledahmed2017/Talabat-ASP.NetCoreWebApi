using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TalabatCore.Entities;
using TalabatCore.Repositories;

namespace TalabatRepository.GenericRepository
{
    public class BasketRepository : IBusketRep
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
           this._database=redis.GetDatabase();
            
        }
        public async Task<bool> DeletebasketAsync(string Id)
        {
            return await _database.KeyDeleteAsync(Id);
        }

        public async Task<CustomerBasket> GetBusketAsync(string Id)
        {
           var busket =await _database.StringGetAsync(Id);
            return busket.IsNullOrEmpty?null:JsonSerializer.Deserialize<CustomerBasket>(busket);
        }

        public async Task<CustomerBasket> UpdateBusketAsync(CustomerBasket busket)
        {
            var CreatedOrUpdated = await _database.StringSetAsync(busket.Id,JsonSerializer.Serialize(busket),TimeSpan.FromDays(30));
            if (CreatedOrUpdated == false) return null;
            return await GetBusketAsync(busket.Id);
        }
    }
}
