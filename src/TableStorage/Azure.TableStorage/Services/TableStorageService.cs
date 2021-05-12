using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Azure.TableStorage
{
    internal class TableStorageService : AzureServiceBase, ITableStorageService
    {
        public TableStorageService(IOptions<TableStorageOptions> o) : base(o)
        { }

        public async Task Create(string queueName)
        {            
        }                

        public async Task Delete(string queueName)
        {
        }
    }
}
