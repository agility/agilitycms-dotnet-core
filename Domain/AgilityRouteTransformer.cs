﻿using System.Linq;
using System.Threading.Tasks;
using Agility.NET5.Core.Helpers;
using Agility.NET5.FetchAPI.Helpers;
using Agility.NET5.FetchAPI.Models.API;
using Agility.NET5.FetchAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Agility.NET5.Core.Domain
{
    public class AgilityRouteTransformer : DynamicRouteValueTransformer
    {
        private readonly FetchApiService _fetchApiService;
        private readonly AppSettings _appSettings;
        private readonly IMemoryCache _cache;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AgilityRouteTransformer(FetchApiService fetchApiService,
            IOptions<AppSettings> appSettings,
            IMemoryCache cache,
            IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor)
        {
            _fetchApiService = fetchApiService;
            _cache = cache;
            _env = env;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public override async ValueTask<RouteValueDictionary> TransformAsync(HttpContext
            httpContext, RouteValueDictionary values)
        {
            var sitemapPages = 
                await TransformerMiddlewareHelpers.GetOrCacheSitemapPages(
                        _env, _appSettings, _fetchApiService, _httpContextAccessor, _cache);

            var path = values.Values.First();

            SitemapPage agilityPage;

            if (path == null)
            {
                agilityPage =
                    sitemapPages.FirstOrDefault();
            }
            else
            {
                agilityPage =
                    sitemapPages.Find(
                        s => TransformerMiddlewareHelpers.DoesAgilityPageExist(s, path.ToString()));
            }

            if (agilityPage == null) return values;

            return new RouteValueDictionary()
            {
                {"page", "/AgilityPage"},
                {"agilityPage",agilityPage}

            };
        }
    }
}