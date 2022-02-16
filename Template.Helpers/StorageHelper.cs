//namespace Template.DataAccess.Helpers
//{
//    using System.Collections.Generic;
//    using System.IO;
//    using System.Linq;
//    using System.Threading.Tasks;
//    using Microsoft.WindowsAzure.Storage;
//    using Microsoft.WindowsAzure.Storage.Table;    
//    using Microsoft.WindowsAzure.Storage.Queue;
//    using Newtonsoft.Json;


//    /// <summary>
//    /// Data access methods
//    /// </summary>
//    public class StorageHelper
//    {
//        private readonly string connectionString;

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="configuration"></param>
//        public StorageHelper(string connectionString)
//        {
//            this.connectionString = connectionString;
//        }


//        /// <summary>
//        /// Gets the cloud table reference
//        /// </summary>
//        internal async Task<CloudTable> GetTableReferenceAsync(string storageTable)
//        {
//            var storageAccount = CloudStorageAccount.Parse(this.connectionString);
//            var tableClient = storageAccount.CreateCloudTableClient();
//            var table = tableClient.GetTableReference(storageTable);
//            await table.CreateIfNotExistsAsync();
//            return table;
//        }


//        /// <summary>
//        /// Add or updates an entity
//        /// </summary>
//        public async Task AddUpdateAsync<EntityType>(string storageTable, EntityType entity)
//            where EntityType : TableEntity, new()
//        {
//            var table = await GetTableReferenceAsync(storageTable);
//            var insertOperation = TableOperation.InsertOrReplace(entity);
//            await table.ExecuteAsync(insertOperation);
//        }


//        /// <summary>
//        /// Add or updates a batch of entities
//        /// </summary>
//        protected async Task AddUpdateAsync<EntityType>(string storageTable, IEnumerable<EntityType> entities)
//            where EntityType : TableEntity, new()
//        {
//            var batchOperation = new TableBatchOperation();
//            var lastPartitionKey = string.Empty;
//            var table = await GetTableReferenceAsync(storageTable);

//            var orderedEntities = entities.OrderBy(e => e.PartitionKey).ToList();

//            foreach (var entity in orderedEntities)
//            {
//                if (lastPartitionKey == string.Empty)
//                {
//                    lastPartitionKey = entity.PartitionKey;
//                }

//                if (lastPartitionKey == entity.PartitionKey)
//                {
//                    batchOperation.InsertOrReplace(entity);
//                }
//                else
//                {
//                    await table.ExecuteBatchAsync(batchOperation);
//                    batchOperation.Clear();
//                    batchOperation = new TableBatchOperation();
//                    lastPartitionKey = entity.PartitionKey;
//                    batchOperation.InsertOrReplace(entity);
//                }

//                if (batchOperation.Count != 100) continue;

//                await table.ExecuteBatchAsync(batchOperation);

//                batchOperation.Clear();
//                batchOperation = new TableBatchOperation();
//                lastPartitionKey = string.Empty;
//            }

//            if (batchOperation.Count > 0)
//            {
//                await table.ExecuteBatchAsync(batchOperation);
//                batchOperation.Clear();
//            }
//        }



//        /// <summary>
//        /// Returns data from a specific table in Azure
//        /// </summary>
//        public async Task<IEnumerable<EntityType>> GetItemsAsync<EntityType>(string storageTable, string partitionKey = "", int maxCount = -1)
//        where EntityType : TableEntity, new()
//        {
//            TableQuery<EntityType> tableQuery;

//            if (partitionKey.Length > 0)
//            {
//                var filterCondition =
//                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, $"{partitionKey}");
//                tableQuery = new TableQuery<EntityType>().Where(filterCondition);
//            }
//            else
//            {
//                tableQuery = new TableQuery<EntityType>();
//            }

//            return await GetItemsAsync(storageTable, tableQuery, maxCount);
//        }



//        /// <summary>
//        /// Implementation of azure storage get 
//        /// </summary>
//        protected async Task<IEnumerable<EntityType>> GetItemsAsync<EntityType>(string storageTable, TableQuery<EntityType> tableQuery, int maxCount = -1)
//            where EntityType : TableEntity, new()
//        {
//            var items = new List<EntityType>();
//            TableContinuationToken continuationToken = null;

//            var table = await GetTableReferenceAsync(storageTable);

//            do
//            {
//                var tableQueryResult = await table.ExecuteQuerySegmentedAsync(tableQuery, continuationToken);
//                continuationToken = tableQueryResult.ContinuationToken;
//                items.AddRange(tableQueryResult.Results);
//                if (maxCount != -1)
//                {
//                    if (items.Count > maxCount) break;
//                }

//            } while (continuationToken != null);

//            return items;
//        }


//        /// <summary>
//        /// Returns all items from a certain table
//        /// </summary>
//        /// <returns></returns>
//        internal async Task<T> GetItemAsync<T>(string storageTable, string partitionKey, string rowKey) where T : TableEntity, new()
//        {
//            var table = await GetTableReferenceAsync(storageTable);
//            var retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
//            var result = await table.ExecuteAsync(retrieveOperation);
//            return (T)result.Result;
//        }

//        /// <summary>
//        /// Deletes an item
//        /// </summary>        
//        internal async Task<bool> DeleteAsync<T>(string storageTable, string partitionKey, string rowKey) where T : TableEntity, new()
//        {
//            var table = await GetTableReferenceAsync(storageTable);
//            var retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
//            var result = await table.ExecuteAsync(retrieveOperation);
//            var item = (T)result.Result;

//            var deleteOperation = TableOperation.Delete(item);
//            await table.ExecuteAsync(deleteOperation);
//            return true;
//        }


//        /// <summary>
//        /// Adds a media file
//        /// </summary>         
//        public async Task<string> AddOrUpdateFileAsync(string containerName, string folderName, string filename, Stream stream)
//        {
//            var storageAccount = CloudStorageAccount.Parse(this.connectionString);
//            var blobClient = storageAccount.CreateCloudBlobClient();
//            var container = blobClient.GetContainerReference(containerName);
//            var folder = container.GetDirectoryReference(folderName);
//            var blockBlob = folder.GetBlockBlobReference(filename);
//            await blockBlob.UploadFromStreamAsync(stream);
//            return blockBlob.Uri.ToString();
//        }


//        /// <summary>
//        /// Deletes media file
//        /// </summary>         
//        public async Task<bool> DeleteFilesync(string containerName, string filename)
//        {
//            var storageAccount = CloudStorageAccount.Parse(this.connectionString);
//            var blobClient = storageAccount.CreateCloudBlobClient();
//            var container = blobClient.GetContainerReference(containerName);
//            var blockBlob = container.GetBlockBlobReference(filename);
//            await blockBlob.DeleteAsync();
//            return true;
//        }


//        /// <summary>
//        /// Creates a new blob container
//        /// </summary>        
//        public async Task<bool> CreateBlobContainer(string containerName)
//        {
//            var storageAccount = CloudStorageAccount.Parse(this.connectionString);
//            var blobClient = storageAccount.CreateCloudBlobClient();
//            var container = blobClient.GetContainerReference(containerName);            
//            await container.CreateIfNotExistsAsync();            
//            return true;
//        }


//        /// <summary>
//        /// Creates a new blob container
//        /// </summary>        
//        public async Task<bool> DeleteBlobContainer(string containerName)
//        {
//            var storageAccount = CloudStorageAccount.Parse(this.connectionString);
//            var blobClient = storageAccount.CreateCloudBlobClient();
//            var container = blobClient.GetContainerReference(containerName);
//            await container.DeleteIfExistsAsync();
//            return true;
//        }


//        /// <summary>
//        /// Add element message to a Queue
//        /// </summary>        
//        public async Task<bool> AddToQueueAsync(string queueName, IEnumerable<string> elements)
//        {
//            var storageAccount = CloudStorageAccount.Parse(this.connectionString);
//            var queueClient = storageAccount.CreateCloudQueueClient();
//            var queue = queueClient.GetQueueReference(queueName);
//            await queue.CreateIfNotExistsAsync();

//            // Create a message and add it to the queue.
//            foreach (var element in elements)
//            {
//                var queueMessage = new CloudQueueMessage(element);
//                await queue.AddMessageAsync(queueMessage);
//            }
//            return true;
//        }


//        /// <summary>
//        /// Add an element to a Queue
//        /// </summary>        
//        public async Task<bool> AddToQueueAsync<T>(string queueName, IEnumerable<T> messages)
//        {
//            var storageAccount = CloudStorageAccount.Parse(this.connectionString);
//            var queueClient = storageAccount.CreateCloudQueueClient();
//            var queue = queueClient.GetQueueReference(queueName);
//            await queue.CreateIfNotExistsAsync();

//            // Create a message and add it to the queue.
//            foreach (var message in messages)
//            {
//                var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(message));
//                await queue.AddMessageAsync(queueMessage);
//            }
//            return true;
//        }


//        /// <summary>
//        /// Peek an element in the queue but dont remove it
//        /// </summary>        
//        public async Task<string> PeekQueueAsync(string queueName)
//        {
//            var storageAccount = CloudStorageAccount.Parse(this.connectionString);
//            var queueClient = storageAccount.CreateCloudQueueClient();
//            var queue = queueClient.GetQueueReference(queueName);
//            await queue.CreateIfNotExistsAsync();
//            var peekedMessage = await queue.PeekMessageAsync();
//            return peekedMessage == null ? string.Empty : peekedMessage.AsString;
//        }


//        /// <summary>
//        /// Gets and element from the queue and optionally delete it
//        /// </summary>        
//        public async Task<string> GetFromQueueAsync(string queueName, bool deleteMessage)
//        {
//            var storageAccount = CloudStorageAccount.Parse(this.connectionString);
//            var queueClient = storageAccount.CreateCloudQueueClient();
//            var queue = queueClient.GetQueueReference(queueName);
//            await queue.CreateIfNotExistsAsync();
//            var message = await queue.GetMessageAsync();
//            if (deleteMessage && message != null)
//            {
//                await queue.DeleteMessageAsync(message);
//            }
//            return message != null ? message.AsString : string.Empty;
//        }


//        /// <summary>
//        /// Deletes the top element from the queue
//        /// </summary>        
//        public async Task<bool> DeleteFromQueueAsync(string queueName)
//        {
//            var storageAccount = CloudStorageAccount.Parse(this.connectionString);
//            var queueClient = storageAccount.CreateCloudQueueClient();
//            var queue = queueClient.GetQueueReference(queueName);
//            await queue.CreateIfNotExistsAsync();
//            var message = await queue.GetMessageAsync();
//            if (message != null)
//            {
//                await queue.DeleteMessageAsync(message);
//            }
//            return true;
//        }


//        /// <summary>
//        /// Returns the queue length
//        /// </summary>        
//        public async Task<int?> GetQueueLengthAsync(string queueName)
//        {
//            var storageAccount = CloudStorageAccount.Parse(this.connectionString);
//            var queueClient = storageAccount.CreateCloudQueueClient();
//            var queue = queueClient.GetQueueReference(queueName);
//            await queue.CreateIfNotExistsAsync();

//            await queue.FetchAttributesAsync();

//            int? cachedMessageCount = queue.ApproximateMessageCount;

//            return cachedMessageCount;
//        }
//    }
//}
