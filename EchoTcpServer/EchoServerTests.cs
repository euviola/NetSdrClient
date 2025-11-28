using NUnit.Framework;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using EchoServerApp;

namespace EchoServerTests
{
    [TestFixture]
    public class EchoServerTests
    {
        [Test]
        public async Task StartAsync_ShouldStartServerWithoutException()
        {
            var server = new EchoServer(9100);

            Assert.DoesNotThrowAsync(async () => await server.StartAsync());
            server.Stop();
        }

        [Test]
        public async Task Server_ShouldAcceptMultipleClients()
        {
            var server = new EchoServer(9101);
            _ = server.StartAsync();

            await Task.Delay(100);

            for (int i = 0; i < 3; i++)
            {
                using var client = new TcpClient();
                await client.ConnectAsync("127.0.0.1", 9101);
                Assert.IsTrue(client.Connected);
            }

            server.Stop();
        }

        [Test]
        public async Task Server_ShouldEchoBackDifferentDataSizes()
        {
            var server = new EchoServer(9102);
            _ = server.StartAsync();
            await Task.Delay(100);

            using var client = new TcpClient();
            await client.ConnectAsync("127.0.0.1", 9102);

            var stream = client.GetStream();

            byte[] sent = new byte[256];
            new Random().NextBytes(sent);

            await stream.WriteAsync(sent, 0, sent.Length);

            byte[] received = new byte[256];
            int read = await stream.ReadAsync(received, 0, received.Length);

            Assert.AreEqual(256, read);
            CollectionAssert.AreEqual(sent, received);

            server.Stop();
        }

        [Test]
        public async Task Stop_ShouldRefuseNewConnections()
        {
            var server = new EchoServer(9103);
            _ = server.StartAsync();

            await Task.Delay(100);
            server.Stop();

            await Task.Delay(50);

            Assert.ThrowsAsync<SocketException>(async () =>
            {
                using var client = new TcpClient();
                await client.ConnectAsync("127.0.0.1", 9103);
            });
        }

        [Test]
        public async Task EchoServer_StartTwice_ShouldNotThrowAndNotCrash()
        {
            var server = new EchoServer(9104);

            await server.StartAsync();

            // Якщо сервер написаний правильно – другий StartAsync просто ігнорується або працює idempotently
            Assert.DoesNotThrowAsync(async () => await server.StartAsync());

            server.Stop();
        }
    }

    [TestFixture]
    public class UdpTimedSenderTests
    {
        [Test]
        public void StartSending_ShouldThrowIfCalledTwice()
        {
            using var sender = new UdpTimedSender("127.0.0.1", 9999);

            sender.StartSending(100);

            Assert.Throws<InvalidOperationException>(() => sender.StartSending(100));
        }

        [Test]
        public void StartSending_WithInvalidInterval_ShouldThrow()
        {
            using var sender = new UdpTimedSender("127.0.0.1", 9999);

            Assert.Throws<ArgumentOutOfRangeException>(() => sender.StartSending(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => sender.StartSending(-10));
        }

        [Test]
        public void Dispose_ShouldStopSendingWithoutExceptions()
        {
            using var sender = new UdpTimedSender("127.0.0.1", 9999);

            sender.StartSending(50);

            Assert.DoesNotThrow(() => sender.Dispose());
        }
    }
}
