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
    public interface IPruebasBusinessLogic
    {
        /// <summary>
        /// Adds an item
        /// </summary>        
        Task<Result<Prueba>> AddPruebaAsync(Prueba request);


        /// <summary>
        /// Updates an item
        /// </summary>        
        Task<Result<Prueba>> UpdatePruebaAsync(Prueba request);


        /// <summary>
        /// Load a single item
        /// </summary>
        /// <param name="pruebaId"></param>
        /// <returns></returns>
        Task<Result<Prueba>> LoadPruebaAsync(Guid pruebaId);


        /// <summary>
        /// Gets all the items
        /// </summary>        
        Task<Result<Prueba[]>> LoadPruebasAsync();


        /// <summary>
        /// Deletes an item
        /// </summary>
        Task<Result> DeletePruebaAsync(Guid pruebaId);
    }
}
