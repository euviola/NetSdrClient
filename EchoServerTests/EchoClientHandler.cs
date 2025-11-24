using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using EchoServer;

namespace EchoServerTests
{
    public class EchoClientHandler : IClientHandler
    {
        public async Task HandleAsync(TcpClient client, CancellationToken token)
        {
            using var stream = client.GetStream();
            byte[] buffer = new byte[8192];

            int bytesRead;
            while (!token.IsCancellationRequested &&
                (bytesRead = await stream.ReadAsync(buffer, token)) > 0)
            {
                await stream.WriteAsync(buffer.AsMemory(0, bytesRead), token);
            }
        }
    }
}
