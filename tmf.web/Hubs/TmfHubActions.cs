using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tmf.web.Hubs
{
    public static class TmfHubActions
    {
        // SignalR
        // Push en temps réel du nouvel Order aux clients connecté
        public static void CreateItem()
        {
            var hub = SignalR.GlobalHost.ConnectionManager.GetHubContext<TmfHub>();

            var item = new Item();
            item.Title = "order";
            item.ChangedBy = new User()
            {
                Name = "tom"
            };

            hub.Clients.itemAdded(item);
        }
    }
}