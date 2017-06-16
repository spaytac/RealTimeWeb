using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace nC.SP.SimpleChat.WebParts.SimpleChat
{
    public partial class SimpleChatUserControl : UserControl
    {
        #region Fields
        private SimpleChat parentWebPart;
        private string webPartId;
        #endregion

        #region Properties
        private SimpleChat ParentWebPart
        {
            get
            {
                if (this.parentWebPart == null)
                {
                    this.parentWebPart = this.Parent as SimpleChat;
                }
                return this.parentWebPart;
            }
        }

        public string WebPartId
        {
            get
            {
                if (string.IsNullOrEmpty(this.webPartId))
                {
                    this.webPartId = this.ParentWebPart.ID;
                }
                return this.webPartId;
            }
        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
