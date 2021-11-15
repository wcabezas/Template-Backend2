namespace Template.Common.DataAccess
{
    using Template.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;

    /// <summary>
    /// Items data access interface
    /// </summary>
    public interface IItemsDataAccess : IDataAccess
    {
        /// <summary>
        /// Adds or update an item
        /// </summary>        
        Task<Item> AddUpdateItemAsync(Item item);

        /// <summary>
        /// Gets all the items
        /// </summary>        
        Task<List<Item>> LoadItemsAsync();


        /// <summary>
        /// Adds an item to a queue 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> QueueItemAsync(Item item);


        /// <summary>
        /// Returns an item
        /// </summary>
        Task<Item> LoadItemAsync(Guid itemId);
    }
}
