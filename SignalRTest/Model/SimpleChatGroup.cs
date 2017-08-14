using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRTest.Model
{
    [Serializable]
    public class SimpleChatGroup
    {
        public string Id { get; set; }
        public Dictionary<string, SimpleChatUser> Users { get; set; }
        public List<SimpleChatMessage> Messages { get; set; }

        public SimpleChatGroup()
        {
            if (this.Users == null)
            {
                this.Users = new Dictionary<string, SimpleChatUser>();
            }

            if (this.Messages == null)
            {
                this.Messages = new List<SimpleChatMessage>();
            }
        }
    }
}