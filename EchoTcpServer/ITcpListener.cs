using System.Net.Sockets;
using System.Net;
using EchoServer;

namespace EchoServer
{
    public interface ITcpListener
    {
        void Start();
        void Stop();
        Task<TcpClient> AcceptTcpClientAsync();
    }
}