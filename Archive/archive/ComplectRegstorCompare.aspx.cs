using System;
using System.Data;
using System.Data.SqlClient;

namespace WebArchiveR6
{
    public partial class ComplectRegstorCompare : System.Web.UI.Page
    {
        public string _data = "jqComplectRegstorCompare";
        public string _id = "";
        int _id_base = 0;
        public string _base = "";
        public string _ret = "";

        protected DataTable GetData()
        {
            // 1020208071 - 5 версий

            //if (Session[_data] == null)
            //{
            DataTable dt_res = new DataTable();
            dt_res.Columns.Add(new DataColumn("ID"));
            dt_res.Columns.Add(new DataColumn("id_archive1"));
            dt_res.Columns.Add(new DataColumn("nn1"));
            dt_res.Columns.Add(new DataColumn("barcode1"));
            dt_res.Columns.Add(new DataColumn("status1"));
            
            dt_res.Columns.Add(new DataColumn("space"));

            dt_res.Columns.Add(new DataColumn("id_regstor_list"));
            dt_res.Columns.Add(new DataColumn("id_archive2"));
            dt_res.Columns.Add(new DataColumn("nn2"));
            dt_res.Columns.Add(new DataColumn("barcode2"));
            dt_res.Columns.Add(new DataColumn("status2"));

            dt_res.Columns.Add(new DataColumn("space2"));

            dt_res.Columns.Add(new DataColumn("id_regstor"));
            dt_res.Columns.Add(new DataColumn("name"));
            dt_res.Columns.Add(new DataColumn("regstor_barcode"));
            dt_res.Columns.Add(new DataColumn("storage"));
            dt_res.Columns.Add(new DataColumn("case_index"));


            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            try
            {
                conn.Open();

                string sql =
                    "SELECT b.id, b.[id_archive], d.nn, b.[barcode], c.name as status FROM [dbo].[_complectnew] a " +
                    "Join [dbo].[_complectnew_list] b on b.id_complectnew=a.id " +
                    "Join [dbo].[" + _base + "_docversion] d on d.barcode=b.barcode and b.barcode>0 and d.del=0 and d.main=1" +
                    "LEFT JOIN  [dbo].[_status] c on c.id=d.id_status " +
                    "Where a.id = " + _id + " and b.id_archive>0 and b.del=0";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 30;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable dt_complect_archive = new DataTable();
                sqlDataAdapter.Fill(dt_complect_archive);
                int count = 0;

                foreach (DataRow row in dt_complect_archive.Rows)
                {
                    sql =
                        "SELECT a.id, a.id_regstor,b.id_archive, b.nn, a.barcode,c.name as status,f.barcode as regstor_barcode, s.name as storage,f.case_index, f.name " +
                        "FROM [dbo].[_regstor_list] a " +
                        "LEFT JOIN [dbo].[_regstor] f on f.id=a.id_regstor " +
                        "LEFT JOIN [dbo].[_storage] s on s.id=f.id_storage " +
                        "JOIN [dbo].[" + _base + "_docversion]b on b.id=(select top 1 id  from [dbo].[" + _base + "_docversion] where barcode=a.barcode and del=0 order by nn desc) " +
                        "LEFT JOIN  [dbo].[_status] c on c.id=b.id_status " +
                        "WHERE a.barcode in (select barcode from [dbo].[" + _base + "_docversion] where id_archive=" + row["id_archive"].ToString() + " and del=0 and barcode>0) " +
                        "and a.del=0 and f.id_base=" + _id_base;

                    cmd = new SqlCommand(sql, conn);
                    sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt_regstor = new DataTable();
                    sqlDataAdapter.Fill(dt_regstor);
                    foreach (DataRow row2 in dt_regstor.Rows)
                    {
                        DataRow new_row = dt_res.NewRow();
                        new_row["id"] = ++count;
                        new_row["id_archive1"] = row["id_archive"];
                        new_row["nn1"] = row["nn"];
                        new_row["barcode1"] = row["barcode"];
                        new_row["status1"] = row["status"];
                        
                        if (row["barcode"].ToString() != row2["barcode"].ToString()) new_row["space"] = "!";

                        new_row["id_regstor_list"] = row2["id"];
                        new_row["id_archive2"] = row2["id_archive"];
                        new_row["nn2"] = row2["nn"];
                        new_row["barcode2"] = row2["barcode"];
                        new_row["status2"] = row2["status"];
                        
                        new_row["space2"] = " ";

                        new_row["id_regstor"] = row2["id_regstor"];
                        new_row["name"] = row2["name"];
                        new_row["regstor_barcode"] = row2["regstor_barcode"];
                        new_row["storage"] = row2["storage"];
                        new_row["case_index"] = row2["case_index"];

                       
                        dt_res.Rows.Add(new_row);
                    }
                }
                Session[_data] = dt_res;
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Response.Clear();
                Response.Write(faFunc.GetExceptionMessage(ex));
                Response.End();
            }
            //}
            //return (Session[_data] as DataTable);
            return dt_res;
        }

        // Запрос данных
        protected void jqArchivePerform_DataRequesting(object sender, Trirand.Web.UI.WebControls.JQGridDataRequestEventArgs e)
        {
            jqArchivePerform.DataSource = GetData();
            jqArchivePerform.DataBind();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            _id = (Request.QueryString["id"] ?? "").Trim();
            _id_base = 0;
            if (int.TryParse((Request.QueryString["id_base"] ?? "").Trim(), out _id_base))
                _base = faFunc.GetBaseNameById(_id_base);
            if (_id == "" || _id_base == 0)
            {
                Response.Clear();
                Response.Write("Bad Param!");
                Response.End();
            }
        }
    }
}