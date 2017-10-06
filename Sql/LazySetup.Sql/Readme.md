# Sql setup

### Startup.cs
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddSql("<Insert connectionstring here>")
}
```

And when to use it, you just inject ```ISqlProvider``` into the constructor and from there it's Dapper
