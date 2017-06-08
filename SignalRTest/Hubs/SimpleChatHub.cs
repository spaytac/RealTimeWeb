using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRTest.Hubs
{
    public class SimpleChatHub : Hub
    {
        public void Send(string name, string message, string time, string color)
        {
            Clients.All.broadcastMessage(name, message, time, color);
        }
    }
}