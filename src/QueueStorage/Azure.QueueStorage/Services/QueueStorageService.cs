using Azure.Storage.Queues;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Azure.QueueStorage
{
    internal class QueueStorageService<T> : AzureServiceBase, IQueueStorageService<T>
    {
        Func<T, CancellationToken, Task> Handler;

        public QueueStorageService(IOptions<QueueStorageOptions> o) : base(o)
        { }

        public async Task<bool> Create(string queueName)
        {
            try
            {
                var queueClient = new QueueClient(Options.ConnectionString, queueName);
                await queueClient.CreateIfNotExistsAsync();

                if (await queueClient.ExistsAsync())
                {
                    WriteLine($"Queue created: '{queueClient.Name}'");
                    return true;
                }
                else
                {
                    WriteLine($"Make sure the Azurite storage emulator running and try again.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Exception: {ex.Message}\n\n");
                WriteLine($"Make sure the Azurite storage emulator running and try again.");
                return false;
            }
        }

        public async Task Publish(string queueName, T message)
        {
            var queueClient = new QueueClient(Options.ConnectionString, queueName);

            await queueClient.CreateIfNotExistsAsync();

            if (await queueClient.ExistsAsync())            
                queueClient.SendMessage(JsonSerializer.Serialize(message, typeof(T)));            

            WriteLine($"Inserted: {message}");
        }

        public void RegisterHandler(Func<T, CancellationToken, Task> Handler)
        {
            this.Handler = Handler;
        }

        public async Task Subscribe(string queueName)
        {
            var queueClient = new QueueClient(Options.ConnectionString, queueName);

            if (await queueClient.ExistsAsync())
            {
                var retrievedMessage = await queueClient.ReceiveMessageAsync();

                var message = JsonSerializer.Deserialize<T>(retrievedMessage.Value.Body);

                if (message != null)
                    await Handler.Invoke(message, new CancellationToken());

                WriteLine($"Dequeued message: '{message.GetType()}'");

                // Delete the message
                queueClient.DeleteMessage(retrievedMessage.Value.MessageId, retrievedMessage.Value.PopReceipt);
            }
        }

        public async Task Delete(string queueName)
        {
            var queueClient = new QueueClient(Options.ConnectionString, queueName);

            if (await queueClient.ExistsAsync())
                await queueClient.DeleteAsync();

            WriteLine($"Queue deleted: '{queueClient.Name}'");
        }
    }
}
