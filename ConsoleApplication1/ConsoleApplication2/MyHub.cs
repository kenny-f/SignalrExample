using System;
using System.Threading;
using System.Threading.Tasks;
using SignalR.Hubs;

namespace Server
{
    public class MyHub : Hub
    {
        public void Foo()
        {
            Caller.bar("Hello");
            Caller.baz(1);
            Caller.foo(new { Name = "David Fowler", Age = 24 });
            Caller.multipleParams(1, 2, new { Name = "John" });
            Caller.notify();
        }

        public string GetString()
        {
            return "David";
        }

        public Task<int> GetTask(Person person)
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                return person.Age;
            });
        }

        public void ThrowError()
        {
            throw new InvalidOperationException("Throwing an exception");
        }

        public string InitilizeState()
        {
            Caller.name = "Damian";

            return Caller.id;
        }

        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}