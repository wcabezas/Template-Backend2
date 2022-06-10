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
    public class Solicitudes
    {
        /// <summary>
        /// Receive all the depedencies by dependency injection
        /// </summary>        
        private readonly ISolicitudesBusinessLogic businessLogic;


        /// <summary>
        /// Receive all the depedencies by DI
        /// </summary>        
        public Solicitudes(ISolicitudesBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }


        /// <summary>
        /// Submits a new item
        /// </summary>       
        [OpenApiOperation("SubmitSolicitud", new[] { "Solicitudes" }, Description = "Creates a new solicitud on the data storage")]
        [OpenApiRequestBody("application/json", typeof(Solicitud), Required = true, Description = "Solicitud object")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result<Solicitud>), Description = "The new solicitud")]
        [Function(nameof(SubmitSolicitudAsync))]
        public async Task<HttpResponseData> SubmitSolicitudAsync(
         [HttpTrigger(AuthorizationLevel.Function, "post", Route = "solicitudes")] HttpRequestData request)
        {
            return await request.CreateResponse(this.businessLogic.AddSolicitudAsync, request.DeserializeBody<Solicitud>(), response =>
            {
                // Adds the proper hateoas links to this item
                
            });
        }


        /// <summary>
        /// Submits a new item
        /// </summary>       
        [OpenApiOperation("UpdateSolicitud", new[] { "Solicitudes" }, Description = "Updates and Solicitud on the data storage")]
        [OpenApiRequestBody("application/json", typeof(Solicitud), Required = true, Description = "Solicitud object")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result<Solicitud>), Description = "The solicitud to update")]
        [Function(nameof(UpdateSolicitudAsync))]
        public async Task<HttpResponseData> UpdateSolicitudAsync(
         [HttpTrigger(AuthorizationLevel.Function, "put", Route = "solicitudes")] HttpRequestData request)
        {
            return await request.CreateResponse(this.businessLogic.AddSolicitudAsync, request.DeserializeBody<Solicitud>(), response =>
            {
                // Adds the proper hateoas links to this item
                
            });
        }


        /// <summary>
        /// Returns an item
        /// </summary>        
        [OpenApiOperation("GetSolicitud", new[] { "Solicitudes" }, Description = "Return an Solicitudes from the data storage")]
        [OpenApiParameter("solicitudId", Type = typeof(Guid), Required = true, Description = "Solicitud Id to retrieve")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result<Solicitud[]>), Description = "A result object containing an Solicitudes")]
        [Function(nameof(Solicitudes.GetSolicitudAsync))]
        public async Task<HttpResponseData> GetSolicitudAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "solicitudes/{solicitudId}")] HttpRequestData request, Guid solicitudId)
        {
            return await request.CreateResponse(this.businessLogic.LoadSolicitudAsync, solicitudId, response =>
            {
                // Adds the proper hateoas links to this item
                
            });
        }



        /// <summary>
        /// Returns an item
        /// </summary>        
        [OpenApiOperation("DeleteSolicitud", new[] { "Solicitudes" }, Description = "Deletes an solicitud from the data storage")]
        [OpenApiParameter("solicitudId", Type = typeof(Guid), Required = true, Description = "Solicitud Id to remove")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result), Description = "The result of removing the element")]
        [Function(nameof(Solicitudes.DeleteSolicitudesAsync))]
        public async Task<HttpResponseData> DeleteSolicitudesAsync(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "solicitudes/{solicitudId}")] HttpRequestData request, Guid solicitudId)
        {
            return await request.CreateResponse(this.businessLogic.DeleteSolicitudAsync, solicitudId);
        }



        /// <summary>
        /// Returns a list of items
        /// </summary>        
        [OpenApiOperation("GetSolicitudes", new[] { "Solicitudes" }, Description = "Return all the solicitudes from the data storage")]
        [OpenApiParameter("page", In = ParameterLocation.Query, Description = "Page index")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result<Solicitud[]>), Description = "A result object containing a collection of solicitudes")]
        [Function(nameof(Solicitudes.GetSolicitudesAsync))]
        public async Task<HttpResponseData> GetSolicitudesAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "solicitudes")] HttpRequestData request, int page)
        {
            return await request.CreateResponse(this.businessLogic.LoadSolicitudesAsync, response =>
            {
                // Adds the proper hateoas links to each item in the collection
                //foreach (var solicitud in response.Data)
                //{
                    
                //}
                

            });
        }
    }
}
