using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using nC.SP.WHOTS.Utilities;

namespace nC.SP.WHOTS.EventReceiver.SiteEventReceiver
{
    /// <summary>
    /// Web Events
    /// </summary>
    public class SiteEventReceiver : SPWebEventReceiver
    {
        /// <summary>
        /// A site was deleted.
        /// </summary>
        public override void WebDeleted(SPWebEventProperties properties)
        {
            base.WebDeleted(properties);
            SignalRClient.SendMessage(string.Format("Web '{0}' deleted", properties.WebId));
        }

        /// <summary>
        /// A site was moved.
        /// </summary>
        public override void WebMoved(SPWebEventProperties properties)
        {
            base.WebMoved(properties);
            SignalRClient.SendMessage(string.Format("Web '{0}' moved", properties.WebId));
        }

        /// <summary>
        /// A site was provisioned.
        /// </summary>
        public override void WebProvisioned(SPWebEventProperties properties)
        {
            base.WebProvisioned(properties);
            SignalRClient.SendMessage(string.Format("Web '{0}' provisioned", properties.Web.Url));
        }


    }
}