using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using OData.Dapper.Api.Models;
using OData.Dapper.Api.Repository;

namespace OData.Dapper.Api
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
            //services.AddTransient<ICustomerRepository, CustomerRepository>();
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = configurationBuilder.Build();
            services.AddTransient<ICustomerRepository>(c => new CustomerRepository(config["ConnectionStrings:CustomersDbConnection"]));
            services.AddTransient<ISupplierRepository>(c => new SupplierRepository(config["ConnectionStrings:CustomersDbConnection"]));
            //OData Configuration
            services.AddOData();
            services.AddControllers(mvcOptions => mvcOptions.EnableEndpointRouting = false);
            services.AddMvcCore(action => action.EnableEndpointRouting = false);


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options
            .WithOrigins
            ("http://localhost:7071",
                "http://localhost:1058/")
            .AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseMvc(builder =>
            {
                // This enable dependency injection allows for use of both OData and WebAPI endpoints.
                builder.EnableDependencyInjection();
                builder
                    .Select()
                    .Expand()
                    /*This is the magic shit right below here. You see that "null" right there?
                     Respect that motherfucker. Dude kept me confused for 3 weeks. Damned.
                     */
                    .MaxTop(null)
                    .Filter()
                    .OrderBy()
                    .Count()
                    .Expand();
                builder.MapODataServiceRoute("api", "api", GetEdmModel());
            });
            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
        private static IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<Supplier>("Suppliers");
            return odataBuilder.GetEdmModel();
        }
    }
}
