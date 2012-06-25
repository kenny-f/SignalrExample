using System;
using System.Threading.Tasks;
using SignalR.Client.Hubs;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main()
        {
            var connection = new HubConnection("http://localhost:8991/");

            var myHub = connection.CreateProxy("Server.MyHub");

            // Static type
//            myHub.On<Person>("foo", person =>
//            {
//                Console.WriteLine("static => Name: {0}, Age:{1}", person.Name, person.Age);
//            });

            // No params, just notify me
//            myHub.On("notify", () => Console.WriteLine("Notified!"));

            // Dynamic type (no need to specify type arguments if you like dynamic :))
//            myHub.On("foo", person =>
//            {
//                Console.WriteLine("dynamic => Name: {0}, Age: {1}", person.Name, person.Age);
//            });

            // Simple types
//            myHub.On<string>("bar", s =>
//            {
//                Console.WriteLine("Bar({0})", s);
//            });

//            myHub.On<int>("baz", i =>
//            {
//                Console.WriteLine("Baz({0})", i);
//            });

//            myHub.On<int, int, dynamic>("multipleParams", (a, b, c) =>
//            {
//                Console.WriteLine("multipleParams({0}, {1}, {2})", a, b, c);
//            });

            // IObservable<T>
//            myHub.AsObservable("foo")
//                 .Subscribe(args =>
//                 {
//                     Console.WriteLine(args[0]);
//                 });

            connection.Start().Wait();

            // Invoke the foo method
            // await myHub.Invoke("Foo");
//            myHub.Invoke("Foo").Wait();

            // Invoke hub method with a result
            // string result = await myHub.Invoke("GetString");
            myHub.Invoke<string>("GetString")
                 .ContinueWith(t =>
                 {
                     Console.WriteLine("GetString => " + t.Result);
                 });

            // Method that takes a complex parameter
            var p = new Person { Name = "John Doe", Age = 24 };
            myHub.Invoke<int>("GetTask", p)
                 .ContinueWith(t =>
                 {
                     Console.WriteLine("Age: {0}", t.Result);
                 });

            myHub.Invoke("ThrowError")
                 .ContinueWith(t =>
                 {
                     Console.WriteLine("Faulted: {0} -> {1}", t.IsFaulted, t.Exception.GetBaseException().Message);
                 });

            // Client side state
            myHub["id"] = "some value set from client";
            myHub.Invoke<string>("InitilizeState")
                 .ContinueWith(t =>
                 {
                     Console.WriteLine("Name set from server => {0}", myHub["name"]);
                     Console.WriteLine("Result: {0}", t.Result);
                 });

            Console.Read();
        }

//        static void Main(string[] args)
//        {
//            var connection = new HubConnection("http://localhost/8991/");
//            IHubProxy myHub = connection.CreateProxy("MyHub");
//            connection.Start().ContinueWith(
//                x=>
//                    {
//                        if (x.IsFaulted)
//                        {
//                            Console.WriteLine("Failed to start: {0}", x.Exception.GetBaseException());
//                        }
//                        else
//                        {
//                            Console.WriteLine("Success! Connected with cliet connection id {0}", connection.ConnectionId);
//                        }
//                    }).Wait();
//            
//
//            Console.Read();
//
//            myHub.Invoke<string>("Send", "from client").ContinueWith(
//                x=>
//                    {
//                        Console.WriteLine("Came back from server trip " + x.Result);
//                    });
//
//            Console.Read();
//
//        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
