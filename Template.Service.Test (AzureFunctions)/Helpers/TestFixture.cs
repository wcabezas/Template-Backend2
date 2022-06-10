using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Template.Common.BusinessLogic;
using Template.Service.Middleware;
using Template.Common.Providers;
using Template.Common.DataAccess;
using Template.SqlDataAccess;
using Template.BusinessLogic;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;

namespace Template.Service.Tests.Helpers
{
    /// <summary>
    /// Test fixture to use a shared single context between all the test
    /// </summary>
    public class TestFixture
    {
        /// <summary>
        /// Items functions
        /// </summary>
        public Template.Service.Functions.Items ItemsService { get; set; }

        public Template.Service.Functions.Pruebas PruebasService { get; set; }

        public Template.Service.Functions.Solicitudes SolicitudesService { get; set; }

        /// <summary>
        /// Setups the server, http client and host proceess
        /// </summary>
        public TestFixture()
        {
            var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults(builder =>
            {
                builder.UseMiddleware<SessionMiddleware>();
                builder.UseNewtonsoftJson();                
            })
            .ConfigureAppConfiguration(config => config
                .SetBasePath(Environment.CurrentDirectory)
                 .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables())
            .ConfigureServices(services =>
            {
                services.AddScoped<ISessionProvider, SessionProvider>();
                services.AddScoped<IItemsDataAccess, ItemsDataAccess>();
                services.AddScoped<IItemsBusinessLogic, ItemsBusinessLogic>();
                services.AddScoped<IPruebasDataAccess, PruebasDataAccess>();
                services.AddScoped<IPruebasBusinessLogic, PruebasBusinessLogic>();
                services.AddScoped<ISolicitudesDataAccess, SolicitudesDataAccess>();
                services.AddScoped<ISolicitudesBusinessLogic, SolicitudesBusinessLogic>();
                services.AddScoped<IDatabaseConnection<DatabaseContext>, DatabaseConnection>();
            })
            .Build();
         
            var itemsBUsinessLogic = host.Services.GetRequiredService<IItemsBusinessLogic>();
            this.ItemsService = new Template.Service.Functions.Items(itemsBUsinessLogic);

            var pruebasBUsinessLogic = host.Services.GetRequiredService<IPruebasBusinessLogic>();
            this.PruebasService = new Template.Service.Functions.Pruebas(pruebasBUsinessLogic);

            var solicitudesBUsinessLogic = host.Services.GetRequiredService<ISolicitudesBusinessLogic>();
            this.SolicitudesService = new Template.Service.Functions.Solicitudes(solicitudesBUsinessLogic);
        }


        /// <summary>
        /// Clean up everything created during the tests
        /// </summary>
        public void Dispose()
        {
            // ... clean up test data from the database ...
        }
    }
}
