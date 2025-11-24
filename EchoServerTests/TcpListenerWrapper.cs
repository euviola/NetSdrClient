using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using EchoServer;

namespace EchoServerTests
{
    public class TcpListenerWrapper : ITcpListener
{
    private readonly TcpListener _listener;

    public TcpListenerWrapper(IPAddress address, int port)
    {
        _listener = new TcpListener(address, port);
    }

    public void Start() => _listener.Start();
    public void Stop() => _listener.Stop();
    public Task<TcpClient> AcceptTcpClientAsync() => _listener.AcceptTcpClientAsync();
}
}
