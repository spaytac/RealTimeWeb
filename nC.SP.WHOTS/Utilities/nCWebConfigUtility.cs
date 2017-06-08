using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nC.SP.WHOTS.Utilities
{
    public class nCWebConfigUtility
    {
        private static Dictionary<string, nCWebConfigUtility> _instances = new Dictionary<string, nCWebConfigUtility>();

        //static holder for singleton
        private static readonly Lazy<nCWebConfigUtility> _instance
            = new Lazy<nCWebConfigUtility>(() => new nCWebConfigUtility());

        /// <summary>
        /// Holds the owner
        /// </summary>
        private string owner;

        public string Owner
        {
            get
            {
                return this.owner;
            }
        }

        /// <summary>
        /// Gets or sets the sequence
        /// </summary>
        private uint Sequence
        {
            get;
            set;
        }

        private List<SPWebConfigModification> AddedModifications { get; set; }
        private List<SPWebConfigModification> RemovedModifications { get; set; }

        private SPWebApplication WebApp { get; set; }

        private nCWebConfigUtility()
        {
            if (string.IsNullOrEmpty(owner))
            {
                owner = GetType().FullName;
            }
            if (this.AddedModifications == null)
            {
                this.AddedModifications = new List<SPWebConfigModification>();
            }

            if (this.RemovedModifications == null)
            {
                this.RemovedModifications = new List<SPWebConfigModification>();
            }
        }

        internal static nCWebConfigUtility Instance
        {
            get
            {
                if (!_instances.ContainsKey("local"))
                {
                    _instances["local"] = _instance.Value;
                }

                return _instances["local"];
            }
        }

        public static nCWebConfigUtility GetInstance(string owner)
        {
            if (!_instances.ContainsKey(owner))
            {
                _instances[owner] = new nCWebConfigUtility();
                _instances[owner].owner = owner;
            }

            return _instances[owner];
        }

        /// <summary>
        /// Adds a new xml attribute to the web config file.
        /// </summary>
        /// <param name="name">
        /// Name of the attribute.
        /// </param>
        /// parentPath">
        /// The parent of this attribute.
        /// </param>
        /// <param name="value">
        /// The value of the attribute.
        /// </param>
        internal void CreateAttribute(SPWebApplication webApplication, string name, string parentPath, string value)
        {
            var webConfigModification = new SPWebConfigModification(name, parentPath)
            {
                Owner = owner,
                Sequence = Sequence,
                Type = SPWebConfigModification.SPWebConfigModificationType.EnsureAttribute,
                Value = value
            };
            AddConfigModification(webApplication, webConfigModification);
        }

        /// <summary>
        /// Adds a new xml node to the web config file.
        /// </summary>
        /// <param name="name">
        /// Name of the node.
        /// </param>
        /// parentPath">
        /// The parent of this node
        /// </param>
        /// <param name="value">
        /// The value of the node.
        /// </param>
        internal void CreateNode(SPWebApplication webApplication, string name, string parentPath, string value)
        {
            var webConfigModification = new SPWebConfigModification(name, parentPath)
            {
                Owner = owner,
                Sequence = Sequence,
                Type = SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode,
                Value = value
            };
            AddConfigModification(webApplication, webConfigModification);
        }

        // <summary>
        /// updates an attribute within the web config file.
        /// </summary>
        /// <param name="name">
        /// Name of the attribute.
        /// </param>
        /// parentPath">
        /// The parent of this attribute.
        /// </param>
        /// <param name="value">
        /// The value of the attribute.
        /// </param>
        internal void UpdateAttribute(SPWebApplication webApplication, string name, string parentPath, string value)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(parentPath) && !string.IsNullOrEmpty(value))
            {
                CreateAttribute(webApplication, name, parentPath, value);
            }
        }

        /// <summary>
        /// Only use this if you need to add a section that does not have to be removed and may contain child nodes from other solutions.
        /// </summary>
        /// <param name="name">
        /// The name of the section.
        /// </param>
        /// <param name="parentPath">
        /// The parent path in the web.config file.
        /// </param>
        internal void CreateSection(SPWebApplication webApplication, string name, string parentPath)
        {
            var webConfigModification = new SPWebConfigModification(name, parentPath)
            {
                Owner = owner,
                Sequence = Sequence,
                Type = SPWebConfigModification.SPWebConfigModificationType.EnsureSection
            };
            AddConfigModification(webApplication, webConfigModification);
        }

        /// <summary
        /// Adds the config modification.
        /// </summary>
        /// <param name="modification">
        /// The modification to apply.
        /// </param>
        private void AddConfigModification(SPWebApplication webApplication, SPWebConfigModification modification)
        {
            //webApplication.WebConfigModifications.Add(modification);
            if (this.AddedModifications.FirstOrDefault(
                x => x.Name.Equals(modification.Name, StringComparison.InvariantCultureIgnoreCase) && 
                    x.Owner.Equals(modification.Owner, StringComparison.InvariantCultureIgnoreCase) && 
                    x.Path.Equals(modification.Path, StringComparison.InvariantCultureIgnoreCase)) == null)
            {
                this.AddedModifications.Add(modification);
            }
            Sequence++;
        }

        /// <summary>
        /// Removes the modifications of the webconfig of the current webapplication.
        /// </summary>
        /// <param name="webApplication">
        /// The web application.
        /// </param>
        internal void RemoveInternal(SPWebApplication webApplication)
        {
            if (webApplication == null)
            {
                throw new ArgumentNullException("webApplication");
            }

            var toRemove = webApplication.WebConfigModifications.Where(modification => modification != null)
                .Where(modification => string.Compare(modification.Owner, owner, true, CultureInfo.CurrentCulture) == 0).ToList();

            foreach (var modification in toRemove)
            {
                if (this.RemovedModifications.FirstOrDefault(
                x => x.Name.Equals(modification.Name, StringComparison.InvariantCultureIgnoreCase) &&
                    x.Owner.Equals(modification.Owner, StringComparison.InvariantCultureIgnoreCase) &&
                    x.Path.Equals(modification.Path, StringComparison.InvariantCultureIgnoreCase)) == null)
                {
                    this.RemovedModifications.Add(modification);
                }
            }
        }

        /// <summary>
        /// Updates the webconfig of the current webapplication with the modifications.
        /// </summary>
        /// <param name="webApplication">
        /// The webapplication that needs to be configured.
        /// </param>
        internal void UpdateWebConfig(SPWebApplication webApplication)
        {
            try
            {
                var updateWebConfig = false;
                if (this.AddedModifications.Count > 0)
                {
                    updateWebConfig = true;
                    foreach (var modification in this.AddedModifications)
                    {
                        webApplication.WebConfigModifications.Add(modification);
                    }

                    this.AddedModifications = new List<SPWebConfigModification>();
                }

                if (this.RemovedModifications.Count > 0)
                {
                    updateWebConfig = true;
                    foreach (var modification in this.RemovedModifications)
                    {
                        webApplication.WebConfigModifications.Remove(modification);
                    }

                    this.RemovedModifications = new List<SPWebConfigModification>();
                }

                if (updateWebConfig)
                {
                    webApplication.Update();
                    webApplication.WebService.ApplyWebConfigModifications();
                }
            }
            catch (Exception ex)
            {
                // Add your exception handling and logging here
            }
        }
        
    }
}
