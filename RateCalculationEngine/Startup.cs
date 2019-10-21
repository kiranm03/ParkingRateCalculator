using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RateCalculationEngine.Factory;
using RateCalculationEngine.RateCalculator;
using RateCalculationEngine.Worker;

namespace RateCalculationEngine
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddSingleton<IValidator, Validator>();
            services.AddSingleton<ICalculationEngine, CalculationEngine>();
            services.AddSingleton<IRateCalculator, RateCalculator.RateCalculator>();
            services.AddSingleton<IFlatRateCalculator, FlatRateCalculator>();
            services.AddSingleton<EarlyBirdRate>();
            services.AddSingleton<NightRate>();
            services.AddSingleton<WeekendRate>();
            services.AddSingleton<HourlyRate>();
            services.AddSingleton<FlatRate>();
            services.AddSingleton<FlatRateStrategyFactory>();
            services.AddSingleton<RateTypeStrategyFactory>();
            services.AddSingleton<IFlatRateStrategy[]>(provider =>
            {
                var factory = provider.GetService<FlatRateStrategyFactory>() as FlatRateStrategyFactory;
                return factory.Create();
            });
            services.AddSingleton<IRateTypeStrategy[]>(provider =>
            {
                var factory = provider.GetService<RateTypeStrategyFactory>() as RateTypeStrategyFactory;
                return factory.Create();
            });

            //services.AddSingleton<IConfiguration>(a =>
            //{
            //    var builder = new ConfigurationBuilder()
            //                                .SetBasePath(Directory.GetCurrentDirectory())
            //                                .AddJsonFile("appsettings.json");
            //    return builder.Build();
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                var result = JsonConvert.SerializeObject(new { error = exception.Message });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));

            app.UseMvc();
        }
    }
}
