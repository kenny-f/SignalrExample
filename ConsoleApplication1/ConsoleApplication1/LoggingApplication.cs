﻿using System;
using System.Threading;
using log4net;
using log4net.SignalR;
using SignalR.Client.Hubs;

namespace ConsoleApplication1
{
    public class LoggingApplication
    {
        public void Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            string url = "http://localhost:63141/";

            var connection = new HubConnection(url);

            IHubProxy loggerHub = connection.CreateProxy(typeof(SignalrConsoleAppenderHub).GetHubName());

            SignalrConsoleAppender.Instance.Hub = loggerHub;

            startConnection(connection);

            Thread.Sleep(10000);

            LogManager.GetLogger("TheLogger").Info("from console app logger");
        }

        static void startConnection(HubConnection connection)
        {
            connection.Start().ContinueWith(
                t =>
                    {
                        if (t.IsFaulted)
                        {
                            Console.WriteLine("Error opening connection");
                        }
                        else
                        {
                            Console.WriteLine("Connected");
                        }
                    }).Wait();
        }
    }
}