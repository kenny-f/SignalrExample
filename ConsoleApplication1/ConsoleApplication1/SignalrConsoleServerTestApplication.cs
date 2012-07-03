using System;
using ConsoleServer;
using SignalR.Client.Hubs;

namespace ConsoleApplication1
{
    public class SignalrConsoleServerTestApplication
    {
        public void Start()
        {
            string url = "http://localhost:8999/";

            var connection = new HubConnection(url);

            IHubProxy serverHub = connection.CreateProxy(typeof(ServerHub).GetHubName());
            
            serverHub.On("foo", () => Console.WriteLine("notified!"));
            
            serverHub.On("foo", s => Console.WriteLine(s));
            
            startConnection(connection);

            serverHub.Invoke("SendMessage", "hello from Console Client").Wait();
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