namespace Template.Common.DataAccess
{
    using Template.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;

    /// <summary>
    /// Items data access interface
    /// </summary>
    public interface IPruebasDataAccess : IDataAccess
    {
        /// <summary>
        /// Adds or update an item
        /// </summary>        
        Task<Prueba> AddUpdatePruebaAsync(Prueba prueba);

        /// <summary>
        /// Gets all the items
        /// </summary>        
        Task<List<Prueba>> LoadPruebasAsync();


        /// <summary>
        /// Returns an item
        /// </summary>
        Task<Prueba> LoadPruebaAsync(Guid pruebaId);


        /// <summary>
        /// Removes an item
        /// </summary>      
        Task<bool> DeletePruebaAsync(Guid pruebaId);
    }
}
