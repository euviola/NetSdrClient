using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace NetSdrClientApp.Networking
{
    public class BadReference
    {
        public EchoServer.EchoServer Server = new EchoServer.EchoServer(5000);
    }
}
