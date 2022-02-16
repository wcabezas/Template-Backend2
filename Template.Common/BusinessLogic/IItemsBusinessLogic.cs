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
    public interface IItemsBusinessLogic
    {
        /// <summary>
        /// Adds an item
        /// </summary>        
        Task<Result<Item>> AddItemAsync(Item request);


        /// <summary>
        /// Updates an item
        /// </summary>        
        Task<Result<Item>> UpdateItemAsync(Item request);


        /// <summary>
        /// Load a single item
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<Result<Item>> LoadItemAsync(Guid itemId);


        /// <summary>
        /// Gets all the items
        /// </summary>        
        Task<Result<Item[]>> LoadItemsAsync();


        /// <summary>
        /// Deletes an item
        /// </summary>
        Task<Result> DeleteItemAsync(Guid itemId);
    }
}
