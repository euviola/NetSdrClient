using EchoServer;

namespace EchoServerTests
{
    public class MinimalClientHandler : IClientHandler
        {
            public Task HandleAsync(System.Net.Sockets.TcpClient client, System.Threading.CancellationToken token)
            {
                return Task.CompletedTask;
            }
        }
}
