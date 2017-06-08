using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nC.SP.SignalR.Utilities
{
    internal static class nCSharedSignalRUtilities
    {
        internal static void CreateNode(this nCWebConfigUtility instance, SPWebApplication webapp, string name, string parentPath, string value)
        {
            instance.CreateNode(webapp, name, parentPath, value);
        }

        internal static void CreateAttribute(this nCWebConfigUtility instance, SPWebApplication webapp, string name, string parentPath, string value)
        {
            instance.CreateAttribute(webapp, name, parentPath, value);
        }

        internal static void CreateSection(this nCWebConfigUtility instance, SPWebApplication webapp, string name, string parentPath)
        {
            instance.CreateSection(webapp, name, parentPath);
        }

        internal static void UpdateAttribute(this nCWebConfigUtility instance, SPWebApplication webapp, string name, string parentPath, string value)
        {
            instance.UpdateAttribute(webapp, name, parentPath, value);
        }

        internal static void RemoveInternal(this nCWebConfigUtility instance, SPWebApplication webapp)
        {
            instance.RemoveInternal(webapp);
        }

        internal static void Update(this nCWebConfigUtility instance, SPWebApplication webapp)
        {
            instance.UpdateWebConfig(webapp);
        }
    }
}
