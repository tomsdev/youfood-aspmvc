using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SignalR.Hubs;

namespace tmf.web.Hubs
{
    [HubName("tmfHub")]
    public class TmfHub : Hub
    {
        public TmfHub()
        {
            
        }

        public Task Join(string group)
        {
            return Groups.Add(Context.ConnectionId, group);
        }
    }

}