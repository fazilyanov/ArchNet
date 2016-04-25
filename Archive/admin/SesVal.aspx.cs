using System;

namespace ArchNet
{
    public partial class SesVal : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            Response.Clear();
            Response.Write(faFunc.GetSessionValues());
            Response.End();
        }
    }
}