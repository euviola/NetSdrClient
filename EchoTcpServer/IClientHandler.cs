using System.Net.Sockets;
using EchoServer;

namespace EchoServer
{
    public interface IClientHandler
    {
        Task HandleAsync(TcpClient client, CancellationToken token);
    }
}
