//using System.Net;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
//using Microsoft.Extensions.Logging;
//using Template.Common.BusinessLogic;
//using Template.Models;

//namespace Template.Service.Functions
//{
//    /// <summary>
//    /// Sample API witha a queue trigger
//    /// </summary>
//    public class Queues : BaseFunction
//    {
//        /// <summary>
//        /// Receive all the depedencies by dependency injection
//        /// </summary>        
//        private readonly IItemsBusinessLogic businessLogic;


//        /// <summary>
//        /// Receive all the depedencies by DI
//        /// </summary>        
//        public Queues(IItemsBusinessLogic businessLogic, ILogger<Queues> logger) : base(logger)
//        {
//            this.businessLogic = businessLogic;
//        }



//        ///// <summary>
//        ///// POST Method to add an item to a queue
//        ///// resulting in triggering the OnItemsQueue method
//        ///// </summary>       
//        //[OpenApiOperation("Queue", "Add an item to a queue", Description = "Adds an item to a queue")]
//        //[OpenApiRequestBody("application/json", typeof(Request<Item>), Required = true, Description = "Item to add")]
//        //[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Response<Item>), Summary = "A response object", Description = "A response with the added item")]
//        //[FunctionName(nameof(AddItemToQueueAsync))]
//        //public async Task<IActionResult> AddItemToQueueAsync(
//        // [HttpTrigger(AuthorizationLevel.Function, "post", Route = "queue")] HttpRequest request)
//        //{
//        //    var itemRequest = new Request<Item>(request.Body);
//        //    return await Execute(this.businessLogic.QueueItemAsync, itemRequest);
//        //}


//        /// <summary>
//        /// Function triggers when an items is added to a queue
//        /// </summary>
//        /// <param name="item"></param>
//        /// <returns></returns>
//        [FunctionName("OnItemsQueue")]
//        public async Task OnItemsQueue([QueueTrigger("items-queue")] Item item)
//        {
//            logger.LogInformation($"Executing OnItemsQueue");
//            await Execute(this.businessLogic.AddUpdateItemAsync, item);
//        }
//    }
//}