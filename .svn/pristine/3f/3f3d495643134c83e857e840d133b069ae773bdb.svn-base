using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace CdplATS.API.Configuration
{
    public class RedisCacheHelper
    {
        private readonly IDistributedCache _cache;

        public RedisCacheHelper(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };

            var jsonData = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, jsonData, options);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var jsonData = await _cache.GetStringAsync(key);
            return jsonData != null ? JsonSerializer.Deserialize<T>(jsonData) : default;
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}

