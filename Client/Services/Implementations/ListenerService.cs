using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Clients;
using Client.Entities;

namespace Client.Services.Implementations
{
    public class ListenerService : IListenerService
    {
        private readonly IAzureHttpClient _azureHttpClient;

        private readonly IEnumerable<ServerModel> _serverModels;
        
        public ListenerService(IAzureHttpClient azureHttpClient, IEnumerable<ServerModel> serverModels)
        {
            _azureHttpClient = azureHttpClient;
            _serverModels = serverModels;
        }

        public async Task<IEnumerable<PingServerResult>> GetInstanceIdServers()
        {
            var results = new List<PingServerResult>();
            
            foreach (var server in _serverModels)
            {
                try
                {
                    var pingServer = await _azureHttpClient.PingServer(server.Url);

                    results.Add(new PingServerResult()
                    {
                        Name = server.Name,
                        Url = server.Url,
                        InstanceId = pingServer.InstanceId,
                        Ping = pingServer.Ping
                    });
                }
                catch (Exception)
                {
                    results.Add(new PingServerResult()
                    {
                        Name = server.Name,
                        Url = server.Url,
                        InstanceId = "no data",
                        Ping = "no data"
                    });
                }
            }

            return results;
        }
    }
}