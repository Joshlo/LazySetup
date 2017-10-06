# Token setup

### Startup.cs
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddTokenAuthorize(new TokenValidationParameters());
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseTokenAuthorize(new TokenOptions("<insert identifier>"))
}
```

When initializing ```TokenOptions``` there are some defaults that should be overwritten.
1. Issuer defaults to "LazySetup". This is not necessary to change, unless you want something more accurate for your app.
2. Audience defaults to "http://localhost" which is fine for testing, but should be changed to your host when going into production
3. SigningCredentials defaults to "new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("NotSoSecure12345")), SecurityAlgorithms.HmacSha256)". I strongly recommend that this is overwritten, since it could help other decrypt the token if they know the key.


