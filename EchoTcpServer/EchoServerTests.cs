using NUnit.Framework;
using NSubstitute;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace EchoServerTests;

public class EchoServerTests
{
    [Test]
    public async Task StartAsync_Should_Start_Listener()
    {
        var listener = Substitute.For<ITcpListener>();
        listener.AcceptTcpClientAsync()
            .Returns(Task.FromResult(new TcpClient()));

        var handler = Substitute.For<IClientHandler>();

        var server = new EchoServer.EchoServer(listener, handler);

        var task = server.StartAsync();
        await Task.Delay(50);

        listener.Received(1).Start();

        server.Stop();
    }

    [Test]
    public async Task StartAsync_Should_Invoke_Handler()
    {
        var listener = Substitute.For<ITcpListener>();
        listener.AcceptTcpClientAsync()
            .Returns(Task.FromResult(new TcpClient()));

        var handler = Substitute.For<IClientHandler>();

        var server = new EchoServer.EchoServer(listener, handler);

        var task = server.StartAsync();
        await Task.Delay(50);

        await handler.Received(1)
            .HandleAsync(Arg.Any<TcpClient>(), Arg.Any<CancellationToken>());

        server.Stop();
    }

    [Test]
    public void Stop_Should_Stop_Listener()
    {
        var listener = Substitute.For<ITcpListener>();
        var handler = Substitute.For<IClientHandler>();

        var server = new EchoServer.EchoServer(listener, handler);

        server.Stop();

        listener.Received(1).Stop();
    }
}
