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

namespace Template.Service.Functions
{
    /// <summary>
    /// Sample Items API using http triggers
    /// </summary>
    public class Items
    {
        /// <summary>
        /// Receive all the depedencies by dependency injection
        /// </summary>        
        private readonly IItemsBusinessLogic businessLogic;


        /// <summary>
        /// Receive all the depedencies by DI
        /// </summary>        
        public Items(IItemsBusinessLogic businessLogic) 
        {
            this.businessLogic = businessLogic;
        }


        /// <summary>
        /// Submits a new item
        /// </summary>       
        [OpenApiOperation("Items", "Create a new item", Description = "Creates a new item on the data storage")]
        [OpenApiRequestBody("application/json", typeof(Request<Item>), Required = true, Description = "Item object")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result<Item>), Description = "The new item")]
        [Function(nameof(SubmitItemAsync))]
        public async Task<HttpResponseData> SubmitItemAsync(
         [HttpTrigger(AuthorizationLevel.Function, "post", Route = "items")] HttpRequestData request)
        {
            return await request.CreateResponse(this.businessLogic.AddUpdateItemAsync, request.DeserializeBody<Item>(), response =>
            {
                // Adds the proper hateoas links to this item
                response.Links = new ResponseLink[]
                {
                    new ResponseLink("self", $"/items/{response.Data.ItemId}"),
                    new ResponseLink("delete", $"/items/{response.Data.ItemId}")
                };

            });
        }



        /// <summary>
        /// Returns an item
        /// </summary>        
        [OpenApiOperation("GetItem", new[] { "Items" }, Description = "Return an items from the data storage")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result<Item[]>), Description = "A result object containing an items")]
        [Function(nameof(Items.GetItemsAsync))]
        public async Task<HttpResponseData> GetItemsAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "items/{itemId}")] HttpRequestData request, Guid itemId)
        {
            return await request.CreateResponse(this.businessLogic.LoadItemAsync, itemId, response =>
            {
                // Adds the proper hateoas links to this item
                response.Links = new ResponseLink[]
                {
                    new ResponseLink("self", $"/items/{itemId}"),
                    new ResponseLink("delete", $"/items/{itemId}")
                };

            });
        }



        /// <summary>
        /// Returns an item
        /// </summary>        
        [OpenApiOperation("DeleteItem", new[] { "Items" }, Description = "Deletes an from the data storage")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result), Description = "The result of removing the element")]
        [Function(nameof(Items.GetItemsAsync))]
        public async Task<HttpResponseData> DeleteItemsAsync(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "items/{itemId}")] HttpRequestData request, Guid itemId)
        {
            return await request.CreateResponse(this.businessLogic.DeleteItemAsync, itemId);
        }



        /// <summary>
        /// Returns a list of items
        /// </summary>        
        [OpenApiOperation("GetItems", new[] { "Items" }, Description = "Return all the items from the data storage")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result<Item[]>), Description = "A result object containing a collection of items")]
        [Function(nameof(Items.GetItemsAsync))]
        public async Task<HttpResponseData> GetItemsAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "items")] HttpRequestData request)
        {
            return await request.CreateResponse(this.businessLogic.LoadItemsAsync, response =>
            {
                // Adds the proper hateoas links to each item in the collection
                foreach(var item in response.Data)
                {

                }
                response.Links = new ResponseLink[]
                {
                    new ResponseLink("nextPage", "/items"),
                };
                
            });
        }
    }
}
