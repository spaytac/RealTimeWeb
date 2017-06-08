using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nC.SP.WHOTS.Utilities
{
    public static class SignalRClient
    {
        public static void SendMessage(string message)
        {
            var connection = new HubConnection("http://a-sp13s-04:1427/signalr");
            connection.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            var messageHub = connection.CreateHubProxy("WHOTSHub");
            connection.Start().Wait();
            messageHub.Invoke("SendEventMessage", message);
        }
    }
}
