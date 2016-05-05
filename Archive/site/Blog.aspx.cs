using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArchNet
{
    public partial class Blog : System.Web.UI.Page
    {
        public string _js = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // Достаем id последней прочитанной новости
            int last_msg_id = 0;
            int.TryParse(faFunc.GetUserSetting("last_msg_id"), out last_msg_id);

            // Достаем общее количество новостей
            int all_change_log_count = 0;
            int.TryParse(faFunc.GetSiteSetting("all_change_log_count"), out all_change_log_count);

            int odd = all_change_log_count - last_msg_id;
            if (odd > 0)
                for (int i = last_msg_id; i < all_change_log_count; i++)
                    _js += "$('#n" + (i + 1) + "').html('&nbsp;<span class=\"label label-primary\">Новое</span>');";

            // Если пользователь сюда пришел, то  считаем что он все новые новости прочитал.
            faFunc.SetUserSetting("last_msg_id", all_change_log_count.ToString());
            Session["NewChangeLogCount"] = 0;
        }
    }
}