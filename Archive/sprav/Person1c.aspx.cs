using System;
using System.Data;
using System.Data.SqlClient;

namespace WebArchiveR6
{
    public partial class Person1c : System.Web.UI.Page
    {
        public faList list;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Session["textfio1c"] = Session["textfio1c"] ?? "Нефтекамск";
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
        }

        protected DataTable GetData()
        {
            DataTable res = null;
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            string cur_basename = Request.QueryString["b"] != "" ? Request.QueryString["b"] : "";
            string where = (Session["textuser1c"] ?? "").ToString().Trim();

            // Показывает подразделение и таблици 1с если есть локальная копия подразделения в справочнике базы то подставляет, иначе - пусто
            string sql =
                "SELECT a.*, a.id_1c as id, d.id as id_depart,d.name as departname, b.full_name as department,c.org_name as organization FROM _user_1c a " +
                "LEFT JOIN _department_1c b ON a.department_ID=b.id " +
                "LEFT JOIN " + cur_basename + "_department d ON a.department_ID=d.id_1c " +
                "LEFT JOIN _organization_1c c ON b.organization_ID=c.id " +
                (where != "" ? "where a.fio_full like '%" + where + "%'" + " OR a.id_1c like '%" + where + "%'" : "") + " order by a.fio";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                res = new DataTable();
                sqlDataAdapter.Fill(res);
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Clear();
                Response.Write(ex.Message);
                conn.Close();
            }
            return res;
        }

        // Запрос данных
        protected void jqUser_DataRequesting(object sender, Trirand.Web.UI.WebControls.JQGridDataRequestEventArgs e)
        {
            jqUser.DataSource = GetData();
            jqUser.DataBind();
        }
    }
}