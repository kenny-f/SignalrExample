using SignalR.Hubs;

namespace log4net.signalr
{
    public class Chat : Hub
    {
        public void Send(string message)
        {
            
            Clients.addMessage(message);
        }
    }
}