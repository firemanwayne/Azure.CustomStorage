using Azure.QueueStorage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterServicesExtension
    {
        public static IServiceCollection AddAzureQueueServices(this IServiceCollection s, Action<QueueStorageOptions> o)
        {
            s.Configure(o);            

            s.TryAddScoped<IQueueStorageService>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<QueueStorageOptions>>();
                return new QueueStorageService(options);
            });

            return s;
        }
    }
}
