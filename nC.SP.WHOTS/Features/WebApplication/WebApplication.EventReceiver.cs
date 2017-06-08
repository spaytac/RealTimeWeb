using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using nC.SP.WHOTS.Utilities;

namespace nC.SP.WHOTS.Features.WebApplication
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("d414d8e0-7f76-412b-a9e5-c038e0538ee9")]
    public class WebApplicationEventReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPDiagnosticsService.Local.WriteTrace(0,
                                                  new SPDiagnosticsCategory(
                                                      "nC.SP.WHOTS",
                                                      TraceSeverity.Medium,
                                                      EventSeverity.Information),
                                                  TraceSeverity.Medium,
                                                  string.Format(
                                                      "Feature: Registering HTTPModule for nC.SP.WHOTS"),
                                                  null);
            var webapp = properties.Feature.Parent as SPWebApplication;
            var webConfModInstance = nCWebConfigUtility.GetInstance("SignalRRegisterClientnCSPWHOTSEventReceiver");
            webapp.WebConfigAddBindingRedirect(webConfModInstance, "Newtonsoft.Json", "30ad4fe6b2a6aeed", "0.0.0.0-9.0.0.0", "9.0.0.0");

            webapp.WebConfigUpdate(webConfModInstance);
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPDiagnosticsService.Local.WriteTrace(0,
                                                  new SPDiagnosticsCategory(
                                                      "nC.SP.WHOTS",
                                                      TraceSeverity.Medium,
                                                      EventSeverity.Information),
                                                  TraceSeverity.Medium,
                                                  string.Format("Feature: Removing HTTPModule for nC.SP.WHOTS"),
                                                  null);

            var webapp = properties.Feature.Parent as SPWebApplication;

            var webConfModInstance = nCWebConfigUtility.GetInstance("SignalRRegisterClientnCSPWHOTSEventReceiver");
            webapp.WebConfigRemoveInternal(webConfModInstance);
            webapp.WebConfigUpdate(webConfModInstance);
        }
    }
}
