using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LastCurrencyAPI.Core.Domain;
using LastCurrencyAPI.Core.Operations;
using LastCurrencyAPI.Core.Services;
using LastCurrencyAPI.Infrastructure.Processor.Currency;
using LastCurrencyAPI.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LastCurrencyAPI.Application
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
            services.AddMemoryCache();
            services.AddMvc();

            services.AddTransient<ICurrencyOperation, CurrencyOperation>();

            services.AddSingleton<ICurrencyService, CurrencyService>();
            services.AddSingleton<Queue<Alert>>();
            services.AddSingleton<USDProcessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, USDProcessor usdProcessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            usdProcessor.Start();
        }
    }
}
