namespace Template.Service.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Template.Common.BusinessLogic;
    using Template.Models;


    /// <summary>
    /// API controller for the Items collection
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ItemsController : BaseController
    {
        private IItemsBusinessLogic itemsBusinesLogic;

        /// <summary>
        /// Receives by DI the dependencies
        /// </summary>
        public ItemsController(ILogger<ItemsController> logger, IItemsBusinessLogic itemsBusinesLogic) : base(logger)
        {
            this.itemsBusinesLogic = itemsBusinesLogic;
        }


        /// <summary>
        /// Return an item
        /// </summary>
        [HttpGet("{itemId}")]
        public async Task<IActionResult> GetItemAsync(Guid itemId)
        {
            return await Execute(this.itemsBusinesLogic.LoadItemAsync, itemId);
        }


        /// <summary>
        /// Returna a collection of items
        /// </summary>
        [HttpGet()]
        public async Task<IActionResult> GetItemsAsync()
        {
            return await Execute<Item[]>(this.itemsBusinesLogic.LoadItemsAsync);
        }



        /// <summary>
        /// Creates a new item
        /// </summary>
        [HttpPost()]
        public async Task<IActionResult> SubmitItemAsync(
           [FromBody] Item item)
        {
            return await Execute(this.itemsBusinesLogic.AddItemAsync, item);
        }
    }
}
