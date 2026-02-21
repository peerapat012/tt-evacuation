using System.Text.Json;
using StackExchange.Redis;

namespace tt_api.Services.Caching;

public class RedisService(IConnectionMultiplexer redis) : IRedisService
{
    private readonly IDatabase _db = redis.GetDatabase();
    
    public T? GetData<T>(string key)
    {
        var data = (string?)_db?.StringGet(key);
        if (string.IsNullOrEmpty(data)) return default(T);
        
        return JsonSerializer.Deserialize<T>(data!);
    }

    public void SetData<T>(string key, T value)
    {
        if(value == null) return;
        var expireTime = TimeSpan.FromMinutes(1);
        _db?.StringSet(key, JsonSerializer.Serialize(value), expireTime);
    }
}