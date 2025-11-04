using NetSdrClientApp.Networking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetSdrClientAppTests
{
    using NetSdrClientApp.Networking;
using System;
using System.Threading;
using System.Threading.Tasks;

public class FakeTcpClient : ITcpClient
{
    public bool Connected { get; set; }
    public event EventHandler<byte[]> MessageReceived;

    public void Connect() => Connected = true;
    public void Disconnect() => Connected = false;

    public Task SendMessageAsync(byte[] message)
    {
        Task.Run(() =>
        {
            Thread.Sleep(10);
            MessageReceived?.Invoke(this, new byte[] { 0xAA, 0xBB });
        });

        return Task.CompletedTask;
    }
}


    public class FakeUdpClient : IUdpClient
{
    public event EventHandler<byte[]> MessageReceived;

    public Task StartListeningAsync() => Task.CompletedTask;
    public void StopListening() { }
    
    public void Exit() { }
}
}
