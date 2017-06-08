using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(SignalRTest.Configuration.StartUp))]
namespace SignalRTest.Configuration
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HubConfiguration();
            config.EnableJSONP = true;
            app.UseCors(CorsOptions.AllowAll);
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR(config);
        }
    }
}