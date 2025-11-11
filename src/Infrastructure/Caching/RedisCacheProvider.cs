using System.Text.Json;
using Application.Abstractions;
using StackExchange.Redis;

namespace Infrastructure.Caching;
public class RedisCacheProvider(IConnectionMultiplexer mux) : ICacheProvider
{
    public async Task<T?> GetAsync<T>(string key, CancellationToken ct)
    {
        var v = await mux.GetDatabase().StringGetAsync(key);
        return v.HasValue ? JsonSerializer.Deserialize<T>(v!) : default;
    }

    public Task SetAsync<T>(string key, T value, TimeSpan ttl, CancellationToken ct)
      => mux.GetDatabase().StringSetAsync(key, JsonSerializer.Serialize(value), ttl);
}
