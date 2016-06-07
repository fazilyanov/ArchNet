using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace ArchNet
{
    public partial class ArchiveBulkEdit : System.Web.UI.Page
    {
        public string _data = "jqArchiveBulkEdit";
        public string _ret = "";
        public string _page = "";
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if ((Request.Form["oper"] ?? "").ToString() == "presave")
            {
                Response.Clear();

                string cph_archive_id_doctree = (Request.Form["cph_archive_id_doctree"] ?? "").ToString();
                string archive_id_doctree = (Request.Form["archive_id_doctree"] ?? "").ToString();
                //
                string cph_archive_id_doctype = (Request.Form["cph_archive_id_doctype"] ?? "").ToString();
                string archive_id_doctype = (Request.Form["archive_id_doctype"] ?? "").ToString();
                //
                string cph_archive_id_frm_contr = (Request.Form["cph_archive_id_frm_contr"] ?? "").ToString();
                string archive_id_frm_contr = (Request.Form["archive_id_frm_contr"] ?? "").ToString();
                //
                string archive_docpack = (Request.Form["archive_docpack"] ?? "").ToString();
                //
                string cph_archive_id_prjcode = (Request.Form["cph_archive_id_prjcode"] ?? "").ToString();
                string archive_id_prjcode = (Request.Form["archive_id_prjcode"] ?? "").ToString();
                //
                string cph_archive_id_parent = (Request.Form["cph_archive_id_parent"] ?? "").ToString();
                string archive_id_parent = (Request.Form["archive_id_parent"] ?? "").ToString();
                //
                string cph_archive_id_perf = (Request.Form["cph_archive_id_perf"] ?? "").ToString();
                string archive_id_perf = (Request.Form["archive_id_perf"] ?? "").ToString();
                //
                string cph_archive_id_depart = (Request.Form["cph_archive_id_depart"] ?? "").ToString();
                string archive_id_depart = (Request.Form["archive_id_depart"] ?? "").ToString();
                //
                string cph_archive_hidden = (Request.Form["cph_archive_hidden"] ?? "").ToString();
                string archive_hidden = (Request.Form["archive_hidden"] ?? "").ToString();
                //
                //string cph_archive_hidden = (Request.Form["cph_archive_hidden"] ?? "").ToString();
                //string archive_hidden = (Request.Form["archive_hidden"] ?? "").ToString();
                //

                string confirm_text = "<div style='margin: 50px;'>Для всех элементов в списке будут заменены следующие атрибуты:<br />";
                string confirm_url = "?oper=save";
                int field_count = 0;

                if (cph_archive_id_doctree != "" && archive_id_doctree != "")
                {
                    confirm_text += "Документ на <b>" + cph_archive_id_doctree + " </b>(id=" + archive_id_doctree + ")<br/>";
                    confirm_url += "&archive_id_doctree=" + archive_id_doctree;
                    field_count++;
                }

                if (cph_archive_id_doctype != "" && archive_id_doctype != "")
                {
                    confirm_text += "Вид документа на <b>" + cph_archive_id_doctype + " </b>(id=" + archive_id_doctype + ")<br/>";
                    confirm_url += "&archive_id_doctype=" + archive_id_doctype;
                    field_count++;
                }

                if (cph_archive_hidden != "" && archive_hidden != "")
                {
                    confirm_text += "Скрытый на <b>" + cph_archive_hidden + " </b>(id=" + archive_hidden + ")<br/>";
                    confirm_url += "&archive_hidden=" + archive_hidden;
                    field_count++;
                }

                if (cph_archive_id_frm_contr != "" && archive_id_frm_contr != "")
                {
                    confirm_text += "Контрагент на <b>" + cph_archive_id_frm_contr + " </b>(id=" + archive_id_frm_contr + ")<br/>";
                    confirm_url += "&archive_id_frm_contr=" + archive_id_frm_contr;
                    field_count++;
                }
                if (archive_docpack != "")
                {
                    confirm_text += "Пакет на <b>" + archive_docpack + "<b><br/>";
                    confirm_url += "&archive_docpack=" + archive_docpack;
                    field_count++;
                }

                if (cph_archive_id_prjcode != "" && archive_id_prjcode != "")
                {
                    confirm_text += "Код проекта на <b>" + cph_archive_id_prjcode + " </b>(id=" + archive_id_prjcode + ")<br/>";
                    confirm_url += "&archive_id_prjcode=" + archive_id_prjcode;
                    field_count++;
                }
                if (cph_archive_id_parent != "" && archive_id_parent != "")
                {
                    confirm_text += "Старший документ на <b>" + cph_archive_id_parent + " </b>(id=" + archive_id_parent + ")<br/>";
                    confirm_url += "&archive_id_parent=" + archive_id_parent;
                    field_count++;
                }

                if (cph_archive_id_perf != "" && archive_id_perf != "")
                {
                    confirm_text += "Исполнитель на <b>" + cph_archive_id_perf + " </b>(id=" + archive_id_perf + ")<br/>";
                    confirm_url += "&archive_id_perf=" + archive_id_perf;
                    field_count++;
                }

                if (cph_archive_id_depart != "" && archive_id_depart != "")
                {
                    confirm_text += "Получатель на <b>" + cph_archive_id_depart + " </b>(id=" + archive_id_depart + ")<br/>";
                    confirm_url += "&archive_id_depart=" + archive_id_depart;
                    field_count++;
                }
                if (field_count > 0)
                    Response.Write(confirm_text + "<br/><br/><a href=" + confirm_url + ">Подтвердить замену</a></div>");
                else
                    Response.Write("Не введены данные для замены");
                Response.End();
            }
            else if ((Request.QueryString["oper"] ?? "").ToString() == "save")
            {
                Response.Clear();
                string ret = "";
                string _stamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                //
                string archive_id_doctree = (Request.QueryString["archive_id_doctree"] ?? "").ToString();
                string archive_id_doctype = (Request.QueryString["archive_id_doctype"] ?? "").ToString();
                string archive_id_frm_contr = (Request.QueryString["archive_id_frm_contr"] ?? "").ToString();
                string archive_docpack = (Request.QueryString["archive_docpack"] ?? "").ToString();
                string archive_id_prjcode = (Request.QueryString["archive_id_prjcode"] ?? "").ToString();
                string archive_id_parent = (Request.QueryString["archive_id_parent"] ?? "").ToString();
                string archive_id_perf = (Request.QueryString["archive_id_perf"] ?? "").ToString();
                string archive_id_depart = (Request.QueryString["archive_id_depart"] ?? "").ToString();
                string archive_hidden = (Request.QueryString["archive_hidden"] ?? "").ToString();
                //


                DataTable dt = GetData();
                if (dt.Rows.Count > 0)
                {
                    SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction("bulk_edit_tr");
                    try
                    {

                        SqlCommand cmd = new SqlCommand("", conn, trans);

                        if (archive_id_doctree != "")
                        {
                            foreach (DataRow r in dt.Rows)
                            {
                                cmd.CommandText = "UPDATE [dbo].[" + Master.cur_basename + "_archive] SET [id_doctree]=" + archive_id_doctree + " where id = " + r["id"].ToString();
                                cmd.ExecuteNonQuery();
                                faFunc.ToJournal(cmd, (Session["user_id"] ?? "0").ToString(), 2, (int)r["id"], Session[Master.cur_basename + "_id"].ToString(), 1, "BulkEdit " + _stamp + "\n[id_doctree] " + r["id_doctree"].ToString() + " -> " + archive_id_doctree + "\n");
                            }
                        }

                        if (archive_id_doctype != "")
                        {
                            foreach (DataRow r in dt.Rows)
                            {
                                cmd.CommandText = "UPDATE [dbo].[" + Master.cur_basename + "_archive] SET [id_doctype]=" + archive_id_doctype + " where id = " + r["id"].ToString();
                                cmd.ExecuteNonQuery();
                                faFunc.ToJournal(cmd, (Session["user_id"] ?? "0").ToString(), 2, (int)r["id"], Session[Master.cur_basename + "_id"].ToString(), 1, "BulkEdit " + _stamp + "\n[id_doctype] " + r["id_doctype"].ToString() + " -> " + archive_id_doctype + "\n");
                            }
                        }

                        if (archive_hidden != "")
                        {
                            foreach (DataRow r in dt.Rows)
                            {
                                cmd.CommandText = "UPDATE [dbo].[" + Master.cur_basename + "_archive] SET [hidden]=" + archive_hidden + " where id = " + r["id"].ToString();
                                cmd.ExecuteNonQuery();
                                faFunc.ToJournal(cmd, (Session["user_id"] ?? "0").ToString(), 2, (int)r["id"], Session[Master.cur_basename + "_id"].ToString(), 1, "BulkEdit " + _stamp + "\n[hidden] " + r["hidden"].ToString() + " -> " + archive_hidden + "\n");
                            }
                        }

                        if (archive_id_frm_contr != "")
                        {
                            foreach (DataRow r in dt.Rows)
                            {
                                cmd.CommandText = "UPDATE [dbo].[" + Master.cur_basename + "_archive] SET [id_frm_contr]=" + archive_id_frm_contr + " where id = " + r["id"].ToString();
                                cmd.ExecuteNonQuery();
                                faFunc.ToJournal(cmd, (Session["user_id"] ?? "0").ToString(), 2, (int)r["id"], Session[Master.cur_basename + "_id"].ToString(), 1, "BulkEdit " + _stamp + "\n[id_frm_contr] " + r["id_frm_contr"].ToString() + " -> " + archive_id_frm_contr + "\n");
                            }
                        }
                        if (archive_docpack != "")
                        {
                            foreach (DataRow r in dt.Rows)
                            {
                                cmd.CommandText = "UPDATE [dbo].[" + Master.cur_basename + "_archive] SET [docpack]=" + archive_docpack + " where id = " + r["id"].ToString();
                                cmd.ExecuteNonQuery();
                                faFunc.ToJournal(cmd, (Session["user_id"] ?? "0").ToString(), 2, (int)r["id"], Session[Master.cur_basename + "_id"].ToString(), 1, "BulkEdit " + _stamp + "\n[docpack] " + r["docpack"].ToString() + " -> " + archive_docpack + "\n");
                            }
                        }

                        if (archive_id_prjcode != "")
                        {
                            foreach (DataRow r in dt.Rows)
                            {
                                cmd.CommandText = "UPDATE [dbo].[" + Master.cur_basename + "_archive] SET [id_prjcode]=" + archive_id_prjcode + " where id = " + r["id"].ToString();
                                cmd.ExecuteNonQuery();
                                faFunc.ToJournal(cmd, (Session["user_id"] ?? "0").ToString(), 2, (int)r["id"], Session[Master.cur_basename + "_id"].ToString(), 1, "BulkEdit " + _stamp + "\n[id_prjcode] " + r["id_prjcode"].ToString() + " -> " + archive_id_prjcode + "\n");
                            }
                        }

                        if (archive_id_parent != "")
                        {
                            foreach (DataRow r in dt.Rows)
                            {
                                cmd.CommandText = "UPDATE [dbo].[" + Master.cur_basename + "_archive] SET [id_parent]=" + archive_id_parent + " where id = " + r["id"].ToString();
                                cmd.ExecuteNonQuery();
                                faFunc.ToJournal(cmd, (Session["user_id"] ?? "0").ToString(), 2, (int)r["id"], Session[Master.cur_basename + "_id"].ToString(), 1, "BulkEdit " + _stamp + "\n[id_parent] " + r["id_parent"].ToString() + " -> " + archive_id_parent + "\n");
                            }
                        }

                        if (archive_id_perf != "")
                        {
                            foreach (DataRow r in dt.Rows)
                            {
                                cmd.CommandText = "UPDATE [dbo].[" + Master.cur_basename + "_archive] SET [id_perf]=" + archive_id_perf + " where id = " + r["id"].ToString();
                                cmd.ExecuteNonQuery();
                                faFunc.ToJournal(cmd, (Session["user_id"] ?? "0").ToString(), 2, (int)r["id"], Session[Master.cur_basename + "_id"].ToString(), 1, "BulkEdit " + _stamp + "\n[id_perf] " + r["id_perf"].ToString() + " -> " + archive_id_perf + "\n");
                            }
                        }

                        if (archive_id_depart != "")
                        {
                            foreach (DataRow r in dt.Rows)
                            {
                                cmd.CommandText = "UPDATE [dbo].[" + Master.cur_basename + "_archive] SET [id_depart]=" + archive_id_depart + " where id = " + r["id"].ToString();
                                cmd.ExecuteNonQuery();
                                faFunc.ToJournal(cmd, (Session["user_id"] ?? "0").ToString(), 2, (int)r["id"], Session[Master.cur_basename + "_id"].ToString(), 1, "BulkEdit " + _stamp + "\n[id_depart] " + r["id_depart"].ToString() + " -> " + archive_id_depart + "\n");
                            }
                        }

                        trans.Commit();
                        conn.Close();
                        ret = "Успешно заменено записей: " + dt.Rows.Count;
                        faFunc.ToLog(7, "Записей: " + dt.Rows.Count);

                    }
                    catch (Exception ex)
                    {
                        ret  = faFunc.GetExceptionMessage(ex);
                        try
                        {
                            trans.Rollback();
                            conn.Close();
                        }
                        catch (Exception ex2)
                        {
                            ret = ex2.GetType() + ":" + ex2.Message;
                        }

                    }
                }


                Response.Write(ret);
                Response.End();
            }
            else
            {
                JQGridColumn _c = null;
                _page = (Page.RouteData.Values["p_page"] ?? "").ToString();

                _c = new JQGridColumn();
                _c.DataField = "id";
                _c.Width = 58;
                _c.Visible = true;
                _c.HeaderText = "ID";
                _c.DataFormatString = "";
                _c.TextAlign = Trirand.Web.UI.WebControls.TextAlign.Center;
                jqArchiveBulkEdit.Columns.Add(_c);

                _c = new JQGridColumn();
                _c.DataField = "id_doctree_name_text";
                _c.Width = 100;
                _c.Visible = true;
                _c.HeaderText = "Документ";
                _c.DataFormatString = "";
                _c.TextAlign = Trirand.Web.UI.WebControls.TextAlign.Center;
                jqArchiveBulkEdit.Columns.Add(_c);

                if (_page != "dog" && _page != "empl" && _page != "tech")
                {
                    _c = new JQGridColumn();
                    _c.DataField = "id_doctype_name_text";
                    _c.Width = 70;
                    _c.Visible = true;
                    _c.HeaderText = "Вид докум.";
                    _c.DataFormatString = "";
                    _c.TextAlign = Trirand.Web.UI.WebControls.TextAlign.Center;
                    jqArchiveBulkEdit.Columns.Add(_c);
                }


                if (_page != "norm" && _page != "empl")
                {
                    _c = new JQGridColumn();
                    _c.DataField = "id_frm_contr_name_text";
                    _c.Width = 200;
                    _c.Visible = true;
                    _c.HeaderText = "Контрагент";
                    _c.DataFormatString = "";
                    _c.TextAlign = Trirand.Web.UI.WebControls.TextAlign.Left;
                    jqArchiveBulkEdit.Columns.Add(_c);
                }


                if (_page != "dog")
                {
                    _c = new JQGridColumn();
                    _c.DataField = "docpack";
                    _c.Width = 70;
                    _c.Visible = true;
                    _c.HeaderText = "Пакет";
                    _c.DataFormatString = "";
                    _c.TextAlign = Trirand.Web.UI.WebControls.TextAlign.Center;
                    jqArchiveBulkEdit.Columns.Add(_c);
                }

                if (_page != "ord" && _page != "empl" && _page != "tech" && _page != "oth" && _page != "norm")
                {
                    _c = new JQGridColumn();
                    _c.DataField = "id_prjcode_code_new_text";
                    _c.Width = 150;
                    _c.Visible = true;
                    _c.HeaderText = "Код проекта";
                    _c.DataFormatString = "";
                    _c.TextAlign = Trirand.Web.UI.WebControls.TextAlign.Left;
                    jqArchiveBulkEdit.Columns.Add(_c);
                }
                if (_page != "ord" && _page != "tech" && _page != "norm")
                {
                    _c = new JQGridColumn();
                    _c.DataField = "id_parent_num_doc_text";
                    _c.Width = 150;
                    _c.Visible = true;
                    _c.HeaderText = "Старший докум.";
                    _c.DataFormatString = "";
                    _c.TextAlign = Trirand.Web.UI.WebControls.TextAlign.Left;
                    jqArchiveBulkEdit.Columns.Add(_c);
                }
                if (_page != "empl" && _page != "tech")
                {
                    _c = new JQGridColumn();
                    _c.DataField = "id_perf_name_text";
                    _c.Width = 150;
                    _c.Visible = true;
                    _c.HeaderText = "Исполнитель";
                    _c.DataFormatString = "";
                    _c.TextAlign = Trirand.Web.UI.WebControls.TextAlign.Left;
                    jqArchiveBulkEdit.Columns.Add(_c);
                }
                if (_page != "tech")
                {
                    _c = new JQGridColumn();
                    _c.DataField = "id_depart_name_text";
                    _c.Width = 200;
                    _c.Visible = true;
                    _c.HeaderText = "Получатель";
                    _c.DataFormatString = "";
                    _c.TextAlign = Trirand.Web.UI.WebControls.TextAlign.Left;
                    jqArchiveBulkEdit.Columns.Add(_c);
                }
               
                    _c = new JQGridColumn();
                    _c.DataField = "hidden_name_text";
                    _c.Width = 200;
                    _c.Visible = true;
                    _c.HeaderText = "Скрытый";
                    _c.DataFormatString = "";
                    _c.TextAlign = Trirand.Web.UI.WebControls.TextAlign.Left;
                    jqArchiveBulkEdit.Columns.Add(_c);
                
            }
        }

        protected DataTable GetData()
        {
            return (Session[_data] as DataTable);
        }

        // Запрос данных 
        protected void jqArchiveBulkEdit_DataRequesting(object sender, Trirand.Web.UI.WebControls.JQGridDataRequestEventArgs e)
        {
            jqArchiveBulkEdit.DataSource = GetData();
            jqArchiveBulkEdit.DataBind();
        }


        // УДАЛЕНИЕ
        protected void jqArchiveBulkEdit_RowDeleting(object sender, Trirand.Web.UI.WebControls.JQGridRowDeleteEventArgs e)
        {
            DataTable dt = GetData();
            DataRow row = dt.Rows.Find(e.RowKey);
            row.Delete();
            jqArchiveBulkEdit.DataSource = Session[_data] = dt;
            jqArchiveBulkEdit.DataBind();
        }


    }
}