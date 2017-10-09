# Token 

## Getting Started

### Prerequisites

Before this middleware can work, and implementation for **ITokenHandler** must be made.

### Installing

#### Configuration

First we add the configuration for validating the tokens.

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddTokenAuthorize(new TokenValidationParameters());
}
```

Then we tell the app to use the middleware for generating tho tokens.

```
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseTokenAuthorize(new TokenOptions());
}
```

#### Overwrite Defaults

When initializing **TokenOptions** there are some defaults that can be overwritten.
* Issuer defaults to "LazySetup". This is not necessary to change, unless you want something more accurate for your app.
* Audience defaults to "http://localhost" which is fine for testing, but should be changed to your host when going into production.
* SigningCredentials defaults to `new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("NotSoSecure12345")), SecurityAlgorithms.HmacSha256)`. I strongly recommend that this is overwritten, since it could help other decrypt the token if they know the key.
* Path defaults to /token.
* Expiration defaults to 1 day. Depending on the system, this might need to be shorter or longer.

#### Claims

When defining claims, just create a class where all the properies that are not null, will be converted to claims

```
public class MyClaims : TokenClaims
{
	public string Username { get; set; } = "TestUser";
	public int Id { get; set; } = 1;
}
```

### Use
Make a **POST** request to the path specified in the setup (default is /token), with a body looking as following:

```
{
	"identifier": "",
	"password": "",
	"grant_type": "",
	"refresh_token": ""
}
```

If the user logs in, then identifier/password should be set.

If there already is a refresh token, then refresh_token should be sat and grant_type must be set to "refresh"

> When a user is logging out the refresh token should be invalidated where it's stored.

## Authors
* **Kenneth G. Pedersen** [Joshlo](https://github.com/joshlo)

## License

[License](https://github.com/joshlo/license.md)