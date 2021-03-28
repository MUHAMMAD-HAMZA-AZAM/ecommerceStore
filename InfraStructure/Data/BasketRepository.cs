﻿using Core.Interfaces;
using Core.PocoEntities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfraStructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await _database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string BasketId)
        {
            var data = await _database.StringGetAsync(BasketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

            public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var created = await _database.StringSetAsync(customerBasket.Id,
                JsonSerializer.Serialize(customerBasket),TimeSpan.FromDays(30));
            if (!created) return null;
            return await GetBasketAsync(customerBasket.Id);
        }
    }
}
