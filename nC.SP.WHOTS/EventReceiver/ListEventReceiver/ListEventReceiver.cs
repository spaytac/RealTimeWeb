using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using nC.SP.WHOTS.Utilities;

namespace nC.SP.WHOTS.EventReceiver.ListEventReceiver
{
    /// <summary>
    /// List Events
    /// </summary>
    public class ListEventReceiver : SPListEventReceiver
    {
        /// <summary>
        /// A field was added.
        /// </summary>
        public override void FieldAdded(SPListEventProperties properties)
        {
            base.FieldAdded(properties);
            
            //var listItem = properties.ListItem;
            //SignalRClient.SendMessage("ItemUpdated", string.Format("Item with id '{0}' updated in '{1}'", listItem.ID, listItem.ParentList.RootFolder.Url));
            SignalRClient.SendMessage(string.Format("Field '{0}' added in '{1}'", properties.FieldName, properties.List.RootFolder.Url));
        }

        /// <summary>
        /// A field was removed.
        /// </summary>
        public override void FieldDeleted(SPListEventProperties properties)
        {
            base.FieldDeleted(properties);
            SignalRClient.SendMessage(string.Format("Field '{0}' deleted in '{1}'", properties.FieldName, properties.List.RootFolder.Url));
        }

        /// <summary>
        /// A field was updated.
        /// </summary>
        public override void FieldUpdated(SPListEventProperties properties)
        {
            base.FieldUpdated(properties);
            SignalRClient.SendMessage(string.Format("Field '{0}' updated in '{1}'", properties.FieldName, properties.List.RootFolder.Url));
        }

        /// <summary>
        /// A list was added.
        /// </summary>
        public override void ListAdded(SPListEventProperties properties)
        {
            base.ListAdded(properties);
            SignalRClient.SendMessage(string.Format("List '{0}' added in '{1}'", properties.List.Title, properties.Web.Url));
        }

        /// <summary>
        /// A list was deleted.
        /// </summary>
        public override void ListDeleted(SPListEventProperties properties)
        {
            base.ListDeleted(properties);
            SignalRClient.SendMessage(string.Format("List '{0}' deleted in '{1}'", !string.IsNullOrEmpty(properties.ListTitle) ? properties.ListTitle : properties.ListId.ToString("B"), properties.Web.Url));
        }


    }
}