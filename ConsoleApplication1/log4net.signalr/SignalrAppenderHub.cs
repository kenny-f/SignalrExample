using SignalR.Hubs;

namespace log4net.SignalR
{
    public class SignalrAppenderHub : Hub
    {
        public void Listen()
        {
            SignalrAppender.Instance.MessageLogged = OnMessageLogged;
        }

        public void OnMessageLogged(LogEntryForConsole e)
        {
            e.Message = e.Message + Caller.id;

           Clients.onLoggedEvent(e);
        }

        public void LogMessage()
        {
            LogManager.GetLogger(typeof(SignalrAppenderHub)).Info("just a log message");
        }
    }
}