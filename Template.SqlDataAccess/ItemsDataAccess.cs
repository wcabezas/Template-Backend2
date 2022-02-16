namespace Template.SqlDataAccess
{
    using Template.Models;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Template.Common.DataAccess;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Template.SqlDataAccess.Entities;
    using Template.Common.Providers;

    /// <summary>
    /// Data access implementation for Azure Storage
    /// </summary>
    public class ItemsDataAccess :  BaseDataAccess, IItemsDataAccess
    {
        private readonly ILogger<ItemsDataAccess> logger;
        private readonly ISessionProvider sessionProvider;


        /// <summary>
        /// Gets the connection string from the configuration
        /// </summary>
        /// <param name="configuration"></param>
        public ItemsDataAccess(IConfiguration configuration, IDatabaseConnection<DatabaseContext> connection, 
            ISessionProvider sessionProvider, ILogger<ItemsDataAccess> logger) : base(connection)
        {
            this.logger = logger;
            this.sessionProvider = sessionProvider;
        }


        /// <inheritdoc/>  
        public async Task<Item> AddUpdateItemAsync(Item item)
        {
            this.logger.LogInformation("Executing ItemsDataAccess.AddUpdateItemnAsync");
            var entity = ItemEntity.FromModel(item);
            entity.Updated = DateTimeOffset.Now;
            entity.UpdatedBy = this.sessionProvider?.Username;
            await this.DatabaseContext.Items.Upsert(entity).RunAsync();
            return item;
        }


        /// <inheritdoc/>   
        public async Task<List<Item>> LoadItemsAsync()
        {
            this.logger.LogInformation("Executing ItemsDataAccess.LoadItemsAsync");
            var items = await this.DatabaseContext.Items.Where(x=>!x.Deleted).Select(x => x.ToModel()).ToListAsync();
            return items;
        }


        /// <inheritdoc/>   
        public async Task<Item> LoadItemAsync(Guid itemId)
        {
            this.logger.LogInformation("Executing ItemsDataAccess.LoadItemAsync");
            var item = await this.DatabaseContext.Items.FirstOrDefaultAsync(x=>x.ItemId == itemId);
            return item?.ToModel();
        }


        /// <inheritdoc/>   
        public async Task<bool> DeleteItemAsync(Guid itemId)
        {
            this.logger.LogInformation("Executing ItemsDataAccess.DeleteItemAsync");
            var item = await this.DatabaseContext.Items.FirstOrDefaultAsync(x => x.ItemId == itemId);
            if (item != null)
            {
                item.Updated = DateTimeOffset.Now;
                item.UpdatedBy = this.sessionProvider?.Username;
                item.Deleted = true;
                await this.DatabaseContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
