using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRTest.Model
{
    [Serializable]
    public class SimpleChatUser
    {
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public string Color { get; set; }
    }
}