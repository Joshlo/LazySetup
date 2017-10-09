# Token 

## Setup

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddTokenAuthorize(new TokenValidationParameters());
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseTokenAuthorize(new TokenOptions())
}
```

When initializing **TokenOptions** there are some defaults that should be overwritten.
1. Issuer defaults to "LazySetup". This is not necessary to change, unless you want something more accurate for your app.
2. Audience defaults to "http://localhost" which is fine for testing, but should be changed to your host when going into production.
3. SigningCredentials defaults to "new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("NotSoSecure12345")), SecurityAlgorithms.HmacSha256)". I strongly recommend that this is overwritten, since it could help other decrypt the token if they know the key.
4. Path defaults to /token.
5. Expiration defaults to 1 day. This might be shorter or longer depending on the system.

Last but not least there must be an implementation for `ITokenValidation`.
All 3 methods must be implemented.
> When a user is logging out the refresh token should be invalidated where it's stored.

## Use
Make a **POST request** to the path specified in the setup (default is /token), with a body looking as following:

```
{
	"identifier": "",
	"password": "",
	"grant_type": "",
	"refresh_token": ""
}
```

If it's the initial request, only identifier/password is required, where if it's because the token has expired, the grant_type must be set to "refresh" and the refresh_token must be set.