using Azure.BlobStorage.Options;
using Microsoft.Extensions.Options;

namespace Azure.BlobStorage
{
    public abstract class AzureServiceBase
    {
        protected BlobStorageOptions Options { get; }

        protected AzureServiceBase(IOptions<BlobStorageOptions> o)
        {
            Options = o.Value;
        }
    }
}
