# Sql

An easy way to query your sql database

## Getting Started

### Installation

#### Configuration

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddSql("<Insert connectionstring here>")
}
```

### Use

Just inject **ISqlProvider** into the constructor and from there it's [Dapper](https://github.com/StackExchange/Dapper)

```
private ISqlProvider _sqlProvider;
public UserRepo(ISqlProvider sqlProvider)
{
	_sqlProvider = sqlProvider;
}

public Task<IEnumerable<T>> GetUser(int id)
{
	const string sql = "select * from users where Id = @id";

	return _sqlProvider.QueryAsync<T>(sql, new { id });
}
```

## Authors
* **Kenneth G. Pedersen** [Joshlo](https://github.com/joshlo)

## License

[License](https://github.com/joshlo/LazySetup/blob/master/license.md)