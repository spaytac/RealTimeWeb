using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SignalRTest.Hubs
{
    public class WHOTSHub : Hub
    {
        public IHubContext WHOTSHubContext { get; private set; }

        public WHOTSHub()
        {
            this.WHOTSHubContext = GlobalHost.ConnectionManager.GetHubContext<WHOTSHub>();
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public void SendEventMessage(string message)
        {
            try
            {
                this.WHOTSHubContext.Clients.All.EventAdded(message);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}