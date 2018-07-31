using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DutchTreat
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DutchContext>(cfg=>
            {
                cfg.UseSqlServer(_configuration.GetConnectionString("DutchConnectionString"));
            });
            services.AddTransient<IMailServices, NullMailServices>();
            services.AddTransient<DutchSeeder>();
            services.AddScoped<IDutchRepository, DutchRepository>();


            //Add real Service later
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/error");

            //Serve static files
            app
                .UseStaticFiles()
                .UseMvc(cfg=> {
                    cfg.MapRoute("Default", "{controller}/{action}/{id?}", new { controller = "App", action = "Index" });
                });


            if (env.IsDevelopment())
            {
                //Seed the database
                using (var scope=app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                    seeder.Seed();
                }
            }
        }
    }
}
