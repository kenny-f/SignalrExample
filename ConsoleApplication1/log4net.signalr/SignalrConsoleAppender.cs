using System;
using log4net.Core;
using SignalR.Client.Hubs;

namespace log4net.SignalR
{
    public class SignalrConsoleAppender : Appender.AppenderSkeleton
    {
        private FixFlags _fixFlags = FixFlags.All;

        public Action<LogEntry> MessageLogged;

        public static SignalrConsoleAppender Instance { get; private set; }

        public SignalrConsoleAppender()
        {
            Instance = this;
        }

        virtual public FixFlags Fix
        {
            get { return _fixFlags; }
            set { _fixFlags = value; }
        }

        public IHubProxy Hub;

        override protected void Append(LoggingEvent loggingEvent)
        {
            // LoggingEvent may be used beyond the lifetime of the Append()
            // so we must fix any volatile data in the event
            loggingEvent.Fix = Fix;

            var formattedEvent = RenderLoggingEvent(loggingEvent);
            var logEntry = new LogEntryForConsole
                               {
                                   Level = loggingEvent.Level.Name,
                                   Message = loggingEvent.ExceptionObject != null
                                                 ? loggingEvent.ExceptionObject.Message
                                                 : loggingEvent.RenderedMessage,
                                   TimeStamp = loggingEvent.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")
                               };

            if (Hub != null)
            {
                Hub.Invoke("SayHi", loggingEvent.RenderedMessage)
                    .ContinueWith(t=>
                                      {
                                          if (t.IsFaulted)
                                          {
                                              Console.WriteLine("error calling SayHi: "+t.Exception);
                                          }
                                      }
                    ).Wait();
                

                Hub.Invoke("OnMessageLogged", logEntry)
                    .ContinueWith(t=>
                                      {
                                          if (t.IsFaulted)
                                          {
                                              Console.WriteLine("error calling OnMessageLogged: "+t.Exception);
                                          }
                                      }
                    ).Wait();
            }
        }
    }
}