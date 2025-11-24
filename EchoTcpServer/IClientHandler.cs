using System.Net.Sockets;

namespace EchoServerTests
{
    public interface IClientHandler
    {
        Task HandleAsync(TcpClient client, CancellationToken token);
    }
}
