using System.Net.Sockets;
using Xunit;

public class EchoServerTests
{
    [Fact]
    public async Task EchoServer_StartAndStop_Works()
    {
        // Arrange
        var server = new EchoServer(9000);
        var serverTask = server.StartAsync(); // запуск без await

        // Act — підключаємо клієнта
        await Task.Delay(100); // треба дочекатись Start()
        using var client = new TcpClient();
        await client.ConnectAsync("127.0.0.1", 9000);

        // Assert — з'єднання є
        Assert.True(client.Connected);

        // Stop server
        server.Stop();
        await Task.Delay(50); // дати коректно зупинитись
    }

    [Fact]
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
        Assert.Equal(sent.Length, read);
        Assert.Equal(sent, buffer);

        server.Stop();
    }

    [Fact]
    public async Task EchoServer_Stop_StopsServer()
    {
        var server = new EchoServer(9002);
        var task = server.StartAsync();

        await Task.Delay(100);
        server.Stop();

        await Task.Delay(50);

        // Trying connect
        await Assert.ThrowsAsync<SocketException>(() =>
        {
            var c = new TcpClient();
            return c.ConnectAsync("127.0.0.1", 9002);
        });
    }

    public class UdpTimedSenderTests
    {
        [Fact]
        public void UdpTimedSender_ThrowsIfStartedTwice()
        {
            using var sender = new UdpTimedSender("127.0.0.1", 12345);
            sender.StartSending(100);

            Assert.Throws<InvalidOperationException>(() => sender.StartSending(100));
        }
    }

    [Fact]
    public void UdpTimedSender_Dispose_NoException()
    {
        using var sender = new UdpTimedSender("127.0.0.1", 12345);
        sender.StartSending(50);
        sender.Dispose(); // просто не повинен впасти
    }
}
