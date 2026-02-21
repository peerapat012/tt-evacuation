namespace tt_api.Services.Caching;

public interface IRedisService
{
    T? GetData<T>(string key);
    void SetData<T>(string key, T value);
    void DeleteData(string key);
}