using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace ArchNet.ajax
{
    public partial class dd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string result = "", id = "", field1 = "", field2 = "", sql = "", term = "", table = "", where = "";
            term = (Request.QueryString["term"] ?? "").Trim();
            table = (Request.QueryString["table"] ?? "").Trim();
            where = (Request.QueryString["where"] ?? "").Trim();


            if (!String.IsNullOrEmpty(table) && Session["drop_down_list" + table + where] == null)
            {
                switch (table)
                {
                    case "_base":
                        sql = "SELECT id, namerus, name FROM [dbo].[_base] where del=0 " + (where != "" ? " AND " + where : "") + " AND (name like '%" + term + "%' OR namerus LIKE '%" + term + "%') ORDER BY tabindex";
                        break;
                    case "_doctype":
                        sql = "SELECT id, name FROM [dbo].[_doctype] where del=0 ";
                        break;
                    case "_doctype_complect":
                        sql = "SELECT id, name FROM [dbo].[_doctype_complect] where del=0 ";
                        break;
                    case "_edittype":
                        sql = "SELECT id, name FROM [dbo].[_edittype] where del=0";
                        break;
                    case "_table":
                        sql = "SELECT id, description, name FROM [dbo].[_table] where del=0 AND (name like '%" + term + "%' OR description LIKE '%" + term + "%')  ORDER BY description";
                        break;
                    case "_status":
                        sql = "SELECT id, name FROM [dbo].[_status] where del=0 AND name like '%" + term + "%'";
                        break;
                    case "_status2":
                        sql = "SELECT id, name FROM [dbo].[_status2] where del=0 AND name like '%" + term + "%'";
                        break;
                    case "_status3":
                        sql = "SELECT id, name FROM [dbo].[_status3] where del=0";
                        break;
                    case "_validity":
                        sql = "SELECT id, name FROM [dbo].[_validity] where del=0";
                        break;
                    case "_ntd_category":
                        sql = "SELECT id, name FROM [dbo].[_ntd_category] where del=0";
                        break;
                    case "_ntd_depart":
                        sql = "SELECT id, name FROM [dbo].[_ntd_depart] where del=0 order by name";
                        break;
                    case "_source":
                        sql = "SELECT id, name FROM [dbo].[_source] where del=0 AND name like '%" + term + "%'";
                        break;
                    case "_source2":
                        sql = "SELECT id, name FROM [dbo].[_source2] where del=0 " + (where != "" ? " AND " + where : "");
                        break;
                    case "_access":
                        sql = "SELECT id, description, name FROM [dbo].[_access] where del=0 " + (where != "" ? " AND " + where : "") + " AND name like '%" + term + "%' ORDER BY description";
                        break;
                    case "_role":
                        sql = "SELECT id, name FROM [dbo].[_role] where del=0 AND name like '%" + term + "%' ORDER BY name";
                        break;
                    case "_yesno":
                        sql = "SELECT id, name FROM [dbo].[_yesno] where del=0 AND name like '%" + term + "%' ORDER BY name";
                        break;
                    case "_truefalse":
                        sql = "SELECT id, name FROM [dbo].[_truefalse] where del=0";
                        break;
                    case "_state":
                        sql = "SELECT id, name FROM [dbo].[_state] where del=0 AND name like '%" + term + "%' ORDER BY name";
                        break;
                    case "_month":
                        sql = "SELECT id, name FROM [dbo].[_month] where del=0";
                        break;
                    case "_scoretype":
                        sql = "SELECT id, name FROM [dbo].[_scoretype] where del=0";
                        break;
                    case "_storage":
                        sql = "SELECT id, name FROM [dbo].[_storage] where del=0 order by name";
                        break;
                    case "_regstor_group":
                        sql = "SELECT id, name FROM [dbo].[_regstor_group] where del=0 order by name";
                        break;
                    default:
                        sql = "SELECT id, name FROM [dbo].[" + table + "] where del=0 order by name";
                        break;
                }

                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
                conn.Close();

                result = "[";
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        id = row.ItemArray[0].ToString().Replace("\"", "'");
                        switch (table)
                        {
                            case "_base":
                            case "_table":
                            case "_access":
                                field1 = row.ItemArray[1].ToString().Replace("\"", "'");
                                field2 = row.ItemArray[2].ToString().Replace("\"", "'");
                                result += "{\"label\":\"" + field1 + " (" + field2 + ")" + "\", \"id\":\"" + id + "\", \"name\":\"" + field1 + "\"},";
                                break;
                            default:
                                field1 = row.ItemArray[1].ToString().Replace("\"", "'").Trim();
                                if (field1 == "") { field1 = "Пусто"; }
                                result += "{\"label\":\"" + field1 + "\", \"id\":\"" + id + "\", \"name\":\"" + field1 + "\"},";
                                break;
                        }
                    }
                    result = result.Substring(0, result.Length - 1);
                }
                result += "]";
                Session["drop_down_list" + table + where] = result;
            }
            else result = Session["drop_down_list" + table + where].ToString();
            Response.Clear();
            Response.Write(result);
            Response.End();
        }
    }
}