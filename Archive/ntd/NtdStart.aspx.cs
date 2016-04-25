using System;
using System.Data.SqlClient;

namespace WebArchiveR6
{
    public partial class NtdStart : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["ntd_category_list"] == null)
            {
                string list_item = "<a href=\"#\" onclick=\"{0}\" class=\"list-group-item\" style=\"margin-bottom: 2px; border: 1px solid #888;\">{1}</a>";
                SqlCommand cmd;
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);

                conn.Open();
                cmd = new SqlCommand("SELECT a.id, a.name FROM _ntd_category a WHERE a.del = 0", conn);
                SqlDataReader rdr = cmd.ExecuteReader();

                string tmp = String.Format(list_item, "$.ajax({url: '/ajax/setses.aspx?key=_ntd_id_category_filter&value=0',type: 'POST',success: function (html) { window.location.href='" + GetRouteUrl("ntd", null) + "' }});", "Все документы Реестра"); ;
                while (rdr.Read())
                {
                    tmp += String.Format(list_item, "$.ajax({url: '/ajax/setses.aspx?key=_ntd_id_category_filter&value=" + rdr["id"].ToString() + "&key1=_ntd_id_category_filter_text&value1=' + escape('" + rdr["name"].ToString() + "'),type: 'POST',success: function (html) { window.location.href='" + GetRouteUrl("ntd", null) + "' }});", rdr["name"].ToString());
                }
                Session["ntd_category_list"] = tmp;

                cmd.Dispose();
                conn.Close();
                rdr.Close();
                rdr.Dispose();
                Session["ntd_sprav_list"] =
                    String.Format(list_item, "window.location.href='" + GetRouteUrl("ntdcategory", null) + "'", "Категории НТД")+
                    String.Format(list_item, "window.location.href='" + GetRouteUrl("ntddepart", null) + "'", "Подразделения для НТД"); 
            }
        }
    }
}