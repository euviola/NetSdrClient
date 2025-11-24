using System.Net.Sockets;
using System.Net;

namespace EchoServer
{
    public interface ITcpListener
    {
        void Start();
        void Stop();
        Task<TcpClient> AcceptTcpClientAsync();
    }
}
