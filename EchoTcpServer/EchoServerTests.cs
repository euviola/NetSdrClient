using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace EchoServerTests
{
    public class EchoServerTests
    {
        [Test]
        public async Task StartAsync_StartsListener_AndStopsAfterCancel()
        {
            var listener = Substitute.For<ITcpListener>();

            listener.AcceptTcpClientAsync().Returns(async _ =>
            {
                await Task.Delay(Timeout.Infinite);
                return new TcpClient();
            });

            var handler = Substitute.For<IClientHandler>();

            var server = new EchoServer.EchoServer(listener, handler);

            var runTask = server.StartAsync();

            await Task.Delay(50);

            listener.Received(1).Start();

            server.Stop();

            await Task.Delay(50);

            listener.Received(1).Stop();
        }

        [Test]
        public void Stop_Should_Stop_Listener_Immediately()
        {
            var listener = Substitute.For<ITcpListener>();
            var handler = Substitute.For<IClientHandler>();

            var server = new EchoServer.EchoServer(listener, handler);

            server.Stop();

            listener.Received(1).Stop();
        }
    }
}
