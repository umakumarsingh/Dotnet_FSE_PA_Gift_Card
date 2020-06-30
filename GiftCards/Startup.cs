using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiftCards.BusinessLayer.Interface;
using GiftCards.BusinessLayer.Services;
using GiftCards.DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GiftCards
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc();
            //injecting Settings in the Options accessor model
            services.Configure<Mongosettings>(Options =>
            {
                Options.Connection = Configuration.GetSection("MongoConnection:Connection").Value;
                Options.DatabaseName = Configuration.GetSection("MongoConnection:DatabaseName").Value;
            });
          
            services.AddScoped<IMongoDBContext, MongoDBContext>();
            services.AddScoped<IContactUsServices, ContactUsServices>();
            services.AddScoped<IBuyerServices, BuyerServices>();
            services.AddScoped<IViewOrderServices, ViewOrderServices>();
          
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
