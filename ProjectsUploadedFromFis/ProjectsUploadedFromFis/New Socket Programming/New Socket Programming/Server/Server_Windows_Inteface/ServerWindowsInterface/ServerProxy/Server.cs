using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDLL
{
    [Serializable]
    public class Server 
    {
        AbstractServer server;

        public void Start(ServerType serverType, string port)
        {
           server = SeverFactory.GetServer(serverType, port);
           server.Start();
        }

        public void Fun1()
        {

        }

        public void Dispose()
        {
            server.Dispose();

        }

        public override string ToString()
        {

            return "Test String";
        }
    }
}
