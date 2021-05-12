using Azure.BlobStorage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterServicesExtension
    {
        public static IServiceCollection AddAzureBlobStorage(this IServiceCollection s, Action<BlobStorageOptions> o)
        {
            s.Configure(o);

            s.TryAddScoped<ITableStorageService>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<BlobStorageOptions>>();
                return new TableStorageService(options);
            });

            s.TryAddScoped<IBlobStorageService>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<BlobStorageOptions>>();
                return new BlobStorageService(options);
            });           

            return s;
        }
    }
}
