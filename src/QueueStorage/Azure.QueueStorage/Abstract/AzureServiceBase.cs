using Microsoft.Extensions.Options;

namespace Azure.QueueStorage
{
    internal abstract class AzureServiceBase
    {
        protected QueueStorageOptions Options { get; }

        protected AzureServiceBase(IOptions<QueueStorageOptions> o)
        {
            Options = o.Value;
        }
    }
}
