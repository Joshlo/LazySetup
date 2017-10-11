# Redis

A quick way to setup Redis cache

## Getting Started

### Instillation

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddRedis("<Insert RedisHost here>");
}
```

### Use

Just inject **IRedisClient** into the constructor.
The client has 4 methods.
* **GetAsync<T>** Gets the value by the provided key and maps it to T
* **SetAsync<T>** Sets the value gotten from a Func<T>
* **SetAsync** Sets the value from the provided object
* **GetSetAsync<T>** Tries to get the value by the provided key. If no data is found it executes the Func<T> and stores it before returning the data