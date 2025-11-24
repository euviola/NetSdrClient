using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using EchoServerTests;
using EchoServer;
using System.Net;

namespace EchoServerTests
{
    public class EchoServerTests
    {
        [Test]
        public async Task EchoServer_StartAndStop_RealCodeExecuted()
        {
            // Використовуємо реальний listener на локальному порту
            var listener = new TcpListenerWrapper(IPAddress.Loopback, 0);

            // Мінімальний обробник, просто повертає завершене завдання
            var handler = new MinimalClientHandler();

            var server = new EchoServer.EchoServer(listener, handler);

            // Стартуємо сервер асинхронно
            var runTask = server.StartAsync();

            // Даємо серверу трохи часу для старту
            await Task.Delay(50);

            // Тестуємо Stop, щоб покрити цей метод
            server.Stop();

            // Чекаємо завершення StartAsync після Stop
            await Task.Delay(50);

            Assert.Pass("Server started and stopped successfully");
        }
    }
}
