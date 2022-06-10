namespace Template.Common.DataAccess
{
    using Template.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;

    /// <summary>
    /// Items data access interface
    /// </summary>
    public interface ISolicitudesDataAccess : IDataAccess
    {
        /// <summary>
        /// Adds or update an item
        /// </summary>        
        Task<Solicitud> AddUpdateSolicitudAsync(Solicitud solicitud);

        /// <summary>
        /// Gets all the items
        /// </summary>        
        Task<List<Solicitud>> LoadSolicitudesAsync();


        /// <summary>
        /// Returns an item
        /// </summary>
        Task<Solicitud> LoadSolicitudAsync(Guid solicitudId);


        /// <summary>
        /// Removes an item
        /// </summary>      
        Task<bool> DeleteSolicitudAsync(Guid solicitudId);
    }
}
