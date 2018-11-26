# clrmd-middleware

ASP.NET Core Middleware for introspecting current dotnet process using [Microsoft.Diagnostics.Runtime](https://github.com/Microsoft/clrmd)

Call services.AddClrMd() in your ConfigureServices Method:

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    services.AddClrMd();
}
```

Register the Middleware in your Configure-Method by calling app.UseClrMd()

```cs
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
       app.UseDeveloperExceptionPage();
    }
    else
    {
      app.UseHsts();
    }

    app.UseClrMd();

    app.UseHttpsRedirection();
    app.UseMvc();
}
```

calling the diagnostics endpoint under **http://localhost:56868/diagnostics**
