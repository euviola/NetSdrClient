using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using EchoServer;

namespace EchoServerTests
{
    public interface ITcpListener
    {
        void Start();
        void Stop();
        Task<TcpClient> AcceptTcpClientAsync();
    }
}
