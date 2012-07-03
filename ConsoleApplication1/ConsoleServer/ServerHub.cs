using System;
using SignalR.Hubs;

namespace ConsoleServer
{
    public class ServerHub : Hub
    {
        public void SendMessage(string message)
        {
            Console.WriteLine(string.Format("Message from ServerHub: {0}",message));
            Clients.foo(message);
        }
    }
}