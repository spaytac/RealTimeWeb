using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using nC.SP.WHOTS.Utilities;

namespace nC.SP.WHOTS.EventReceiver.ItemEventReceiver
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class ItemEventReceiver : SPItemEventReceiver
    {
        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            var listItem = properties.ListItem;
            SignalRClient.SendMessage(string.Format("Item with id '{0}' added in '{1}'", listItem.ID, listItem.ParentList.RootFolder.Url));
        }

        /// <summary>
        /// An item was updated.
        /// </summary>
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            base.ItemUpdated(properties);
            var listItem = properties.ListItem;
            SignalRClient.SendMessage(string.Format("Item with id '{0}' updated in '{1}'", listItem.ID, listItem.ParentList.RootFolder.Url));
        }

        /// <summary>
        /// An item was deleted.
        /// </summary>
        public override void ItemDeleted(SPItemEventProperties properties)
        {
            base.ItemDeleted(properties);
            SignalRClient.SendMessage(string.Format("Item with id '{0}' deleted in '{1}'", properties.ListItemId, properties.List.RootFolder.Url));
        }

        /// <summary>
        /// An item was checked in.
        /// </summary>
        public override void ItemCheckedIn(SPItemEventProperties properties)
        {
            base.ItemCheckedIn(properties);
            var listItem = properties.ListItem;
            SignalRClient.SendMessage(string.Format("Item with id '{0}' checked in in '{1}'", listItem.ID, listItem.ParentList.RootFolder.Url));
        }

        /// <summary>
        /// An item was checked out.
        /// </summary>
        public override void ItemCheckedOut(SPItemEventProperties properties)
        {
            base.ItemCheckedOut(properties);
            var listItem = properties.ListItem;
            SignalRClient.SendMessage(string.Format("Item with id '{0}' checked out in '{1}'", listItem.ID, listItem.ParentList.RootFolder.Url));
        }

        /// <summary>
        /// An item was unchecked out.
        /// </summary>
        public override void ItemUncheckedOut(SPItemEventProperties properties)
        {
            base.ItemUncheckedOut(properties);
            var listItem = properties.ListItem;
            SignalRClient.SendMessage(string.Format("Item with id '{0}' unchecked out in '{1}'", listItem.ID, listItem.ParentList.RootFolder.Url));
        }

        /// <summary>
        /// An attachment was added to the item.
        /// </summary>
        public override void ItemAttachmentAdded(SPItemEventProperties properties)
        {
            base.ItemAttachmentAdded(properties);
            var listItem = properties.ListItem;
            SignalRClient.SendMessage(string.Format("New attachement: item with id '{0}' in list '{1}'", listItem.ID, listItem.ParentList.RootFolder.Url));
        }

        /// <summary>
        /// An attachment was removed from the item.
        /// </summary>
        public override void ItemAttachmentDeleted(SPItemEventProperties properties)
        {
            base.ItemAttachmentDeleted(properties);
            var listItem = properties.ListItem;
            SignalRClient.SendMessage(string.Format("Deleted attachement: item with id '{0}' in list '{1}'", listItem.ID, listItem.ParentList.RootFolder.Url));
        }


    }
}