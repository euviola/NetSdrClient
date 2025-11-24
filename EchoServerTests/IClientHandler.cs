using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using EchoServer;

namespace EchoServerTests
{
    public interface IClientHandler
    {
        Task HandleAsync(TcpClient client, CancellationToken token);
    }
}
