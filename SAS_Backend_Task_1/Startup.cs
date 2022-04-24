using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SAS_Backend_Task_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAS_Backend_Task_1
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
            services.AddControllersWithViews();

            var aSettings = new ServiceASettings();
            var bSettings = new ServiceBSettings();

            Configuration.Bind("ServiceASettings", aSettings);
            Configuration.Bind("ServiceBSettings", bSettings);

            Func<IServiceProvider, ServiceA> serviceABuilder = (t) => new ServiceA(new ServiceB(bSettings), aSettings);
            Func<IServiceProvider, ServiceB> serviceBBuilder = (t) => new ServiceB(bSettings);

            services.AddTransient<IServiceA, ServiceA>(serviceABuilder);
            services.AddScoped<IServiceB1, ServiceB>(serviceBBuilder);
            services.AddSingleton<IServiceB2, ServiceB>(serviceBBuilder);

            services.AddSingleton<IUserStore, UserStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "user",
                    pattern: "{controller=Users}/{action=Register}/");
            });
        }

        /*
         * What generally happens: 
         * Transient A: n page loads
         * Scoped B1: n*2+1 page loads
         * Singleton B2: n*2+1 page loads
         * 
         * What's happening? 
         * 
         * Transient A loads itself every time, increasing its counter every time AND ServiceB's one as well. (A+1 & B1,B2+1)
         * Scoped B1 increases ServiceB's counter as well. (B1,B2+1)
         * Singleton B2 only increases its value on startup. (B1,B2+1 x1)
         * 
         * Therefore, on every page reload (new consequent request) A1 increases its value once and B1 & B2's twice. B2 offsets this on startup by one. Hence the 2*n+1.
         */
    }
}
