using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Entities;

namespace Client.Services
{
    public interface IListenerService
    {
        Task<IEnumerable<PingServerResult>> GetInstanceIdServers();
    }
}