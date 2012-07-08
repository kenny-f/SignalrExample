using System;
using System.Threading;
using log4net;
using log4net.SignalR;
using SignalR.Client.Hubs;

namespace ConsoleApplication1
{
    public class FakeLogGenerator
    {
        public void Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            string url = "http://localhost:63141/";

            var connection = new HubConnection(url);

            IHubProxy loggerHub = connection.CreateProxy(typeof(SignalrConsoleAppenderHub).GetHubName());

            SignalrConsoleAppender.Instance.Hub = loggerHub;

            startConnection(connection);

            int counter = 0;
            while (true)
            {
                Thread.Sleep(10000);
                Console.WriteLine(counter);
                LogManager.GetLogger("TheLogger").InfoFormat("Count: {0}", counter);
                counter++;
            }
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