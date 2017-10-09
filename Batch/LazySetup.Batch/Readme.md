# Batch

A middleware to handle batch requests

## Getting Started

### Installation

#### Configuration

```
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseBatchRequest(path);
}
```

#### Overwrite Defaults

Path defaults to "/batch"

### Use

#### Request

```
[
	{
		"method": string,
		"RelativeUrl": string,
		"body": json string
	}
]
```

#### Response