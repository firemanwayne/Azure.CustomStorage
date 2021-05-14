using Azure.Data.Tables;
using System;

namespace Azure.TableStorage.Abstract
{
    public abstract class AzureTableEntity : ITableEntity
    {
        protected AzureTableEntity() { }

        protected AzureTableEntity(string partitionKey)
        {
            PartitionKey = partitionKey;
            RowKey = Guid.NewGuid().ToString();
            ETag = new();
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public ETag ETag { get; set; }
        public DateTimeOffset? Timestamp => DateTimeOffset.UtcNow;
    }
}
