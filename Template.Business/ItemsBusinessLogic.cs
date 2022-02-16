namespace Template.BusinessLogic
{
    using System.Collections.Generic;
    using Template.Models;
    using System.Threading.Tasks;
    using Template.Common.BusinessLogic;
    using Template.Common.DataAccess;
    using Microsoft.Extensions.Logging;
    using System;
    using Template.Common.Models;

    /// <summary>
    /// Items Business Logic methods
    /// </summary>
    public class ItemsBusinessLogic : IItemsBusinessLogic
    {
        private readonly ILogger<ItemsBusinessLogic> logger;
        private readonly IItemsDataAccess dataAccess;


        /// <summary>
        /// Gets by DI the dependeciees
        /// </summary>
        /// <param name="dataAccess"></param>
        public ItemsBusinessLogic(IItemsDataAccess dataAccess, ILogger<ItemsBusinessLogic> logger)
        {
            this.logger = logger;
            this.dataAccess = dataAccess;
        }


        /// <inheritdoc/>   
        public async Task<Result<Item>> AddUpdateItemAsync(Item request)
        {
            try
            {
                this.dataAccess.OpenDatabase();
                this.logger?.LogInformation("Executing ItemsBusinessLogic.AddUpdateItemAsync");
                var item = await dataAccess.AddUpdateItemAsync(request);
                return new Result<Item>(item);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new Result<Item>(ex.Message);
            }
            finally
            {
                this.dataAccess.CloseDatabase();
            }
        }


        /// <inheritdoc/>     
        public async Task<Result<Item[]>> LoadItemsAsync()
        {
            try
            {
                this.dataAccess.OpenDatabase();
                this.logger?.LogInformation("Executing ItemsBusinessLogic.LoadItemsAsync");
                var items = await dataAccess.LoadItemsAsync();
                return new Result<Item[]>(items.ToArray());
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new Result<Item[]>(ex.Message);
            }
            finally
            {
                this.dataAccess.CloseDatabase();
            }
        }



        /// <inheritdoc/>     
        public async Task<Result<Item>> LoadItemAsync(Guid itemId)
        {
            try
            {
                this.dataAccess.OpenDatabase();
                this.logger?.LogInformation("Executing ItemsBusinessLogic.LoadItemAsync");
                var item = await dataAccess.LoadItemAsync(itemId);
                return new Result<Item>(item);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new Result<Item>(ex.Message);
            }
            finally
            {
                this.dataAccess.CloseDatabase();
            }
        }


        /// <inheritdoc/>     
        public async Task<Result> DeleteItemAsync(Guid itemId)
        {
            try
            {
                this.dataAccess.OpenDatabase();
                this.logger?.LogInformation("Executing ItemsBusinessLogic.DeleteItemAsync");
                //var item = await dataAccess.de(itemId);
                return new Result(true);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new Result(false, ex.Message);
            }
            finally
            {
                this.dataAccess.CloseDatabase();
            }
        }


        
    }
}
