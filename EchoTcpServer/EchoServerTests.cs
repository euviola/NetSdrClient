using NUnit.Framework;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace EchoServerTests
{
    [TestFixture]
    public class EchoServerTests
    {
        [Test]
        public async Task EchoServer_StartAndStop_Works()
        {
            // Arrange
            var server = new EchoServer(9000);
            _ = server.StartAsync(); // запуск без await

            // Act — підключаємо клієнта
            await Task.Delay(100); // треба дочекатись Start()
            using var client = new TcpClient();
            await client.ConnectAsync("127.0.0.1", 9000);

            // Assert — з'єднання є
            Assert.IsTrue(client.Connected);

            // Stop server
            server.Stop();
            await Task.Delay(50); // дати коректно зупинитись
        }

        [Test]
        public async Task EchoServer_EchoesData()
        {
            // Arrange
            var server = new EchoServer(9001);
            _ = server.StartAsync();

            await Task.Delay(100);
            using var client = new TcpClient();
            await client.ConnectAsync("127.0.0.1", 9001);

            var stream = client.GetStream();

            byte[] sent = { 1, 2, 3, 4 };
            await stream.WriteAsync(sent, 0, sent.Length);

            byte[] buffer = new byte[4];
            int read = await stream.ReadAsync(buffer, 0, buffer.Length);

            // Assert
            Assert.AreEqual(sent.Length, read);
            Assert.AreEqual(sent, buffer);

            server.Stop();
        }

        [Test]
        public async Task EchoServer_Stop_StopsServer()
        {
            var server = new EchoServer(9002);
            _ = server.StartAsync();

            await Task.Delay(100);
            server.Stop();

            await Task.Delay(50);

            // Trying connect
            Assert.ThrowsAsync<SocketException>(async () =>
            {
                var c = new TcpClient();
                await c.ConnectAsync("127.0.0.1", 9002);
            });
        }
    }

    [TestFixture]
    public class UdpTimedSenderTests
    {
        [Test]
        public void UdpTimedSender_ThrowsIfStartedTwice()
        {
            using var sender = new UdpTimedSender("127.0.0.1", 12345);
            sender.StartSending(100);

            Assert.Throws<InvalidOperationException>(() => sender.StartSending(100));
        }

        [Test]
        public void UdpTimedSender_Dispose_NoException()
        {
            using var sender = new UdpTimedSender("127.0.0.1", 12345);
            sender.StartSending(50);

            // просто не повинен впасти
            Assert.DoesNotThrow(() => sender.Dispose());
        }
    }

}
