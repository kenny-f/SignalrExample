using System;
using System.Collections;
using System.Collections.Generic;

namespace MvcApplication1.Models
{
    public class ClientList
    {
        static readonly ClientList _instance = new ClientList();
        readonly List<string> _connectedClients = new List<string>();
        readonly List<string> _messages = new List<string>();

        static ClientList() { }

        private ClientList() { }

        public static ClientList Instance
        {
            get { return _instance; }
        }

        public IEnumerable<string> Messages
        {
            get { return _messages; }
        }

        public void Add(string connectionId)
        {
            _connectedClients.Add(connectionId);
        }

        public void Remove(string connectionId)
        {
            _connectedClients.Remove(connectionId);
        }

        public void AddMessage(string s)
        {
            _messages.Add(s);
        }
    }
}