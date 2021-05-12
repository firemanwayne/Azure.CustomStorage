using Microsoft.Extensions.Options;

namespace Azure.TableStorage
{
    internal abstract class AzureServiceBase
    {
        protected TableStorageOptions Options { get; }

        protected AzureServiceBase(IOptions<TableStorageOptions> o)
        {
            Options = o.Value;
        }
    }
}
