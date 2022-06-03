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
    public class PruebasController : BaseController
    {
        private IPruebasBusinessLogic pruebasBusinesLogic;

        /// <summary>
        /// Receives by DI the dependencies
        /// </summary>
        public PruebasController(ILogger<ItemsController> logger, IPruebasBusinessLogic pruebasBusinesLogic) : base(logger)
        {
            this.pruebasBusinesLogic = pruebasBusinesLogic;
        }


        /// <summary>
        /// Return an item
        /// </summary>
        [HttpGet("{pruebaId}")]
        public async Task<IActionResult> GetPruebaAsync(Guid pruebaId)
        {
            return await Execute(this.pruebasBusinesLogic.LoadPruebaAsync, pruebaId);
        }


        /// <summary>
        /// Returna a collection of items
        /// </summary>
        [HttpGet()]
        public async Task<IActionResult> GetPruebasAsync()
        {
            return await Execute<Prueba[]>(this.pruebasBusinesLogic.LoadPruebasAsync);
        }



        /// <summary>
        /// Creates a new item
        /// </summary>
        [HttpPost()]
        public async Task<IActionResult> SubmitPruebaAsync(
           [FromBody] Prueba prueba)
        {
            return await Execute(this.pruebasBusinesLogic.AddPruebaAsync, prueba);
        }
    }
}
