using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Service.Configuration
{
    /// <summary>
    /// Open API Configuration 
    /// </summary>
    public class OpenApiOptions : DefaultOpenApiConfigurationOptions
    {
        public override OpenApiInfo Info { get; set; } = new OpenApiInfo()
        {
            Version = "3.0.0",
            Title = "Azure Functions Template API",
            Description = "A sample API that runs on Azure Functions 3.x with OpenAPI specification.",
            TermsOfService = new Uri("https://www.arkanosoft.com"),
            Contact = new OpenApiContact()
            {
                Name = "Arkano",
                Email = "soporte@arkanosoft.com",
                Url = new Uri("mailto:soporte@arkanosoft.com"),
            },
        };
    }
}