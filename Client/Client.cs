using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Client.Clients;
using Client.Clients.Implementations;
using Client.Entities;
using Client.Services;
using Client.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Client
{
    class Client
    {
        private static volatile bool isStop = false;
        
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);
            var configuration = builder.Build();

            var serverUrlAddresses = JsonConvert.DeserializeObject<IEnumerable<ServerModel>>(configuration["ServersJson"]);
            
            IAzureHttpClient azureHttpClient = new AzureHttpClient();
            IListenerService listenerService = new ListenerService(azureHttpClient, serverUrlAddresses);

            Console.CancelKeyPress += (sender, eventArgs) => isStop = true;
            
            while (!isStop)
            {
                Console.Clear();
                foreach (var server in await listenerService.GetInstanceIdServers())
                {
                    Console.WriteLine($"{server.Name} - {server.Url}\tInstanceId: {server.InstanceId} [{server.Ping} ms.]");
                }
                Console.WriteLine("Press Ctrl+C or Ctrl+Break for exit");
                Thread.Sleep(1000);
            }
        }
    }
}