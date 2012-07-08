using System.Collections.Generic;

namespace log4net.signalr
{

    public class Clients
    {
        static Clients _instance;
        List<string> _clients = new List<string>();

        public static Clients Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Clients();
                }
                return _instance;
            }
        }

        public void Add(string id)
        {
            _clients.Add(id);
        }
    }
}