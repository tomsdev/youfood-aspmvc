using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;

namespace tmf.web.Hubs
{
    public class Chat : Hub
    {
        private static ConcurrentDictionary<string, string> _chat = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public void Send(string message)
        {
            _chat.TryAdd(message, "test");

            // Call the addMessage method on all clients
            Clients.addMessage(message);
        }
    }
}