# Elasticsearch

This consists of two parts.
One is setting up Elasticsearch NEST and the other is the possibility to link indexes in C#

## Getting Started

### Installation

#### Configuration

There are four ways to setup Elasticsearch.
The fist two are for a single node cluster and the last two is for a cluster with multiple nodes
This also sets up the ElasticLinkHandler.

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddElasticsearch("<Insert url here>", int maxDepth);
	services.AddElasticsearch("<Insert url here>", "<username>", "<password>", int maxDepth);
	services.AddElasticsearch(new List<string>(){"<Insert url here>"}, int maxDepth);
	services.AddElasticsearch(new List<string>(){"<Insert url here>"}, "<username>", "<password>", int maxDepth);
}
```

MaxDepth defaults to 3, but can be overwritten here in the setup and again when calling the method.

### Use

#### NEST

If you want to use the NEST module then just inject **IElasticClient** and from there it's [NEST](https://github.com/elastic/elasticsearch-net)

```
private IElasticClient _elasticClient;
public UserRepo(IElasticClient elasticClient)
{
	_elasticClient = elasticClient;
}
```

#### Linking

In our example we have two indexes.

**users**
{
	"id": int
	...
	...
}

**addresses**
{
	"id": int,
	"userId": int
	...
	...
}

When we want to link a property, we decorate it with an attribute called **ElasticLink**

ElasticLink takes following
* **targetIndex** - string - Name on the index the property should link to
* **targetType** - string - Name on the type in the index.
* **targetField** - string - Name on the field on the linked index that it should match against.
* **valueField** - string - Name on the property in the current class where to find the value
* **size** - int - How many records it should return
* **sort** - IList<ISort> - The sort order of the linked values

```
public class User
{
	public int Id { get; set; }

	[ElasticLink("addresses", "address", "id", "id", 1, null)]
	public Address { get; set; }
}

public class Address
{

}
```

When we want to use it, we inject **IElasticLinkHandler**.
```
public class UserSearch
{
	private IElasticLinkHandler _linkHandler;
	public UserSearch(IElasticLinkHandler linkHandler)
	{
		_linkHandler = linkHandler;
	}

	public async Task Method(CancellationToken cancellationToken)
	{
		var result = await _linkHandler.LinkedSearchAsync<User>(s => s.Index("users").MatchAll(), cancellationToken, 3);
	}
}
```

The response contains three properties
* **Documents** - IEnumerable<T> - The result from the query
* **TotalDocuments** - long - Total documents found matching the query. This does not count the linked documents
* **QueryTime** - TimeSpan - When the LinkedSearchAsync<> is called, a timer starts to give the total time to retrieve the documents and all the linked documents.