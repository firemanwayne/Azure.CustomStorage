using Azure.Data.Tables.Models;
using Azure.TableStorage.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure.TableStorage
{
    public interface ITableStorageService
    {
        Task Create<T>();
        Task Delete<T>();
        Task Insert<T>(T entity) where T : AzureTableEntity, new();
        IAsyncEnumerable<TableItem> GetTables();
    }
}
