﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using System;
using Template.Common.DataAccess;
using Template.DataAccess;
using Template.Common.BusinessLogic;
using Template.BusinessLogic;

namespace Template.Service
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
              .ConfigureFunctionsWorkerDefaults(builder =>
              {
                  builder.UseNewtonsoftJson();
              })
              .ConfigureOpenApi()
              .ConfigureAppConfiguration(config => config
                  .SetBasePath(Environment.CurrentDirectory)
                   .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                  .AddEnvironmentVariables())
              .ConfigureServices(services =>
              {
                  services.AddScoped<IItemsDataAccess, ItemsDataAccess>();
                  services.AddScoped<IItemsBusinessLogic, ItemsBusinessLogic>();
                  services.AddScoped<IDatabaseConnection<DatabaseContext>, DatabaseConnection>();
              })
              .Build();

            host.Run();
        }
    }
}
