using SignalR.Hubs;

namespace log4net.signalr
{
    public class Chat : Hub
    {
        public void Send(string message)
        {
            Clients.addMessage(message);
            LogManager.GetLogger("TheLogger").Info("from logger");
        }

//        public void CreatePerson(string name, int age)
//        {
//            var person = new Person {Age = age, Name = name};
//            Clients.addPerson(person);
//        }

        public void CreatePerson(Person person)
        {
            Clients.addPerson(person);
        }
    }
}