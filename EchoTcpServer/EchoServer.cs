using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EchoServer;

namespace EchoServer
{
    public class EchoServer
    {
        private readonly ITcpListener _listener;
        private readonly IClientHandler _handler;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public EchoServer(ITcpListener listener, IClientHandler handler)
        {
            _listener = listener;
            _handler = handler;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task StartAsync()
        {
            _listener.Start();

            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                TcpClient client;
                try
                {
                    client = await _listener.AcceptTcpClientAsync();
                }
                catch (ObjectDisposedException)
                {
                    break;
                }

                _ = Task.Run(() => _handler.HandleAsync(client, _cancellationTokenSource.Token));
            }
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _listener.Stop();
        }
    }
}