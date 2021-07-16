# agilitycms-dotnet5-core
Agility package for page management and URL redirects. Agility CMS .NET Core uses Agility CMS .NET Core behind the scenes and is a dependency.

# Setup
1. Clone  Agility CMS .NET Core repo at https://github.com/agility/agilitycms-dotnet5-fetch-api
2. Clone and configure Agility CMS .NET 5 Starter at https://github.com/agility/agilitycms-dotnet5-starter
3. Clone and configure Agility CMS .NET Fetch API repo at https://github.com/agility/agilitycms-dotnet5-fetch-api
5. Open Agility CMS .NET 5 Starter and add the project  Agility CMS .NET Core to your solution
![image](https://user-images.githubusercontent.com/6853592/125960452-7af853bd-e49e-442b-90c6-4b72e83fac93.png)
4. Add a dependency to the Agility.NET5.Starter for Agility CMS .NET Core by right clicking 'Dependencies' under Agility.NET5.Starter
![image](https://user-images.githubusercontent.com/6853592/125955180-eebb9395-c807-48be-a355-6f32eff63b0c.png)
5. Select 'Add Project Reference' and check Agility.NET5.Core
![image](https://user-images.githubusercontent.com/6853592/125960630-0348bb27-4bb5-4760-882f-992785d8e01f.png)
6. Add a dependency to the Agility.NET5.Core for Agility CMS .NET Fetch API by right clicking 'Dependencies' under Agility.NET5.Core
![image](https://user-images.githubusercontent.com/6853592/125960981-e848eefa-b732-4449-bc3b-3950bd464a88.png)
7. Select 'Add Project Reference' and check Agility.NET5.FetchAPI
![image](https://user-images.githubusercontent.com/6853592/125961089-4a4945f7-5553-4856-bf3b-a4f6516c827d.png)

# Core
## Page Management
To add page management to your project (_Agility CMS .NET Starter or empty project_) you need to update ```Startup.cs``` with the following.

In ```ConfigureServices(IServiceCollection services)``` add ```services.AddSingleton<AgilityRouteTransformer>();```

and in ```Configure(IApplicationBuilder app, IWebHostEnvironment env)``` add
```
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapRazorPages();
        endpoints.MapDynamicPageRoute<AgilityRouteTransformer>("/{**slug}");
    });
```

You can change ```AgilityRouteTransformer.cs``` to fit your needs or you can create your own Route Transformer. Keep in mind to use the ```AgilityRouteTransformer.cs``` it requires having the AgilityPage.cshtml in your Pages folder.

## URL Redirects
To add URL redirects to your project (_Agility CMS .NET Starter or empty project_) you need to update ```Startup.cs``` with the following.

In ```Configure(IApplicationBuilder app, IWebHostEnvironment env)``` add
```
    app.UseMiddleware<AgilityRedirectMiddleware>();
```

You can change to ```AgilityRedirectMiddleware.cs``` to fit your needs or you can create your own Redirect Middleware. 







