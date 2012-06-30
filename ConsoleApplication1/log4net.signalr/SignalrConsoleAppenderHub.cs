using SignalR;
using SignalR.Hubs;

namespace log4net.SignalR
{
    public class SignalrConsoleAppenderHub : Hub
    {
       public void OnMessageLogged(LogEntryForConsole e)
       {
           e.Message = e.Message + Caller.id;
            Clients.onLoggedEvent(e);
        }
    }
}
