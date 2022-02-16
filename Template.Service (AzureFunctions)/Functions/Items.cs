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
        [OpenApiRequestBody("application/json", typeof(Item), Required = true, Description = "Item object")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result<Item>), Description = "The new item")]
        [Function(nameof(SubmitItemAsync))]
        public async Task<HttpResponseData> SubmitItemAsync(
         [HttpTrigger(AuthorizationLevel.Function, "post", Route = "items")] HttpRequestData request)
        {
            return await request.CreateResponse(this.businessLogic.AddItemAsync, request.DeserializeBody<Item>(), response =>
            {
                // Adds the proper hateoas links to this item
                response.Links = new Dictionary<string, string>();
                response.Links.Add("self", $"/items/{response.Data.ItemId}");
                response.Links.Add("delete", $"/items/{response.Data.ItemId}");
                response.Links.Add("put", $"/items");
            });
        }


        /// <summary>
        /// Submits a new item
        /// </summary>       
        [OpenApiOperation("Items", "Updates an item", Description = "Updates and item on the data storage")]
        [OpenApiRequestBody("application/json", typeof(Item), Required = true, Description = "Item object")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result<Item>), Description = "The item to update")]
        [Function(nameof(UpdateItemAsync))]
        public async Task<HttpResponseData> UpdateItemAsync(
         [HttpTrigger(AuthorizationLevel.Function, "put", Route = "items")] HttpRequestData request)
        {
            return await request.CreateResponse(this.businessLogic.AddItemAsync, request.DeserializeBody<Item>(), response =>
            {
                // Adds the proper hateoas links to this item
                response.Links = new Dictionary<string, string>();
                response.Links.Add("self", $"/items/{response.Data.ItemId}");
                response.Links.Add("delete", $"/items/{response.Data.ItemId}");
                response.Links.Add("put", $"/items");              
            });
        }


        /// <summary>
        /// Returns an item
        /// </summary>        
        [OpenApiOperation("GetItem", new[] { "Items" }, Description = "Return an items from the data storage")]
        [OpenApiParameter("itemId", Type = typeof(Guid), Required = true, Description = "Item Id to retrieve")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result<Item[]>), Description = "A result object containing an items")]
        [Function(nameof(Items.GetItemAsync))]
        public async Task<HttpResponseData> GetItemAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "items/{itemId}")] HttpRequestData request, Guid itemId)
        {
            return await request.CreateResponse(this.businessLogic.LoadItemAsync, itemId, response =>
            {
                // Adds the proper hateoas links to this item
                response.Links = new Dictionary<string, string>();
                response.Links.Add("self", $"/items/{itemId}");
                response.Links.Add("delete", $"/items/{itemId}");              
            });
        }



        /// <summary>
        /// Returns an item
        /// </summary>        
        [OpenApiOperation("DeleteItem", new[] { "Items" }, Description = "Deletes an from the data storage")]
        [OpenApiParameter("itemId", Type=typeof(Guid), Required = true, Description = "Item Id to remove")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Result), Description = "The result of removing the element")]
        [Function(nameof(Items.DeleteItemsAsync))]
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
                    item.Links = new Dictionary<string, string>();
                    item.Links.Add("self", $"/items/{item.ItemId}");
                    item.Links.Add("delete", $"/items/{item.ItemId}");
                }
                //response.Links = new ResponseLink[]
                //{
                //    new ResponseLink("nextPage", "/items"),
                //};
                
            });
        }
    }
}
