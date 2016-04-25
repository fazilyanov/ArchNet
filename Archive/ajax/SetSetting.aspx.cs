using System;
namespace WebArchiveR6.ajax {
    public partial class SetSetting : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            string _key = Request.QueryString["key"];
            string _value = Request.QueryString["value"];
            if (_key != null && _value != null)
                faFunc.SetUserSetting(_key, _value);
        }
    }
}