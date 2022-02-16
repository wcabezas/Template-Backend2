namespace Template.BusinessLogic
{
    using Template.Models;
    using System.Threading.Tasks;
    using Template.Common.BusinessLogic;
    using Template.Common.DataAccess;
    using Microsoft.Extensions.Logging;
    using System;
    using Template.Common.Models;
    using Template.Common.Providers;

    /// <summary>
    /// Items Business Logic methods
    /// </summary>
    public class ItemsBusinessLogic : IItemsBusinessLogic
    {
        private readonly ILogger<ItemsBusinessLogic> logger;
        private readonly IItemsDataAccess dataAccess;
        private readonly ISessionProvider sessionProvider;

        /// <summary>
        /// Gets by DI the dependeciees
        /// </summary>
        /// <param name="dataAccess"></param>
        public ItemsBusinessLogic(IItemsDataAccess dataAccess,   ISessionProvider sessionProvider, ILogger<ItemsBusinessLogic> logger)
        {
            this.logger = logger;
            this.dataAccess = dataAccess;
            this.sessionProvider = sessionProvider;
        }


        /// <inheritdoc/>   
        public async Task<Result<Item>> AddItemAsync(Item request)
        {
            try
            {
                this.dataAccess.OpenDatabase();
                this.logger?.LogInformation("Executing ItemsBusinessLogic.AddItemAsync");
                request.CreatedBy = this.sessionProvider?.Username;
                request.Created = DateTimeOffset.Now;
                request.UpdatedBy = this.sessionProvider?.Username;
                request.Updated = DateTimeOffset.Now;
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
        public async Task<Result<Item>> UpdateItemAsync(Item request)
        {
            try
            {
                this.dataAccess.OpenDatabase();
                this.logger?.LogInformation("Executing ItemsBusinessLogic.UpdateItemAsync");
                request.UpdatedBy = this.sessionProvider?.Username;
                request.Updated = DateTimeOffset.Now;
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
                var result = await dataAccess.DeleteItemAsync(itemId);
                return new Result(result);
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
