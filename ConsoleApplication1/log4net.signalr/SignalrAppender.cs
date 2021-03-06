﻿using System;
using log4net.Core;

namespace log4net.SignalR
{
    public class SignalrAppender : Appender.AppenderSkeleton
    {
        private FixFlags _fixFlags = FixFlags.All;

        public Action<LogEntryForConsole> MessageLogged;

        public static SignalrAppender Instance { get; private set; }

        public SignalrAppender()
        {
            Instance = this;
        }

        virtual public FixFlags Fix
        {
            get { return _fixFlags; }
            set { _fixFlags = value; }
        }

        override protected void Append(LoggingEvent loggingEvent)
        {
            // LoggingEvent may be used beyond the lifetime of the Append()
            // so we must fix any volatile data in the event
            loggingEvent.Fix = Fix;

            var formattedEvent = RenderLoggingEvent(loggingEvent);


            var handler = MessageLogged;
            if (handler != null)
            {
                var logEntry = new LogEntryForConsole
                {
                    Level = loggingEvent.Level.Name,
                    Message = loggingEvent.ExceptionObject != null
                                  ? loggingEvent.ExceptionObject.Message
                                  : loggingEvent.RenderedMessage,
                    TimeStamp = loggingEvent.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };
                handler(logEntry);
            }
        }
    }
}