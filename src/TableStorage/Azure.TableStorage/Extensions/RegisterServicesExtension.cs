using Azure.TableStorage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterServicesExtension
    {
        public static IServiceCollection AddAzureQueueServices(this IServiceCollection s, Action<TableStorageOptions> o)
        {
            s.Configure(o);            

            s.TryAddScoped<ITableStorageService>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<TableStorageOptions>>();
                return new TableStorageService(options);
            });

            return s;
        }
    }
}
