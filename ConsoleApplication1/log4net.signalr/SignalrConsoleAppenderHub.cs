using SignalR.Hubs;

namespace log4net.SignalR
{
    public class SignalrConsoleAppenderHub : Hub
    {
       public void OnMessageLogged(LogEntryForConsole e)
        {
            Clients.onLoggedEvent(e);
        }
    }
}