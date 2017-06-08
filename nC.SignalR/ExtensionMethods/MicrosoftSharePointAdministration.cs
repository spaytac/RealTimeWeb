using nC.SP.SignalR.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.SharePoint.Administration
{
    public static class MicrosoftSharePointAdministration
    {
        private static nCWebConfigUtility WebConfigUtility {
            get
            {
                return nCWebConfigUtility.Instance;
            }
        }

        internal static void WebConfigCreateNode(this SPWebApplication webapp, string name, string parentPath, string value)
        {
            WebConfigUtility.CreateNode(webapp, name, parentPath, value);
        }

        internal static void WebConfigCreateAttribute(this SPWebApplication webapp, string name, string parentPath, string value)
        {
            WebConfigUtility.CreateAttribute(webapp, name, parentPath, value);
        }

        internal static void WebConfigCreateSection(this SPWebApplication webapp, string name, string parentPath)
        {
            WebConfigUtility.CreateSection(webapp, name, parentPath);
        }

        internal static void WebConfigUpdateAttribute(this SPWebApplication webapp, string name, string parentPath, string value)
        {
            WebConfigUtility.UpdateAttribute(webapp, name, parentPath, value);
        }

        internal static void WebConfigRemoveInternal(this SPWebApplication webapp)
        {
            WebConfigUtility.RemoveInternal(webapp);
        }

        internal static void WebConfigUpdate(this SPWebApplication webApp)
        {
            WebConfigUtility.UpdateWebConfig(webApp);
        }

        public static void WebConfigCreateNode(this SPWebApplication webapp, nCWebConfigUtility instance, string name, string parentPath, string value)
        {
            instance.CreateNode(webapp, name, parentPath, value);
        }

        public static void WebConfigCreateAttribute(this SPWebApplication webapp, nCWebConfigUtility instance, string name, string parentPath, string value)
        {
            instance.CreateAttribute(webapp, name, parentPath, value);
        }

        public static void WebConfigCreateSection(this SPWebApplication webapp, nCWebConfigUtility instance, string name, string parentPath)
        {
            instance.CreateSection(webapp, name, parentPath);
        }

        public static void WebConfigUpdateAttribute(this SPWebApplication webapp, nCWebConfigUtility instance, string name, string parentPath, string value)
        {
            instance.UpdateAttribute(webapp, name, parentPath, value);
        }

        public static void WebConfigRemoveInternal(this SPWebApplication webapp, nCWebConfigUtility instance)
        {
            instance.RemoveInternal(webapp);
        }

        public static void WebConfigAddBindingRedirect(this SPWebApplication webApp, nCWebConfigUtility instance, string libraryName, string libraryPublicToken, string oldVersion, string newVersion)
        {
            instance.RemoveInternal(webApp);

            String path = "configuration/runtime/*[namespace-uri()='urn:schemas-microsoft-com:asm.v1' and local-name()='assemblyBinding']";
            String name = String.Format("*[namespace-uri()='urn:schemas-microsoft-com:asm.v1' and local-name()='dependentAssembly']/*[namespace-uri()='urn:schemas-microsoft-com:asm.v1' and local-name()='assemblyIdentity'][@name='{0}']/parent::*", libraryName);
            String webConfigValue = String.Format(@"
                <dependentAssembly>
                    <!-- Added automatically at {4} -->
                    <assemblyIdentity name='{0}' publicKeyToken='{1}' culture='neutral' />
                    <bindingRedirect oldVersion='{2}' newVersion='{3}' />
                </dependentAssembly>", libraryName, libraryPublicToken, oldVersion, newVersion, DateTime.Now);

            instance.CreateNode(webApp, name, path, webConfigValue);
        }

        public static void WebConfigUpdate(this SPWebApplication webApp, nCWebConfigUtility instance)
        {
            instance.Update(webApp);
        }
    }
}
