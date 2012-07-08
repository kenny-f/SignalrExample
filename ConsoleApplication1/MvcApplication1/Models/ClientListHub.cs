using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SignalR.Hubs;

namespace MvcApplication1.Models
{
    public class ClientListHub : Hub, IConnected, IDisconnect
    {
        public ClientListHub()
        {
            
        }

        public void SendMessage(string s)
        {
            ClientList.Instance.AddMessage(s);
            Clients.pushMessage(s);
        }

        public Task Connect()
        {
            ClientList.Instance.Add(Context.ConnectionId);
            foreach (string message in ClientList.Instance.Messages)
            {
                Caller.pushMessage(message);
            }
            return Clients.joined(Context.ConnectionId, DateTime.Now.ToString());
        }

        public Task Reconnect(IEnumerable<string> groups)
        {
            return Clients.rejoined(Context.ConnectionId, DateTime.Now.ToString());
        }

        public Task Disconnect()
        {
            ClientList.Instance.Remove(Context.ConnectionId);
            return Clients.leave(Context.ConnectionId, DateTime.Now.ToString());
        }
    }
}