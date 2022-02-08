using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System;


namespace Template.Service.Configuration
{
    /// <summary>
    /// Open API Configuration 
    /// </summary>
    public class OpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
    {
        public override OpenApiInfo Info { get; set; } = new OpenApiInfo()
        {
            Version = "1.0.0",
            Title = "Azure Functions Template API",
            Description = "A sample API that runs on Azure Functions 4.x with OpenAPI specification.",
            TermsOfService = new Uri("https://www.arkanosoft.com"),
            Contact = new OpenApiContact()
            {
                Name = "Arkano",
                Email = "soporte@arkanosoft.com",
                Url = new Uri("mailto:soporte@arkanosoft.com"),
            },
        };

        /// <summary>
        /// Open API Version
        /// </summary>
        public override OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V3;
    }
}