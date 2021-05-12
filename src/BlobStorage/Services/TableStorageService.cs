using Azure.BlobStorage.Options;
using Microsoft.Extensions.Options;

namespace Azure.BlobStorage
{
    internal class TableStorageService : AzureServiceBase, ITableStorageService
    {
        public TableStorageService(IOptions<BlobStorageOptions> o) : base(o)
        {
        }
    }
}
