using Azure.TableStorage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterServicesExtension
    {
        public static IServiceCollection AddAzureTableServices(this IServiceCollection s, Action<TableStorageOptions> o)
        {
            s.Configure(o);

            s.TryAddScoped<ITableStorageService, TableStorageService>();            

            return s;
        }
    }
}
