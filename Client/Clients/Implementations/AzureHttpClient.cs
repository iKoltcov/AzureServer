using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Entities;

namespace Client.Clients.Implementations
{
    public class AzureHttpClient : IAzureHttpClient
    {
        public async Task<PingResult> PingServer(string url)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback += HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    httpClient.BaseAddress = new Uri(url);
                    httpClient.Timeout = new TimeSpan(0, 0, 10);
                    
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    using (var request = await httpClient.GetAsync("api/instance"))
                    {
                        stopwatch.Stop();
                        var result = await request.Content.ReadAsStringAsync();
                        
                        return new PingResult()
                        {
                            InstanceId = result,
                            Ping = stopwatch.ElapsedMilliseconds.ToString()
                        };
                    }
                }
            }
        }
    }
}