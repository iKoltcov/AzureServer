using System.Threading.Tasks;
using Client.Entities;

namespace Client.Clients
{
    public interface IAzureHttpClient
    {
        Task<PingResult> PingServer(string url);
    }
}