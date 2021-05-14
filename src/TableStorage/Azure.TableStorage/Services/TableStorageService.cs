using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Azure.TableStorage.Abstract;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;

namespace Azure.TableStorage
{
    internal class TableStorageService : AzureServiceBase, ITableStorageService
    {
        readonly TableServiceClient serviceClient;
        public TableStorageService(IOptions<TableStorageOptions> o) : base(o)
        {
            serviceClient = new TableServiceClient(
                Options.TableUri,
                new TableSharedKeyCredential(Options.AccountName, Options.AccountKey));
        }

        public async Task Create<T>()
            => await serviceClient.CreateTableIfNotExistsAsync(typeof(T).Name);                    

        public async Task Delete<T>()
            => await serviceClient.DeleteTableAsync(typeof(T).Name);        

        public IAsyncEnumerable<TableItem> GetTables() => serviceClient.QueryAsync();

        public async Task Insert<T>(T entity) where T : AzureTableEntity, new()
        {
            var client = serviceClient.GetTableClient(typeof(T).Name);
            await client.CreateIfNotExistsAsync();

            try
            {
                await client.UpsertEntityAsync(entity);            
            }
            catch(Exception ex)
            {
                WriteLine(ex.Message);
            }
        }
    }
}
