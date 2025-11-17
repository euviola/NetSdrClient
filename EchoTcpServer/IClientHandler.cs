using System.Net.Sockets;

public interface IClientHandler
{
    Task HandleAsync(TcpClient client, CancellationToken token);
}