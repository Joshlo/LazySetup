# Tracking

## Setup

This is really simple

```
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseTracking(TrackingTypes.All);
}
```

Following TrackingTypes exist:
* All
* Success
* Errors

Last but not least there must be an implementation for **ITrackingHandler**.

## Authors
* **Kenneth G. Pedersen** [Joshlo](https://github.com/joshlo)