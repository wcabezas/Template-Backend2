namespace Template.Service.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;
    using Template.Common.BusinessLogic;
    using Template.Models;


    /// <summary>
    /// API controller for the Items collection
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SolicitudesController : BaseController
    {
        private readonly ISolicitudesBusinessLogic SolicitudesBusinessLogic;

        /// <summary>
        /// Receives by DI the dependencies
        /// </summary>
        public SolicitudesController(ILogger<SolicitudesController> logger, ISolicitudesBusinessLogic solicitudesBusinesLogic) : base(logger)
        {
            this.SolicitudesBusinessLogic = solicitudesBusinesLogic;
        }


        /// <summary>
        /// Return an item
        /// </summary>
        [HttpGet("{solicitudId}")]
        public async Task<IActionResult> GetSolicitudAsync(Guid solicitudId)
        {
            return await Execute(this.SolicitudesBusinessLogic.LoadSolicitudAsync, solicitudId);
        }


        /// <summary>
        /// Returna a collection of items
        /// </summary>
        [HttpGet()]
        public async Task<IActionResult> GetSolicitudesAsync()
        {
            return await Execute<Solicitud[]>(this.SolicitudesBusinessLogic.LoadSolicitudesAsync);
        }



        /// <summary>
        /// Creates a new item
        /// </summary>
        [HttpPost()]
        public async Task<IActionResult> SubmitSolicitudAsync(
           [FromBody] Solicitud solicitud)
        {
            return await Execute(this.SolicitudesBusinessLogic.AddSolicitudAsync, solicitud);
        }
    }
}
