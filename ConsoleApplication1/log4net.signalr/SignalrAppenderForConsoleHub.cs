using SignalR.Hubs;

namespace log4net.SignalR
{
    public class SignalrAppenderForConsoleHub : Hub
    {
        public void SayHi(string message)
        {
            Clients.sayHi(message);
        }

        public void OnMessageLogged(LogEntryForConsole e)
        {
            Clients.onLoggedEvent(e);
        }
    }
}