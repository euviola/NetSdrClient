using System.Threading.Tasks;
using Xunit;
using NetSdrClientApp;
using NetSdrClientAppTests;

public class NetSdrClientTests
{
    [Fact]
    public async Task ConnectAsync_ShouldConnectAndSendInitMessages()
    {
        var fakeTcp = new FakeTcpClient();
        var fakeUdp = new FakeUdpClient();
        var client = new NetSdrClient(fakeTcp, fakeUdp);

        await client.ConnectAsync();

        Xunit.Assert.True(fakeTcp.Connected);
    }

    [Fact]
    public async Task StartIQAsync_ShouldSetIQStartedTrue()
    {
        var fakeTcp = new FakeTcpClient { Connected = true };
        var fakeUdp = new FakeUdpClient();
        var client = new NetSdrClient(fakeTcp, fakeUdp);

        await client.StartIQAsync();

        Xunit.Assert.True(client.IQStarted);
    }

    [Fact]
    public async Task StopIQAsync_ShouldSetIQStartedFalse()
    {
        var fakeTcp = new FakeTcpClient { Connected = true };
        var fakeUdp = new FakeUdpClient();
        var client = new NetSdrClient(fakeTcp, fakeUdp);
        await client.StartIQAsync();

        await client.StopIQAsync();

        Xunit.Assert.False(client.IQStarted);
    }

    [Fact]
    public async Task ChangeFrequencyAsync_ShouldNotThrow()
    {
        var fakeTcp = new FakeTcpClient { Connected = true };
        var fakeUdp = new FakeUdpClient();
        var client = new NetSdrClient(fakeTcp, fakeUdp);

        await client.ChangeFrequencyAsync(1000000, 1);
    }

    [Fact]
    public void Disconnect_ShouldSetConnectedFalse()
    {
        var fakeTcp = new FakeTcpClient { Connected = true };
        var fakeUdp = new FakeUdpClient();
        var client = new NetSdrClient(fakeTcp, fakeUdp);

        client.Disconect();

        Xunit.Assert.False(fakeTcp.Connected);
    }
}
