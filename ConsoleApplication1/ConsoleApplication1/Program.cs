using System;
using System.Threading;
using log4net;
using log4net.signalr;
using log4net.SignalR;
using SignalR.Client.Hubs;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            string url = "http://localhost:63141/";

            var connection = new HubConnection(url);

            IHubProxy hub = connection.CreateProxy("chat");

            IHubProxy loggerHub = connection.CreateProxy("signalrAppenderForConsoleHub");

            SignalrConsoleAppender.Instance.Hub = loggerHub;

            startConnection(connection);

            invokeSend(hub);

            Thread.Sleep(10000);

            LogManager.GetLogger("TheLogger").Info("from console app logger");

            // Keep going until somebody hits 'x'
            while (true)
            {
                ConsoleKeyInfo ki = Console.ReadKey(true);
                if (ki.Key == ConsoleKey.X)
                {
                    break;
                }
            }
        }

        static void invokeSend(IHubProxy hub)
        {
            hub.Invoke("Send", "from Console invoking the send method on chat hub").ContinueWith(
                t =>
                    {
                        if (t.IsFaulted)
                        {
                            Console.WriteLine("error calling send");
                        }
                        else
                        {
                            Console.WriteLine("send complete");
                        }
                    }
                );
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
