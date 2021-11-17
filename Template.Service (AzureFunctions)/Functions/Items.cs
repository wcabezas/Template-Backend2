using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Template.Models;
using Template.Common.BusinessLogic;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using System.Net;
using System.Collections.Generic;

namespace Template.Service.Functions
{
    /// <summary>
    /// Sample Items API using http triggers
    /// </summary>
    public class Items : BaseFunction
    {
        /// <summary>
        /// Receive all the depedencies by dependency injection
        /// </summary>        
        private readonly IItemsBusinessLogic businessLogic;


        /// <summary>
        /// Receive all the depedencies by DI
        /// </summary>        
        public Items(IItemsBusinessLogic businessLogic, ILogger<Items> logger) : base(logger)
        {
            this.businessLogic = businessLogic;
        }


        /// <summary>
        /// Submits a new item
        /// </summary>       
        [OpenApiOperation("Items", "Create a new item", Description = "Creates a new item on the data storage")]
        [OpenApiRequestBody("application/json", typeof(Request<Item>), Required = true, Description = "Item object")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Response<Item>), Summary = "Created item", Description = "Created item")]
        [FunctionName(nameof(SubmitItemAsync))]
        public async Task<IActionResult> SubmitItemAsync(
         [HttpTrigger(AuthorizationLevel.Function, "post", Route = "items")] HttpRequest request)
        {
            var item = DeserializeBody<Item>(request.Body);
            return await Execute(this.businessLogic.AddUpdateItemAsync, item);
        }



        /// <summary>
        /// Returns a list of items
        /// </summary>        
        [OpenApiOperation("Items", "Returns all items", "Return all the items from the data storage")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result<List<Item>>),
            Summary = "A collection of items", Description = "A result object containing a collection of items")]
        [FunctionName(nameof(Items.GetItemsAsync))]
        public async Task<IActionResult> GetItemsAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "items")] HttpRequest request)
        {
            return await Execute(this.businessLogic.LoadItemsAsync);
        }
    }
}