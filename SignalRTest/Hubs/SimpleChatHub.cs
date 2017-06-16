using Microsoft.AspNet.SignalR;
using SignalRTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;

namespace SignalRTest.Hubs
{
    public class SimpleChatHub : Hub
    {
        private Dictionary<string, SimpleChatGroup> GroupsByIds { get; set; }

        private ObjectCache ChatCache { get; set; }

        public IHubContext SimpleChatHubContext { get; private set; }

        public SimpleChatHub()
        {
            this.ChatCache = MemoryCache.Default;

            if (!this.ChatCache.Contains("SimpleChatHub"))
            {
                if (this.GroupsByIds == null)
                {
                    this.GroupsByIds = new Dictionary<string, SimpleChatGroup>();
                }

                this.ChatCache.Add("SimpleChatHub", this.GroupsByIds, null);
            } 
            else
            {
                this.GroupsByIds = this.ChatCache.Get("SimpleChatHub") as Dictionary<string, SimpleChatGroup>;
            }

            this.SimpleChatHubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleChatHub>();
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public async Task JoinChatRoom(string groupId, string name, string color)
        {
            var updateCache = false;
            if (!this.GroupsByIds.ContainsKey(groupId))
            {
                this.GroupsByIds.Add(groupId, new SimpleChatGroup());
                updateCache = true;
            }

            if (!this.GroupsByIds[groupId].Users.ContainsKey(name))
            {
                updateCache = true;
                this.GroupsByIds[groupId].Users[name] = new SimpleChatUser() { Name = name, Color = color, ConnectionId = Context.ConnectionId };
            }

            if (updateCache)
            {
                this.ChatCache["SimpleChatHub"] = this.GroupsByIds;
            }
            await this.SimpleChatHubContext.Groups.Add(Context.ConnectionId, groupId);
            this.SimpleChatHubContext.Clients.Client(Context.ConnectionId).AllGroupMessages(this.GroupsByIds[groupId].Messages);
        }

        public void SendMessage(string groupId, string name, string message, string time)
        {
            try
            {
                var color = this.GroupsByIds[groupId].Users[name].Color;
                var newMessage = new SimpleChatMessage() { Color = color, Message = message, Name = name, Time = time };
                this.GroupsByIds[groupId].Messages.Add(newMessage);
                this.SimpleChatHubContext.Clients.Group(groupId).MessageAdded(newMessage);
                this.ChatCache["SimpleChatHub"] = this.GroupsByIds;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}