# Elasticsearch

## Getting Started

### Installation

#### Configuration

There are four ways to setup Elasticsearch.
The fist two are for a single node cluster and the last two is for a cluster with multiple nodes

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddElasticsearch("<Insert url here>");
	services.AddElasticsearch("<Insert url here>", "<username>", "<password>");
	services.AddElasticsearch(new List<string>(){"<Insert url here>"});
	services.AddElasticsearch(new List<string>(){"<Insert url here>"}, "<username>", "<password>");
}
```

### Use

Just inject **IElasticClient** into the constructor and from there it's [NEST](https://github.com/elastic/elasticsearch-net)

```
private IElasticClient _elasticClient;
public UserRepo(IElasticClient elasticClient)
{
	_elasticClient = elasticClient;
}
```