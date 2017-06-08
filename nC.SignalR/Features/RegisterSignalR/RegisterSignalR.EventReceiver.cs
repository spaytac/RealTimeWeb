using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using nC.SP.SignalR.Utilities;

namespace nC.SP.SignalR.Features.RegisterSignalR
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("e9c37e8c-0587-40f8-a31f-a0e9ee9c4230")]
    public class RegisterSignalREventReceiver : SPFeatureReceiver
    {
        //IMPORTANT
        /*
         You have to manually edit the following lines within the WebConfig
         <handlers>
            (…) 
            <!-- remove name="ExtensionlessUrl-ISAPI-4.0_64bit" / -->
            <!-- remove name="ExtensionlessUrl-ISAPI-4.0_32bit" / -->
            (…) 
        </handlers>
         https://sharepointpros.wordpress.com/2015/01/26/using-signalr-2-2-in-a-sharepoint-2013-farm-solution-3/
         */
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPDiagnosticsService.Local.WriteTrace(0,
                                                  new SPDiagnosticsCategory(
                                                      "nC.SPSignalR",
                                                      TraceSeverity.Medium,
                                                      EventSeverity.Information),
                                                  TraceSeverity.Medium,
                                                  string.Format(
                                                      "Feature: Registering HTTPModule for nC.SPSignalR"),
                                                  null);
            var webapp = properties.Feature.Parent as SPWebApplication;
            var webConfModInstance = nCWebConfigUtility.GetInstance("SignalRRegisterEventReceiver");

            webapp.WebConfigCreateNode(webConfModInstance, "add[@key=\"owin:AutomaticAppStartup\"]", "configuration/appSettings", "<add key=\"owin:AutomaticAppStartup\" value=\"true\" />");

            webapp.WebConfigCreateAttribute(webConfModInstance, "legacyCasModel", "configuration/system.web/trust", "false");

            webapp.WebConfigCreateNode(webConfModInstance,
                "add[@name=\"Owin\"][@verb=\"*\"][@path=\"/signalr\"][@type=\"Microsoft.Owin.Host.SystemWeb.OwinHttpHandler, Microsoft.Owin.Host.SystemWeb, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\"]",
                "configuration/system.webServer/handlers",
                "<add name=\"Owin\" verb=\"*\" path=\"/signalr\" type=\"Microsoft.Owin.Host.SystemWeb.OwinHttpHandler, Microsoft.Owin.Host.SystemWeb, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" />");

            webapp.WebConfigAddBindingRedirect(webConfModInstance, "Microsoft.Owin", "31bf3856ad364e35", "0.0.0.0-3.0.1.0", "3.0.1.0");
            webapp.WebConfigAddBindingRedirect(webConfModInstance, "Microsoft.Owin.Security", "31bf3856ad364e35", "0.0.0.0-3.0.1.0", "3.0.1.0");
            webapp.WebConfigAddBindingRedirect(webConfModInstance, "Newtonsoft.Json", "30ad4fe6b2a6aeed", "0.0.0.0-9.0.0.0", "9.0.0.0");

            webapp.WebConfigUpdate(webConfModInstance);
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPDiagnosticsService.Local.WriteTrace(0,
                                                  new SPDiagnosticsCategory(
                                                      "nC.SPSignalR",
                                                      TraceSeverity.Medium,
                                                      EventSeverity.Information),
                                                  TraceSeverity.Medium,
                                                  string.Format("Feature: Removing HTTPModule for nC.SPSignalR"),
                                                  null);

            var webapp = properties.Feature.Parent as SPWebApplication;

            var webConfModInstance = nCWebConfigUtility.GetInstance("SignalRRegisterEventReceiver");
            webapp.WebConfigRemoveInternal(webConfModInstance);
            webapp.WebConfigUpdate(webConfModInstance);
        }
    }
}
