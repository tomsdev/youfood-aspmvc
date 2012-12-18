using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tmf.web.Hubs
{
    public static class TmfHubActions
    {
        // SignalR
        // Push in realtime the new Order to connected clients
        public static void AddOrder(Guid idOrder, string group)
        {
            var context = SignalR.GlobalHost.ConnectionManager.GetHubContext<TmfHub>();
            context.Clients[group].orderAdded(idOrder);
        }
    }
}