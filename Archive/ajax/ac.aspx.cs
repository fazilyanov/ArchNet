using System;
using System.Data;
using System.Data.SqlClient;

namespace ArchNet.ajax
{
    public partial class ac : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string result = "", id = "", field1 = "", field2 = "", field3 = "", sql = "", term = "", table = "", table2 = "", where = "", me = "";
            term = (Request.QueryString["term"] ?? "").Trim().Replace("'", "").Replace("_", "[_]");
            where = (Request.QueryString["where"] ?? "").Trim();
            table = table2 = (Request.QueryString["table"] ?? "").Trim();
            table = table.Contains("_archive") ? "archive" : (table.Contains("_person") ? "person" : (table.Contains("_department") ? "department" : table));

            me = table == "_user" ? "{\"label\":\"Я \",\"id\":\"" + Session["user_id"].ToString() + "\",\"name\":\"" + Session["user_name"].ToString() + "\"}," : "";
            if (!String.IsNullOrEmpty(table) && !String.IsNullOrEmpty(term))
            {
                string qr = faFunc.ConvertEngToRus(term).Replace("'", "").Replace("_", "[_]");

#warning убрать qe из запросов
                string qe = term;// faFunc.ConvertRusToEng(term).Replace("'", "").Replace("_", "[_]");// нехотятс русского на английский, курвы

                switch (table)
                {
                    //
                    case "archive":
                        sql = "SELECT TOP 15 id, num_doc, id as code FROM [dbo].[" + table2 + "] where del=0 AND ( num_doc like '%" + qr + "%' OR num_doc like '%" + qe + "%' OR id LIKE '%" + term + "%') ORDER BY num_doc";
                        break;

                    case "person":
                        sql = "SELECT TOP 15 id, name, prim, id_status FROM [dbo].[" + table2 + "] where del=0 AND (name like '%" + qr + "%' OR name_full like '%" + qr + "%' OR prim like '%" + qr + "%' OR name like '%" + qe + "%' OR name_full like '%" + qe + "%' OR prim like '%" + qe + "%')";
                        break;

                    case "department":
                        sql = "SELECT TOP 15 id, name FROM [dbo].[" + table2 + "] where del=0 AND ( name like '%" + qr + "%' OR name like '%" + qe + "%' OR id  LIKE '" + term + "')";
                        break;
                    //
                    case "_country":
                        sql = "SELECT TOP 15 id, name FROM [dbo].[_country] where del=0 AND name like '%" + term + "%' OR id  LIKE '" + term + "'  ORDER BY name";
                        break;

                    case "_frm":
                        sql = "SELECT TOP 15 id, name, 'ИНН '+ CAST(inn as nvarchar(50))  FROM [dbo].[_frm] where del=0 AND (name like '%" + qr + "%' OR name_full LIKE '%" + qr + "%' OR name like '%" + qe + "%' OR name_full LIKE '%" + qe + "%' OR inn LIKE '%" + term + "%')";
                        break;

                    case "_region":
                        sql = "SELECT TOP 15 id, name FROM [dbo].[_region] where del=0 AND name like '%" + term + "%' OR id  LIKE '" + term + "'";
                        break;

                    case "_user":
                        sql = "SELECT TOP 15 id, name, login FROM [dbo].[_user] where del=0 AND (name like '%" + qr + "%' OR login LIKE '%" + qr + "%' OR name like '%" + qe + "%' OR login LIKE '%" + qe + "%') ORDER BY name";
                        break;

                    case "_prjcode":
                        sql = "SELECT TOP 15 id, code_new, name FROM [dbo].[_prjcode] where del=0 AND ( code_new like '%" + qr + "%' OR code_old like '%" + qr + "%' OR name LIKE '%" + qr + "%' OR code_new like '%" + qe + "%' OR code_old like '%" + qe + "%' OR name LIKE '%" + qe + "%')";
                        break;

                    case "_access":
                        sql = "SELECT TOP 15 id, description FROM [dbo].[_access] where del=0 " + (where != "" ? " AND " + where : "") + " AND (name like '%" + qr + "%' OR description like '%" + qr + "%' OR name like '%" + qe + "%' OR description like '%" + qe + "%')";
                        break;

                    case "_doctree":
                        sql = "SELECT TOP 25 a.id, ISNULL(b.name+ ' / ','') + a.name + ' (id=' + CAST(a.id as nvarchar(50))  + ')' FROM [dbo].[_doctree] a  left join [dbo].[_doctree] b on b.id=a.id_parent where a.del=0 AND (a.name like '%" + qr + "%' OR a.name like '%" + qe + "%')";
                        break;

                    default:
                        break;
                }

                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                conn.Close();

                result = "[" + me;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        id = row.ItemArray[0].ToString().Replace("\"", "'");
                        switch (table)
                        {
                            case "_base":
                            case "_frm":
                            case "_table":
                            case "_user":
                            case "archive":
                            case "_prjcode":

                                field1 = row.ItemArray[1].ToString().Replace("\"", "'");
                                field2 = row.ItemArray[2].ToString().Replace("\"", "'");
                                result += "{\"label\":\"" + field1 + " (" + field2 + ")" + "\", \"id\":\"" + id + "\", \"name\":\"" + field1 + "\"},";
                                break;

                            case "person":

                                field1 = row.ItemArray[1].ToString().Replace("\"", "'");
                                field2 = row.ItemArray[2].ToString().Replace("\"", "'");
                                field3 = row.ItemArray[3].ToString();
                                string lbl = field1 + (field2 != "" ? " (" + field2 + ")" : "") + (field3 != "1" ? " (Уволен) " : "");
                                result += "{\"label\":\"" + lbl + "\", \"id\":\"" + id + "\", \"name\":\"" + field1 + "\"},";
                                break;

                            case "_country":
                            case "department":
                            case "_doctype":
                            case "_edittype":
                            case "_region":
                            case "_status":
                            case "_access":
                            case "_doctree":

                                field1 = row.ItemArray[1].ToString().Replace("\"", "'");
                                result += "{\"label\":\"" + field1 + "\", \"id\":\"" + id + "\", \"name\":\"" + field1 + "\"},";
                                break;

                            default:
                                break;
                        }
                    }
                    result = result.Substring(0, result.Length - 1);
                }
                else
                    result += me + "{\"label\":\"Ничего не найдено\",\"id\":\"0\",\"name\":\"\"}";
                result += "]";
            }
            else
                result = "[" + me + "{\"label\":\"Начните вводить для поиска\",\"id\":\"0\",\"name\":\"\"}]";
            Response.Clear();
            Response.Write(result);
            Response.End();
        }
    }
}