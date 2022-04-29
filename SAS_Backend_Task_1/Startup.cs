using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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

            services.Configure<ServiceASettings>(cfg => Configuration.Bind("ServiceASettings", cfg));
            services.Configure<ServiceBSettings>(cfg => Configuration.Bind("ServiceBSettings", cfg));

            services.AddTransient<IServiceA, ServiceA>();
            services.AddScoped<IServiceB1, ServiceB>();
            services.AddSingleton<IServiceB2, ServiceB>();

            services.Configure<IServiceA>(Configuration);

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
         * 
         * ~~~~~
         * 
         * UPD: the explanation above is not applicable anymore. I used to inject IServiceB1 into ServiceA via factory method t=>new ServiceA(new ServiceB(), serviceASettings);
         * 
         * Now that I refactored the code with the IOptions pattern, ASP grabs the scoped ServiceB without creating it.
         * 
         * A - n loads
         * B1, B2 - n+1 loads, because the Singleton service increases the counter by one at the very start.
         * 
         * I left the previous explanation for legacy preservation and entertainment purposes :D
         */
    }
}
