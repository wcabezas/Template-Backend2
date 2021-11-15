namespace Template.DataAccess
{
    using Template.Models;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Template.DataAccess.Helpers;
    using Template.DataAccess.Entities;
    using Template.Common.DataAccess;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;
    using System;

    /// <summary>
    /// Data access implementation for Azure Storage
    /// </summary>
    public class ItemsDataAccess :  BaseDataAccess, IItemsDataAccess
    {
        private readonly StorageHelper storageHelper;
        private readonly ILogger<ItemsDataAccess> logger;


        /// <summary>
        /// Gets the connection string from the configuration
        /// </summary>
        /// <param name="configuration"></param>
        public ItemsDataAccess(IConfiguration configuration, IDatabaseConnection<DatabaseContext> connection, ILogger<ItemsDataAccess> logger) : base(connection)
        {
            this.logger = logger;
            this.storageHelper = new StorageHelper(configuration.GetConnectionString("StorageConnectionString"));
        }


        /// <inheritdoc/>  
        public async Task<Item> AddUpdateItemAsync(Item item)
        {
            this.logger.LogInformation("Executing ItemsDataAccess.AddUpdateItemnAsync");
            var entity = ItemEntity.FromModel(item);
            await this.DatabaseContext.Items.Upsert(entity).RunAsync();
            return item;
        }


        /// <inheritdoc/>   
        public async Task<List<Item>> LoadItemsAsync()
        {
            this.logger.LogInformation("Executing ItemsDataAccess.LoadItemsAsync");
            var items = await this.DatabaseContext.Items.Select(x => x.ToModel()).ToListAsync();
            return items;
        }


        /// <inheritdoc/>   
        public async Task<Item> LoadItemAsync(Guid itemId)
        {
            this.logger.LogInformation("Executing ItemsDataAccess.LoadItemsAsync");
            var item = await this.DatabaseContext.Items.FirstOrDefaultAsync(x=>x.ItemId == itemId);
            return item?.ToModel();
        }


        /// <inheritdoc/>   
        public async Task<bool> QueueItemAsync(Item item)
        {
            return await this.storageHelper.AddToQueueAsync<Item>("items-queue", new[] { item });
        }
    }
}
