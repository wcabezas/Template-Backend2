using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Template.Models;
using Template.Common.BusinessLogic;
using System.Net;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Template.Common.Models;
using Template.Service.Extensions;
using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace Template.Service.Functions
{
    /// <summary>
    /// Sample Items API using http triggers
    /// </summary>
    public class Pruebas
    {
        /// <summary>
        /// Receive all the depedencies by dependency injection
        /// </summary>        
        private readonly IPruebasBusinessLogic businessLogic;


        /// <summary>
        /// Receive all the depedencies by DI
        /// </summary>        
        public Pruebas(IPruebasBusinessLogic businessLogic) 
        {
            this.businessLogic = businessLogic;
        }


        /// <summary>
        /// Submits a new item
        /// </summary>       
        [OpenApiOperation("SubmitPrueba", new[] { "Pruebas" }, Description = "Creates a new prueba on the data storage")]
        [OpenApiRequestBody("application/json", typeof(Prueba), Required = true, Description = "Prueba object")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result<Prueba>), Description = "The new prueba")]
        [Function(nameof(SubmitPruebaAsync))]
        public async Task<HttpResponseData> SubmitPruebaAsync(
         [HttpTrigger(AuthorizationLevel.Function, "post", Route = "pruebas")] HttpRequestData request)
        {
            return await request.CreateResponse(this.businessLogic.AddPruebaAsync, request.DeserializeBody<Prueba>(), response =>
            {
                // Adds the proper hateoas links to this item
                
            });
        }


        /// <summary>
        /// Submits a new item
        /// </summary>       
        [OpenApiOperation("UpdatePrueba", new[] { "Pruebas" }, Description = "Updates and prueba on the data storage")]
        [OpenApiRequestBody("application/json", typeof(Prueba), Required = true, Description = "Prueba object")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result<Prueba>), Description = "The prueba to update")]
        [Function(nameof(UpdatePruebaAsync))]
        public async Task<HttpResponseData> UpdatePruebaAsync(
         [HttpTrigger(AuthorizationLevel.Function, "put", Route = "pruebas")] HttpRequestData request)
        {
            return await request.CreateResponse(this.businessLogic.AddPruebaAsync, request.DeserializeBody<Prueba>(), response =>
            {
                // Adds the proper hateoas links to this item
                             
            });
        }


        /// <summary>
        /// Returns an item
        /// </summary>        
        [OpenApiOperation("GetPrueba", new[] { "Pruebas" }, Description = "Return an pruebas from the data storage")]
        [OpenApiParameter("pruebaId", Type = typeof(Guid), Required = true, Description = "Prueba Id to retrieve")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result<Prueba[]>), Description = "A result object containing an pruebas")]
        [Function(nameof(Items.GetItemAsync))]
        public async Task<HttpResponseData> GetPruebaAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "pruebas/{pruebaId}")] HttpRequestData request, Guid pruebaId)
        {
            return await request.CreateResponse(this.businessLogic.LoadPruebaAsync, pruebaId, response =>
            {
                // Adds the proper hateoas links to this item
                            
            });
        }



        /// <summary>
        /// Returns an item
        /// </summary>        
        [OpenApiOperation("DeletePrueba", new[] { "Pruebas" }, Description = "Deletes an from the data storage")]
        [OpenApiParameter("pruebaId", Type=typeof(Guid), Required = true, Description = "Prueba Id to remove")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result), Description = "The result of removing the element")]
        [Function(nameof(Pruebas.DeletePruebasAsync))]
        public async Task<HttpResponseData> DeletePruebasAsync(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "pruebas/{pruebasId}")] HttpRequestData request, Guid pruebaId)
        {
            return await request.CreateResponse(this.businessLogic.DeletePruebaAsync, pruebaId);
        }



        /// <summary>
        /// Returns a list of items
        /// </summary>        
        [OpenApiOperation("GetPruebas", new[] { "Pruebas" }, Description = "Return all the pruebas from the data storage")]
        [OpenApiParameter("page", In = ParameterLocation.Query, Description = "Page index")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result<Prueba[]>), Description = "A result object containing a collection of pruebas")]
        [Function(nameof(Pruebas.GetPruebasAsync))]
        public async Task<HttpResponseData> GetPruebasAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "pruebas")] HttpRequestData request, int page)
        {
            return await request.CreateResponse(this.businessLogic.LoadPruebasAsync, response =>
            {
                // Adds the proper hateoas links to each item in the collection
                
                response.Links = new Dictionary<string, string>();
                response.Links.Add("nextPage", $"/pruebas?page={page + 1}");

            });
        }
    }
}
