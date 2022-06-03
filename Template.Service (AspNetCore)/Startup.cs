using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Template.BusinessLogic;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using Template.Common.BusinessLogic;
using Template.Common.DataAccess;
using Template.SqlDataAccess;
using Template.Common.Providers;

namespace Template.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public Startup(IConfiguration configuration)
        {
        }



        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Enable coors
            services.AddCors(options =>
            {
                options.AddPolicy("ServicePolicty",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            // Add all the dependencies
            services.AddScoped<ISessionProvider, SessionProvider>();
            services.AddScoped<IItemsDataAccess, ItemsDataAccess>();
            services.AddScoped<IItemsBusinessLogic, ItemsBusinessLogic>();
            services.AddScoped<IPruebasDataAccess, PruebasDataAccess>();
            services.AddScoped<IPruebasBusinessLogic, PruebasBusinessLogic>();
            services.AddScoped<IDatabaseConnection<DatabaseContext>, DatabaseConnection>();
           
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API",
                    Description = "API",

                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {

#if DEBUG
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
#else
                // Fix the swagger route when published as release on IIS
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "API");
#endif
            });

            app.UseRouting();

            // This should be set after UseRouting for using Cors
            app.UseCors("ServicePolicty");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
