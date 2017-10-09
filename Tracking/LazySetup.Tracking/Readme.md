# Tracking

## Getting Started

### Prerequisites

Before this middleware can work, an implementation for **ITrackingHandler** must be made.

### Installation

This is really simple

#### Configuration

```
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseTracking(TrackingTypes.All);
}
```

Following TrackingTypes exist:
* All
* Success - Status code 200 - 299
* Errors - Anything that isn't in 200 - 299 status code


## Authors
* **Kenneth G. Pedersen** [Joshlo](https://github.com/joshlo)

## License

[License](https://github.com/joshlo/LazySetup/blob/master/license.md)