using System.Collections.Generic;
using Template.Models;
using System.Threading.Tasks;
using System;
using Template.Common.Models;

namespace Template.Common.BusinessLogic
{
    /// <summary>
    /// Items business logic interface
    /// </summary>
    public interface ISolicitudesBusinessLogic
    {
        /// <summary>
        /// Adds an item
        /// </summary>        
        Task<Result<Solicitud>> AddSolicitudAsync(Solicitud request);


        /// <summary>
        /// Updates an item
        /// </summary>        
        Task<Result<Solicitud>> UpdateSolicitudAsync(Solicitud request);


        /// <summary>
        /// Load a single item
        /// </summary>
        /// <param name="solicitudId"></param>
        /// <returns></returns>
        Task<Result<Solicitud>> LoadSolicitudAsync(Guid solicitudId);


        /// <summary>
        /// Gets all the items
        /// </summary>        
        Task<Result<Solicitud[]>> LoadSolicitudesAsync();


        /// <summary>
        /// Deletes an item
        /// </summary>
        Task<Result> DeleteSolicitudAsync(Guid solicitudId);
    }
}
