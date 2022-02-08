using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sync.API.Persistance;
using Sync.API.Services;

namespace Sync.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration);
            var serviceProvider = services.BuildServiceProvider();
            var settings = serviceProvider.GetService<IOptions<AppSettings>>();

            services.AddHttpClientServices(Configuration);

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_0);

            services.AddDbContext<SyncContext>(options =>
            {
                options.UseSqlServer(settings.Value.ConnectionString,
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });

                options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Sync HTTP API",
                    Version = "v1",
                    Description = "Sync microservice, HTTP API, Data-Driven/CRUD pattern."
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<AppSettings> settings)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Globalization and Localization
            var cultureProviders = new[]
            {
                new QueryStringRequestCultureProvider()  //Deja solo este proveedor para poder pasar la cultura en el query string para efectos de prueba, pero que no se usen ni CookieRequestCultureProvider ni AcceptLanguageHeaderRequestCultureProvider (definida por el navegador). LCS.
            };

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("en"),
                new CultureInfo("es-ES"),
                new CultureInfo("es-MX"),
                new CultureInfo("es"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(settings.Value.DefaultCulture),
                RequestCultureProviders = cultureProviders,
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });
            //End globalization and localization

            app.UseSwagger()
              .UseSwaggerUI(c =>
              {
                  string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                  c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Employees.API");
              });


            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    static class ServiceCollectionExtensions
    {
        // Adds all Http client services (like Service-Agents) using resilient Http requests based on HttpClient factory and Polly's policies 
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IShopify, Shopify>();

            return services;
        }
    }
}
