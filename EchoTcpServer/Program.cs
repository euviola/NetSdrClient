using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EchoServerTests;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var listener = new TcpListenerWrapper(IPAddress.Any, 5000);
        var handler = new EchoClientHandler();

        var server = new EchoServer.EchoServer(listener, handler);

        await server.StartAsync();
    }
}
