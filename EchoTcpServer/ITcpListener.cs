using System.Net.Sockets;
using System.Net;

public interface ITcpListener
{
    void Start();
    void Stop();
    Task<TcpClient> AcceptTcpClientAsync();
}