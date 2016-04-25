using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;

namespace WebArchiveR6
{
    public partial class ArchiveStructur : System.Web.UI.Page
    {
        public string _data = "jqArchiveStructur";
        public string _ret = "";
        public string _id = "0";

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session[Master.cur_basename + "_access_structur_view"] == null)
            {
                Response.Clear();
                Response.Write("Нет доступа");
                Response.End();
                return;
            }
            if (Request.QueryString["id"] == null)
            {
                Response.Clear();
                Response.Write("Передан пустой параметр.");
                Response.End();
                return;
            }
            else
                jqArchiveStructur.SelectedRow = _id = Request.QueryString["id"].ToString();
        }

        protected DataTable GetData()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("[dbo].[GetParentTree]", conn);
            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@p_base", Master.cur_basename));
            cmd.Parameters.Add(new SqlParameter("@p_id", _id));

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable datatable = new DataTable();
            sqlDataAdapter.Fill(datatable);
            return datatable;
        }

        // Запрос данных 
        protected void jqArchiveStructur_DataRequesting(object sender, Trirand.Web.UI.WebControls.JQGridDataRequestEventArgs e)
        {
            jqArchiveStructur.DataSource = GetData();
            jqArchiveStructur.DataBind();    
        }

    }
}