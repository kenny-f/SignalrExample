using SignalR.Hubs;

namespace log4net.signalr
{
    public class MultipleWorkersHub : Hub
    {
        public void SendMessage(string id, string message)
        {
            Clients.addMessage(id, message);
        }
    }
}