using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRTest.Model
{
    [Serializable]
    public class SimpleChatMessage
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Message { get; set; }
        public string DateTime { get; set; }
    }
}