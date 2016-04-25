using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebArchiveR6.ajax
{
    public partial class id_docversion : System.Web.UI.Page
    {
        string result = @"
<table id='detail'>
  <tr>
    <th>Код ЭА</th>
    <td>{0}</td>
    <th>Дата ред.</th>
    <td>{1}</td>
    <th>Дата док.</th>
    <td>{2}</td>
 </tr>
 <tr>
    <th>Документ</th>
    <td>{3}</td>
    <th>№ Документа</th>
    <td>{4}</td>
    <th>Вид док.</th>
    <td>{5}</td>
  </tr>
  <tr>
    <th>Контрагент</th>
    <td>{6}</td>
    <th>Содержание</th>
    <td>{7}</td>
    <th>Примечание</th>
    <td>{8}</td>
 </tr>
  <tr>
    <th>Оператор ЭА</th>
    <td>{9}</td>
    <th>Сумма</th>
    <td>{10}</td>
    <th>Пакет док.</th>
    <td>{11}</td>
 </tr>
  <tr>
    <th>Код проекта</th>
    <td>{12}</td>
    <th>Участок</th>
    <td>{13}</td>
    <th>Дата в 1с</th>
    <td>{14}</td>
 </tr>
  <tr>
    <th>Исполнитель</th>
    <td>{15}</td>
    <th>Получатель</th>
    <td>{16}</td>
    <th>Отв.подразделение</th>
    <td>{17}</td>
  </tr>
  <tr>
    <th>Статус</th>
    <td>{18}</td>
    <th>Подписанты</th>
    <td>{19}</td>
    <th>Подразделения</th>
    <td>{20}</td>
  </tr>
</table>
<textarea id='archive_text_{0}'rows='22' cols='200' name='text'>{21}</textarea>";
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            string cb = Request.QueryString["cb"];
            string h = Request.QueryString["h"];
            if (!String.IsNullOrEmpty(id) && !String.IsNullOrEmpty(cb) && !String.IsNullOrEmpty(h))
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                int _offset = 1;
                int _limit = 1;
                SqlCommand cmd = new SqlCommand("[dbo].[SelectArchive]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_base", cb));
                cmd.Parameters.Add(new SqlParameter("@p_where", "a.id=" + id));
                cmd.Parameters.Add(new SqlParameter("@p_order_by", "id"));
                cmd.Parameters.Add(new SqlParameter("@p_order_by_dir", "ASC"));
                cmd.Parameters.Add(new SqlParameter("@p_offset", _offset));
                cmd.Parameters.Add(new SqlParameter("@p_page", "0"));
                cmd.Parameters.Add(new SqlParameter("@p_page_acc", "1"));
                cmd.Parameters.Add(new SqlParameter("@p_page_dog", "1"));
                cmd.Parameters.Add(new SqlParameter("@p_page_ord", "1"));
                cmd.Parameters.Add(new SqlParameter("@p_page_empl", "1"));
                cmd.Parameters.Add(new SqlParameter("@p_page_tech", "1"));
                cmd.Parameters.Add(new SqlParameter("@p_page_oth", "1"));
                cmd.Parameters.Add(new SqlParameter("@p_page_ohs", "1"));
                cmd.Parameters.Add(new SqlParameter("@p_structur1", "0"));
                cmd.Parameters.Add(new SqlParameter("@p_structur2", "0"));
                cmd.Parameters.Add(new SqlParameter("@p_limit", _limit));
                cmd.Parameters.Add(new SqlParameter("@p_hiden", h));
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable datatable = new DataTable();
                sqlDataAdapter.Fill(datatable);
                
                SqlCommand cmd2 = new SqlCommand("[dbo].[GetSigns]", conn);
                cmd2.Parameters.Add(new SqlParameter("@p_base", cb));
                cmd2.Parameters.Add(new SqlParameter("@p_id_archive", id));
                cmd2.CommandType = CommandType.StoredProcedure;            
                SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(cmd2);
                DataTable datatable2 = new DataTable();
                sqlDataAdapter2.Fill(datatable2);

                SqlCommand cmd3 = new SqlCommand("[dbo].[GetDeparts]", conn);
                cmd3.Parameters.Add(new SqlParameter("@p_base", cb));
                cmd3.Parameters.Add(new SqlParameter("@p_id_archive", id));
                cmd3.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDataAdapter3 = new SqlDataAdapter(cmd3);
                DataTable datatable3 = new DataTable();
                sqlDataAdapter3.Fill(datatable3);

                //SqlCommand cmd4 = new SqlCommand("[dbo].[GetDoctextById]", conn);
                //cmd4.Parameters.Add(new SqlParameter("@p_base", cb));
                //cmd4.Parameters.Add(new SqlParameter("@p_id_archive", id));
                //cmd4.CommandType = CommandType.StoredProcedure;
                //SqlDataAdapter sqlDataAdapter4 = new SqlDataAdapter(cmd4);
                //DataTable datatable4 = new DataTable();
                //sqlDataAdapter4.Fill(datatable4);

                result = String.Format(result,
                datatable.Rows[0]["id"].ToString(),
                datatable.Rows[0]["date_upd"].ToString(),
                datatable.Rows[0]["date_doc"].ToString(),
                datatable.Rows[0]["id_docname"].ToString(),
                datatable.Rows[0]["n_doc"].ToString(),
                datatable.Rows[0]["id_doctype"].ToString(),
                datatable.Rows[0]["id_frm"].ToString(),
                datatable.Rows[0]["content"].ToString(),
                datatable.Rows[0]["prim"].ToString(),
                datatable.Rows[0]["id_user"].ToString(),
                datatable.Rows[0]["summ"].ToString(),
                datatable.Rows[0]["docpack"].ToString(),
                datatable.Rows[0]["id_prj_code"].ToString(),
                datatable.Rows[0]["id_perf"].ToString(),
                datatable.Rows[0]["id_structur1"].ToString(),
                datatable.Rows[0]["id_structur2"].ToString(),
                datatable.Rows[0]["id_status"].ToString(),
                (datatable2.Rows.Count>0)?datatable2.Rows[0]["signs"].ToString():"",
                (datatable3.Rows.Count > 0)?datatable3.Rows[0]["departs"].ToString() : "",
                datatable.Rows[0]["doctext"].ToString());
                result = "<style>  #detail th {text-align: left; background: #e6e6e6; padding: 8px; border: 1px solid #aaaaaa;font-size:12px;}" +
                          "#detail td { padding: 8px; border: 1px solid #aaaaaa; font-size:12px;}</style>" + result;
                conn.Close();
                Response.Clear();
                Response.Write(result);
                Response.End();

            }
        }
    }
}