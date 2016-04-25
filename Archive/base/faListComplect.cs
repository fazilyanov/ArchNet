using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Trirand.Web.UI.WebControls;

// FAZL october 2014

namespace WebArchiveR6
{
    public class faListComplect
    {
        #region CONST

        public string _act = "";
        public string _from = "";
        public string _frombase = "";
        public string _id = "";
        public string _mode = "";
        private string[] act_list = new string[] { "add", "view", "del", "copy", "file", "select", "copybase" };
        private string[] cond_int = new string[] { "=", "!=", ">", "<", ">=", "<=" };
        private string[] cond_str = new string[] { "=", "*" };
        private int int_value = 0;

        #endregion CONST

        #region PROP

        public Dictionary<string, string> ActionMenuItems
        {
            get;
            set;
        }

        public string BaseName
        {
            get;
            set;
        }

        public int CursorCount
        {
            get;
            set;
        }

        public Dictionary<string, faCursor> Cursors
        {
            get;
            set;
        }

        public int EditFormHeight
        {
            get;
            set;
        }

        public int EditFormWidth
        {
            get;
            set;
        }

        public bool EnableSuperVisorCheckBox { get; set; }

        public string IDBase
        {
            get;
            set;
        }

        public string JS
        {
            get;
            set;
        }

        public Dictionary<string, string> JSFunctionList
        {
            get;
            set;
        }

        public Dictionary<string, string> JSReadyList
        {
            get;
            set;
        }

        public faCursor MainCursor
        {
            get;
            set;
        }

        public faPage Page
        {
            get;
            set;
        }

        public NameValueCollection RequestGet
        {
            get;
            set;
        }

        public NameValueCollection RequestPost
        {
            get;
            set;
        }

        public string RouteName
        {
            get;
            set;
        }

        public bool ShowCheckBox { get; set; }
        public bool ShowFilterPanel { get; set; }
        public bool ShowHiddenDoc { get; set; }
        public bool ShowArrows { get; set; }

        #endregion PROP

        #region COMMON

        public faListComplect()
        {
            ShowFilterPanel = false;
            ShowCheckBox = false;
            ShowHiddenDoc = false;
            ShowArrows = false;
            EnableSuperVisorCheckBox = false;
            Cursors = new Dictionary<string, faCursor>();
            ActionMenuItems = new Dictionary<string, string>();
            JSFunctionList = new Dictionary<string, string>();
            JSReadyList = new Dictionary<string, string>();
            CursorCount = 0;
            EditFormHeight = 300;
            EditFormWidth = 1000;
            Page = faPage.none;
            IDBase = "0";
            BaseName = "";
        }

        public void AddCursor(faCursor cur)
        {
            Cursors[cur.Alias] = cur;
            CursorCount++;
        }

        public DataRowView GetTableInfo(int id)
        {
            DataView _dv = new DataView(GetDataTable(), "id = " + id, "id", DataViewRowState.CurrentRows);
            if (_dv.Count > 0)
                return _dv[0];
            else
                return null;
        }

        public DataRowView GetTableInfo(string name)
        {
            DataView _dv = new DataView(GetDataTable(), "name = '" + name + "'", "id", DataViewRowState.CurrentRows);
            if (_dv.Count > 0)
                return _dv[0];
            else
                return null;
        }

        public void Render(HtmlForm form, System.Web.UI.Page _page)
        {
            RequestPost = HttpContext.Current.Request.Form;
            RequestGet = HttpContext.Current.Request.QueryString;
            HttpResponse Resp = HttpContext.Current.Response;
            _act = (RequestGet["act"] ?? "").ToString().Trim().ToLower();
            _id = (RequestGet["id"] ?? "").ToString().Trim().ToLower();
            _from = (RequestGet["from"] ?? "").ToString().Trim().ToLower();
            _mode = (RequestGet["mode"] ?? "").ToString().Trim().ToLower();

            if (_id == "" && (_act == "" || _act == "select"))
            {
                JQGrid jqAlias = new JQGrid();
                PrepareJQGrid(out jqAlias);
                form.Controls.Add(new LiteralControl(RenderFilterPanel()));
                form.Controls.Add(jqAlias);
                AddViewControls(MainCursor, form);
                JS = System.Text.RegularExpressions.Regex.Replace(GenerateListJS(jqAlias), "  +", "");
            }
            else if (_act == "setting")
            {
                string _ret =
                    "<div class=\"row\">" +
                    "   <div class=\"col-md-3\">" +
                    "   </div>" +
                    "   <div class=\"col-md-6\"><br/><br/>  " +
                    "       <div class=\"panel panel-primary\">" +
                    "           <div class=\"panel-heading\">" +
                    "               <h3 class=\"panel-title\">Настройка полей</h3>" +
                    "           </div>" +
                    "       <div class=\"panel-body\">";
                foreach (faField fld in MainCursor.Fields)
                {
                    if (fld.View.Visible)
                        _ret += fld.View.CaptionShort + "<br/>";
                }
                _ret += " </div>" +
                "   </div>" +
                "   <div class=\"col-md-3\">" +
                "   </div>" +
                "</div>" +
                "<script type='text/javascript'>" +
                "</script>";
                form.Controls.Add(new LiteralControl(_ret));

                // <a href="#" onclick="hide_search_panel();" title="Скрыть панель фильтров" style="margin-right: 3px;float: right;font-size: 15px;"><span class="gi gi-settings"></span></a>
                // <a href="#" onclick="hide_search_panel();" title="Скрыть панель фильтров" style="margin-right: 5px;float: right;font-size: 15px;"><span class="gi gi-floppy_disk"></span></a></div>
            }
            else if (_act == "export_excel")
            {
                int totalrows;
                DataTable dt = new DataTable();
                dt = GetData(MainCursor, 0, 10000, "id", "Asc", out totalrows);

                using (ExcelPackage xlPackage = new ExcelPackage())
                {
                    ExcelWorksheet ws = xlPackage.Workbook.Worksheets.Add(MainCursor.Alias);

                    if (ws != null)
                    {
                        int i = 0;
                        int row = 0;
                        List<string> visfld = new List<string>();
                        foreach (faField fld in MainCursor.Fields)
                            if (fld.View.Visible)
                            {
                                row = 1;
                                i++;
                                ws.Cells[row, i].Value = fld.View.CaptionShort;
                                ws.Cells[row, i].Style.Font.Bold = true;
                                foreach (DataRow r in dt.Rows)
                                    ws.Cells[++row, i].Value = r[fld.LookUp.Key != "" ? fld.Data.FieldName + "_" + fld.LookUp.Field + "_text" : fld.Data.FieldName];
                                ws.Column(i).Width = fld.View.Width / 5;
                                if (fld.View.TextAlign == "center")
                                    ws.Column(i).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                switch (fld.Filter.Control)
                                {
                                    case faControl.TextBoxInteger:
                                        ws.Column(i).Style.Numberformat.Format = "0";

                                        break;

                                    case faControl.TextBoxNumber:
                                        ws.Column(i).Style.Numberformat.Format = "0.00";
                                        break;

                                    case faControl.TextBoxFullSearch:
                                    case faControl.TextArea:
                                    case faControl.TextBox:
                                        break;

                                    case faControl.DatePicker:
                                        ws.Column(i).Style.Numberformat.Format = "dd.mm.yyyy";
                                        break;

                                    case faControl.DateTimePicker:
                                        ws.Column(i).Style.Numberformat.Format = "dd.mm.yyyy hh:MM";
                                        break;

                                    case faControl.DropDown:
                                    case faControl.AutoComplete:
                                    case faControl.TreeGrid:
                                    case faControl.File:
                                        break;

                                    default:
                                        break;
                                }
                                ws.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            }

                        ws.Cells[1, 1, row, i].AutoFilter = true;
                        //using (ExcelRange r = ws.Cells[1, dt.Columns.Count])
                        //{
                        //    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                        //    r.Style.Font.Bold = true;
                        //}
                    }
                    // we had better add some document properties to the spreadsheet

                    // set some core property values
                    faFunc.ToLog(5, "Записей: " + totalrows);
                    xlPackage.Workbook.Properties.Title = "Выгрузка данных";
                    xlPackage.Workbook.Properties.Author = HttpContext.Current.Session["user_name"].ToString();
                    xlPackage.SaveAs(Resp.OutputStream);
                    Resp.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Resp.AddHeader("content-disposition", "attachment;  filename=" + MainCursor.Alias + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".xlsx");
                }
            }
            else if (_act == "export_csv")
            {
                int totalrows;
                DataTable dt = new DataTable();
                DateTime _time = DateTime.Now;

                string _head = "";
                string _body = "";
                string _fname = Path.Combine(Properties.Settings.Default.filepath, "tempfiles\\") + MainCursor.Alias + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".csv";
                StreamWriter sw = new StreamWriter(_fname, false, System.Text.Encoding.UTF8);

                dt = GetData(MainCursor, 0, 100000, MainCursor.SortColumn, MainCursor.SortDirection, out totalrows);
                foreach (faField fld in MainCursor.Fields)
                    if (fld.View.Visible)
                        _head += fld.View.CaptionShort + ";";
                sw.Write(_head + "\n");

                foreach (DataRow r in dt.Rows)
                {
                    _body = "";
                    foreach (faField fld in MainCursor.Fields)
                        if (fld.View.Visible)
                            _body += r[fld.LookUp.Key != "" ? fld.Data.FieldName + "_" + fld.LookUp.Field + "_text" : fld.Data.FieldName].ToString().Replace(";", "") + ";";
                    sw.Write(_body + "\n");
                }
                sw.Close();
                Resp.Clear();
                Resp.Write("Файл успешно сформирован за " + (DateTime.Now - _time).ToString() + "  ");
                faFunc.ToLog(3, "Записей: " + dt.Rows.Count.ToString());
                //все гуд норм загрузку только сделать
                Resp.Write("<a href='/Archive/GetFile.aspx?down=" + _fname + "'>Скачать</a>");
            }
            else if (_id != null && _act != null)
            {
                List<string> list = new List<string>(Cursors.Keys);
                foreach (string k in list)
                {
                    JQGrid jqCursor1 = new JQGrid();
                    PrepareCursorJQGrid(Cursors[k].Alias, out jqCursor1);
                    form.Controls.Add(new LiteralControl("<div style=\"display:none;\">"));
                    form.Controls.Add(jqCursor1);
                    form.Controls.Add(new LiteralControl("</div>"));
                    Cursors[k].ClientID = jqCursor1.ClientID;
                }
                AddEditControls(MainCursor, form);
                string alert = "";
                string res = "";
                switch (RequestPost["oper"])
                {
                    case "save":
                        if (Save(out res))
                            Resp.Redirect(_page.GetRouteUrl(RouteName, new
                            {
                                p_base = BaseName
                            }) + "?id=" + res + "&act=view&saved=ok");
                        else
                            alert = faFunc.Alert(faAlert.Danger, res);
                        break;

                    case "close":
                        Close();
                        Resp.Clear();

                        // Resp.Write("<script type='text/javascript'>window.close();</script>");
                        Resp.Write("<script type='text/javascript'>window.location=location.href.slice(0,location.href.indexOf('\\?'));</script>");
                        Resp.End();
                        break;

                    #region Next

                    case "next":
                        Close();
                        Resp.Clear();
                        if (_id != "" && HttpContext.Current.Session[MainCursor.SesCurName + "_queue"] != null)
                        {
                            string[] a = (string[])HttpContext.Current.Session[MainCursor.SesCurName + "_queue"];
                            int idx = Array.IndexOf(a, _id) + 1;
                            if (idx != -1 && idx <= a.GetUpperBound(0))
                            {
                                string next_id = a[idx];
                                Resp.Write(
                                "<script type='text/javascript'>" +
                               "var loc = ((location.href.indexOf('\\?')>-1?location.href.slice(0,location.href.indexOf('\\?')):location.href.replace('#','')) + '?id=" + next_id + "&act=view');" +
                               "if (location.href.indexOf('&s=1') + 1) loc=loc+'&s=1';" +
                               "window.location = loc;" +
                               "</script>");
                                Resp.End();
                            }
                            else
                            {
                                Resp.Write(
                                "<script type='text/javascript'>" +
                               "if (location.href.indexOf('&s=1') + 1)" +
                               "{window.location = '/archive/" + BaseName + "/srch';}" +
                               "else window.location = location.href.slice(0, location.href.indexOf('\\?'))" + (RouteName == "department" ? " + 'pre'" : "") +
                                ";</script>");
                                Resp.End();
                            }
                        }
                        break;

                    #endregion Next

                    #region Prev

                    case "prev":
                        Close();
                        Resp.Clear();
                        if (_id != "" && HttpContext.Current.Session[MainCursor.SesCurName + "_queue"] != null)
                        {
                            string[] a = (string[])HttpContext.Current.Session[MainCursor.SesCurName + "_queue"];
                            int idx = Array.IndexOf(a, _id) - 1;
                            if (idx != -1 && idx >= a.GetLowerBound(0))
                            {
                                string next_id = a[idx];
                                Resp.Write(
                                "<script type='text/javascript'>" +
                               "var loc = ((location.href.indexOf('\\?')>-1?location.href.slice(0,location.href.indexOf('\\?')):location.href.replace('#','')) + '?id=" + next_id + "&act=view');" +
                               "if (location.href.indexOf('&s=1') + 1) loc=loc+'&s=1';" +
                               "window.location = loc;" +
                               "</script>");
                                Resp.End();
                            }
                            else
                            {
                                Resp.Write(
                                "<script type='text/javascript'>" +
                               "if (location.href.indexOf('&s=1') + 1)" +
                               "{window.location = '/archive/" + BaseName + "/srch';}" +
                               "else window.location = location.href.slice(0, location.href.indexOf('\\?'))" + (RouteName == "department" ? " + 'pre'" : "") +
                                ";</script>");
                                Resp.End();
                            }
                        }
                        break;

                    #endregion Prev

                    case "delete":
                        if (Del(out res))
                        {
                            Close();
                            Resp.Clear();
                            Resp.Write("<script type='text/javascript'>window.close();</script>");
                            Resp.End();
                        }
                        else
                            alert = faFunc.Alert(faAlert.Danger, res);
                        break;

                    default:
                        break;
                }

                if (RequestGet["saved"] != null && alert == "")
                {
                    alert = faFunc.Alert(faAlert.Saved);
                    JSReadyList.Add("hidealert", "setTimeout('$(\"#alert_success_msg\").parent().hide();', 5000);");
                }

                form.Controls.Add(new LiteralControl(RenderEditForm(alert)));
                JS = System.Text.RegularExpressions.Regex.Replace(GenerateFormJS(), "  +", "");
            }
        }

        protected void ClearData(faCursor cur)
        {
            HttpContext.Current.Session.Contents.Remove(cur.SesCurName + (_id != "" ? "_" + _id : "") + (_mode != "" ? "_" + _mode : ""));
        }

        protected DataTable GetData(faCursor cur, int _offset, int _limit, string _ordercolumn, string _orderdir, out int _totalrow)
        {
            string _n = "";
            string _before = "";
            string _v = "";
            string _sql = "";
            string _from = "";
            string _where = "";
            string _orderby = "";
            string _after = "";
            _totalrow = 0;

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            conn.Open();

            // WHERE
            foreach (faField fld in cur.Fields)
            {
                if (fld.Filter.Enable)
                {
                    switch (fld.Filter.Control)
                    {
                        case faControl.TextBox:
                            if (fld.Filter.Value != "")
                            {
                                if (fld.Data.Table != "")
                                {
                                    _n = fld.Data.Table + "_" + fld.Data.FieldName;
                                    _before += "CREATE TABLE #child_" + _n + " (child int);";
                                    _after += "IF OBJECT_ID(N'tempdb..#child_" + _n + " ', N'U') IS NOT NULL drop table #child_" + _n + ";";
                                    _before += "INSERT #child_" + _n + " SELECT a." + fld.Data.RefField + " FROM " + fld.Data.Table + " a WHERE a.del=0 " + (fld.Data.Where != "" && fld.Data.Where != "main=1" ? " AND a." + fld.Data.Where : "");

                                    _before += " AND a.[" + fld.Data.FieldName + "]" + (fld.Filter.Condition == "*" || fld.Filter.Condition == "" ? " LIKE '%" + fld.Filter.Value + "%'" : " = '" + fld.Filter.Value + "'");
                                    _before += ";";
                                    _where += " AND (a.[id] in (SELECT child FROM #child_" + _n + "))";
                                }
                                else
                                {
                                    _where += " AND a.[" + fld.Data.FieldName + "]" + (fld.Filter.Condition == "*" || fld.Filter.Condition == "" ? " LIKE '%" + fld.Filter.Value + "%'" : " = '" + fld.Filter.Value + "'");
                                }
                            }
                            break;

                        case faControl.TextBoxFullSearch:
                            if (fld.Filter.Value != "")
                                _where += " AND contains (a.[" + fld.Data.FieldName + "],'\"" + fld.Filter.Value + "\"')";
                            break;

                        case faControl.TextBoxInteger:
                            if (fld.Filter.Value != "")
                                _where += " AND a.[" + fld.Data.FieldName + "]" + (fld.Filter.Condition != "" ? fld.Filter.Condition : "=") + fld.Filter.Value;
                            break;

                        case faControl.TextBoxNumber:
                            if (fld.Filter.Value != "" || fld.Filter.Value2 != "")
                            {
                                _where += (fld.Filter.Value != "" ? " AND a.[" + fld.Data.FieldName + "]" + ">= '" + fld.Filter.Value + "'" : "");
                                _where += (fld.Filter.Value2 != "" ? " AND a.[" + fld.Data.FieldName + "]" + "<= '" + fld.Filter.Value2 + "'" : "");
                            }
                            break;

                        case faControl.DropDown:
                        case faControl.AutoComplete:
                            if (fld.Filter.Value != "0" && fld.Filter.Value != "")
                                if (fld.Data.Table == "")
                                    _where += " AND a.[" + fld.Data.FieldName + "]=" + fld.Filter.Value;
                                else
                                {
                                    _from = " Left Join [" + fld.Data.Table + "] b On b.[" + fld.Data.RefField + "] = a.[id] AND b.del=0 " + (fld.Data.Where != "" ? " AND b." + fld.Data.Where : "");
                                    _where += " AND b.[" + fld.Data.FieldName + "]=" + fld.Filter.Value;
                                }
                            break;

                        case faControl.TreeGrid:
                            _n = "_" + fld.LookUp.Table + "_" + fld.Data.FieldName;
                            _v = (fld.Filter.Value != "0" && fld.Filter.Value != "") ? fld.Filter.Value : (fld.Filter.DefaultValue != "" ? fld.Filter.DefaultValue : "");
                            if (_v != "")
                            {
                                _before += "CREATE TABLE #children" + _n + " (child int);";
                                _after += "IF OBJECT_ID(N'tempdb..#children" + _n + " ', N'U') IS NOT NULL drop table #children" + _n + ";";
                                _before += "INSERT #children" + _n + " VALUES " + _v + ";";
                                //else _before += "INSERT INTO #children" + _n + "(child) SELECT id FROM " + fld.LookUp.Table + " WHERE id_parent=0;";
                                _before += "WHILE @@ROWCOUNT > 0 BEGIN ";
                                _before += "INSERT #children" + _n + " SELECT a.id FROM " + fld.LookUp.Table + " a JOIN #children" + _n + " c ON a.id_parent = c.child ";
                                _before += "WHERE NOT EXISTS(SELECT 1 FROM #children" + _n + " WHERE child = a.id) END;";
                                _where += " AND (a.[" + fld.Data.FieldName + "] in (SELECT child FROM #children" + _n + "))";
                            }
                            break;

                        case faControl.DatePicker:
                            if (fld.Filter.Value != "" || fld.Filter.Value2 != "")
                            {
                                if (fld.Data.Table != "")
                                {
                                    _n = fld.Data.Table + "_" + fld.Data.FieldName;

                                    _before += "CREATE TABLE #child_" + _n + " (child int);";
                                    _after += "IF OBJECT_ID(N'tempdb..#child_" + _n + " ', N'U') IS NOT NULL drop table #child_" + _n + ";";
                                    _before += "INSERT #child_" + _n + " SELECT a." + fld.Data.RefField + " FROM " + fld.Data.Table + " a WHERE a.del=0 ";

                                    _before += (fld.Filter.Value != "" ? " AND a.[" + fld.Data.FieldName + "]" + ">= CONVERT(DATETIME,'" + fld.Filter.Value + " 00:00',104)" : "");
                                    _before += (fld.Filter.Value2 != "" ? " AND a.[" + fld.Data.FieldName + "]" + "<= CONVERT(DATETIME,'" + fld.Filter.Value2 + " 23:59',104)" : "");
                                    _before += ";";
                                    _where += " AND (a.[id] in (SELECT child FROM #child_" + _n + "))";
                                }
                                else
                                {
                                    _where += (fld.Filter.Value != "" ? " AND a.[" + fld.Data.FieldName + "]" + ">= CONVERT(DATETIME,'" + fld.Filter.Value + " 00:00',104)" : "");
                                    _where += (fld.Filter.Value2 != "" ? " AND a.[" + fld.Data.FieldName + "]" + "<= CONVERT(DATETIME,'" + fld.Filter.Value2 + " 23:59',104)" : "");
                                }
                            }
                            break;

                        case faControl.DateTimePicker:
                            if (fld.Filter.Value != "" || fld.Filter.Value2 != "")
                            {
                                _where += (fld.Filter.Value != "" ? " AND a.[" + fld.Data.FieldName + "]" + ">= CONVERT(DATETIME,'" + fld.Filter.Value + "',104)" : "");
                                _where += (fld.Filter.Value2 != "" ? " AND a.[" + fld.Data.FieldName + "]" + "<= CONVERT(DATETIME,'" + fld.Filter.Value2 + "',104)" : "");
                            }
                            break;

                        default:
                            break;
                    }
                }
                _where += (fld.Data.Table == "" && fld.Data.Where != "" ? " AND (a." + fld.Data.Where + ")" : "");
            }
            //if (cur.Alias == "_complectnew")
            //{
            //    bool _mix = (HttpContext.Current.Session["common_access_complect_mix"] ?? "").ToString() == "1";
            //    bool _bank = (HttpContext.Current.Session["common_access_complect_bank"] ?? "").ToString() == "1";
            //    if (_mix && !_bank) _where += " AND a.[id_doctype_complect]=1 ";
            //    else if (!_mix && !_bank) _where += " AND (a.[id_creator]=1 OR a.[id_perf]=1) ";
            //}
            _before += "CREATE TABLE #tempo(row_num int identity(1, 1) NOT NULL, id int  NOT NULL);";
            _after += "IF OBJECT_ID(N'tempdb..#tempo', N'U') IS NOT NULL drop table #tempo;";
            _sql += "INSERT INTO #tempo (id) ";
            _sql += "   SELECT a.id ";//" + (_offset + _limit).ToString() + "
            _sql += "   FROM (SELECT a.* FROM [" + cur.Alias + "] a " + _from + " WHERE a.del=0 " + _where + " ) a ";

            foreach (faField fld in cur.Fields)
            {
                if (fld.Data.FieldName + "_" + fld.LookUp.Field + "_text" == _ordercolumn && fld.LookUp.Key != "")
                {
                    if (fld.Data.Table != "")
                        _orderby = " Left Join [" + fld.Data.Table + "] c On c.[" + fld.Data.RefField + "] = a.[id] AND c.del=0 " + (fld.Data.Where != "" ? " AND c." + fld.Data.Where : "") +
                            " Left Join [" + fld.LookUp.Table + "] b	On	c.[" + fld.Data.FieldName + "]=b.[" + fld.LookUp.Key + "] ORDER BY b.[" + fld.LookUp.Field + "] " + _orderdir;
                    else
                        _orderby = " Left Join [" + fld.LookUp.Table + "] b	On	a.[" + fld.Data.FieldName + "]=b.[" + fld.LookUp.Key + "] ORDER BY b.[" + fld.LookUp.Field + "] " + _orderdir;
                }

                if (fld.Data.Table != "" && fld.Data.FieldName == _ordercolumn)
                {
                    _orderby = " Join [" + fld.Data.Table + "] b On	a.[id]=b.[" + fld.Data.RefField + "] AND b.del=0 " + (fld.Data.Where != "" ? " AND b." + fld.Data.Where : "") + " ORDER BY b.[" + fld.Data.FieldName + "] " + _orderdir;
                }
            }
            _sql += (_orderby != "" ? _orderby : (_ordercolumn == "ver" ? " ORDER BY a.[id_archive] " + _orderdir + ",a.[nn] " + _orderdir : " ORDER BY a.[" + _ordercolumn + "] " + _orderdir)) + " Offset " + _offset + " Row Fetch Next " + _limit + " Rows Only;";

            _sql += "declare @tr int;SELECT @tr=count(*) FROM [" + cur.Alias + "] a " + _from + " WHERE a.del=0 " + _where + ";";

            _sql += "SELECT @tr as totalrows,";
            foreach (faField fld in cur.Fields)
            {
                if (fld.Data.FieldCalc != "")
                    _sql += fld.Data.FieldCalc + " as  [" + fld.Data.FieldName + "], ";
                if (!fld.LookUp.Again && fld.Data.FieldCalc == "")
                    _sql += (fld.Data.Table == "" ? "a" : fld.Data.Table) + ".[" + fld.Data.FieldName + "] " + ", ";
                if (fld.LookUp.Key != "")
                    _sql += (fld.LookUp.TableAlias != "" ? fld.LookUp.TableAlias : fld.LookUp.Table) + ".[" + fld.LookUp.Field + "] as " + fld.Data.FieldName + "_" + fld.LookUp.Field + "_text, ";
            }
            _sql += "a.del ";
            _sql += "FROM [" + cur.Alias + "]	a ";
            foreach (faField fld in cur.Fields)
            {
                // Справочники
                if (fld.LookUp.Key != "" && !fld.LookUp.Again)
                    _sql += "LEFT JOIN " + fld.LookUp.Table + (fld.LookUp.TableAlias != "" ? " as " + fld.LookUp.TableAlias : "") +
                        " ON " + (fld.Data.Table == "" ? "a" : fld.Data.Table) + ".[" + fld.Data.FieldName + "] = " + (fld.LookUp.TableAlias != "" ? fld.LookUp.TableAlias : fld.LookUp.Table) +
                        ".[" + fld.LookUp.Key + "] AND " + (fld.LookUp.TableAlias != "" ? fld.LookUp.TableAlias : fld.LookUp.Table) + ".del=0 ";
                // Дочерние
                if (fld.Data.Table != "" && !fld.Data.Again)
                    _sql += "LEFT JOIN " + fld.Data.Table + " ON a.[id] = " + fld.Data.Table + ".[" + fld.Data.RefField + "] AND " +
                        fld.Data.Table + ".del=0 " + (fld.Data.Where != "" ? " AND " + fld.Data.Table + "." + fld.Data.Where : "");
            }
            _sql += " WHERE a.id in (SELECT id FROM #tempo) ";

            _sql = _before + _sql + _after;
            HttpContext.Current.Session["last_sql"] = _sql;
            SqlCommand cmd = new SqlCommand(_sql, conn);
            cmd.CommandTimeout = 600;
            //stopWatch.Start();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            sqlDataAdapter.Fill(dt);
            //stopWatch.Stop();
            //_elapsed=stopWatch.ElapsedMilliseconds;
            // cmd.CommandText = _sqlcount;
            // _totalrow = (int)cmd.ExecuteScalar();
            //
            if (dt.Rows.Count > 0)
                _totalrow = (int)dt.Rows[0][0];
            dt.Columns.Add("status", typeof(byte));
            dt.Columns["status"].ReadOnly = false;
            dt.Columns["status"].DefaultValue = 0;
            foreach (DataRow r in dt.Rows)
                r["status"] = 0;
            dt.AcceptChanges();
            //
            conn.Close();
            dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };
            return dt;
        }

        protected DataTable GetDataList(faCursor cur)
        {
            string _s = cur.SesCurName + (_id != "" ? "_" + _id : "") + (_mode != "" ? "_" + _mode : "");
            MainCursor.SelectedRow = _id;
            if (HttpContext.Current.Session[_s] == null)
            {
                int _tr = 0;
                DataTable dt = GetData(cur, 0, 10000, "id", "Asc", out _tr);
                if ((_mode == "complect" || _mode == "complectnew") && cur.SesCurName == BaseName + "_docversion_cursor")
                {
                    dt.Columns.Add("from");
                    DataRow newrow = dt.NewRow();
                    newrow["id"] = -1;
                    newrow["main"] = 1;
                    newrow["status"] = 1;
                    newrow["id_status"] = 0;
                    newrow["barcode"] = RequestGet["b"] ?? "";
                    newrow["file"] = RequestGet["f"] ?? "";
                    newrow["from"] = _from;
                    newrow["id_source"] = 2;
                    newrow["id_source_name_text"] = "Бумажный экземпляр";

                    dt.Rows.InsertAt(newrow, 0);
                }

                // Добавляем столбец с базой
                if (cur.SesCurName == "_complectnew_list_cursor")
                {
                    SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                    conn.Open();
                    string cb = "";
                    SqlCommand cmd = new SqlCommand("", conn);
                    if (_id != "0")
                    {
                        cmd.CommandText = "SELECT a.id_base FROM [dbo].[_complectnew] as a where a.id=@id and a.del=0";
                        cmd.Parameters.AddWithValue("id", _id);
                        int _id_base = (int)cmd.ExecuteScalar();
                        cb = faFunc.GetBaseNameById(_id_base);
                        foreach (DataRow row in dt.Rows) row["id_base"] = cb;

                        #region Запретить открывать скрытые проведенные документы

                        //if (HttpContext.Current.Session[cb + "_access_archive_hidden"] == null)
                        //{
                        //    string _where = "0";
                        //    foreach (DataRow r in dt.Rows) _where += "," + r["id_archive"].ToString();

                        //    cmd.CommandText = "SELECT a.id FROM [dbo].[" + cb + "_archive] as a where a.del=0 and a.hidden=2 and a.id in (" + _where + ")";
                        //    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                        //    DataTable dt_h = new DataTable();
                        //    sqlDataAdapter.Fill(dt_h);
                        //    if (dt_h.Rows.Count > 0)
                        //    {
                        //        foreach (DataRow r1 in dt.Rows)
                        //            foreach (DataRow r2 in dt_h.Rows)
                        //                if (r1["id_archive"].ToString() == r2["id"].ToString())
                        //                    r1["file"] = "ad";
                        //        dt.AcceptChanges();
                        //    }
                        //}

                        #endregion Запретить открывать скрытые проведенные документы
                    }
                    conn.Close();
                }

                HttpContext.Current.Session[_s] = dt;
            }
            return (HttpContext.Current.Session[_s] as DataTable);
        }

        protected DataTable GetDataTable()
        {
            string _key = "tables";
            DataTable dt = new DataTable();
            dt = HttpContext.Current.Cache[_key] as DataTable;
            if (dt == null)
            {
                dt = new DataTable();
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                string sql = "SELECT * FROM [_table]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 600;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
                conn.Close();
                dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };
                HttpContext.Current.Cache.Insert(_key, dt, null, DateTime.Now.AddHours(2), Cache.NoSlidingExpiration);
            }
            return dt;
        }

        protected DataTable GetDataTree(string table, string where)
        {
            string _key = "data_tree_" + table;

            DataTable buf = new DataTable();

            buf = HttpContext.Current.Cache[_key] as DataTable;
            if (buf == null)
            {
                buf = new DataTable();
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                string sql = "SELECT a.* FROM [" + table + "_pre] a ORDER BY a.pos";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 600;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(buf);
                conn.Close();
                buf.PrimaryKey = new DataColumn[] { buf.Columns["id"] };
                HttpContext.Current.Cache.Insert(_key, buf, null, DateTime.Now.AddHours(2), Cache.NoSlidingExpiration);
            }

            DataTable dt = new DataTable();
            dt = buf.Clone();
            if (table == "_doctree")
            {
                string mode = (RequestGet["m"] ?? "").ToString();
                if (Page == faPage.srch || (Page == faPage.select && (mode == "2" || mode == "3")))
                {
                    string[] _p = { "acc", "dog", "ord", "oth", "empl", "ohs", "tech", "bank", "norm" };

                    foreach (string name in _p)//Enum.GetNames(typeof(faPage))
                        if (HttpContext.Current.Session[BaseName + "_access_archive_" + name + "_view"] != null || HttpContext.Current.Session[BaseName + "_access_archive_" + name + "_edit"] != null)
                            foreach (DataRow row in buf.Rows) if ((int)row["top_parent"] == (int)faFunc.GetPageType(name))
                                    dt.ImportRow(row);
                }
                else if (Page == faPage.select)
                {
                    foreach (DataRow row in buf.Rows) if ((int)row["top_parent"] == 15) dt.ImportRow(row);
                }
                else if (Page != faPage.none)
                {
                    foreach (DataRow row in buf.Rows) if ((int)row["top_parent"] == (int)Page)
                            dt.ImportRow(row);
                }
            }
            else
                foreach (DataRow row in buf.Rows)
                    dt.ImportRow(row);

            dt.AcceptChanges();
            if (where != "")
            {
                where = where.ToLower();
                string it = "";
                foreach (DataRow row in dt.Rows)
                    if (row.RowState != DataRowState.Deleted && row["treetext"].ToString().ToLower().Contains(where))
                    {
                        row["tree_expanded"] = true;
                        it = row["tree_parent"].ToString();
                        for (int i = 0; i < 5; i++)
                            foreach (DataRow r1 in dt.Rows)
                                if (r1.RowState != DataRowState.Deleted && r1["id"].ToString() == it)
                                {
                                    r1["tree_expanded"] = true;
                                    it = r1["tree_parent"].ToString();
                                }
                    }
                foreach (DataRow row in dt.Rows) if (row.RowState != DataRowState.Deleted && !(bool)row["tree_expanded"])
                        row.Delete();
            }
            dt.AcceptChanges();
            return dt;
        }

        protected DataTable GetLookUpList(faField f)
        {
            string _s = "look_up_list_" + f.Data.FieldName;
            if (HttpContext.Current.Session[_s] == null)
            {
                string sql = "";
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                sql = "SELECT ";
                sql += "[" + f.LookUp.Key + "],[" + f.LookUp.Field + "] as name FROM ";
                sql += "[" + f.LookUp.Table + "] WHERE del=0 " + (f.LookUp.Where != "" ? " AND " + f.LookUp.Where : "") + " ORDER BY [" + f.LookUp.Field + "]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
                conn.Close();
                dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };
                return dt;
            }
            return (HttpContext.Current.Session[_s] as DataTable);
        }

        #endregion COMMON

        #region LIST

        public void AddViewControls(faCursor cur, Control owner)
        {
            JQGrid jqGrid;
            for (int i = 0; i < cur.FieldCount; i++)
            {
                if (cur.Fields[i].Filter.Enable)
                {
                    if (cur.Fields[i].Filter.Control == faControl.TreeGrid)
                    {
                        string _js = "";
                        jqGrid = new JQGrid();
                        jqGrid.ID = "jqGrid" + cur.Fields[i].LookUp.Table;
                        jqGrid.Width = 800;
                        jqGrid.Height = 420;
                        JQGridColumn _c;
                        _c = new JQGridColumn();
                        _c.DataField = "id";
                        _c.Visible = false;
                        jqGrid.Columns.Add(_c);
                        _c = new JQGridColumn();
                        _c.DataField = "treetext";
                        _c.HeaderText = "Наименование";
                        _c.Width = 550;
                        _c.Sortable = false;
                        jqGrid.Columns.Add(_c);
                        jqGrid.ToolBarSettings.ShowAddButton = false;
                        jqGrid.ToolBarSettings.ShowDeleteButton = false;
                        jqGrid.ToolBarSettings.ShowEditButton = false;
                        jqGrid.ToolBarSettings.ShowRefreshButton = false;
                        jqGrid.ClientSideEvents.RowDoubleClick = "tree_" + cur.Fields[i].LookUp.Table + "_click";
                        jqGrid.TreeGridSettings.Enabled = true;

                        owner.Controls.Add(new LiteralControl("<div class=\"modal_form\" id=\"form" + cur.Fields[i].LookUp.Table + "_tree\" title=\"Выбор\" >"));
                        owner.Controls.Add(new LiteralControl("<input name=\"texttree" + cur.Fields[i].LookUp.Table + "\" type=\"text\" id=\"texttree" + cur.Fields[i].LookUp.Table +
                            "\" placeholder=\"Поиск\" onkeydown=\"if(event.keyCode==13){SetTreeText_" + cur.Fields[i].LookUp.Table + "($('#texttree" + cur.Fields[i].LookUp.Table +
                            "').val());}\" class=\"form-control\" style=\"width: 98%; margin-bottom: 5px;\" value = '" + (HttpContext.Current.Session["texttree_" + cur.Fields[i].LookUp.Table] ?? "").ToString() + "'>"));
                        owner.Controls.Add(jqGrid);
                        owner.Controls.Add(new LiteralControl("</div>"));

                        JSFunctionList["SetTreeText_" + cur.Fields[i].LookUp.Table + "(texttree)"] =
                            "$.ajax({url: '/ajax/setses.aspx?key=texttree_" + cur.Fields[i].LookUp.Table + "&value=' + escape(texttree),type: 'POST',success: function (html) { jQuery('#" + jqGrid.ClientID + "').trigger('reloadGrid'); }});";

                        _js += "var grid = jQuery('#" + jqGrid.ClientID + "');";
                        _js += "var row = grid.getRowData(id);";
                        _js += "$('#cph_" + cur.Fields[i].LookUp.Table + cur.Fields[i].Data.FieldName + "').val(row.treetext);";
                        _js += "$('#" + cur.Fields[i].LookUp.Table + cur.Fields[i].Data.FieldName + "').val('('+row.id+')');";
                        _js += "cls(\"#clear_" + cur.Fields[i].LookUp.Table + cur.Fields[i].Data.FieldName + "\");";
                        _js += "$('#form" + cur.Fields[i].LookUp.Table + "_tree').dialog('close');";
                        _js += "return false;";

                        this.JSFunctionList["tree_" + cur.Fields[i].LookUp.Table + "_click(id)"] = _js;

                        string _t = cur.Fields[i].LookUp.Table;
                        jqGrid.DataRequesting += (s, e) =>
                        {
                            (s as JQGrid).DataSource = GetDataTree(_t, (HttpContext.Current.Session["texttree_" + _t] ?? "").ToString().Trim());
                            (s as JQGrid).DataBind();
                        };
                    }
                }
            }
        }

        public string GenerateListJS(JQGrid jqGrid)
        {
            faCursor f = MainCursor;
            string _n = "";
            string _ret = "<script type='text/javascript'>";
            // window resize
            _ret += "$(window).bind('resize', function () {var grid = jQuery('#" + jqGrid.ClientID + "');";
            _ret += "grid.setGridWidth($('.container-fluid').width()-8);var h = $(window).height() -($('#panel_hide').is(':visible')?$('#panel_hide').height():0)-($('#panel_search').is(':visible')?$('#panel_search').height():0) - 120;grid.setGridHeight(h);grid.setGridWidth($('.container-fluid').width()-8);}).trigger('resize');\n";
            // window ready
            _ret += "jQuery(document).ready(function(){";
            _ret += "";
            List<string> list = new List<string>(JSReadyList.Keys);
            foreach (string k in list)
                _ret += JSReadyList[k];

            if (ShowFilterPanel)
            {
                for (int i = 0; i < f.FieldCount; i++)
                {
                    if (f.Fields[i].Filter.Enable)
                    {
                        _n = f.Fields[i].LookUp.Table + f.Fields[i].Data.FieldName;
                        switch (f.Fields[i].Filter.Control)
                        {
                            case faControl.TextBox:
                            case faControl.TextBoxFullSearch:
                            case faControl.TextBoxInteger:
                                _ret += "if ($('#" + _n + "').val() != '') cls('#clear_" + _n + "');";
                                break;

                            case faControl.TextBoxNumber:
                                _ret += "if ($('#cph_" + _n + "_begin').val() != '' || $('#cph_" + _n + "_end').val() != '') cls('#clear_" + _n + "');";
                                break;

                            case faControl.DropDown:
                                _ret += "if ($('#" + _n + "').val() != '0') cls('#clear_" + _n + "');";
                                _ret += "$('#cph_" + _n + "').autocomplete({";
                                _ret += "       source: '/ajax/dd.aspx?table=" + f.Fields[i].LookUp.Table + "',";
                                _ret += "       minLength: 1,";
                                _ret += "       delay: 10,";
                                _ret += "       select: function (event, ui) {";
                                _ret += "           $(\"#" + _n + "\").val(ui.item." + f.Fields[i].LookUp.Key + ");";
                                _ret += "           $(\"#cph_" + _n + "\").val(ui.item.name);";
                                _ret += "           if ($('#" + _n + "').val()!='0')cls('#clear_" + _n + "'); else cld('#clear_" + _n + "');";
                                _ret += "           return false;";
                                _ret += "       }";
                                _ret += "});";
                                break;

                            case faControl.AutoComplete:
                                _ret += "if ($('#" + _n + "').val() != '0') cls('#clear_" + _n + "');";
                                _ret += "$('#cph_" + _n + "').autocomplete({";
                                _ret += "       source: '/ajax/ac.aspx?table=" + f.Fields[i].LookUp.Table + "',";
                                _ret += "       minLength: 1,";
                                _ret += "       delay: 500,";
                                _ret += "       select: function (event, ui) {";
                                _ret += "           $(\"#" + _n + "\").val(ui.item." + f.Fields[i].LookUp.Key + ");";
                                _ret += "           $(\"#cph_" + _n + "\").val(ui.item.name);";
                                _ret += "           if ($('#" + _n + "').val()!='0')cls('#clear_" + _n + "'); else cld('#clear_" + _n + "');";
                                _ret += "           return false;";
                                _ret += "       }";
                                _ret += "});";
                                break;

                            case faControl.DatePicker:
                                _ret += "if ($('#cph_" + _n + "_begin').val() != '' || $('#cph_" + _n + "_end').val() != '') cls('#clear_" + _n + "');";
                                _ret += "$('#cph_" + _n + "_begin').datepicker({changeMonth: true,changeYear: true});";
                                _ret += "$('#cph_" + _n + "_end').datepicker({changeMonth: true,changeYear: true});";
                                _ret += "$('#cph_" + _n + "_begin').mask('99.99.9999',{placeholder:'дд.мм.гггг'});";
                                _ret += "$('#cph_" + _n + "_end').mask('99.99.9999',{placeholder:'дд.мм.гггг'});";
                                break;

                            case faControl.DateTimePicker:
                                _ret += "if ($('#cph_" + _n + "_begin').val() != '' || $('#cph_" + _n + "_end').val() != '') cls('#clear_" + _n + "');";
                                _ret += "$('#cph_" + _n + "_begin').datetimepicker();";
                                _ret += "$('#cph_" + _n + "_end').datetimepicker();";
                                _ret += "$('#cph_" + _n + "_begin').mask('99.99.9999 99:99',{placeholder:'дд.мм.гггг мм:сс'});";
                                _ret += "$('#cph_" + _n + "_end').mask('99.99.9999 99:99',{placeholder:'дд.мм.гггг мм:сс'});";
                                break;

                            case faControl.TreeGrid:
                                _ret += "if ($('#" + _n + "').val() != '0') cls('#clear_" + _n + "');";
                                _ret += "$('#form" + f.Fields[i].LookUp.Table + "_tree').dialog({width: 830,height: 'auto', resizable: false, autoOpen: false, modal: true, closeOnEscape: true});";
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            _ret += "$(window).resize();";
            _ret += "});";
            // _ret += "setTimeout('$(window).resize();', 1000);";

            if (ShowFilterPanel)
            {
                _ret += "function hide_search_panel() {$(\"#panel_search\").hide(500, function(){$(\"#panel_hide\").show();$(window).resize();});}";
                _ret += "function show_search_panel() {$(\"body\").css({\"overflow\":\"hidden\"});$(\"#panel_search\").show(500, function(){$(\"#panel_hide\").hide();$(window).resize();});}";

                _ret += "function cls(selector) {";
                _ret += "$(selector).removeClass('btn-default');";
                _ret += "$(selector).addClass('btn-success');";
                _ret += "$(selector).attr('onmouseover','$(this).text(\"Очистить\")');";
                _ret += "$(selector).attr('onmouseout',\"$(this).text('\" + $(selector).text().trim() + \"')\");}";

                _ret += "function cld(selector) {";
                _ret += "$(selector).removeClass('btn-success');";
                _ret += "$(selector).addClass('btn-default');";
                _ret += "$(selector).attr('onmouseover','');}";
            }

            list = new List<string>(JSFunctionList.Keys);
            foreach (string k in list)
            {
                _ret += "function " + k + "{" + JSFunctionList[k] + "}";
            }
            return _ret + "</script>";
        }

        public void PrepareFilterValues()
        {
            string _str = "", _begin = "", _end = "";
            int _int = 0;
            string _n = "";
            decimal _num = 0;
            DateTime _datetime;
            faCursor cur = MainCursor;
            if (ShowFilterPanel && RequestPost["cph_btn_apply_filter"] != null)
            {
                foreach (faField fld in cur.Fields)
                {
                    if (fld.Filter.Enable)
                    {
                        _n = fld.LookUp.Table + fld.Data.FieldName;
                        fld.Filter.Value = fld.Filter.Value2 = fld.Filter.Condition = fld.Filter.Text = "";
                        switch (fld.Filter.Control)
                        {
                            case faControl.TextBox:
                            case faControl.TextBoxFullSearch:
                                _str = (RequestPost["cph_" + _n] ?? "").Trim().Replace("'", "");
                                if (fld.Edit.Max > 0 && _str.Length > fld.Edit.Max)
                                    _str = _str.Substring(0, fld.Edit.Max);
                                if (_str.Length > 0)
                                {
                                    fld.Filter.Value = _str;
                                    _str = (RequestPost["cph_" + _n + "_cond"] ?? "*").Trim();
                                    fld.Filter.Condition = cond_str.Contains(_str) ? _str : "";
                                }
                                break;

                            case faControl.TextBoxInteger:
                                if (int.TryParse((RequestPost["cph_" + _n] ?? "").Trim(), out _int))
                                {
                                    fld.Filter.Value = _int.ToString();
                                    _str = (RequestPost["cph_" + _n + "_cond"] ?? "=").Trim();
                                    fld.Filter.Condition = cond_int.Contains(_str) ? _str : "";
                                }
                                break;

                            case faControl.TextBoxNumber:
                                Regex r = new Regex("[^-?0-9.]*");
                                _begin = (RequestPost["cph_" + _n + "_begin"] ?? "").Trim().Replace(",", ".");
                                _end = (RequestPost["cph_" + _n + "_end"] ?? "").Trim().Replace(",", ".");
                                _begin = r.Replace(_begin, "");
                                _end = r.Replace(_end, "");
                                if (_begin != "" && Decimal.TryParse(_begin, NumberStyles.Any, CultureInfo.InvariantCulture, out _num))
                                {
                                    fld.Filter.Value = _num.ToString(CultureInfo.InvariantCulture);
                                }
                                if (_end != "" && Decimal.TryParse(_end, NumberStyles.Any, CultureInfo.InvariantCulture, out _num))
                                {
                                    fld.Filter.Value2 = _num.ToString(CultureInfo.InvariantCulture);
                                }
                                break;

                            case faControl.DropDown:
                            case faControl.AutoComplete:
                                if ((RequestPost["cph_" + _n] ?? "").Trim() != "" && int.TryParse((RequestPost[_n] ?? "").Trim(), out _int))
                                {
                                    fld.Filter.Value = _int.ToString();
                                    fld.Filter.Text = RequestPost["cph_" + _n].Trim();
                                }
                                break;

                            case faControl.TreeGrid:
                                if ((RequestPost["cph_" + _n] ?? "").Trim() != "")
                                {// && int.TryParse((RequestPost[_n] ?? "").Trim(), out _int)
                                    fld.Filter.Value = (RequestPost[_n] ?? "").Trim();
                                    fld.Filter.Text = RequestPost["cph_" + _n].Trim();
                                }
                                break;

                            case faControl.DatePicker:
                                _begin = (RequestPost["cph_" + _n + "_begin"] ?? "").Trim();
                                _end = (RequestPost["cph_" + _n + "_end"] ?? "").Trim();

                                if (_begin != "")
                                {
                                    if (DateTime.TryParse(_begin, out _datetime))
                                        fld.Filter.Value = _datetime.ToShortDateString();
                                    else
                                    {
                                        CultureInfo provider = CultureInfo.InvariantCulture;
                                        try
                                        {
                                            DateTime result = DateTime.ParseExact(_begin, "dd.mm.yy", provider);
                                            fld.Filter.Value = result.ToShortDateString();
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                if (_end != "")
                                {
                                    if (DateTime.TryParse(_end, out _datetime))
                                        fld.Filter.Value2 = _datetime.ToShortDateString();
                                    else
                                    {
                                        CultureInfo provider = CultureInfo.InvariantCulture;
                                        try
                                        {
                                            DateTime result = DateTime.ParseExact(_end, "dd.mm.yy", provider);
                                            fld.Filter.Value2 = result.ToShortDateString();
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                break;

                            case faControl.DateTimePicker:
                                _begin = (RequestPost["cph_" + _n + "_begin"] ?? "").Trim();
                                _end = (RequestPost["cph_" + _n + "_end"] ?? "").Trim();

                                if (_begin != "")
                                {
                                    if (DateTime.TryParse(_begin, out _datetime))
                                        fld.Filter.Value = _datetime.ToString("dd.MM.yyyy HH:mm"); //_datetime.ToShortDateString() + " " + _datetime.ToShortTimeString();
                                    //else Нахуя? есть же маска ввода
                                    //{
                                    //    CultureInfo provider = CultureInfo.InvariantCulture;
                                    //    try
                                    //    {
                                    //        DateTime result = DateTime.ParseExact(_begin, "dd.mm.yy", provider);
                                    //        fld.Filter.Value = result.ToShortDateString() + " " + _datetime.ToShortTimeString();
                                    //    }
                                    //    catch
                                    //    {
                                    //    }
                                    //}
                                }
                                if (_end != "")
                                {
                                    if (DateTime.TryParse(_end, out _datetime))
                                        fld.Filter.Value2 = _datetime.ToString("dd.MM.yyyy HH:mm");
                                    //else
                                    //{
                                    //    CultureInfo provider = CultureInfo.InvariantCulture;
                                    //    try
                                    //    {
                                    //        DateTime result = DateTime.ParseExact(_end, "dd.mm.yy", provider);
                                    //        fld.Filter.Value2 = result.ToShortDateString() + " " + _datetime.ToShortTimeString();
                                    //    }
                                    //    catch
                                    //    {
                                    //    }
                                    //}
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
                HttpContext.Current.Session.Remove(cur.SesCurName);
            }
            else if (RequestPost["cph_btn_clear_filter"] != null)
            {
                foreach (faField fld in cur.Fields)
                    fld.Filter.Value = fld.Filter.Value2 = fld.Filter.Condition = fld.Filter.Text = "";
                HttpContext.Current.Session.Remove(cur.SesCurName);
            }
            //else if (Page == faPage.select) {
            //    foreach (faField fld in cur.Fields)
            //        if (fld.Data.FieldName == "id_frm_contr") {
            //            fld.Filter.Value = (RequestGet["id_frm_contr"] ?? "").ToString();
            //            fld.Filter.Text = (RequestGet["id_frm_contr_t"] ?? "").ToString();
            //        }
            //}
            if (!String.IsNullOrEmpty(RequestGet["sidx"]) && !String.IsNullOrEmpty(RequestGet["sidx"]))
            {
                cur.SortColumn = RequestGet["sidx"];
                cur.SortDirection = RequestGet["sord"];
            }
        }

        public void PrepareJQGrid(out JQGrid jqGrid)
        {
            //
            faCursor cur = MainCursor;
            jqGrid = new JQGrid();
            jqGrid.ID = "jqGrid_" + cur.Alias;
            jqGrid.AppearanceSettings.ShrinkToFit = false;
            jqGrid.ColumnReordering = true;
            jqGrid.AppearanceSettings.Caption = cur.Caption;
            JSReadyList.Add("title", "document.title = '" + cur.Caption + "';");
            //jqGrid.AppearanceSettings.ShowFooter = true;
            //
            string buf = faFunc.GetUserSetting(cur.Alias + (Page != faPage.none ? "_" + Page : "") + "_page_size");
            jqGrid.PagerSettings.PageSize = cur.PageSize = int.Parse((buf != "" ? buf : "30"));
            //
            jqGrid.PagerSettings.PageSizeOptions = "[10,20,30,50,100,200]";
            jqGrid.SortSettings.InitialSortColumn = cur.SortColumn;
            if (RequestPost["cph_btn_apply_filter"] == null)
            {
                jqGrid.PagerSettings.CurrentPage = cur.CurrentPageNumber;
                jqGrid.SelectedRow = cur.SelectedRow;
            }
            jqGrid.SortSettings.InitialSortDirection = (cur.SortDirection.ToLower() == "desc" ? SortDirection.Desc : SortDirection.Asc);
            //
            JSReadyList.Add("title_button",
                "$('#gbox_cph_" + jqGrid.ID + "').find('.ui-jqgrid-titlebar').append('" +
                "<a href=\"#\" onclick=\"save_column_view();\" title=\"Сохранить текущий вид таблицы\" style=\"margin-right: 5px;float: right;font-size: 15px;\"><span class=\"gi gi-floppy_disk\"></span></a>" +
                "<a href=\"#\" onclick=\"$(\\'#cph_" + jqGrid.ID + "\\').columnChooser();\" title=\"Скрыть/Показать столбцы\" style=\"margin-right: 10px;float: right;font-size: 15px;\"><span class=\"gi gi-eye_close\"></span></a>');");
            JSFunctionList.Add("save_column_view()",
                "var grid=$('#cph_" + jqGrid.ID + "');" +
                "var a=grid.getGridParam('colModel');" +
                "var ret='';for (i=0; i < a.length; i++) if (!a[i].hidden) ret+=a[i].name+'='+a[i].width+',';" +
                "ret=ret.replace(/,$/, '');$.ajax({url: '/ajax/setsetting.aspx?key=" + cur.Alias + (Page != faPage.none ? "_" + Page : "") + "_column_view&value=' + escape(ret),type: 'POST',success: function (html) { alert('Вид таблицы сохранен.');}});");

            //
            if (Page == faPage.select)
            {
                JQGridToolBarButton jqBtnSelect = new JQGridToolBarButton();
                jqBtnSelect.OnClick = "ReturnSelected_" + cur.Alias;
                jqBtnSelect.ButtonIcon = "ui-icon-extlink";
                jqBtnSelect.Text = "Выбрать&nbsp;&nbsp;";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnSelect);
                jqGrid.ClientSideEvents.RowDoubleClick = "ReturnSelected_" + cur.Alias;
                JSFunctionList.Add("ReturnSelected_" + cur.Alias + "()",
                    "var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                    "var id = grid.jqGrid('getGridParam', 'selrow');" +
                    "if (id>0) {var row = grid.getRowData(id);window.opener.changeBut" + (RequestGet["m"] ?? "").ToString() + "(id,row.num_doc); self.close();}" +
                    "else alert('Выберите запись');  return false;");
                JSReadyList.Add("navbar.hide", "$('.navbar-right').hide();");
            }

            if (cur.EnableViewButton && Page != faPage.select)
            {
                JQGridToolBarButton jqBtnView = new JQGridToolBarButton();
                jqBtnView.OnClick = "GoToView_" + cur.Alias;
                jqBtnView.ButtonIcon = "ui-icon-document";
                jqBtnView.Text = "Открыть&nbsp;&nbsp;";
                jqBtnView.ToolTip = "Открыть для просмотра или редактирования";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnView);
                JSFunctionList.Add("GoToView_" + cur.Alias + "()",
                    "var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                    "var id = grid.jqGrid('getGridParam', 'selrow');" +
                    "if (id>0) window.location =((location.href.indexOf('\\?')>-1?location.href.slice(0,location.href.indexOf('\\?')):location.href.replace('#','')) + '?id=' + id+ '&act=view');" +
                    "else alert('Выберите запись');  return false;");
                jqGrid.ClientSideEvents.RowDoubleClick = "GoToView_" + cur.Alias;
            }

            if (RouteName == "complectnewlist")
            {
                JQGridToolBarButton jqBtnViewComplect = new JQGridToolBarButton();
                jqBtnViewComplect.OnClick = "GoToViewComplect_" + cur.Alias;
                jqBtnViewComplect.ButtonIcon = "ui-icon-document";
                jqBtnViewComplect.Text = "Комплект&nbsp;&nbsp;";
                jqBtnViewComplect.ToolTip = "Открыть соответствующий записи комплект";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnViewComplect);
                JSFunctionList.Add("GoToViewComplect_" + cur.Alias + "()",
                    "var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                    "var id = grid.jqGrid('getGridParam', 'selrow');" +
                    "if (id>0) {" +
                    "   var row = grid.getRowData(id);" +
                    "   var f = row.id_complectnew;" +
                    "   window.open('" + "/complectnew/?id='+f+'&act=view');" +
                    "}else alert('Выберите запись');  return false;");
                jqGrid.ClientSideEvents.RowDoubleClick = "GoToViewComplect_" + cur.Alias;
            }

            if (cur.EnableAddButton && Page != faPage.select)
            {
                JQGridToolBarButton jqBtnAdd = new JQGridToolBarButton();
                jqBtnAdd.OnClick = "GoToAdd_" + cur.Alias;
                jqBtnAdd.ButtonIcon = "ui-icon-plus";
                jqBtnAdd.Text = "Новая запись&nbsp;&nbsp;";
                jqBtnAdd.ToolTip = "Добавить новую запись";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnAdd);
                JSFunctionList.Add("GoToAdd_" + cur.Alias + "()", "window.location =((location.href.indexOf('\\?')>-1?location.href.slice(0,location.href.indexOf('\\?')):location.href.replace('#','')) + '?id=0&act=add');return false;");
            }
            if (cur.EnableCopyButton && Page != faPage.select)
            {
                JQGridToolBarButton jqBtnCopy = new JQGridToolBarButton();
                jqBtnCopy.OnClick = "GoToCopy_" + cur.Alias;
                jqBtnCopy.ButtonIcon = "ui-icon-copy";
                jqBtnCopy.Text = "Скопировать&nbsp;&nbsp;";
                jqBtnCopy.ToolTip = "Создать новую запись на основе выбранной";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnCopy);
                JSFunctionList.Add("GoToCopy_" + cur.Alias + "()",
                    "var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                    "var id = grid.jqGrid('getGridParam', 'selrow');" +
                    "if (id>0) window.location =((location.href.indexOf('\\?')>-1?location.href.slice(0,location.href.indexOf('\\?')):location.href.replace('#','')) + '?id=0&from=' + id+ '&act=copy');" +
                    "else alert('Выберите запись');  return false;");
            }
            /*if (f.EnableDelButton)
            {
                JQGridToolBarButton jqBtnDel = new JQGridToolBarButton();
                jqBtnDel.OnClick = "GoToDel";
                jqBtnDel.ButtonIcon = "ui-icon-trash";
                jqBtnDel.Text = "Удалить&nbsp;&nbsp;";
                jqBtnDel.ToolTip = "Удалить выделенную запись";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnDel);
             *
             * _ret += "function GoToDel(){";
                _ret += "   var grid = jQuery('#" + jqGrid.ClientID + "');";
                _ret += "   var id = grid.jqGrid('getGridParam', 'selrow');";
                _ret += "   if (id>0) window.open((location.href.indexOf('\\?')>-1?location.href.slice(0,location.href.indexOf('\\?')):location.href.replace('#','')) + '?id=' + id+ '&act=del');";
                _ret += "   else alert('Выберите запись');  return false;}";
            }*/
            if (cur.EnableFileButton)
            {
                JQGridToolBarButton jqBtnFile = new JQGridToolBarButton();
                jqBtnFile.OnClick = "GoToFile_" + cur.Alias;
                jqBtnFile.ButtonIcon = "ui-icon-arrowthickstop-1-s";
                jqBtnFile.Text = "Файл&nbsp;&nbsp;";
                jqBtnFile.ToolTip = "Открыть файл";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnFile);
                JSFunctionList.Add("GoToFile_" + cur.Alias + "()",
                    "var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                    "var id = grid.jqGrid('getGridParam', 'selrow');" +
                    "if (id>0) { " +
                    "   var row = grid.getRowData(id);" +
                    "   var f = row.file;" +
                    "   if (f!='') window.open('/Archive/GetFile.aspx?id='+id +'&b=' + '" + BaseName + "'+'&f=' + f + '&k='+$.md5(id));" +
                    "   else alert('Файл не указан');" +
                    "}" +
                    "else alert('Выберите запись');  return false;");
            }
            //
            // buf = faFunc.GetSetting("double_click_action");
            //if (Page == faPage.select)
            //    jqGrid.ClientSideEvents.RowDoubleClick = "ReturnSelected_" + cur.Alias;
            //else if (cur.EnableFileButton && (buf == "" || buf == "file"))
            //    jqGrid.ClientSideEvents.RowDoubleClick = "GoToFile_" + cur.Alias;
            //else
            //    jqGrid.ClientSideEvents.RowDoubleClick = "GoToView_" + cur.Alias;

            if (cur.EnableExcelButton && Page != faPage.select)
            {
                JQGridToolBarButton jqBtnExport = new JQGridToolBarButton();
                jqBtnExport.OnClick = "GoToExport_" + cur.Alias;
                jqBtnExport.ButtonIcon = "ui-icon-extlink";
                jqBtnExport.Text = "Excel&nbsp;&nbsp;";
                jqBtnExport.ToolTip = "Выгрузить в MS Excel";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnExport);
                JSFunctionList.Add("GoToExport_" + cur.Alias + "()",
                    "var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                    "var rc = grid.jqGrid('getGridParam','records');" +
                    "if (rc>0 && rc<10001) { " +
                    "   window.open((location.href.indexOf('\\?')>-1?location.href.slice(0,location.href.indexOf('\\?')):location.href.replace('#','')) + '?act=export_excel');" +
                    "}" +
                    "else alert('Выгрузить можно минимум 1 и не более 10 000 записей');  return false;");
            }

            if (cur.EnableCSVButton && Page != faPage.select)
            {
                JQGridToolBarButton jqBtnCSV = new JQGridToolBarButton();
                jqBtnCSV.OnClick = "GoToCSV_" + cur.Alias;
                jqBtnCSV.ButtonIcon = "ui-icon-extlink";
                jqBtnCSV.Text = "CSV&nbsp;&nbsp;";
                jqBtnCSV.ToolTip = "Выгрузить в формат CSV";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnCSV);
                JSFunctionList.Add("GoToCSV_" + cur.Alias + "()",
                    "var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                    "var rc = grid.jqGrid('getGridParam','records');" +
                    "if (rc>0) { " +
                    "   window.open((location.href.indexOf('\\?')>-1?location.href.slice(0,location.href.indexOf('\\?')):location.href.replace('#','')) + '?act=export_csv');" +
                    "}" +
                    "else alert('Выгрузить можно минимум 1 запись');  return false;");
            }

            JSReadyList.Add("redcolorcell",
                "grid = $('#cph_" + jqGrid.ID + "');" +
                "grid.bind('jqGridLoadComplete', function(e, data) {" +
                    "var iCol1 = 0," +
                    "   iCol2 = 0," +
                    "  iRow = 0;" +
                    "var cm = grid.jqGrid('getGridParam', 'colModel')," +
                    "   i = 0," +
                    "  l = cm.length;" +
                    "for (; i < l; i++) {" +
                    "  if (cm[i].name === 'important_name_text') {" +
                       "     iCol1 = i;" +
                       " }" +
                       " if (cm[i].name === 'id') {" +
                       "     iCol2 = i; " +
                       " }" +
                    "}" +
                    "var cRows = this.rows.length;" +
                    "var iRow;" +
                    "var row;" +
                    "var className;" +
                    "for (iRow = 0; iRow < cRows; iRow++) {" +
                      "  row = this.rows[iRow];" +
                      "  var x1 = $(row.cells[iCol1]);" +
                      "  var x2 = $(row.cells[iCol2]);" +
                      "  if (x1[0].innerText.trim() == 'Да') {" +
                      "      x2[0].className = 'redcolorcell';" +
                      "  }" +
                    "}" +
                "});");

            JSReadyList.Add("redbgcell",
                "grid = $('#cph_" + jqGrid.ID + "');" +
                "grid.bind('jqGridLoadComplete', function(e, data) {" +
                    "var iCol1 = 0," +
                    "   iCol2 = 0," +
                    "  iRow = 0;" +
                    "var cm = grid.jqGrid('getGridParam', 'colModel')," +
                    "   i = 0," +
                    "  l = cm.length;" +
                    "for (; i < l; i++) {" +
                    "  if (cm[i].name === 'date_reg_overdue') {" +
                       "     iCol1 = i;" +
                       " }" +
                       " if (cm[i].name === 'date_reg') {" +
                       "     iCol2 = i; " +
                       " }" +
                    "}" +
                    "var cRows = this.rows.length;" +
                    "var iRow;" +
                    "var row;" +
                    "var className;" +
                    "for (iRow = 0; iRow < cRows; iRow++) {" +
                      "  row = this.rows[iRow];" +
                      "  var x1 = $(row.cells[iCol1]);" +
                      "  var x2 = $(row.cells[iCol2]);" +
                      "  if (x1[0].innerText.trim() == '1') {" +
                      "      x2[0].className = 'redbgcell';" +
                      "  }" +
                    "}" +
                "});");

            //jqGrid.RowSelecting += new JQGrid.JQGridRowSelectEventHandler(jqGrid_RowSelecting);
            jqGrid.DataRequesting += new JQGrid.JQGridDataRequestEventHandler(jqGrid_DataRequesting);

            #region CellBinding

            //jqGrid.CellBinding += new JQGrid.JQGridCellBindEventHandler(jqGrid_CellBinding);

            #endregion CellBinding

            JQGridColumn[] _c = null;

            //string fldset = faFunc.GetSetting(cur.Alias + (Page != faPage.none ? "_" + Page : "") + "_column_view");
            //if (fldset != "") {//
            //    string[] Arr = fldset.Split(',');
            //    string[] f;
            //    foreach (string a in Arr) {
            //        f = a.Split('=');
            //        if=
            //        foreach (faField fld in cur.Fields) {
            //            if (f[0] == (fld.LookUp.Key == "" ? fld.LookUp.Table + fld.Data.FieldName : fld.Data.FieldName + "_" + fld.LookUp.Field + "_text")) {
            //                _c[i] = new JQGridColumn();
            //        _c[i].DataField = cur.Fields[i].LookUp.Key == "" ? cur.Fields[i].LookUp.Table + cur.Fields[i].Data.FieldName : cur.Fields[i].Data.FieldName + "_" + cur.Fields[i].LookUp.Field + "_text";
            //        _c[i].Width = cur.Fields[i].View.Width;
            //        _c[i].Visible = cur.Fields[i].View.Visible;
            //        _c[i].HeaderText = cur.Fields[i].View.CaptionShort != "" ? cur.Fields[i].View.CaptionShort : cur.Fields[i].View.Hint;
            //        _c[i].DataFormatString = cur.Fields[i].View.FormatString;
            //        switch (cur.Fields[i].View.TextAlign) {
            //            case "left":
            //                _c[i].TextAlign = Trirand.Web.UI.WebControls.TextAlign.Left;
            //                break;
            //            case "right":
            //                _c[i].TextAlign = Trirand.Web.UI.WebControls.TextAlign.Right;
            //                break;
            //            case "center":
            //                _c[i].TextAlign = Trirand.Web.UI.WebControls.TextAlign.Center;
            //                break;
            //            default:
            //                _c[i].TextAlign = Trirand.Web.UI.WebControls.TextAlign.Left;
            //                break;
            //        }
            //        jqGrid.Columns.Add(_c[i]);

            //            }
            //        }

            //    }
            //}
            //else {
            int i = -1;
            foreach (faField fld in cur.Fields)
            {
                if (fld.Edit.Control != faControl.TextArea)
                {
                    i++;
                    Array.Resize(ref _c, i + 1);
                    _c[i] = new JQGridColumn();
                    _c[i].DataField = fld.LookUp.Key == "" ? fld.LookUp.Table + fld.Data.FieldName : fld.Data.FieldName + "_" + fld.LookUp.Field + "_text";
                    _c[i].Width = fld.View.Width;
                    _c[i].Visible = fld.View.Visible;
                    _c[i].HeaderText = fld.View.CaptionShort != "" ? fld.View.CaptionShort : fld.View.Hint;
                    _c[i].DataFormatString = fld.View.FormatString;
                    switch (fld.View.TextAlign)
                    {
                        case "left":
                            _c[i].TextAlign = Trirand.Web.UI.WebControls.TextAlign.Left;
                            break;

                        case "right":
                            _c[i].TextAlign = Trirand.Web.UI.WebControls.TextAlign.Right;
                            break;

                        case "center":
                            _c[i].TextAlign = Trirand.Web.UI.WebControls.TextAlign.Center;
                            break;

                        default:
                            _c[i].TextAlign = Trirand.Web.UI.WebControls.TextAlign.Left;
                            break;
                    }
                    //jqGrid.Columns.Add(_c[i]);
                }
            }

            string fldset = faFunc.GetUserSetting(cur.Alias + (Page != faPage.none ? "_" + Page : "") + "_column_view");
            jqGrid.Columns.Add(_c[0]);
            if (fldset != "")
            {
                string[] Arr = fldset.Split(',');
                string[] f;
                foreach (string a in Arr)
                {
                    f = a.Split('=');
                    foreach (JQGridColumn col in _c)
                    {
                        if (f[0] == col.DataField)
                        {
                            col.Width = int.Parse(f[1]);
                            if (!jqGrid.Columns.Contains(col))
                                jqGrid.Columns.Add(col);
                            break;
                        }
                    }
                }
                foreach (JQGridColumn col in _c)
                {
                    if (!jqGrid.Columns.Contains(col))
                    {
                        col.Visible = false;
                        jqGrid.Columns.Add(col);
                    }
                }
            }
            else
                foreach (JQGridColumn col in _c)
                    if (!jqGrid.Columns.Contains(col))
                        jqGrid.Columns.Add(col);
            PrepareFilterValues();
        }

        public string RenderFilterPanel()
        {
            string _ret = "";
            faCursor cur = MainCursor;
            faField fld;
            string n = "";

            if (ShowFilterPanel)
            {
                _ret += "<div id=\"panel_hide\" class=\"panel panel-default\" style=\"margin-bottom: 2px;margin-top: 2px;display: none;\">";
                _ret += "   <div class=\"panel-body\" style=\"padding: 0px;\">";
                _ret += "       <a href=\"#\" onclick=\"show_search_panel();\" title=\"Показать панель фильтров\" style=\"margin-right:3px;float: right;\" >";
                _ret += "           <span class=\"hi hi-chevron-down\"></span>";
                _ret += "       </a>";
                _ret += "   </div>";
                _ret += "</div>";

                _ret += "<div id=\"panel_search\" class=\"panel panel-default\" style=\"margin-bottom: 2px;\">";
                _ret += "   <div class=\"panel-body\" style=\"padding:11px;\">";
                _ret += "       <div class=\"row\" >";
                for (int i = 0; i < cur.FieldCount; i++)
                {
                    fld = cur.Fields[i];
                    if (fld.Filter.Enable)
                    {
                        n = fld.LookUp.Table + fld.Data.FieldName;
                        switch (fld.Filter.Control)
                        {
                            case faControl.TextBox:
                            case faControl.TextBoxFullSearch:
                                _ret += "<div class=\"col-md-3\" style=\"width: 271px;margin-top:3px;margin-bottom: 1px;\">";
                                _ret += "   <div class=\"input-group\">";
                                _ret += "       <div class=\"input-group-btn\">";
                                _ret += "           <button type=\"button\" id=\"clear_" + n + "\" class=\"btn btn-xs btn-default\" ";
                                _ret += "               style=\"width: 100px;\" onclick=\"$('#" + n + "' ).val('');";
                                _ret += "               $('" + n + "_cond' ).val('*');cld('#clear_" + n + "');\" title=\"" + (fld.View.Hint != "" ? fld.View.Hint : fld.View.CaptionShort) + "\">" + (fld.Filter.Caption != "" ? fld.Filter.Caption : fld.View.CaptionShort);
                                _ret += "           </button>";
                                _ret += "           <button type=\"button\" class=\"btn btn-xs btn-default dropdown-toggle\" data-toggle=\"dropdown\">";
                                _ret += "               <span class=\"caret\"></span><span class=\"sr-only\">Toggle Dropdown</span>";
                                _ret += "           </button>";
                                _ret += "           <ul class=\"dropdown-menu\" role=\"menu\">";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#" + n + "_cond' ).val('*');return false;\">Содержит</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#" + n + "_cond' ).val('=');return false;\">Точное соответствие</a></li>";
                                _ret += "               <li class=\"divider\"></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#clear_" + n + "' ).click();return false;\">Очистить</a></li>";
                                _ret += "           </ul>";
                                _ret += "       </div>";
                                _ret += "       <input id=\"" + n + "_cond\" name=\"cph_" + n + "_cond\"  class=\"form-control\" style=\"width: 13px;\"";
                                _ret += "           value=\"" + (fld.Filter.Condition != "" ? fld.Filter.Condition : "*") + "\"/>";
                                _ret += "       <input id=\"" + n + "\" name=\"cph_" + n + "\" class=\"form-control\" style=\"width: 107px;\" ";
                                _ret += "           value=\"" + fld.Filter.Value + "\"";
                                _ret += "           onchange=\"if ($('#" + n + "').val()!='')cls('#clear_" + n + "'); else cld('#clear_" + n + "');\"/>";
                                _ret += "   </div>";
                                _ret += "</div>";
                                break;

                            case faControl.TextBoxInteger:
                                _ret += "<div class=\"col-md-3\" style=\"width: 271px;margin-top:3px;margin-bottom: 1px;\">";
                                _ret += "   <div class=\"input-group\">";
                                _ret += "       <div class=\"input-group-btn\">";
                                _ret += "           <button type=\"button\" id=\"clear_" + n + "\" class=\"btn btn-xs btn-default\" ";
                                _ret += "               style=\"width: 100px;\" onclick=\"$('#" + n + "' ).val('');$('#" + n + "_cond' ).val('=');";
                                _ret += "               cld('#clear_" + n + "');\" title=\"" + (fld.View.Hint != "" ? fld.View.Hint : fld.View.CaptionShort) + "\">" + (fld.Filter.Caption != "" ? fld.Filter.Caption : fld.View.CaptionShort);
                                _ret += "           </button>";
                                _ret += "           <button type=\"button\" class=\"btn btn-xs btn-default dropdown-toggle\" data-toggle=\"dropdown\">";
                                _ret += "               <span class=\"caret\"></span><span class=\"sr-only\">Toggle Dropdown</span>";
                                _ret += "           </button>";
                                _ret += "           <ul class=\"dropdown-menu\" role=\"menu\">";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#" + n + "_cond' ).val('=');return false;\">Равно</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#" + n + "_cond' ).val('!=');return false;\">Не равно</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#" + n + "_cond' ).val('>');return false;\">Больше</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#" + n + "_cond' ).val('<');return false;\">Меньше</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#" + n + "_cond' ).val('>=');return false;\">Больше или равно</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#" + n + "_cond' ).val('<=');return false;\">Меньше или равно</a></li>";
                                _ret += "               <li class=\"divider\"></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#clear_" + n + "' ).click();\">Очистить</a></li>";
                                _ret += "           </ul>";
                                _ret += "       </div>";
                                _ret += "       <input id=\"" + n + "_cond\" name=\"cph_" + n + "_cond\"  class=\"form-control\" style=\"width: 13px;\"";
                                _ret += "           value=\"" + (fld.Filter.Condition != "" ? fld.Filter.Condition : "=") + "\"/>";
                                _ret += "       <input id=\"" + n + "\" name=\"cph_" + n + "\" class=\"form-control\" style=\"width: 107px;\" ";
                                _ret += "           value=\"" + fld.Filter.Value + "\"";
                                _ret += "           onchange=\"if ($('#" + n + "').val()!='')cls('#clear_" + n + "'); else cld('#clear_" + n + "');\"/>";
                                _ret += "   </div>";
                                _ret += "</div>";
                                break;

                            case faControl.TextBoxNumber:
                                _ret += "<div class=\"col-md-3\" style=\"width: 271px;margin-top:3px;margin-bottom: 1px;\">";
                                _ret += "   <div class=\"input-group\">";
                                _ret += "       <div class=\"input-group-btn\">";
                                _ret += "           <button type=\"button\" id=\"clear_" + n + "\" class=\"btn btn-xs btn-default\" ";
                                _ret += "               style=\"width: 100px;\" onclick=\"$('#cph_" + n + "_begin' ).val(''); $('#cph_" + n + "_end' ).val('');cld('#clear_" + n + "');\" title=\"" + (fld.View.Hint != "" ? fld.View.Hint : fld.View.CaptionShort) + "\">";
                                _ret += "               " + (fld.Filter.Caption != "" ? fld.Filter.Caption : fld.View.CaptionShort) + "</button>";
                                _ret += "       </div>";
                                _ret += "       <input id=\"cph_" + n + "_begin\" name=\"cph_" + n + "_begin\" class=\"form-control\" placeholder=\"от\" style=\"width: 69px;\" value=\"" + fld.Filter.Value + "\" ";
                                _ret += "           onchange=\"if ($('#cph_" + n + "_begin').val()!='' || $('#cph_" + n + "_end').val()!='')cls('#clear_" + n + "'); else cld('#clear_" + n + "');\"/>";
                                _ret += "       <input id=\"cph_" + n + "_end\" name=\"cph_" + n + "_end\" class=\"form-control\" placeholder=\"до\" style=\"width: 69px;\" value=\"" + fld.Filter.Value2 + "\" ";
                                _ret += "           onchange=\"if ($('#cph_" + n + "_begin').val()!='' || $('#cph_" + n + "_end').val()!='')cls('#clear_" + n + "'); else cld('#clear_" + n + "');\"/>";
                                _ret += "   </div>";
                                _ret += "</div>";
                                break;

                            case faControl.DropDown:
                            case faControl.AutoComplete:
                                _ret += "<div class=\"col-md-3\" style=\"width: 271px;margin-top:3px;margin-bottom: 1px;\">";
                                _ret += "   <div class=\"input-group\">";
                                _ret += "       <div class=\"input-group-btn\">";
                                _ret += "           <button type=\"button\" id=\"clear_" + n + "\" class=\"btn btn-xs btn-default\"";
                                _ret += "               style=\"width: 100px;\" onclick=\"$('#" + n + "').val('0');$('#cph_" + n + "').val('');";
                                _ret += "               cld('#clear_" + n + "');\" title=\"" + (fld.View.Hint != "" ? fld.View.Hint : fld.View.CaptionShort) + "\">" + (fld.Filter.Caption != "" ? fld.Filter.Caption : fld.View.CaptionShort);
                                _ret += "           </button>";
                                _ret += "       </div>";
                                _ret += "       <input id=\"cph_" + n + "\" name=\"cph_" + n + "\" class=\"form-control\"  style=\"width: 154px;\" ";
                                _ret += "           value=\"" + (fld.Filter.Value != "" && fld.Filter.Value != "0" ? fld.Filter.Text : "") + "\" onclick=\"$('#cph_" + n + "').autocomplete('search',' ');\" ";
                                _ret += "           onchange=\"if ($('#cph_" + n + "').val()!='')cls('#clear_" + n + "'); else cld('#clear_" + n + "');\"/>";
                                _ret += "       <input type=\"hidden\" name=\"" + n + "\" id=\"" + n + "\" value=\"" + (fld.Filter.Value == "" ? "0" : fld.Filter.Value) + "\">";
                                _ret += "   </div>";
                                _ret += "</div>";
                                break;

                            case faControl.TreeGrid:
                                _ret += "<div class=\"col-md-3\" style=\"width: 271px;margin-top:3px;margin-bottom: 1px;\">";
                                _ret += "   <div class=\"input-group\">";
                                _ret += "       <div class=\"input-group-btn\">";
                                _ret += "           <button type=\"button\" id=\"clear_" + n + "\" class=\"btn btn-xs btn-default\"";
                                _ret += "               style=\"width: 100px;\" onclick=\"$('#" + n + "').val('0');$('#cph_" + n + "').val('');";
                                _ret += "               cld('#clear_" + n + "');\" title=\"" + (fld.View.Hint != "" ? fld.View.Hint : fld.View.CaptionShort) + "\">" + (fld.Filter.Caption != "" ? fld.Filter.Caption : fld.View.CaptionShort);
                                _ret += "           </button>";
                                _ret += "       </div>";
                                _ret += "       <input id=\"cph_" + n + "\" name=\"cph_" + n + "\" class=\"form-control\"  style=\"width: 154px;\" ";
                                _ret += "           value=\"" + (fld.Filter.Value != "" && fld.Filter.Value != "0" ? fld.Filter.Text : "") + "\" onclick=\"$('#cph_btn_apply_filter').focus();$('#form" + fld.LookUp.Table + "_tree').dialog('open');$(window).resize();\" ";
                                _ret += "           onchange=\"if ($('#cph_" + n + "').val()!='')cls('#clear_" + n + "'); else cld('#clear_" + n + "');\"/>";
                                _ret += "       <input type=\"hidden\" name=\"" + n + "\" id=\"" + n + "\" value=\"" + (fld.Filter.Value == "" ? "0" : fld.Filter.Value) + "\">";
                                _ret += "   </div>";
                                _ret += "</div>";
                                break;

                            case faControl.DatePicker:
                                _ret += "<div class=\"col-md-3\" style=\"width: 271px;margin-top:3px;margin-bottom: 1px;\">";
                                _ret += "   <div class=\"input-group\">";
                                _ret += "       <div class=\"input-group-btn\">";
                                _ret += "           <button type=\"button\" id=\"clear_" + n + "\" class=\"btn btn-xs btn-default\" ";
                                _ret += "               style=\"width: 100px;\" onclick=\"$('#cph_" + n + "_begin' ).val(''); $('#cph_" + n + "_end' ).val('');cld('#clear_" + n + "');\" title=\"" + (fld.View.Hint != "" ? fld.View.Hint : fld.View.CaptionShort) + "\">";
                                _ret += "               " + (fld.Filter.Caption != "" ? fld.Filter.Caption : fld.View.CaptionShort) + "</button>";
                                _ret += "           <button type=\"button\" class=\"btn btn-xs btn-default dropdown-toggle\" data-toggle=\"dropdown\" >";
                                _ret += "               <span class=\"caret\"></span><span class=\"sr-only\">Toggle Dropdown</span>";
                                _ret += "           </button>";
                                _ret += "           <ul class=\"dropdown-menu\" role=\"menu\">";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#cph_" + n + "_begin').val(formatDate(GetLastMonthBegin()));$('#cph_" + n + "_end').val(formatDate(GetLastMonthEnd()));cls('#clear_" + n + "');return false;\">Прошлый месяц</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#cph_" + n + "_begin').val(formatDate(GetLastWeekBegin()));$('#cph_" + n + "_end').val(formatDate(GetLastWeekEnd()));cls('#clear_" + n + "');return false;\">Прошлая неделя</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#cph_" + n + "_begin').val(formatDate(GetYesterday()));$('#cph_" + n + "_end' ).val(formatDate(GetYesterday()));cls('#clear_" + n + "');return false;\">Вчера</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"var _date = new Date();$('#cph_" + n + "_begin').val(formatDate(_date));$('#cph_" + n + "_end' ).val(formatDate(_date));cls('#clear_" + n + "');return false;\">Сегодня</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#cph_" + n + "_begin').val(formatDate(GetCurrentWeekBegin()));$('#cph_" + n + "_end' ).val(formatDate(GetCurrentWeekEnd()));cls('#clear_" + n + "');return false;\">Текущая неделя</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#cph_" + n + "_begin').val(formatDate(GetCurrentMonthBegin()));$('#cph_" + n + "_end' ).val(formatDate(GetCurrentMonthEnd()));cls('#clear_" + n + "');return false;\">Текущий месяц</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#cph_" + n + "_begin').val(formatDate(GetCurrentYearBegin()));$('#cph_" + n + "_end' ).val(formatDate(GetCurrentYearEnd()));cls('#clear_" + n + "');return false;\">Текущий год</a></li>";
                                _ret += "               <li class=\"divider\"></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#cph_" + n + "_begin' ).val('');$('#cph_" + n + "_end' ).val('');cld('#clear_" + n + "');return false;\">Очистить</a></li>";
                                _ret += "           </ul>";
                                _ret += "       </div>";
                                _ret += "       <input id=\"cph_" + n + "_begin\" name=\"cph_" + n + "_begin\" class=\"form-control\" placeholder=\"с\" style=\"width: 60px;\" value=\"" + fld.Filter.Value + "\" ";
                                _ret += "           onchange=\"if ($('#cph_" + n + "_begin').val()!='' || $('#cph_" + n + "_end').val()!='')cls('#clear_" + n + "'); else cld('#clear_" + n + "');\"/>";
                                _ret += "       <input id=\"cph_" + n + "_end\" name=\"cph_" + n + "_end\" class=\"form-control\" placeholder=\"по\" style=\"width: 60px;\" value=\"" + fld.Filter.Value2 + "\" ";
                                _ret += "           onchange=\"if ($('#cph_" + n + "_begin').val()!='' || $('#cph_" + n + "_end').val()!='')cls('#clear_" + n + "'); else cld('#clear_" + n + "');\"/>";
                                _ret += "   </div>";
                                _ret += "</div>";
                                break;

                            case faControl.DateTimePicker:
                                _ret += "<div class=\"col-md-3\" style=\"width: 271px;margin-top:3px;margin-bottom: 1px;\">";
                                _ret += "   <div class=\"input-group\">";
                                _ret += "       <div class=\"input-group-btn\">";
                                _ret += "           <button type=\"button\" id=\"clear_" + n + "\" class=\"btn btn-xs btn-default\" ";
                                _ret += "               style=\"width: 100px;\" onclick=\"$('#cph_" + n + "_begin' ).val(''); $('#cph_" + n + "_end' ).val('');cld('#clear_" + n + "');\" title=\"" + (fld.View.Hint != "" ? fld.View.Hint : fld.View.CaptionShort) + "\">";
                                _ret += "               " + (fld.Filter.Caption != "" ? fld.Filter.Caption : fld.View.CaptionShort) + "</button>";
                                _ret += "           <button type=\"button\" class=\"btn btn-xs btn-default dropdown-toggle\" data-toggle=\"dropdown\" >";
                                _ret += "               <span class=\"caret\"></span><span class=\"sr-only\">Toggle Dropdown</span>";
                                _ret += "           </button>";
                                _ret += "           <ul class=\"dropdown-menu\" role=\"menu\">";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#cph_" + n + "_begin').val(formatDate(GetLastMonthBegin())+' 00:00');$('#cph_" + n + "_end').val(formatDate(GetLastMonthEnd())+' 23:59');cls('#clear_" + n + "');return false;\">Прошлый месяц</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#cph_" + n + "_begin').val(formatDate(GetLastWeekBegin())+' 00:00');$('#cph_" + n + "_end').val(formatDate(GetLastWeekEnd())+' 23:59');cls('#clear_" + n + "');return false;\">Прошлая неделя</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#cph_" + n + "_begin').val(formatDate(GetYesterday())+' 00:00');$('#cph_" + n + "_end' ).val(formatDate(GetYesterday())+' 23:59');cls('#clear_" + n + "');return false;\">Вчера</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"var _date = new Date();$('#cph_" + n + "_begin').val(formatDate(_date)+' 00:00');$('#cph_" + n + "_end' ).val(formatDate(_date)+' 23:59');cls('#clear_" + n + "');return false;\">Сегодня</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#cph_" + n + "_begin').val(formatDate(GetCurrentWeekBegin())+' 00:00');$('#cph_" + n + "_end' ).val(formatDate(GetCurrentWeekEnd())+' 23:59');cls('#clear_" + n + "');return false;\">Текущая неделя</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#cph_" + n + "_begin').val(formatDate(GetCurrentMonthBegin())+' 00:00');$('#cph_" + n + "_end' ).val(formatDate(GetCurrentMonthEnd())+' 23:59');cls('#clear_" + n + "');return false;\">Текущий месяц</a></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#cph_" + n + "_begin').val(formatDate(GetCurrentYearBegin())+' 00:00');$('#cph_" + n + "_end' ).val(formatDate(GetCurrentYearEnd())+' 23:59');cls('#clear_" + n + "');return false;\">Текущий год</a></li>";
                                _ret += "               <li class=\"divider\"></li>";
                                _ret += "               <li><a href=\"#\" onclick=\"$('#cph_" + n + "_begin' ).val('');$('#cph_" + n + "_end' ).val('');cld('#clear_" + n + "');return false;\">Очистить</a></li>";
                                _ret += "           </ul>";
                                _ret += "       </div>";
                                _ret += "       <input id=\"cph_" + n + "_begin\" name=\"cph_" + n + "_begin\" class=\"form-control\" placeholder=\"с\" style=\"width: 60px;\" value=\"" + fld.Filter.Value + "\" ";
                                _ret += "           onchange=\"if ($('#cph_" + n + "_begin').val()!='' || $('#cph_" + n + "_end').val()!='')cls('#clear_" + n + "'); else cld('#clear_" + n + "');\"/>";
                                _ret += "       <input id=\"cph_" + n + "_end\" name=\"cph_" + n + "_end\" class=\"form-control\" placeholder=\"по\" style=\"width: 60px;\" value=\"" + fld.Filter.Value2 + "\" ";
                                _ret += "           onchange=\"if ($('#cph_" + n + "_begin').val()!='' || $('#cph_" + n + "_end').val()!='')cls('#clear_" + n + "'); else cld('#clear_" + n + "');\"/>";
                                _ret += "   </div>";
                                _ret += "</div>";
                                break;

                            default:
                                break;
                        }
                    }
                }
                _ret += "           <div class=\"col-md-3\" style=\"width: 271px;margin-top:3px;margin-bottom:1px;\">";
                _ret += "               <input type=\"submit\" id=\"btn_apply_filter\" name=\"cph_btn_apply_filter\" value=\"Применить\" class=\"btn btn-xs btn-primary\" style=\"width: 133px;\"/>&nbsp;";
                _ret += "               <button type=\"button\" id=\"btn_clear_filter\" name=\"cph_btn_clear_filter\" class=\"btn btn-xs btn-default\" style=\"width: 132px;\" onclick=\"$('button[id^=\\'clear_\\']').click();\">Сбросить все</button>";//
                _ret += "           </div>";
                _ret += "       </div>";
                _ret += "   </div>";
                _ret += "   <a href=\"#\" onclick=\"hide_search_panel();\" title=\"Скрыть панель фильтров\" style=\"margin-top:-17px;float: right;\">";
                _ret += "       <span class=\"hi hi-chevron-up\"></span></a>";
                _ret += "</div>";
                _ret = System.Text.RegularExpressions.Regex.Replace(_ret, "  +", "");
            }
            else
                _ret = "<div id=\"panel_search\" class=\"panel panel-default\" style=\"margin-bottom: 2px;\"></div>";

            return _ret;
        }

        private void jqGrid_DataRequesting(object sender, JQGridDataRequestEventArgs e)
        {
            int totalrows;
            if (e.SortExpression.ToString() != "")
            {
                MainCursor.SortColumn = e.SortExpression.ToString();
                MainCursor.SortDirection = e.SortDirection.ToString();
            }
            MainCursor.CurrentPageNumber = e.NewPageIndex;

            faFunc.SetUserSetting(MainCursor.Alias + (Page != faPage.none ? "_" + Page : "") + "_page_size", (sender as JQGrid).PagerSettings.PageSize.ToString());
            DataTable dt = GetData(MainCursor, (e.NewPageIndex - 1) * (sender as JQGrid).PagerSettings.PageSize, (sender as JQGrid).PagerSettings.PageSize, MainCursor.SortColumn, MainCursor.SortDirection, out totalrows);

            #region Очередь документов

            if (dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.Sort = MainCursor.SortColumn + " " + MainCursor.SortDirection;
                DataTable dt2 = dv.ToTable();
                string[] queue = new string[dt2.Rows.Count];
                for (int i = 0; i < dt2.Rows.Count; i++)
                    queue[i] = dt2.Rows[i]["id"].ToString();

                HttpContext.Current.Session[MainCursor.SesCurName + "_queue"] = queue;
            }

            #endregion Очередь документов

            (sender as JQGrid).DataSource = dt;
            e.TotalRows = totalrows; e.TotalRows = totalrows;
            //(sender as JQGrid).Columns.FromDataField("id").FooterValue = _elapsed.ToString();
            (sender as JQGrid).DataBind();
        }

        //void jqGrid_CellBinding(object sender, JQGridCellBindEventArgs e)
        //{
        //    var cm = grid.jqGrid('getGridParam','colModel'),i=0,l=cm.length;
        //for (; i<l; i++) {
        //    if (cm[i].name===columnName) {
        //        return i; // return the index
        //    }
        //}
        //return -1;
        //    if (e.ColumnIndex == 0 && e.)
        //    {
        //        e.CellHtml = String.Format("<div  style='color: Red;font-weight:600;'>{0}</div>", e.CellHtml);
        //    }
        //}

        #endregion LIST

        #region EDIT

        public void AddEditControls(faCursor cur, Control owner)
        {
            string _js = "";
            JQGrid jqGrid;
            for (int i = 0; i < cur.FieldCount; i++)
            {
                if (cur.Fields[i].Edit.Enable)
                {
                    if (cur.Fields[i].Edit.Control == faControl.TreeGrid)
                    {
                        jqGrid = new JQGrid();
                        jqGrid.Width = 800;
                        jqGrid.Height = 420;
                        JQGridColumn _c;
                        _c = new JQGridColumn();
                        _c.DataField = "id";
                        _c.Visible = false;
                        jqGrid.Columns.Add(_c);
                        _c = new JQGridColumn();
                        _c.DataField = "treetext";
                        _c.HeaderText = "Наименование";
                        _c.Width = 550;
                        _c.Sortable = false;
                        jqGrid.Columns.Add(_c);
                        jqGrid.ToolBarSettings.ShowAddButton = false;
                        jqGrid.ToolBarSettings.ShowDeleteButton = false;
                        jqGrid.ToolBarSettings.ShowEditButton = false;
                        jqGrid.ToolBarSettings.ShowRefreshButton = false;
                        jqGrid.ClientSideEvents.RowDoubleClick = "tree_" + cur.Fields[i].LookUp.Table + "_click";
                        jqGrid.TreeGridSettings.Enabled = true;

                        owner.Controls.Add(new LiteralControl("<div class=\"modal_form\" id=\"form" + cur.Fields[i].LookUp.Table + "_tree\" title=\"Выбор\" style=\"display:none;\" >"));
                        owner.Controls.Add(new LiteralControl("<input name=\"texttree" + cur.Fields[i].LookUp.Table + "\" type=\"text\" id=\"texttree" + cur.Fields[i].LookUp.Table +
                            "\" placeholder=\"Поиск\" onkeydown=\"if(event.keyCode==13){SetTreeText_" + cur.Fields[i].LookUp.Table + "($('#texttree" + cur.Fields[i].LookUp.Table +
                            "').val());}\" class=\"form-control\" style=\"width: 98%; margin-bottom: 5px;\" value = '" + (HttpContext.Current.Session["texttree_" + cur.Fields[i].LookUp.Table] ?? "").ToString() + "'>"));
                        owner.Controls.Add(jqGrid);
                        owner.Controls.Add(new LiteralControl("</div>"));

                        JSFunctionList["SetTreeText_" + cur.Fields[i].LookUp.Table + "(texttree)"] =
                            "$.ajax({url: '/ajax/setses.aspx?key=texttree_" + cur.Fields[i].LookUp.Table + "&value=' + escape(texttree),type: 'POST',success: function (html) { jQuery('#" + jqGrid.ClientID + "').trigger('reloadGrid'); }});";
                        _js = "";
                        _js += "var grid = jQuery('#" + jqGrid.ClientID + "');";
                        _js += "var row = grid.getRowData(id);";
                        _js += "var leaf = row.tree_leaf;";
                        _js += cur.Fields[i].LookUp.Table == "doctree" ? "if (leaf=='true'){" : "";
                        _js += "$('#text_" + cur.Alias + "_" + cur.Fields[i].Data.FieldName + "').val(row.treetext);";
                        _js += "$('#cph_" + cur.Alias + "_" + cur.Fields[i].Data.FieldName + "').val(row.treetext);";
                        _js += "$('#" + cur.Alias + "_" + cur.Fields[i].Data.FieldName + "').val(row.id);";
                        _js += "$('#form" + cur.Fields[i].LookUp.Table + "_tree').dialog('close');";
                        _js += cur.Fields[i].LookUp.Table == "doctree" ? "}" : "";
                        _js += "return false;";

                        this.JSFunctionList["tree_" + cur.Fields[i].LookUp.Table + "_click(id)"] = _js;

                        string _t = cur.Fields[i].LookUp.Table;
                        jqGrid.DataRequesting += (s, e) =>
                        {
                            (s as JQGrid).DataSource = GetDataTree(_t, (HttpContext.Current.Session["texttree_" + _t] ?? "").ToString());
                            (s as JQGrid).DataBind();
                        };
                    }
                }
            }
        }

        public void Close()
        {
            List<string> list = new List<string>(Cursors.Keys);
            foreach (string k in list)
                ClearData(Cursors[k]);
        }

        public bool Del(out string result)
        {
            DataRow row;
            DataTable dt;
            string user_id = HttpContext.Current.Session["user_id"].ToString();

            int _cid = Int32.Parse(_id);

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            conn.Open();

            #region Проверка на использование

            if (MainCursor.RelTable != null)
            {
                DataRowView _drvt;
                string _table = "";
                string _field = "";
                foreach (string k in MainCursor.RelTable)
                {
                    _table = k.Split('.')[0];
                    _field = k.Split('.')[1];
                    _drvt = GetTableInfo(_table);
                    //
                    if ((bool)_drvt["common"])
                    {
                        SqlCommand cmd_u = new SqlCommand("SELECT id FROM [dbo].[" + _table + "] WHERE del=0 AND " + _field + "=" + _cid, conn);
                        var res_u = cmd_u.ExecuteScalar();
                        cmd_u.Dispose();
                        if (!(res_u is DBNull || res_u == null))
                        {
                            result = String.Format(faEdit.MsgCantDel, _drvt["description"].ToString(), (int)res_u);
                            conn.Close();
                            return false;
                        }
                    }
                    else if (_table != "_archive")
                    {
                        DataTable dtb = new DataTable();
                        SqlCommand cmd_b = new SqlCommand("SELECT name, namerus FROM [dbo].[_base] WHERE del=0", conn);
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd_b);
                        sqlDataAdapter.Fill(dtb);
                        foreach (DataRow rb in dtb.Rows)
                        {
                            SqlCommand cmd_u = new SqlCommand("SELECT id FROM [dbo].[" + rb[0].ToString() + _table + "] WHERE del=0 AND " + _field + "=" + _cid, conn);
                            var res_u = cmd_u.ExecuteScalar();
                            cmd_u.Dispose();
                            if (!(res_u is DBNull || res_u == null))
                            {
                                result = String.Format(faEdit.MsgCantDel, rb[1].ToString() + "." + _drvt["description"].ToString(), (int)res_u);
                                conn.Close();
                                return false;
                            }
                        }
                    }
                }
            }

            #endregion Проверка на использование

            SqlTransaction trans = conn.BeginTransaction(MainCursor.Alias + "_tr");
            try
            {
                SqlCommand cmd = new SqlCommand("", conn, trans);

                string _cond = MainCursor.Fields[0].Filter.Condition;
                MainCursor.Fields[0].Filter.Condition = "=";
                string _val = MainCursor.Fields[0].Filter.Value;
                MainCursor.Fields[0].Filter.Value = _cid.ToString();
                int _tr = 0;
                dt = GetData(MainCursor, 0, 1, "id", "Asc", out _tr);
                MainCursor.Fields[0].Filter.Condition = _cond;
                MainCursor.Fields[0].Filter.Value = _val;
                if (dt.Rows.Count == 0)
                    throw new System.Exception("Записи c ID = " + _cid + " не существует");
                row = dt.Rows[0];
                cmd.CommandText = "UPDATE [dbo].[" + MainCursor.Alias + "] SET del=1 ";
                cmd.CommandText += " WHERE id = @p_id";
                cmd.Parameters.AddWithValue("@p_id", _cid);
                cmd.ExecuteNonQuery();
                faFunc.ToJournal(cmd, user_id, 3, _cid, IDBase, MainCursor.TableID, "", 0);
                trans.Commit();
                conn.Close();
                ClearData(MainCursor);
                result = "";
                return true;
            }
            catch (Exception ex)
            {
                result = faFunc.GetExceptionMessage(ex);
                try
                {
                    trans.Rollback();
                    conn.Close();
                }
                catch (Exception ex2)
                {
                    result += ex2.GetType() + ":" + ex2.Message;
                }
                return false;
            }
        }

        public string GenerateFormJS()
        {
            faCursor cur = MainCursor;
            string _js = "<script type='text/javascript'>";

            // window resize
            _js += "$(window).bind('resize', function () {";
            _js += "   var w = $(window).width(); var h = $('.panel-body').height(); ";
            _js += "   $('textarea').height(h-43); if (h>400){";
            List<string> list = new List<string>(Cursors.Keys);
            foreach (string k in list)
                _js += "$('#" + Cursors[k].ClientID + "').setGridHeight(h-92);";
            _js += " } var wf = " + this.EditFormWidth + ";var wt = wf - 383; jQuery('#mainform').width(wf);jQuery('#tabs').width(wt);$('textarea').width(wt-16);}).trigger('resize');\n";

            _js += "jQuery(document).ready(function(){";

            list = new List<string>(JSReadyList.Keys);
            foreach (string k in list)
            {
                _js += JSReadyList[k];
            }

            _js += "    $(window).resize();";
            _js += "});";

            list = new List<string>(JSFunctionList.Keys);
            foreach (string k in list)
            {
                _js += "function " + k + "{" + JSFunctionList[k] + "}";
            }

            return _js + "</script>";
        }

        public void PrepareCursorJQGrid(string _cn, out JQGrid jqGrid)
        {
            #region settings

            string _n = "";
            string _ret = "";
            faCursor cur = Cursors[_cn];

            ///!!!!!!!!!!
            //if (_act == "copy")
            //{
            //    cur.Fields[0].Filter.Condition = "=";
            //    cur.Fields[0].Filter.Value = "0";
            //}
            ///!!!!!!!!!!
            jqGrid = new JQGrid();
            jqGrid.ID = "jqGrid" + _cn;
            jqGrid.AppearanceSettings.ShrinkToFit = false;
            jqGrid.Height = this.EditFormHeight;
            jqGrid.ColumnReordering = true;
            jqGrid.Width = this.EditFormWidth - 386;
            jqGrid.EditDialogSettings.Width = jqGrid.AddDialogSettings.Width = cur.EditDialogWidth;
            if (cur.ShowPager)
            {
                jqGrid.PagerSettings.PageSize = cur.PageSize;
                jqGrid.PagerSettings.PageSizeOptions = "[10,20,30,50,100,200]";
            }
            else
            {
                jqGrid.PagerSettings.PageSize = 10000;
                JSReadyList.Add(jqGrid.ID + "_hide_pager", "$('#cph_" + jqGrid.ID + "_pager_center').hide();");
                JSReadyList.Add(jqGrid.ID + "_width_pager", "$('#cph_" + jqGrid.ID + "_pager_left').width(550);");
            }
            jqGrid.SortSettings.InitialSortColumn = cur.SortColumn;
            jqGrid.SortSettings.InitialSortDirection = (cur.SortDirection == "desc" ? Trirand.Web.UI.WebControls.SortDirection.Desc : Trirand.Web.UI.WebControls.SortDirection.Asc);

            #endregion settings

            #region AddButton

            if (cur.EnableAddButton)
            {
                JQGridToolBarButton jqBtnAdd = new JQGridToolBarButton();
                jqBtnAdd.OnClick = "AddRow" + _cn;
                jqBtnAdd.ButtonIcon = "ui-icon-plus";
                jqBtnAdd.Text = "Добавить&nbsp;&nbsp;";
                jqBtnAdd.ToolTip = "Добавить новую запись";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnAdd);
                _ret =
                   " var form = $('#form" + _cn + "_edit');" +
                   " form.find('#oper').val('add');";
                foreach (var fld in cur.Fields)
                {
                    if (fld.Edit.Visible)
                    {
                        _n = _cn + "_" + fld.Data.FieldName;
                        switch (fld.Edit.Control)
                        {
                            case faControl.TextBox:
                            case faControl.TextBoxNumber:
                            case faControl.TextBoxInteger:
                            case faControl.DatePicker:
                            case faControl.DateTimePicker:
                            case faControl.NewWindowArchiveID:
                                _ret += " form.find('#" + _n + "').val('" + fld.Edit.DefaultValue + "');";
                                break;

                            case faControl.CheckBox:
                                _ret += " form.find('#" + _n + "').prop('checked', true);";
                                break;

                            case faControl.AutoComplete:
                            case faControl.DropDown:
                            case faControl.TreeGrid:
                            case faControl.NewWindowArchive:
                                _ret += " form.find('#" + _n + "').val('" + fld.Edit.DefaultValue + "');";
                                _ret += " form.find('#cph_" + _n + "').val('" + fld.Edit.DefaultText + "');";
                                _ret += " form.find('#text_" + _n + "').val('" + fld.Edit.DefaultText + "');";
                                break;

                            case faControl.File:
                                _ret += " form.find('#" + _n + "').val('');";
                                _ret += "form.find('#iframe_" + _n + "').parent().show();";
                                break;

                            default:
                                break;
                        }
                    }
                }
                _ret += "$('#form" + _cn + "_edit').parent().find('button:contains(Изменить)').text('Добавить');";
                _ret += "$('#form" + _cn + "_edit').dialog('open');";
                JSFunctionList.Add("AddRow" + _cn + "()", _ret);
            }

            #endregion AddButton

            #region ViewButton

            if (cur.EnableViewButton)
            {
                JQGridToolBarButton jqBtnView = new JQGridToolBarButton();
                jqBtnView.OnClick = "ViewRow" + _cn;
                jqBtnView.ButtonIcon = "ui-icon-document";
                jqBtnView.Text = "Открыть&nbsp;&nbsp;";
                jqBtnView.ToolTip = "Открыть для просмотра или редактирования";
                if (cur.Alias == "_complectnew_reg") jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnView);
                jqGrid.ClientSideEvents.RowDoubleClick = "ViewRow" + _cn;
                _ret =
                    " var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                    " var id = grid.jqGrid('getGridParam', 'selrow');" +
                    " var form = $('#form" + _cn + "_edit');" +
                    " if (id!=0 && id!=null) {" +
                    "   var row = grid.getRowData(id);" +
                    "   form.find('#oper').val('edit');" +
                    "   form.find('#id').val(id);";
                foreach (var fld in cur.Fields)
                {
                    if (fld.Edit.Visible)
                    {
                        _n = _cn + "_" + fld.Data.FieldName;
                        switch (fld.Edit.Control)
                        {
                            case faControl.TextBox:
                            case faControl.TextBoxNumber:
                            case faControl.TextBoxInteger:
                            case faControl.DatePicker:
                            case faControl.NewWindowArchiveID:
                                _ret += " form.find('#" + _n + "').val(row." + fld.Data.FieldName + ");";
                                break;

                            case faControl.CheckBox:
                                _ret += " form.find('#" + _n + "').prop('checked', (row." + fld.Data.FieldName + "=='Yes'));";
                                break;

                            case faControl.File:
                                _ret += " form.find('#" + _n + "').val(row." + fld.Data.FieldName + ");";
                                _ret += fld.Edit.AddOnly ? " if(id>0)form.find('#iframe_" + _n + "').parent().hide();else form.find('#iframe_" + _n + "').parent().show();" : "";
                                break;

                            case faControl.AutoComplete:
                            case faControl.DropDown:
                            case faControl.TreeGrid:
                            case faControl.NewWindowArchive:
                                _ret += " form.find('#" + _n + "').val(row." + fld.Data.FieldName + ");";
                                _ret += " form.find('#cph_" + _n + "').val(row." + fld.Data.FieldName + "_" + fld.LookUp.Field + "_text);";
                                _ret += " form.find('#text_" + _n + "').val(row." + fld.Data.FieldName + "_" + fld.LookUp.Field + "_text);";
                                break;

                            default:
                                break;
                        }
                    }
                }
                _ret += "$('#form" + _cn + "_edit').parent().find('button:contains(Добавить)').text('Изменить');";
                _ret += "$('#form" + _cn + "_edit').dialog('open');" +
                " }else alert('Выберите запись');  return false;";
                JSFunctionList.Add("ViewRow" + _cn + "()", _ret);
            }

            #endregion ViewButton

            #region FileButton

            if (cur.EnableFileButton)
            {
                JQGridToolBarButton jqBtnFile = new JQGridToolBarButton();
                jqBtnFile.OnClick = "FileRow" + _cn;
                jqBtnFile.ButtonIcon = "ui-icon-arrowthickstop-1-s";
                jqBtnFile.Text = "Файл&nbsp;&nbsp;";
                jqBtnFile.ToolTip = "Открыть файл";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnFile);
                _ret = "";
                _ret += "   var grid = jQuery('#cph_" + jqGrid.ClientID + "');";
                _ret += "   var id = grid.jqGrid('getGridParam', 'selrow');";
                _ret += "   if (id) { ";
                _ret += "       var row = grid.getRowData(id);";
                //if (_cn == BaseName + "_complect_list_view") {
                //    _ret += "       var _f = row.file;";
                //    _ret += "       var _c = row.id_archive==0?1:0;";
                //    _ret += "       if (_f!='') window.open('/Archive/GetFile.aspx?id='+id +'&c=' + _c + '&b=' + '" + BaseName + "'+'&f=' + _f + '&k='+$.md5(id));";
                //}
                //else {
                _ret += "       var f = row.file;";
                _ret += "       if (f!='' && f!='ad') window.open('/Archive/GetFile.aspx?id='+id +'&b=' + row.id_base +'&f=' + f + '&k='+$.md5(id));";
                _ret += "       else if (f=='ad') alert('Доступ к файлу запрещен');";
                _ret += "       else if (f=='') alert('Файл не указан');";
                _ret += "   }";
                _ret += "   else alert('Выберите запись');  return false;";
                JSFunctionList.Add("FileRow" + _cn + "()", _ret);
            }

            #endregion FileButton

            #region CustomButtonFindInSP

            if (this.RouteName == "archive" && _cn.Contains("_docversion"))
            {
                JQGridToolBarButton jqBtn = new JQGridToolBarButton();
                jqBtn.OnClick = "GoToSP_" + _cn;
                jqBtn.ButtonIcon = "ui-icon-newwin";
                jqBtn.Text = "Найти в SP&nbsp;&nbsp;";
                jqBtn.ToolTip = "Ищет документ по штрихкоду на портале Share Point";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtn);
                _ret =
                    " var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                    " var id = grid.jqGrid('getGridParam', 'selrow');" +
                    " if (id>0) { " +
                    "    var row = grid.getRowData(id);" +
                    "    var b = row.barcode;" +
                    "    window.open('http://sky-sp1.stg.lan/_layouts/15/osssearchresults.aspx?k='+b);" +
                    "}else alert('Выберите запись');  return false;";
                JSFunctionList.Add("GoToSP_" + _cn + "()", _ret);
            }

            #endregion CustomButtonFindInSP

            switch (this.RouteName)
            {
                case "complect":

                    #region CustomButton1

                    JQGridToolBarButton jqBtn1 = new JQGridToolBarButton();
                    jqBtn1.OnClick = "NewCard_" + _cn;
                    jqBtn1.ButtonIcon = "ui-icon-newwin";
                    jqBtn1.Text = "Создать карточку&nbsp;&nbsp;";
                    jqBtn1.ToolTip = "Создает новую карточку на основе записи";
                    jqGrid.ToolBarSettings.CustomButtons.Add(jqBtn1);
                    _ret =
                       " var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                       " var id = grid.jqGrid('getGridParam', 'selrow');" +
                       " if (id>0) { " +
                       "    var row = grid.getRowData(id);" +
                       "    var f = row.file;" +
                       "    var b = row.barcode;" +
                       "    window.open('/archive/" + BaseName + "/srch?id=0&act=add&mode=complect&from='+id +'&b=zao_stg&f=' + f );" +
                       "}else alert('Выберите запись');  return false;";
                    JSFunctionList.Add("NewCard_" + _cn + "()", _ret);

                    #endregion CustomButton1

                    #region CustomButton2

                    JQGridToolBarButton jqBtn2 = new JQGridToolBarButton();
                    jqBtn2.OnClick = "AddToCard_" + _cn;
                    jqBtn2.ButtonIcon = "ui-icon-newwin";
                    jqBtn2.Text = "Добавить в карточку&nbsp;&nbsp;";
                    jqBtn2.ToolTip = "Добавляет в карточку версию, на основе выбранной записи";
                    jqGrid.ToolBarSettings.CustomButtons.Add(jqBtn2);
                    _ret =
                       " var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                       " var id = grid.jqGrid('getGridParam', 'selrow');" +
                       " if (id>0) { " +
                       "    var row = grid.getRowData(id);" +
                       "    var f = row.file;" +
                       "    var b = row.barcode;" +
                       "    var id_archive=prompt('Введите Код ЭА',0);" +
                       "    window.open('/archive/" + BaseName + "/srch?id='+id_archive+'&act=view&mode=complect&from='+id +'&b=zao_stg&f=' + f );" +
                       "}else alert('Выберите запись');  return false;";
                    JSFunctionList.Add("AddToCard_" + _cn + "()", _ret);

                    #endregion CustomButton2

                    break;

                case "complectnew":
                    if (cur.Alias == "_complectnew_list")
                    {
                        #region CustomButtonFileButton

                        JQGridToolBarButton jqBtnFile = new JQGridToolBarButton();
                        jqBtnFile.OnClick = "FileRow" + _cn;
                        jqBtnFile.ButtonIcon = "ui-icon-arrowthickstop-1-s";
                        jqBtnFile.Text = "Файл&nbsp;&nbsp;";
                        jqBtnFile.ToolTip = "Открыть файл";
                        jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnFile);
                        _ret = "";
                        _ret += "   var grid = jQuery('#cph_" + jqGrid.ClientID + "');";
                        _ret += "   var id = grid.jqGrid('getGridParam', 'selrow');";
                        _ret += "   if (id) { ";
                        _ret += "       var row = grid.getRowData(id);";
                        _ret += "       var ida = row.id_archive;";
                        _ret += "       var f = row.file;";
                        _ret += "       if (f=='ad')";
                        _ret += "       {alert('Доступ к файлу запрещен.');}";
                        _ret += "       else{if (row.file_archive!='') {f=row.file_archive;}";
                        _ret += "       if (f!='') window.open('/Archive/GetFile.aspx?id='+id +'&b=' + row.id_base +'&f=' + f + '&k='+$.md5(id));";
                        _ret += "       else alert('Файл не указан');}";
                        _ret += "   }";
                        _ret += "   else alert('Выберите запись');  return false;";
                        JSFunctionList.Add("FileRow" + _cn + "()", _ret);

                        #endregion CustomButtonFileButton

                        #region CustomButtonFileDownButton

                        JQGridToolBarButton jqBtnFileDown = new JQGridToolBarButton();
                        jqBtnFileDown.OnClick = "GoToFileDown_" + cur.Alias;
                        jqBtnFileDown.ButtonIcon = "ui-icon-arrowthickstop-1-s";
                        jqBtnFileDown.Text = "Все&nbsp;&nbsp;";
                        jqBtnFileDown.ToolTip = "Выгрузить файлы соответствующие документам в списке";
                        jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnFileDown);
                        JSFunctionList.Add("GoToFileDown_" + cur.Alias + "()",
                            "var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                            "var rc = grid.jqGrid('getGridParam', 'reccount');" +
                            "var v=grid.getRowData();" +
                            "var f='';" +
                            "if (rc>0) { " +
                            //"   for(var i=0; i<v.length; i++) {" +
                            //"       if (v[i]['id']>0) f += v[i]['id']+'|';" +
                            //"   }" +
                            //"   f = f.substring(0, f.length - 1);" +
                            "   window.open('/Archive/GetFileMulti.aspx?b='+v[0]['id_base']+'&cur=" + _cn + "_cursor_" + _id + "');" +//"&b=' + '" + BaseName + "'+'&f=' + escape(f));" +
                            "}" +
                            "else alert('Нет записей для выгрузки');  return false;");

                        #endregion CustomButtonFileDownButton

                        #region CustomButtonMultipleUploadFile

                        if (cur.EnableSaveButton)
                        {
                            JQGridToolBarButton jqBtnMultipleUploadFile = new JQGridToolBarButton();
                            jqBtnMultipleUploadFile.OnClick = "GoToMOF_" + _cn;
                            jqBtnMultipleUploadFile.ButtonIcon = "ui-icon-newwin";
                            jqBtnMultipleUploadFile.Text = "Загрузить&nbsp;&nbsp;";
                            jqBtnMultipleUploadFile.ToolTip = "Позволяет загрузить одновременно несколько файлов";
                            jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnMultipleUploadFile);
                            _ret =
                                " var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                                //" var id = grid.jqGrid('getGridParam', 'selrow');" +
                                "var v = $('#iframe_multiple').contents().find('#input_file');" +
                                //test_complectnew_list_cursor_3
                                "v.click();"
                                //" if (id>0) { " +
                                //"    var row = grid.getRowData(id);" +
                                //"    var b = row.barcode;" +
                                //"    window.open('http://sky-sp1.stg.lan/_layouts/15/osssearchresults.aspx?k='+b);" +
                                //"}else alert('Выберите запись');  return false;"
                                ;
                            JSFunctionList.Add("GoToMOF_" + _cn + "()", _ret);
                            _ret = "jQuery('#cph_" + jqGrid.ClientID + "').trigger('reloadGrid');";
                            JSFunctionList.Add("RefreshAfterMOF()", _ret);
                            //"$.ajax({" +
                            //"type: 'POST'," +
                            //"url: location.href + '&jqGridID=cph_jqGrid" + k + "&editMode=1'," +
                            //"data: res," +
                            //"error: function(data) {" +
                            //    "$('#dialog_" + k + "_error').hide();$('#dialog_" + k + "_error').html(data.responseText).show('fast');" +
                            //"}," +
                            //"success: function(data) {" +
                            //    "$('#cph_jqGrid" + k + "').trigger('reloadGrid');" +
                            //    "$('#form" + k + "_edit').dialog('close');" +
                            //"}" +
                        }

                        #endregion CustomButtonMultipleUploadFile

                        #region CustomButtonClipboard

                        if (cur.EnableSaveButton)
                        {
                            if (!String.IsNullOrEmpty(RequestGet["sidx"]))
                            {//для вставки из буфера по текущей сортировке
                                HttpContext.Current.Session["complectnew_cursor_sidx"] = RequestGet["sidx"];
                                HttpContext.Current.Session["complectnew_cursor_sord"] = RequestGet["sord"];
                            }

                            JQGridToolBarButton jqBtnClipboard = new JQGridToolBarButton();
                            jqBtnClipboard.OnClick = "InsertFromCB_" + _cn;
                            jqBtnClipboard.ButtonIcon = "ui-icon-newwin";
                            jqBtnClipboard.Text = "Вставить&nbsp;&nbsp;";
                            jqBtnClipboard.ToolTip = "Позволяет вставить из буфера обмена";
                            jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnClipboard);
                            _ret =
                                " var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                                " var id = grid.jqGrid('getGridParam', 'selrow');" +
                                " var buf = window.clipboardData.getData('Text').trim();" +
                                " if (id && buf.length>0) { " +
                                "   var row = grid.getRowData(id);" +
                                "   $.ajax({" +
                                "   url: '/ajax/InsertFromCB.aspx?id='+id+'&cur=" + _cn + "_cursor_" + _id + "'," +
                                "   type: 'POST'," +
                                "       data: buf," +
                                "       cache: false," +
                                "       contentType: false," +
                                "       processData: false," +
                                "       success: function (html) { window.parent.RefreshAfterMOF();if (html.length>0)alert(html)}," +
                                "       error: function (request, status, error) {  alert(request.responseText); }" +
                                "   });" +
                                "}else alert('Выберите запись для вставки');  return false;"
                                ;

                            JSFunctionList.Add("InsertFromCB_" + _cn + "()", _ret);
                        }

                        #endregion CustomButtonClipboard

                        #region CustomButtonViewCard

                        JQGridToolBarButton jqBtnViewCard = new JQGridToolBarButton();
                        jqBtnViewCard.OnClick = "ViewCard_" + _cn;
                        jqBtnViewCard.ButtonIcon = "ui-icon-newwin";
                        jqBtnViewCard.Text = "Карточка&nbsp;&nbsp;";
                        jqBtnViewCard.ToolTip = "Открывает соответствующую карточку (если указана)";
                        jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnViewCard);
                        _ret =
                            " var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                            " var id = grid.jqGrid('getGridParam', 'selrow');" +
                            " if (id>0) { " +
                            "    var row = grid.getRowData(id);" +

                            "   if (row.id_archive>0) window.open('/archive/'+row.id_base+'/srch?id='+row.id_archive+'&act=view');else alert('Код ЭА не указан.');" +
                            "}else alert('Выберите запись');  return false;";
                        JSFunctionList.Add("ViewCard_" + _cn + "()", _ret);

                        #endregion CustomButtonViewCard

                        #region CustomButtonNewCard

                        if (cur.EnableSaveButton)
                        {
                            JQGridToolBarButton jqBtnNewCard = new JQGridToolBarButton();
                            jqBtnNewCard.OnClick = "NewCard_" + _cn;
                            jqBtnNewCard.ButtonIcon = "ui-icon-newwin";
                            jqBtnNewCard.Text = "Созд.карт&nbsp;&nbsp;";
                            jqBtnNewCard.ToolTip = "Создает новую карточку на основе записи";
                            jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnNewCard);
                            _ret =
                                " var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                                " var id = grid.jqGrid('getGridParam', 'selrow');" +
                                " if (id>0) { " +
                                "    var row = grid.getRowData(id);" +
                                "    var f = row.file;" +
                                "    var fa = row.file_archive;" +
                                "    if (fa!='') alert('Карточка уже была создана ранее\\n Файл документа в архиве: '+fa+'\\n');" +
                                "    else {" +
                                "       var b = row.barcode;" +
                                "       var d = row.date_reg;" +
                                "       window.open('/archive/'+row.id_base+'/srch?id=0&act=add&mode=complectnew&from='+id +'&b=' + b + '&f=' + f + '&d=' + d + '&i=' + $('#_complectnew_inet').val());" +
                                "    }" +
                                "}else alert('Выберите запись');  return false;";
                            JSFunctionList.Add("NewCard_" + _cn + "()", _ret);
                        }

                        #endregion CustomButtonNewCard

                        #region CustomButtonAddToCard

                        if (cur.EnableSaveButton)
                        {
                            JQGridToolBarButton jqBtnAddToCard = new JQGridToolBarButton();
                            jqBtnAddToCard.OnClick = "AddToCard_" + _cn;
                            jqBtnAddToCard.ButtonIcon = "ui-icon-newwin";
                            jqBtnAddToCard.Text = "Доб.в карт&nbsp;&nbsp;";
                            jqBtnAddToCard.ToolTip = "Добавляет в карточку версию, на основе выбранной записи";
                            jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnAddToCard);
                            _ret =
                                " var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                                " var id = grid.jqGrid('getGridParam', 'selrow');" +
                                " if (id>0) { " +
                                "    var row = grid.getRowData(id);" +
                                "    var f = row.file;" +
                                "    var fa = row.file_archive;" +
                                 "    if (fa!='') alert('Карточка уже была создана ранее\\nФайл документа:'+fa+'\\n');" +
                                "    else {" +
                                "    var fa = row.file_archive;" +
                                "    var b = row.barcode;" +
                                "    var d = row.date_reg;" +
                                "   var id_archive=prompt('Введите Код ЭА',0);id_archive=id_archive.replace(/\\D+/g,\"\");" +
                                "    window.open('/archive/' + row.id_base + '/srch?id='+id_archive+'&act=view&mode=complectnew&from='+id +'&b=' + b + '&f=' + f + '&d=' + d  );" +
                                "    }" +
                                "}else alert('Выберите запись');  return false;";
                            JSFunctionList.Add("AddToCard_" + _cn + "()", _ret);
                        }

                        #endregion CustomButtonAddToCard
                    }
                    else if (cur.Alias == "_complectnew_reg")
                    {
                        #region CustomButtonCopyRow

                        JQGridToolBarButton jqBtnCopyRow = new JQGridToolBarButton();
                        jqBtnCopyRow.OnClick = "CopyRow_" + cur.Alias;
                        jqBtnCopyRow.ButtonIcon = "ui-icon-newwin";
                        jqBtnCopyRow.Text = "Скопировать&nbsp;&nbsp;";
                        jqBtnCopyRow.ToolTip = "Добавляет новую строку копированием выделенной";
                        jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnCopyRow);

                        _ret =
                               " var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                               " var id = grid.jqGrid('getGridParam', 'selrow');" +
                               " if (id) { " +
                               "   $.ajax({" +
                               "   url: '/ajax/CopyCursorRow.aspx?id='+id+'&cur=" + _cn + "_cursor_" + _id + "'," +
                               "   type: 'POST'," +
                               "       cache: false," +
                               "       contentType: false," +
                               "       processData: false," +
                               "       success: function (html) {grid.trigger('reloadGrid');setTimeout(function(){grid.jqGrid('setSelection',id)}, 100);  }," +
                               "       error: function (request, status, error) {  alert(request.responseText); }" +
                               "   });" +
                               "}else alert('Выберите запись для копирования');  return false;";

                        JSFunctionList.Add("CopyRow_" + _cn + "()", _ret);

                        #endregion CustomButtonCopyRow

                        #region CustomButtonTorg31

                        JQGridToolBarButton jqBtnTorg31 = new JQGridToolBarButton();
                        jqBtnTorg31.OnClick = "Torg31_" + cur.Alias;
                        jqBtnTorg31.ButtonIcon = "ui-icon-newwin";
                        jqBtnTorg31.Text = "Торг-31&nbsp;&nbsp;";
                        jqBtnTorg31.ToolTip = "Генерирует «Сопроводительный реестр сдачи документов» ТОРГ-31";
                        jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnTorg31);
                        JSFunctionList.Add("Torg31_" + cur.Alias + "()", "window.open('/ajax/Torg31.aspx?cur=" + BaseName + "_complectnew_reg_cursor_" + _id + "&id=" + _id + "');");

                        #endregion CustomButtonTorg31
                    }
                    break;

                default:
                    break;
            }

            #region DelButton

            if (cur.EnableDelButton)
            {
                JQGridToolBarButton jqBtnSep = new JQGridToolBarButton();
                jqBtnSep.ButtonIcon = "none";
                jqBtnSep.Text = "&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnSep);

                JQGridToolBarButton jqBtnDel = new JQGridToolBarButton();
                jqBtnDel.OnClick = "DelRow" + _cn;
                jqBtnDel.ButtonIcon = "ui-icon-trash";
                jqBtnDel.Text = "Удалить&nbsp;&nbsp;";
                jqBtnDel.ToolTip = "Удалить запись";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnDel);
                _ret =
                   " var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                   " var id = grid.jqGrid('getGridParam', 'selrow');" +
                   " var form = $('#form" + _cn + "_edit');" +
                   " if (id!=null && id!=0) {" +
                   "   form.find('#id').val(id);" +
                   "   form.find('#oper').val('del');" +
                   "   var res = form.find(':input').serializeArray();" +
                   "   $.ajax({ type: 'POST',url: location.href+'&jqGridID=cph_jqGrid" + _cn + "&editMode=1',data : res ,success: function(data) {grid.trigger('reloadGrid');}});" +
                   "}else alert('Выберите запись');  return false;";
                JSFunctionList.Add("DelRow" + _cn + "()", _ret);
            }

            #endregion DelButton

            #region AddColumn

            JQGridColumn _c = new JQGridColumn();//cur.FieldCount
            foreach (var fld in cur.Fields)
            {
                _c = new JQGridColumn();
                _c.DataField = fld.LookUp.Key == "" ? fld.Data.FieldName : fld.Data.FieldName + "_" + fld.LookUp.Field + "_text";
                _c.Editable = fld.Edit.Enable;
                _c.Width = fld.View.Width;
                _c.Visible = fld.View.Visible;
                _c.HeaderText = fld.View.CaptionShort;
                _c.DataFormatString = fld.View.FormatString;

                if (fld.Edit.Control == faControl.CheckBox)
                {
                    CheckBoxFormatter df = new CheckBoxFormatter();
                    _c.Formatter.Add(df);
                }
                _c.PrimaryKey = fld.Data.FieldName == "id";
                switch (fld.View.TextAlign)
                {
                    case "left":
                        _c.TextAlign = Trirand.Web.UI.WebControls.TextAlign.Left;
                        break;

                    case "right":
                        _c.TextAlign = Trirand.Web.UI.WebControls.TextAlign.Right;
                        break;

                    case "center":
                        _c.TextAlign = Trirand.Web.UI.WebControls.TextAlign.Center;
                        break;

                    default:
                        _c.TextAlign = Trirand.Web.UI.WebControls.TextAlign.Left;
                        break;
                }
                jqGrid.Columns.Add(_c);
                if (fld.LookUp.Key != "")// для справочников добавляем id в грид
                {
                    _c = new JQGridColumn();
                    _c.DataField = fld.Data.FieldName;
                    _c.Visible = false;
                    jqGrid.Columns.Add(_c);
                }
            }

            #endregion AddColumn

            #region EventDataRequesting

            jqGrid.DataRequesting += (s, e) =>
            {
                // DataView _view = new DataView(dt);
                // _view.rowfilter = "status <> 3";
                (s as JQGrid).DataSource = new DataView(GetDataList(cur), "status <> 3", "id", DataViewRowState.CurrentRows);
                (s as JQGrid).DataBind();
            };

            #endregion EventDataRequesting

            #region EventRowDeleting

            jqGrid.RowDeleting += (s, e) =>
            {
                DataTable dt = GetDataList(cur);

                DataRow row = dt.Rows.Find(e.RowKey);
                if (row["status"].ToString() == "1")
                    row.Delete();
                else
                    row["status"] = "3";

                string _s = cur.SesCurName + (_id != "" ? "_" + _id : "");
                HttpContext.Current.Session[_s] = dt;

                (s as JQGrid).DataSource = new DataView(GetDataList(cur), "status <> 3", "id", DataViewRowState.CurrentRows);
                (s as JQGrid).DataBind();
            };

            #endregion EventRowDeleting

            #region EventRowEditing

            jqGrid.RowEditing += (s, e) =>
            {
                try
                {
                    bool showerror = false;
                    string error = "", value = "";
                    decimal _decimal = 0;
                    DateTime _datetime;
                    int _int = 0;
                    DataTable dt = GetDataList(cur);

                    #region Проверка на обязательность

                    foreach (var fld in cur.Fields)
                    {
                        if (fld.Edit.Enable && fld.Edit.Required)
                        {
                            _n = _cn + "_" + fld.Data.FieldName;
                            switch (fld.Edit.Control)
                            {
                                case faControl.TextBox:
                                case faControl.TextBoxNumber:
                                case faControl.TextBoxInteger:
                                case faControl.DatePicker:
                                case faControl.DateTimePicker:
                                case faControl.NewWindowArchiveID:
                                    showerror = e.RowData[_n].Trim() == "";
                                    break;

                                case faControl.DropDown:
                                case faControl.AutoComplete:
                                case faControl.TreeGrid:
                                case faControl.NewWindowArchive:
                                    showerror = (e.RowData[_n].Trim() == "0" || e.RowData[_n].Trim() == "-1" || e.RowData["cph_" + _n].Trim() == "");
                                    break;
                            }
                        }
                        if (showerror)
                        {
                            (s as JQGrid).ShowEditValidationMessage(String.Format(faEdit.MsgMustFill, fld.View.CaptionShort));
                            return;
                        }
                    }

                    #endregion Проверка на обязательность

                    #region Проверка на уникальность

                    string src = "";
                    string dest = "";
                    foreach (var fld in cur.Fields)
                    {
                        if (fld.Edit.Enable && fld.Edit.Unique)
                        {
                            _n = _cn + "_" + fld.Data.FieldName;
                            foreach (DataRow r in dt.Rows)
                            {
                                src = r[fld.Data.FieldName].ToString().Trim().ToLower();
                                dest = e.RowData[_n].Trim().ToLower();
                                switch (fld.Edit.Control)
                                {
                                    case faControl.TextBox:
                                        if (dest != "" && r["status"].ToString() != "3" && src == dest && r["id"].ToString() != e.RowKey)
                                        {
                                            (s as JQGrid).ShowEditValidationMessage(String.Format(faEdit.MsgDublicate, fld.View.CaptionShort, r["id"].ToString()));
                                            return;
                                        }
                                        break;

                                    case faControl.TextBoxInteger:
                                    case faControl.TextBoxNumber:
                                    case faControl.DropDown:
                                    case faControl.AutoComplete:
                                    case faControl.TreeGrid:
                                    case faControl.NewWindowArchive:
                                        if (dest != "0" && r["status"].ToString() != "3" && src == dest && r["id"].ToString() != e.RowKey)
                                        {
                                            (s as JQGrid).ShowEditValidationMessage(String.Format(faEdit.MsgDublicate, fld.View.CaptionShort, r["id"].ToString()));
                                            return;
                                        }
                                        break;
                                }
                            }
                        }
                    }

                    #endregion Проверка на уникальность

                    DataRow row = dt.Rows.Find(e.RowKey);
                    row["status"] = row["status"].ToString() != "1" ? "2" : row["status"];
                    foreach (var fld in cur.Fields)
                    {
                        if (fld.Edit.Visible)
                        {
                            _n = _cn + "_" + fld.Data.FieldName;
                            value = (e.RowData[_n] ?? "0").Trim();
                            switch (fld.Edit.Control)
                            {
                                case faControl.TextBox:
                                case faControl.File:
                                    if (fld.Edit.Max > 0 && value.Length > fld.Edit.Max)
                                        error = String.Format(faEdit.MsgMaxText, fld.View.CaptionShort, fld.Edit.Max);
                                    else if (fld.Edit.Min > 0 && value.Length < fld.Edit.Min)
                                        error = String.Format(faEdit.MsgMinText, fld.View.CaptionShort, fld.Edit.Min);
                                    row[fld.Data.FieldName] = value;
                                    break;

                                case faControl.TextBoxNumber:
                                    if (Decimal.TryParse(value == "" ? "0" : value, out _decimal))
                                    {
                                        if (fld.Edit.Max > 0 && _decimal > fld.Edit.Max)
                                            error = String.Format(faEdit.MsgMaxInt, fld.View.CaptionShort, fld.Edit.Max);
                                        else if (fld.Edit.Min > 0 && _decimal > 0 && _decimal < fld.Edit.Min)
                                            error = String.Format(faEdit.MsgMinInt, fld.View.CaptionShort, fld.Edit.Min);
                                        row[fld.Data.FieldName] = _decimal;
                                    }
                                    else
                                        error = String.Format(faEdit.MsgBadFormat, fld.View.CaptionShort);
                                    break;

                                case faControl.TextBoxInteger:
                                case faControl.NewWindowArchive:
                                case faControl.NewWindowArchiveID:
                                    if (Int32.TryParse(value == "" ? "0" : value, out _int))
                                    {
                                        if (fld.Edit.Max > 0 && _int > fld.Edit.Max)
                                            error = String.Format(faEdit.MsgMaxInt, fld.View.CaptionShort, fld.Edit.Max);
                                        else if (fld.Edit.Min > 0 && _int > 0 && _int < fld.Edit.Min)
                                            error = String.Format(faEdit.MsgMinInt, fld.View.CaptionShort, fld.Edit.Min);
                                        row[fld.Data.FieldName] = _int;
                                    }
                                    else
                                        error = String.Format(faEdit.MsgBadFormat, fld.View.CaptionShort);
                                    break;

                                case faControl.DatePicker:
                                case faControl.DateTimePicker:
                                    if (value != "")
                                    {
                                        if (DateTime.TryParse(value, out _datetime))
                                        {
                                            row[fld.Data.FieldName] = _datetime;
                                        }
                                        else
                                            error = String.Format(faEdit.MsgBadFormat, fld.View.CaptionShort);
                                    }
                                    break;

                                case faControl.CheckBox:
                                    row[fld.Data.FieldName] = value;
                                    break;

                                case faControl.DropDown:
                                case faControl.AutoComplete:
                                case faControl.TreeGrid:
                                    if (Int32.TryParse(value == "" ? "0" : value, out _int))
                                    {
                                        row[fld.Data.FieldName] = _int;
                                        row[fld.Data.FieldName + "_" + fld.LookUp.Field + "_text"] = _int > 0 ? e.RowData["text_" + _n].Trim() : "";
                                    }
                                    break;
                            }
                            if (error != "")
                            {
                                (s as JQGrid).ShowEditValidationMessage(error);
                                return;
                            }
                        }
                    }
                    string _s = cur.SesCurName + (_id != "" ? "_" + _id : "") + (_mode != "" ? "_" + _mode : "");

                    #region Замена основных версий

                    if (cur.Alias.Contains("_docversion"))
                    {
                        if (row["main"].ToString() == "1")
                        {
                            foreach (DataRow r in dt.Rows)
                            {
                                if (r["main"].ToString() != "0")
                                {
                                    r["main"] = "0";
                                    r["status"] = r["status"].ToString() != "1" ? "2" : r["status"];
                                }
                            }
                            row["main"] = "1";
                        }
                    }

                    #endregion Замена основных версий

                    HttpContext.Current.Session[_s] = dt;
                    (s as JQGrid).DataBind();
                }
                catch (Exception ex)
                {
                    (s as JQGrid).ShowEditValidationMessage(faFunc.GetExceptionMessage(ex));
                }
            };

            #endregion EventRowEditing

            #region EventRowAdding

            jqGrid.RowAdding += (s, e) =>
            {
                bool showerror = false;
                string error = "", value = "";
                decimal _decimal = 0;
                DateTime _datetime;
                int _int = 0;
                DataTable dt = GetDataList(cur);

                #region Проверка на обязательность

                foreach (var fld in cur.Fields)
                {
                    if (fld.Edit.Enable && fld.Edit.Required)
                    {
                        _n = _cn + "_" + fld.Data.FieldName;
                        switch (fld.Edit.Control)
                        {
                            case faControl.TextBox:
                            case faControl.TextBoxNumber:
                            case faControl.TextBoxInteger:
                            case faControl.DatePicker:
                            case faControl.DateTimePicker:
                            case faControl.File:
                            case faControl.NewWindowArchiveID:
                                showerror = e.RowData[_n].Trim() == "";
                                break;

                            case faControl.DropDown:
                            case faControl.AutoComplete:
                            case faControl.TreeGrid:
                            case faControl.NewWindowArchive:
                                showerror = (e.RowData[_n].Trim() == "0" || e.RowData[_n].Trim() == "-1" || e.RowData["cph_" + _n].Trim() == "");
                                break;
                        }
                    }
                    if (showerror)
                    {
                        (s as JQGrid).ShowEditValidationMessage(String.Format(faEdit.MsgMustFill, fld.View.CaptionShort));
                        return;
                    }
                }

                #endregion Проверка на обязательность

                #region Проверка на уникальность

                string src = "";
                string dest = "";
                foreach (var fld in cur.Fields)
                {
                    if (fld.Edit.Enable && fld.Edit.Unique)
                    {
                        _n = _cn + "_" + fld.Data.FieldName;
                        foreach (DataRow r in dt.Rows)
                        {
                            src = r[fld.Data.FieldName].ToString().Trim().ToLower();
                            dest = e.RowData[_n].Trim().ToLower();
                            switch (fld.Edit.Control)
                            {
                                case faControl.TextBox:
                                    if (dest != "" && r["status"].ToString() != "3" && src == dest)
                                    {
                                        (s as JQGrid).ShowEditValidationMessage(String.Format(faEdit.MsgDublicate, fld.View.CaptionShort, r["id"].ToString()));
                                        return;
                                    }
                                    break;

                                case faControl.TextBoxInteger:
                                case faControl.TextBoxNumber:
                                case faControl.DropDown:
                                case faControl.AutoComplete:
                                case faControl.NewWindowArchive:
                                case faControl.TreeGrid:
                                    if (dest != "0" && r["status"].ToString() != "3" && src == dest)
                                    {
                                        (s as JQGrid).ShowEditValidationMessage(String.Format(faEdit.MsgDublicate, fld.View.CaptionShort, r["id"].ToString()));
                                        return;
                                    }
                                    break;
                            }
                        }
                    }
                }

                #endregion Проверка на уникальность

                #region Добавление новой строки

                int min_id = 0;
                foreach (DataRow r in dt.Rows)
                    min_id = Math.Min((int)r["id"], min_id);
                min_id--;
                DataRow newrow = dt.NewRow();
                newrow["id"] = min_id;
                newrow["status"] = 1;
                foreach (var fld in cur.Fields.Skip(1))
                {
                    if (fld.Edit.Visible)
                    {
                        _n = _cn + "_" + fld.Data.FieldName;
                        value = (e.RowData[_n] ?? "0").Trim();
                        switch (fld.Edit.Control)
                        {
                            case faControl.TextBox:
                            case faControl.File:
                                if (fld.Edit.Max > 0 && value.Length > fld.Edit.Max)
                                    error = String.Format(faEdit.MsgMaxText, fld.View.CaptionShort, fld.Edit.Max);
                                else if (fld.Edit.Min > 0 && value.Length < fld.Edit.Min)
                                    error = String.Format(faEdit.MsgMinText, fld.View.CaptionShort, fld.Edit.Min);
                                newrow[fld.Data.FieldName] = value;
                                break;

                            case faControl.TextBoxNumber:
                                if (Decimal.TryParse(value == "" ? "0" : value, out _decimal))
                                {
                                    if (fld.Edit.Max > 0 && _decimal > fld.Edit.Max)
                                        error = String.Format(faEdit.MsgMaxInt, fld.View.CaptionShort, fld.Edit.Max);
                                    else if (fld.Edit.Min > 0 && _decimal > 0 && _decimal < fld.Edit.Min)
                                        error = String.Format(faEdit.MsgMinInt, fld.View.CaptionShort, fld.Edit.Min);
                                    newrow[fld.Data.FieldName] = _decimal;
                                }
                                else
                                    error = String.Format(faEdit.MsgBadFormat, fld.View.CaptionShort);
                                break;

                            case faControl.TextBoxInteger:
                            case faControl.NewWindowArchive:
                            case faControl.NewWindowArchiveID:
                                if (Int32.TryParse(value == "" ? "0" : value, out _int))
                                {
                                    if (fld.Edit.Max > 0 && _int > fld.Edit.Max)
                                        error = String.Format(faEdit.MsgMaxInt, fld.View.CaptionShort, fld.Edit.Max);
                                    else if (fld.Edit.Min > 0 && _int > 0 && _int < fld.Edit.Min)
                                        error = String.Format(faEdit.MsgMinInt, fld.View.CaptionShort, fld.Edit.Min);
                                    newrow[fld.Data.FieldName] = _int;
                                }
                                else
                                    error = String.Format(faEdit.MsgBadFormat, fld.View.CaptionShort);
                                break;

                            case faControl.DatePicker:
                            case faControl.DateTimePicker:
                                if (value != "")
                                {
                                    if (DateTime.TryParse(value, out _datetime))
                                    {
                                        newrow[fld.Data.FieldName] = _datetime;
                                    }
                                    else
                                        error = String.Format(faEdit.MsgBadFormat, fld.View.CaptionShort);
                                }
                                break;

                            case faControl.CheckBox:
                                newrow[fld.Data.FieldName] = value;
                                break;

                            case faControl.DropDown:
                            case faControl.AutoComplete:
                            case faControl.TreeGrid:
                                if (Int32.TryParse(value == "" ? "0" : value, out _int))
                                {
                                    newrow[fld.Data.FieldName] = _int;
                                    newrow[fld.Data.FieldName + "_" + fld.LookUp.Field + "_text"] = _int > 0 ? e.RowData["text_" + _n].Trim() : "";
                                }
                                break;
                        }
                        if (error != "")
                        {
                            (s as JQGrid).ShowEditValidationMessage(error);
                            return;
                        }
                    }
                }

                #region Замена основных версий

                if (cur.Alias.Contains("_docversion"))
                {
                    if (newrow["main"].ToString() == "1")
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            if (r["main"].ToString() != "0")
                            {
                                r["main"] = "0";
                                r["status"] = r["status"].ToString() != "1" ? "2" : r["status"];
                            }
                        }
                    }
                }

                #endregion Замена основных версий

                dt.Rows.InsertAt(newrow, 0);

                #endregion Добавление новой строки

                string _s = cur.SesCurName + (_id != "" ? "_" + _id : "");
                HttpContext.Current.Session[_s] = dt;
                (s as JQGrid).DataBind();
            };

            #endregion EventRowAdding
        }

        public string RenderEditForm(string alert)
        {
            string _ret = "";
            string _buf = "";
            string _v = "";
            string _vt = "";
            if (_mode == "complect")
            {
                string _b = (RequestGet["b"] ?? "").ToString().Trim();
                string _f = (RequestGet["f"] ?? "").ToString().Trim();
            }
            string _n = "";
            int _maxwidth = 535;
            string _inputwidth = (this.EditFormWidth > _maxwidth) ? "200" : "400";

            string _fileuploader = ""; // есть ли на странице аплоадер файлов
            _ret += "<div id=\"mainform\" style=\"margin-right:auto;margin-left:auto;width:800px;\">";
            if (act_list.Contains(_act) && int.TryParse(_id, out int_value))
            {
                faCursor cur = MainCursor;
                string _oldcond = cur.Fields[0].Filter.Condition;
                cur.Fields[0].Filter.Condition = "=";

                //string _oldid = cur.Fields[0].Filter.Value;
                string[] _oldvalue = new string[cur.FieldCount];
                string[] _oldvalue2 = new string[cur.FieldCount];
                for (int i = 0; i < cur.FieldCount; i++)
                {
                    _oldvalue[i] = cur.Fields[i].Filter.Value;
                    cur.Fields[i].Filter.Value = "";
                    _oldvalue2[i] = cur.Fields[i].Filter.Value2;
                    cur.Fields[i].Filter.Value2 = "";
                }

                cur.Fields[0].Filter.Value = _act == "copy" ? _from : _id;

                DataTable dt = new DataTable();
                int _tr = 0;
                dt = GetData(MainCursor, 0, 1, "id", "Asc", out _tr);
                cur.Fields[0].Filter.Condition = _oldcond;
                for (int i = 0; i < cur.FieldCount; i++)
                {
                    cur.Fields[i].Filter.Value = _oldvalue[i];
                    cur.Fields[i].Filter.Value2 = _oldvalue2[i];
                }

                if (dt.Rows.Count != 0 || _act == "add" || _act == "copy")
                {
                    #region Шапка

                    _ret += "<div style=\"height:" + (this.EditFormWidth > _maxwidth ? "51" : "70") + "px;padding-top:15px;\">";
                    if (alert != "")
                    {
                        _ret += alert;
                        JSReadyList.Add("showalert", "$('.alert').hide();setTimeout(\"$('.alert').show('fast');\",1000);");
                    }
                    _ret += "</div>";
                    JSReadyList.Add("title", "document.title = '" + cur.Caption + (_act == "view" ? " / Запись № " + _id : (_act == "add" || _act == "copy" ? " / Новая запись" : "")) + "';");

                    _ret += "   <div class=\"panel panel-primary\" style=\"margin-bottom:10px;\">";
                    _ret += "       <div class=\"panel-heading\">";
                    _ret += "           " + cur.Caption + (_act == "view" ? " / Запись № " + _id : (_act == "add" || _act == "copy" ? " / Новая запись" : ""));
                    _ret += "           <button type=\"button\" style=\"background: transparent;border: 0; padding: 0;float: right;\" onclick=\"window.onbeforeunload = null;$('form').find('#oper').val('close');$('form').submit();\" tittle=\"Закрыть эту страницу\"><span class=\"hi hi-remove\"></span></button>";
                    _ret += "       </div>";
                    _ret += "       <div class=\"panel-body\">";
                    _ret += "           <div id=\"rrr\" style=\"margin-top:-7px;margin-bottom: 7px;float: left;\">";
                    _ret += "                <input id=\"oper\" name=\"oper\" type=\"hidden\" />";

                    string _readonly = "";//title=\"Поле недоступно для редактирования\"

                    foreach (var fld in cur.Fields)
                    {
                        _readonly = cur.EnableSaveButton && fld.Edit.Enable ? "" : " readonly";

                        _n = cur.Alias + "_" + fld.Data.FieldName;
                        if (fld.Edit.Visible && fld.Edit.Control != faControl.TextArea)
                        {
                            _ret += "<div class=\"input-group\" style=\"margin-top:8px;\"> ";
                            _ret += "   <label style=\"width:110px;float:left;\">" + fld.View.CaptionShort + (fld.Edit.Required ? " *" : "") + "</label>";

                            switch (fld.Edit.Control)
                            {
                                case faControl.TextBox:
                                case faControl.TextBoxInteger:
                                    fld.Edit.Value = (RequestPost["oper"] == "save" ? RequestPost[_n] : (_act != "add" ? (_act == "copy" && !fld.Edit.Enable ? fld.Edit.DefaultText : dt.Rows[0][fld.Data.FieldName].ToString()) : fld.Edit.DefaultText));
                                    _ret += "   <input id=\"" + _n + "\" name=\"" + _n + "\" class=\"form-control\" style=\"width: " + _inputwidth + "px;\" value=\"" + HttpUtility.HtmlEncode(fld.Edit.Value) + "\"" + " onfocus=\"$(this).select()\" " + _readonly + " />";
                                    break;

                                case faControl.TextBoxNumber:
                                    fld.Edit.Value = (RequestPost["oper"] == "save" ? RequestPost[_n] : (_act != "add" ? (_act == "copy" && !fld.Edit.Enable ? "" : ((decimal)dt.Rows[0][fld.Data.FieldName]).ToString(CultureInfo.InvariantCulture)) : ""));
                                    _ret += "   <input id=\"" + _n + "\" name=\"" + _n + "\" class=\"form-control\" style=\"width: " + _inputwidth + "px;\" value=\"" + fld.Edit.Value + "\"" + " onfocus=\"$(this).select()\" " + _readonly + " />";
                                    break;

                                case faControl.DatePicker:
                                    fld.Edit.Value = (RequestPost["oper"] == "save" ? RequestPost[_n] : (_act != "add" ? (_act == "copy" && !fld.Edit.Enable ? "" : (dt.Rows[0][fld.Data.FieldName].ToString() == "" ? "" : ((DateTime)dt.Rows[0][fld.Data.FieldName]).ToShortDateString())) : ""));

                                    _ret += "   <input id=\"" + _n + "\" name=\"" + _n + "\" class=\"form-control\" style=\"width: " + _inputwidth + "px;\" value=\"" + fld.Edit.Value + "\" " + _readonly + "/>";
                                    break;

                                case faControl.DateTimePicker:
                                    fld.Edit.Value = (RequestPost["oper"] == "save" ? RequestPost[_n] : (_act != "add" ? (_act == "copy" && !fld.Edit.Enable ? "" : dt.Rows[0][fld.Data.FieldName].ToString()) : ""));
                                    _ret += "   <input id=\"" + _n + "\" name=\"" + _n + "\" class=\"form-control\" style=\"width: " + _inputwidth + "px;\" value=\"" + fld.Edit.Value + "\" " + _readonly + "/>";
                                    break;

                                case faControl.AutoComplete:
                                case faControl.DropDown:
                                    if (RequestPost["oper"] == "save")
                                    {
                                        _v = RequestPost[_n];
                                        _vt = RequestPost["cph_" + _n];
                                    }
                                    else
                                    if ((_act == "view") || (_act == "copy" && fld.Edit.Copied))
                                    {
                                        _v = dt.Rows[0][fld.Data.FieldName].ToString();
                                        _vt = dt.Rows[0][fld.Data.FieldName + "_" + fld.LookUp.Field + "_text"].ToString();
                                    }
                                    else
                                    {
                                        _v = fld.Edit.DefaultValue;
                                        _vt = fld.Edit.DefaultText;
                                    }

                                    fld.Edit.Value = _v;
                                    //fld.Edit.Value = (RequestPost["oper"] == "save" ? RequestPost[_n] : (_act != "add" ? (_act == "copy" && !fld.Edit.Enable ? fld.Edit.DefaultValue : (fld.Edit.Copied ? dt.Rows[0][fld.Data.FieldName].ToString() : fld.Edit.DefaultValue)) : fld.Edit.DefaultValue));
                                    _ret += "   <input id=\"cph_" + _n + "\" name=\"cph_" + _n + "\" class=\"form-control\"  style=\"width: " + _inputwidth + "px;\" ";
                                    //(RequestPost["oper"] == "save" ? RequestPost["cph_" + _n] : (_act != "add" ? (_act == "copy" && !fld.Edit.Enable ? fld.Edit.DefaultText : (fld.Edit.Copied ? dt.Rows[0][fld.Data.FieldName + "_" + fld.LookUp.Field + "_text"].ToString() : fld.Edit.DefaultText)) : fld.Edit.DefaultText))
                                    _ret += "       value=\"" + _vt + "\"";
                                    _ret += "       onclick=\"$(this).select();$(this).autocomplete('search',' ');\" onBlur=\"if ($('#" + _n + "').val()=='0') $(this).val('');if ($(this).val().trim()=='')$('#" + _n + "').val('0');\" " + _readonly + " /> ";
                                    _ret += "   <input type=\"hidden\" name=\"" + _n + "\" id=\"" + _n + "\" value=\"" + fld.Edit.Value + "\">";
                                    break;

                                case faControl.TreeGrid:
                                    fld.Edit.Value = (RequestPost["oper"] == "save" ? RequestPost[_n] : (_act != "add" ? (_act == "copy" && !fld.Edit.Enable ? "" : dt.Rows[0][fld.Data.FieldName].ToString()) : ""));
                                    _ret += "   <input id=\"cph_" + _n + "\" name=\"cph_" + _n + "\" class=\"form-control\"  style=\"width: " + _inputwidth + "px;\" ";
                                    _ret += "       value=\"" + (RequestPost["oper"] == "save" ? RequestPost["cph_" + _n] : (_act != "add" ? (_act == "copy" && !fld.Edit.Enable ? "" : dt.Rows[0][fld.Data.FieldName + "_" + fld.LookUp.Field + "_text"]) : "")) + "\" ";
                                    _ret += _readonly + "/> ";
                                    _ret += "   <input type=\"hidden\" id=\"" + _n + "\" name=\"" + _n + "\" value=\"" + fld.Edit.Value + "\">";
                                    JSReadyList.Add(_n + "_tree",
                                                "$('#form" + fld.LookUp.Table + "_tree').dialog({width: 830,height: 'auto', resizable: false, autoOpen: false, modal: true, closeOnEscape: true," +
                                                " open: function(event, ui) { $(this).parent().children().children(\".ui-dialog-titlebar-close\").replaceWith('<button type=\"button\"  onclick=\"$(\\'#form" + fld.LookUp.Table + "_tree\\').dialog(\\'close\\')\" " +
                                                " class=\"btn btn-xs btn-primary\" style=\"height:21px;right:0px;position:absolute;\" title=\"Закрыть\"><span class=\"fa fa-close fa-lg\"></span></button>');} });" +
                                                "$('#" + "cph_" + _n + "').bind('click keydown', function(event) {if (event.keyCode != 9) $('#form" + fld.LookUp.Table + "_tree').dialog('open');$(window).resize();});");
                                    break;

                                case faControl.NewWindowArchive:
                                    fld.Edit.Value = (RequestPost["oper"] == "save" ? RequestPost[_n] : (_act != "add" ? (_act == "copy" && !fld.Edit.Enable ? "" : dt.Rows[0][fld.Data.FieldName].ToString()) : ""));
                                    _buf = (RequestPost["oper"] == "save" ? RequestPost["cph_" + _n].ToString() : (_act != "add" ? (_act == "copy" && !fld.Edit.Enable ? "" : dt.Rows[0][fld.Data.FieldName + "_" + fld.LookUp.Field + "_text"].ToString()) : ""));
                                    _ret += "   <input id=\"cph_" + _n + "\" name=\"cph_" + _n + "\" class=\"form-control\"  style=\"width: " + _inputwidth + "px;\" ";
                                    _ret += "       value=\"" + _buf + "\"";
                                    _ret += "       onBlur=\"if ($(this).val().trim()=='')$('#" + _n + "').val('0');\" " + _readonly + " /> ";
                                    _ret += "   <input type=\"hidden\" name=\"" + _n + "\" id=\"" + _n + "\" value=\"" + fld.Edit.Value + "\">";
                                    if (!JSFunctionList.ContainsKey("changeBut(id,name)"))
                                        JSFunctionList.Add("changeBut(id,name)", "$('#" + _n + "').val(id);$('#cph_" + _n + "').val(name);return false;");

                                    if (!JSFunctionList.ContainsKey("OpenNewWindow_" + cur.Alias + "()"))
                                        JSFunctionList.Add("OpenNewWindow_" + cur.Alias + "()",
                                            "var id_frm = $('#" + cur.Alias + "_id_frm_contr').val();" +
                                            "var id_frm_text = $('#cph_" + cur.Alias + "_id_frm_contr').val();" +
                                            //"if (id_frm>0) {"+
                                            "$.ajax({url: '/ajax/setses.aspx?key=" + this.BaseName + "_archive_select_id_frm_contr_filter&value=' + escape(id_frm)+'&key1=" + this.BaseName + "_archive_select_id_frm_contr_filter_text&value1=' + escape(id_frm_text),type: 'POST',success: function (html) {window.open('select','modal', 'width=' + (document.body.clientWidth-50) + ',height=' + (document.body.clientHeight-50)+',screenX=25,screenY=25');}});"
                                           //"}"+
                                           // "else window.open('select','modal', 'width=' + (document.body.clientWidth-50) + ',height=' + (document.body.clientHeight-50)+',screenX=25,screenY=25');"
                                           );

                                    //if (!JSFunctionList.ContainsKey("OpenNewWindow_" + cur.Alias + "()"))
                                    //    JSFunctionList.Add("OpenNewWindow_" + cur.Alias + "()",
                                    //        "var link = 'select';" +
                                    //        "window.open(link,'modal', 'width=' + (document.body.clientWidth-50) + ',height=' + (document.body.clientHeight-50)+',screenX=25,screenY=25');");
                                    ////foreach (var sfld in cur.Fields) {
                                    //// if (sfld.Data.FieldName == "id_frm_contr") {
                                    //HttpContext.Current.Session[this.BaseName + "_archive_select_id_frm_contr_filter"] = (dt.Rows[0]["id_frm_contr"] ?? "").ToString();

                                    //HttpContext.Current.Session[this.BaseName + "_archive_select_id_frm_contr_filter_text"] =
                                    //    (dt.Rows[0]["id_frm_contr_name_text"] ?? "").ToString();

                                    //}
                                    JSReadyList.Add("cph" + _n + "_keydown", "$('#cph_" + _n + "').bind('keydown click', function(event) {if (event.keyCode == 32||event.keyCode==null) OpenNewWindow_" + cur.Alias + "();});");
                                    break;

                                case faControl.NewWindowArchiveID:
                                    fld.Edit.Value = (RequestPost["oper"] == "save" ? RequestPost[_n] : (_act != "add" ? (_act == "copy" && !fld.Edit.Enable ? "" : dt.Rows[0][fld.Data.FieldName].ToString()) : ""));
                                    _ret += "   <input id=\"" + _n + "\" name=\"" + _n + "\" class=\"form-control\"  style=\"width: " + _inputwidth + "px;\" ";
                                    _ret += "       value=\"" + fld.Edit.Value + "\" " + _readonly + " /> ";

                                    JSReadyList.Add("cph" + _n + "_keydown", "$('#" + _n + "').bind('keydown click', function(event) {if (event.keyCode == 32||event.keyCode==null) OpenNewWindow2_" + cur.Alias + "();});");
                                    JSFunctionList.Add("changeBut2(id,name)", "$('#" + _n + "').val(id);return false;");
                                    //JSFunctionList.Add("OpenNewWindow2_" + cur.Alias + "()",
                                    //    "var id_frm = $('#" + cur.Alias + "_id_frm_contr').val();" +
                                    //    "var id_frm_text = $('#cph_" + cur.Alias + "_id_frm_contr').val();" +
                                    //    "var link = 'select?m=2';" +
                                    //    "if (id_frm>0) link = link + '&id_frm_contr=' + id_frm + '&id_frm_contr_t=' + id_frm_text;" +
                                    //    "window.open(link,'modal', 'width=' + (document.body.clientWidth-50) + ',height=' + (document.body.clientHeight-50)+',screenX=25,screenY=25');");
                                    if (!JSFunctionList.ContainsKey("OpenNewWindow2_" + cur.Alias + "()"))
                                        JSFunctionList.Add("OpenNewWindow2_" + cur.Alias + "()",
                                            "var id_frm = $('#" + cur.Alias + "_id_frm_contr').val();" +
                                            "var id_frm_text = $('#cph_" + cur.Alias + "_id_frm_contr').val();" +
                                            //"if (id_frm>0) {"+
                                            "$.ajax({url: '/ajax/setses.aspx?key=" + this.BaseName + "_archive_select_id_frm_contr_filter&value=' + escape(id_frm)+'&key1=" + this.BaseName + "_archive_select_id_frm_contr_filter_text&value1=' + escape(id_frm_text),type: 'POST',success: function (html) {window.open('select?m=2','modal', 'width=' + (document.body.clientWidth-50) + ',height=' + (document.body.clientHeight-50)+',screenX=25,screenY=25');}});"
                                           //"}"+
                                           // "else window.open('select','modal', 'width=' + (document.body.clientWidth-50) + ',height=' + (document.body.clientHeight-50)+',screenX=25,screenY=25');"
                                           );
                                    break;

                                default:
                                    break;
                            }
                            _ret += "</div>";
                        }
                    }
                    //<br/> Уведомления:<br/>
                    //_ret += " <div class=\"panel panel-default\" style=\"margin-top: 5px;\">";
                    //_ret += "<div class=\"panel-body\">";
                    if (_act == "add" || _act == "copy" || !cur.EnableSaveButton)
                    {
                        _ret += "<button type=\"button\" title=\"Уведомление исполнителя о создании комплекта\" class=\"btn btn-sm btn-default\" style=\"width:325px;margin-bottom: 5px;\" disabled>" +
                            "Отправить исполнителю</button>";
                        //_ret += "<br/><button type=\"button\" title=\"Уведомление бухгалтера-ревизора о создании комплекта\" class=\"btn btn-sm btn-default\" style=\"width:290px;margin-bottom: 5px;\" disabled>" +
                        //    "Отправить бухгалтеру-ревизору</button>";
                        //_ret += "<br/><button type=\"button\" title =\"Уведомление сотрудника ОЦ о получении документов\" class=\"btn btn-sm btn-default\" style=\"width:290px;margin-bottom: 5px;\" disabled>" +
                        //    "Отправить сотруднику ОЦ</button>";
                        //_ret += "<br/><button type=\"button\" title=\"Уведомление терминального поста о занесении документов ЭА \" class=\"btn btn-sm btn-default\" style=\"width:290px;\" disabled>" +
                        //    "Отправить терминальному посту</button>";
                    }
                    else
                    {
                        bool pm;
                        // bool bm;
                        // bool om;
                        // bool tm;
                        pm = (bool)dt.Rows[0]["perf_mail"];
                        //bm = (bool)dt.Rows[0]["buh_mail"];
                        //om = (bool)dt.Rows[0]["oc_mail"];
                        //tm = (bool)dt.Rows[0]["term_mail"];

                        _ret += "<button type=\"button\" title=\"Уведомление исполнителя о создании комплекта\" class=\"btn btn-sm btn-" +
                            (pm ? "success" : "default") + "\" style=\"width:325px;margin-bottom: 5px;\" onclick=\"SendMailGroup('pm');\">" +
                            (pm ? "Отправлено" : "Отправить") + " исполнителю</button>";
                        //_ret += "<br/><button type=\"button\" title=\"Уведомление бухгалтера-ревизора о создании комплекта\" class=\"btn btn-sm btn-" +
                        //    (bm ? "success" : "default") + "\" style=\"width:290px;margin-bottom: 5px;\" onclick=\"SendMailGroup('bm');\">" +
                        //    (bm ? "Отправлено" : "Отправить") + " бухгалтеру-ревизору</button>";
                        //_ret += "<br/><button type=\"button\" title =\"Уведомление сотрудника ОЦ о получении документов\" class=\"btn btn-sm btn-" +
                        //    (om ? "success" : "default") + "\" style=\"width:290px;margin-bottom: 5px;\" onclick=\"SendMailGroup('om');\">" +
                        //    (om ? "Отправлено" : "Отправить") + " сотруднику ОЦ</button>";
                        //_ret += "<br/><button type=\"button\" title=\"Уведомление терминального поста о занесении документов ЭА \" class=\"btn btn-sm btn-" +
                        //    (tm ? "success" : "default") + "\" style=\"width:290px;\" onclick=\"SendMailGroup('tm');\">" +
                        //    (tm ? "Отправлено" : "Отправить") + " терминальному посту</button>";

                        JSFunctionList.Add("SendMailGroup(gr)",
                        "  if (confirm('Отправить уведомление?')) {" +
                        "$.ajax({" +
                               "   url: '/ajax/SendMailComplect.aspx?ru='+escape('" + HttpContext.Current.Request.Url + "')+'&id=" +
                               _id + "&gr='+gr+'&id_creator=" + dt.Rows[0]["id_creator"].ToString().Trim() + "&id_perf=" + dt.Rows[0]["id_creator"].ToString().Trim() + "'," +
                               "   type: 'POST'," +
                               "       cache: false," +
                               "       contentType: false," +
                               "       processData: false," +
                               "       success: function (html) {alert(html);window.onbeforeunload = null;str='" + HttpContext.Current.Request.Url + "';window.location=str.replace('&saved=ok','');}," +
                               "       error: function (request, status, error) {  alert(request.responseText); }" +
                               "   });" +
                        "}"
                       );
                    }
                    // _ret += "</div></div>";

                    #region Подпись

                    _ret += "<br/>";

                    JSReadyList.Add("hidesigninput", "$('#_complectnew_id_sign').parent().hide();$('#_complectnew_whensign').parent().hide();");

                    if (_act == "add" || _act == "copy" || !cur.EnableSaveButton)
                    {
                        _ret += "<button type=\"button\" class=\"btn btn-sm btn-default\" style=\"width:325px;\" disabled >Подпись исполнителем</button><br/><br/>";
                    }
                    else
                    {
                        bool signed = (int)dt.Rows[0]["id_sign"] > 0;
                        if (signed)
                        {
                            _ret += "<button id=\"scanbutton\" type=\"button\" title=\"Подпись исполнителем, посредством сканера шрих-кодов\" class=\"btn btn-sm btn-success\"" +
                                " style=\"width:325px;white-space: normal;\">Подписано:" + dt.Rows[0]["id_sign_name_text"].ToString() + " (" + ((DateTime)dt.Rows[0]["whensign"]).ToString("dd.MM.yyyy HH:mm") + ")</button><br/><br/>";
                            //JSReadyList.Add("disableinput", "$('#cph__complect_repair_id_perf').prop('disabled',true);");
                            JSReadyList.Add("disableinput",
                                "$('#cph__complectnew_id_base').prop('readonly',true).prop('onclick','').prop('onblur','');" +
                                "$('#cph__complectnew_inet').prop('readonly',true).prop('onclick','').prop('onblur','');" +
                                "$('#cph__complectnew_id_perf').prop('readonly',true).prop('onclick','').prop('onblur','');" +
                                "$('#cph__complectnew_important').prop('readonly',true).prop('onclick','').prop('onblur','');" +
                                "$('#_complectnew_name').prop('readonly',true).prop('onclick','').prop('onblur','');" +
                                "$('#cph_jqGrid_complectnew_reg_pager_left').find('.ui-pg-button').hide();" +
                                "$('#cph_jqGrid_complectnew_reg_pager_left').find('.ui-pg-button:contains(\"Торг\")').show();" +
                                "$('.ui-dialog-buttonset').find('.ui-pg-button:contains(\"Торг\")').show();" +
                                "$('#form_complectnew_reg_edit').dialog().remove();" +
                                (HttpContext.Current.Session["common_access_service_userbarcode"] == null ? "$('#btnDel').prop('disabled',true);" : "")
                                );
                        }
                        else
                        {
                            _ret += "<button id=\"scanbutton\" type=\"button\" title=\"Подпись исполнителем, посредством сканера шрих-кодов\" class=\"btn btn-sm btn-default\"" +
                            " style=\"width:325px;white-space: normal;\" " +
                            "onclick=\"if($('#_complectnew_inet').val()=='8'){ if($(this).val()=='wait'){$(this).val('sleep');unbind_event();}else{$(this).val('wait');bind_event();}$(this).blur();}else alert('Используется только для электронного реестра!');\"" +
                           (HttpContext.Current.Session["common_access_complect_sign"] == null ? " disabled " : "") +
                            ">Подпись исполнителем</button><br/><br/>";
                        }
                        //"<input id=\"target\" type=\"text\" value=\"Hello there\">";
                        //

                        JSFunctionList.Add("bind_event()",
                         "$('#scanbutton').html('<img src=\\'/styles/images/loading.gif\\'>&nbsp; Ожидание ввода..');" +
                         "var pressed = false; " +
                         "var chars = []; " +
                         "$(document).keypress(function(e) {" +
                         "    if (e.which >= 48 && e.which <= 57) {" +
                         "        chars.push(String.fromCharCode(e.which));" +
                         "    }" +
                         // "    console.log(e.which + ':'+ chars.join('|'));" +
                         "    if (pressed == false) {" +
                         "        setTimeout(function(){" +
                         "            if (chars.length == 10) {" +
                         "                var barcode = chars.join('');" +
                         //"                console.log('Barcode Scanned: ' + barcode);" +
                         "                  $.ajax({url: '/ajax/get_user_by_barcode.aspx?&barcode=' + escape(barcode),type: 'POST'," +
                                                    "success: function (html) {" +
                                                        "arr=html.split('|'); " +
                                                        "if(arr[0]==0){alert('Исполнитель с таким штрихкодом не найден.');}" +
                                                        "else{if (confirm('Подписать исполнителем - ' + arr[1] + '?')){$('#_complectnew_id_sign').val(arr[0]);$('#cph__complectnew_id_sign').val(arr[1]);$('#_complectnew_whensign').val(formatDateTime(new Date()));unbind_event();$('#savebutton').click();}}}});" +
                         // "                alert(barcode);" +
                         "            }" +
                         "            chars = [];" +
                         "            pressed = false;" +
                         "        },500);" +
                         "    }" +
                         "    pressed = true;" +
                         "});");

                        JSFunctionList.Add("unbind_event()", "$('#scanbutton').text('Подпись исполнителем');$(document).unbind('keypress');");
                    }

                    #endregion Подпись

                    _ret += "</div>";

                    foreach (var fld in cur.Fields)
                    {
                        _n = cur.Alias + "_" + fld.Data.FieldName;
                        if (fld.Edit.Enable && cur.EnableSaveButton && fld.Edit.Control != faControl.TextArea)
                        {
                            switch (fld.Edit.Control)
                            {
                                case faControl.TextBox:
                                case faControl.TextBoxInteger:
                                    break;

                                case faControl.TextBoxNumber:
                                    JSReadyList.Add("cph" + _n + "_blur", "$('#" + _n + "').val(accounting.formatNumber($('#" + _n + "').val().trim(), 2, ' '));$('#" + _n + "').bind('blur',function(event) {this.value=accounting.formatNumber(this.value.trim(), 2, ' '); });");
                                    JSReadyList.Add("cph" + _n + "_focus", "$('#" + _n + "').bind('focus',function(event) {this.value=accounting.unformat(this.value.trim()); });");
                                    break;

                                case faControl.DatePicker:
                                    JSReadyList.Add(_n + "_datepicker()", "$('#" + _n + "').datepicker({" +
                                                    "changeMonth: true,changeYear: true," +
                                                    "onSelect: function(dateText, inst) { $(this).parent().focus();}," +
                                                    //"onClose: function(dateText, inst) {this.fixFocusIE = true; this.focus();}," +
                                                    //"beforeShow: function(input, inst) {var ua = detect.parse(navigator.userAgent);var result = ua.browser.family=='IE' ? !this.fixFocusIE : true;this.fixFocusIE = false; return result;}" +
                                                    "});");
                                    break;

                                case faControl.DropDown:
                                    JSReadyList.Add(_n + "_autocomplete",
                                                "$('#cph_" + _n + "').autocomplete({" +
                                                "       source: '/ajax/dd.aspx?table=" + fld.LookUp.Table + "&where=" + fld.LookUp.Where + "'," +
                                                "       minLength: 1," +
                                                "       delay: 10," +
                                                "       select: function (event, ui) {" +
                                                "           $(\"#" + _n + "\").val(ui.item." + fld.LookUp.Key + ");" +
                                                "           $(\"#cph_" + _n + "\").val(ui.item.name);" +
                                                "           return false;}" +
                                                "});");
                                    //JSReadyList.Add("cph" + _n + "keyup", "$('#cph_" + _n + "').bind('change keyup input',function(event) {this.value=' ';$('#" + _n + "').val('0');});");
                                    break;

                                case faControl.AutoComplete:
                                    JSReadyList.Add(_n + "_autocomplete",
                                                "$('#cph_" + _n + "').autocomplete({" +
                                                "       source: '/ajax/ac.aspx?table=" + fld.LookUp.Table + "'," +
                                                "       minLength: 1," +
                                                "       delay: 10," +
                                                "       select: function (event, ui) {" +
                                                "           $(\"#" + _n + "\").val(ui.item." + fld.LookUp.Key + ");" +
                                                "           $(\"#cph_" + _n + "\").val(ui.item.name);" +
                                                "           return false;}" +
                                                "});");
                                    JSReadyList.Add("cph" + _n + "keyup", "$('#cph_" + _n + "').bind('change',function(event) {if (this.value.trim()=='')$('#" + _n + "').val('0');});");
                                    break;

                                case faControl.TreeGrid:

                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                    //if ((RequestPost["oper"] ?? "") != "close")
                    // JSReadyList.Add("window_unload", "window.onbeforeunload = function() {return 'Для закрытия окна используйте кнопку «Закрыть» или «крестик» в углу формы';};");
                    //$(window).unload(function() {alert('Handler for .unload() called.');});");

                    #endregion Шапка

                    #region Курсоры

                    string _tabs = "";
                    string _tabsdiv = "";

                    if (CursorCount > 0)
                    {
                        List<string> list = new List<string>(Cursors.Keys);
                        foreach (string k in list)
                        {
                            Cursors[k].Fields[1].Filter.Condition = "=";
                            Cursors[k].Fields[1].Filter.Value = _id; // к связывающему полю

                            _tabs += "<li><a href=\"#tab" + k + "\">" + Cursors[k].Caption + "</a></li>";
                            _tabsdiv += "<div id=\"tab" + k + "\"></div>";

                            JSReadyList.Add("cph_jqGrid" + k + "_tab", "$(\"#gbox_" + "cph_jqGrid" + k + "\").appendTo($(\"#tab" + k + "\"));");

                            _ret += "<div class=\"modal_form\" id=\"form" + k + "_edit\" style=\"display:none;\" >";
                            _ret += "   <input type=\"hidden\" id=\"oper\" name=\"oper\" value=\"\"/>";
                            _ret += "   <input type=\"hidden\" id=\"id\" name=\"id\" value=\"\"/>";
                            _ret += "   <div id=\"dialog_" + k + "_error\" style=\"font-style: italic;color:#ff0000;display:none;\"></div> ";

                            foreach (var fld in Cursors[k].Fields)
                            {
                                if (fld.Edit.Visible)
                                {
                                    _readonly = cur.EnableSaveButton && Cursors[k].EnableSaveButton && fld.Edit.Enable ? "" : " readonly";
                                    _ret += "<div class=\"input-group\" style=\"margin-top:8px;\"> ";
                                    _ret += "   <label style=\"width:110px;float:left;\">" + fld.View.CaptionShort + "</label>";
                                    _n = k + "_" + fld.Data.FieldName;
                                    // Поля
                                    switch (fld.Edit.Control)
                                    {
                                        case faControl.TextBox:
                                        case faControl.TextBoxNumber:
                                        case faControl.TextBoxInteger:
                                        case faControl.DatePicker:
                                        case faControl.DateTimePicker:
                                            _ret += "   <input id=\"" + _n + "\" name=\"" + _n + "\" class=\" form-control \" style=\"width: 200px;\"" +
                                                    "        " + _readonly + " />";
                                            break;

                                        case faControl.CheckBox:
                                            _ret += "   <input id=\"" + _n + "\" name=\"" + _n + "\" type =\"checkbox\" value=\"1\" " + (_readonly != "" ? " onclick='return false'" : "") + " />";
                                            break;

                                        case faControl.AutoComplete:
                                        case faControl.DropDown:
                                            _ret += "   <input id=\"cph_" + _n + "\" name=\"cph_" + _n + "\" class=\"form-control\" style=\"width: 200px;\"";
                                            _ret += "        onBlur=\"if ($('#" + _n + "').val()=='0') {$(this).val('');$('#text_" + _n + "');}\" " + _readonly + " /> ";
                                            _ret += "   <input type=\"hidden\" id=\"text_" + _n + "\" name=\"text_" + _n + "\"/>";
                                            _ret += "   <input type=\"hidden\" id=\"" + _n + "\" name=\"" + _n + "\"/>";
                                            break;

                                        case faControl.TreeGrid:
                                            _ret += "   <input id=\"cph_" + _n + "\" name=\"cph_" + _n + "\" class=\"form-control\" style=\"width: 200px;\"";
                                            _ret += "       onclick=\"$('#form" + fld.LookUp.Table + "_tree').dialog('open');$(window).resize();\" " + _readonly + "/> ";
                                            _ret += "   <input type=\"hidden\" id=\"text_" + _n + "\" name=\"text_" + _n + "\"/>";
                                            _ret += "   <input type=\"hidden\" id=\"" + _n + "\" name=\"" + _n + "\"/>";
                                            break;

                                        case faControl.NewWindowArchive:
                                            _ret += "   <input id=\"" + _n + "\" name=\"" + _n + "\" class=\"form-control\" style=\"width: 200px;\"";
                                            _ret += "        " + _readonly + " />";
                                            // _ret += "        onBlur=\"if ($('#" + _n + "').val()=='0') {$(this).val('');$('#text_" + _n + "');}\" " + _readonly + " /> ";
                                            //_ret += "   <input type=\"hidden\" id=\"text_" + _n + "\" name=\"text_" + _n + "\"/>";
                                            //_ret += "   <input type=\"hidden\" id=\"" + _n + "\" name=\"" + _n + "\"/>";
                                            if (!JSFunctionList.ContainsKey("changeBut3(id,name)"))
                                                JSFunctionList.Add("changeBut3(id,name)", "$('#" + _n + "').val(id);$('#cph_" + _n + "').val(id);return false;");
                                            if (!JSFunctionList.ContainsKey("OpenNewWindow3_" + cur.Alias + "()"))
                                                JSFunctionList.Add("OpenNewWindow3_" + cur.Alias + "()",
                                                    "$.ajax({url: '/ajax/setses.aspx?key=" + this.BaseName + "_archive_select_id_doctree_filter&value=(7)&key1=" + this.BaseName + "_archive_select_id_doctree_filter_text&value1=' + escape('Бухгалтерские документы'),type: 'POST',success: function (html) {window.open('select?m=3','modal', 'width=' + (document.body.clientWidth-50) + ',height=' + (document.body.clientHeight-50)+',screenX=25,screenY=25');}});"
                                                   );
                                            JSReadyList.Add(_n + "_keydown", "$('#" + _n + "').bind('keydown click', function(event) {if (event.keyCode == 32||event.keyCode==null) OpenNewWindow3_" + cur.Alias + "();});");
                                            break;

                                        case faControl.File:
                                            _ret += "<iframe id=\"iframe_" + _n + "\" src=\"\" style=\"width: 221px;height: 25px;border: none;\"></iframe>";
                                            _ret += "<input type=\"hidden\" id=\"" + _n + "\" name=\"" + _n + "\"/>";
                                            _fileuploader = _n;
                                            break;

                                        default:
                                            break;
                                    }
                                    // Обработчики полей
                                    if (fld.Edit.Enable && cur.EnableSaveButton && Cursors[k].EnableSaveButton)
                                        switch (fld.Edit.Control)
                                        {
                                            case faControl.TextBox:
                                                if (fld.Edit.Max > 0)
                                                    JSReadyList.Add(_n + "Max", "$('#" + _n + "').bind('change input'," +
                                                        "function(event) {var v=this.value.trim(); if (v.length>" + fld.Edit.Max + "){ v.slice(0, " + fld.Edit.Max + ");this.value=v;}});");
                                                break;

                                            case faControl.TextBoxNumber:
                                                JSReadyList.Add(_n + "TextBoxNumber", "$('#" + _n + "').bind('change input'," +
                                                    "function(event) {this.value=this.value.replace(',','.').replace(/\\.(?=.*\\.)|[^\\d\\.-]/g,'');});");
                                                break;

                                            case faControl.TextBoxInteger:
                                                JSReadyList.Add(_n + "TextBoxInteger", "$('#" + _n + "').bind('change input'," +
                                                    "function(event) {this.value=this.value.replace(/\\D+/g,\"\");});");
                                                break;

                                            case faControl.DatePicker:
                                                JSReadyList.Add(_n + "_datepicker()", "$('#" + _n + "').datepicker({" +
                                                    "changeMonth: true,changeYear: true," +
                                                    "onSelect: function(dateText, inst) { $(this).parent().focus();}," +
                                                    //"onClose: function(dateText, inst) {this.fixFocusIE = true; this.focus();}," +
                                                    //"beforeShow: function(input, inst) {var ua = detect.parse(navigator.userAgent);var result = ua.browser.family=='IE' ? !this.fixFocusIE : true;this.fixFocusIE = false; return result;}" +
                                                    "});");
                                                break;

                                            case faControl.DateTimePicker:
                                                JSReadyList.Add(_n + "_datetimepicker()", "$('#" + _n + "').datetimepicker();");
                                                break;

                                            case faControl.AutoComplete:
                                                JSReadyList.Add(_n + "_autocomplete",
                                                    "$('#cph_" + _n + "').autocomplete({" +
                                                    "       source: '/ajax/ac.aspx?table=" + fld.LookUp.Table + "&where=" + fld.LookUp.Where + "'," +
                                                    "       minLength: 1, delay: 10," +
                                                    "       select: function (event, ui) {" +
                                                    "           $(\"#" + _n + "\").val(ui.item." + fld.LookUp.Key + ");" +
                                                    "           $(\"#cph_" + _n + "\").val(ui.item.name);" +
                                                    "           $(\"#text_" + _n + "\").val(ui.item.name);" +
                                                    "           return false;}" +
                                                    "});");
                                                JSReadyList.Add("cph" + _n + "keyup", "$('#cph_" + _n + "').bind('change',function(event) {if (this.value.trim()=='')$('#" + _n + "').val('0');});");
                                                JSReadyList.Add("cph" + _n + "click", "$('#cph_" + _n + "').bind('click',function(event) {$('#cph_" + _n + "').autocomplete('search',' ');});");
                                                break;

                                            case faControl.DropDown:
                                                JSReadyList.Add(_n + "_dropdown",
                                                    "$('#cph_" + _n + "').autocomplete({" +
                                                    "       source: '/ajax/dd.aspx?table=" + fld.LookUp.Table + "&where=" + fld.LookUp.Where + "'," +
                                                    "       minLength: 1, delay: 10," +
                                                    "       select: function (event, ui) {" +
                                                    "           $(\"#" + _n + "\").val(ui.item." + fld.LookUp.Key + ");" +
                                                    "           $(\"#cph_" + _n + "\").val(ui.item.name);" +
                                                    "           $(\"#text_" + _n + "\").val(ui.item.name);" +
                                                    "           return false;}" +
                                                    "});");
                                                JSReadyList.Add("cph" + _n + "keyup", "$('#cph_" + _n + "').bind('change keyup input',function(event) {this.value=' ';$('#" + _n + "').val('0');});");
                                                JSReadyList.Add("cph" + _n + "click", "$('#cph_" + _n + "').bind('click',function(event) {$('#cph_" + _n + "').autocomplete('search',' ');});");
                                                break;

                                            case faControl.TreeGrid:
                                                JSReadyList.Add(_n + "_tree",
                                                    "$('#form" + fld.LookUp.Table + "_tree').dialog({width: 830,height: 'auto', resizable: false, autoOpen: false, modal: true, closeOnEscape: true," +
                                                    " open: function(event, ui) { $(this).parent().children().children(\".ui-dialog-titlebar-close\").replaceWith('<button type=\"button\"  onclick=\"$(\\'#form" + fld.LookUp.Table + "_tree\\').dialog(\\'close\\')\" " +
                                                    " class=\"btn btn-xs btn-primary\" style=\"height:21px;right:0px;position:absolute;\" title=\"Закрыть\"><span class=\"fa fa-close fa-lg\"></span></button>');} });" +
                                                    "$('#" + "cph_" + _n + "').bind('keydown', function(event) {if (event.keyCode != 9) $('#form" + fld.LookUp.Table + "_tree').dialog('open');$(window).resize();});");
                                                break;

                                            default:
                                                break;
                                        }
                                    _ret += "</div>";
                                }
                            }
                            _ret += "</div>";
                            if (k == "_complectnew_list")
                            {
                                JSReadyList.Add("form" + k + "dialog_nextbuttonbind",
                                "$('#" + k + "_barcode').keypress(function(event){" +
                                "var keycode = (event.keyCode ? event.keyCode : event.which);" +
                                "if(keycode == '13'){" +
                                "   $.ajax({" +
                                "   url: '/ajax/InsertFromScaner.aspx?id=" + _id + "&cur=" + k + "_cursor_" + _id + "&idlist='+$('#" + k + "_id').val()," +
                                "   type: 'POST'," +
                                "       data: $('#" + k + "_barcode').val()," +
                                "       cache: false," +
                                "       contentType: false," +
                                "       processData: false," +
                                "       success: function (html) { if (html.length>0){alert(html);} else NavigateNext($('#" + k + "_barcode').val());}," +
                                "       error: function (request, status, error) {  alert(request.responseText); }" +
                                "   });" +
                                "}});");
                                JSFunctionList.Add("NavigateNext(barcode)",
                                    "var grid = $('#cph_jqGrid" + k + "');" +
                                    "var gridArr = grid.getDataIDs();" +
                                    "var selrow = grid.getGridParam('selrow');" +
                                    "grid.setCell(selrow,'barcode',barcode);" +
                                    "var curr_index = 0;" +
                                    "$('#form" + k + "_edit').dialog('close');" +
                                    "for (var i = 0; i < gridArr.length; i++) {if (gridArr[i] == selrow)curr_index = i;}" +
                                    "if ((curr_index + 1) < gridArr.length){grid.resetSelection().setSelection(gridArr[curr_index + 1], true);ViewRow" + k + "();}");
                                //#test_complectnew_list_barcode
                            }

                            JSReadyList.Add("form" + k + "dialog",
                            "$('#form" + k + "_edit').dialog({" +
                                "width: 360," +
                                "height: 'auto'," +
                                "resizable: false," +
                                "autoOpen: false," +
                                "modal: true," +
                                "closeOnEscape: true," +
                                "focus: function() {" +
                                    "var dialogIndex = parseInt($(this).parent().css('z-index'), 10);" +
                                    "$(this).find('.ui-autocomplete-input').each(function(i, obj) {" +
                                        "$(obj).autocomplete('widget').css('z-index', dialogIndex + 1)" +
                                    "});" +
                                "}," +
                                "buttons: {" +

                                    (Cursors[k].EnableSaveButton ?
                                    "'Save': {" +
                                        "text: 'Добавить'," +
                                        "style: 'width:75px'," +
                                        "class: 'btn btn-xs btn-primary'," +
                                        "click: function() {" +
                                            (_fileuploader != "" ?
                                                "var v = $('#iframe_" + _fileuploader + "').contents().find('#temp_file').val();" +
                                                "if (v!='')$('#" + _fileuploader + "').val(v);"
                                            : "") +
                                            "var res = $('#form" + k + "_edit').find(':input').serializeArray();" +
                                            "$.ajax({" +
                                                "type: 'POST'," +
                                                "url: location.href + '&jqGridID=cph_jqGrid" + k + "&editMode=1'," +
                                                "data: res," +
                                                "error: function(data) {" +
                                                    "$('#dialog_" + k + "_error').hide();$('#dialog_" + k + "_error').html(data.responseText).show('fast');" +
                                                "}," +
                                                "success: function(data) {" +
                                                    "$('#cph_jqGrid" + k + "').trigger('reloadGrid');" +
                                                    
                                                    (k == "_complectnew_reg" ? "$('#form" + k + "_edit').dialog('close');if ($('#form" + k + "_edit').parent().find('button:contains(Добавить)').text()=='Добавить'){AddRow" + k + "();$('#_complectnew_reg_name').focus();}" : "$('#form" + k + "_edit').dialog('close');") +

                                                "}" +
                                            "});" +
                                        "}" +
                                        "}," : "") +
                                    "'Close': {" +
                                        "text: 'Закрыть'," +
                                        "style: 'width:75px'," +
                                        "class: 'btn btn-xs btn-default'," +
                                        "click: function() {" +
                                            "$('#form" + k + "_edit').dialog('close');" +
                                        "}" +
                                    "}" +
                                "}," +
                                "open: function(event, ui) {" +
                                       (_fileuploader != "" ? "var state = $('#" + _fileuploader + "').val()==''?'default':'success'; $('#iframe_" + _fileuploader + "').attr('src','/ajax/Uploadfile.aspx?state='+state);" : "") +
                                    "$('#dialog_" + k + "_error').hide();" +
                                    "$(this).parent().children().children('.ui-dialog-titlebar-close').hide();" +
                                    "$('#" + k + "_barcode').focus().select();" +
                                "}" +
                            "});");
                        }
                        JSReadyList.Add("activatetab", "$('#tabs').tabs();");
                    }

                    foreach (var fld in cur.Fields)
                    {
                        if (fld.Edit.Visible && fld.Edit.Control == faControl.TextArea)
                        {
                            _tabs += "<li><a href=\"#tab" + fld.Data.FieldName + "\">" + fld.View.CaptionShort + "</a></li>";
                            _tabsdiv += "<div id=\"tab" + fld.Data.FieldName + "\"><textarea id=\"" + cur.Alias + "_" + fld.Data.FieldName + "\" name=\"" + cur.Alias + "_" + fld.Data.FieldName + "\" class=\"form-control\">";
                            _tabsdiv += (_act != "add" ? (RequestPost["oper"] != null ? RequestPost[cur.Alias + "_" + fld.Data.FieldName] : (_act != "copy" ? dt.Rows[0][fld.Data.FieldName] : "")) : "");
                            _tabsdiv += "</textarea></div>";
                        }
                    }
                    if (_tabs != "")
                    {
                        _ret += "<div id=\"tabs\" style=\"float: left;margin-left: 15px;\">";
                        _ret += "<ul>";
                        _ret += _tabs;
                        _ret += "</ul>";
                        _ret += _tabsdiv;
                        _ret += "</div>";
                    }

                    _ret += "       </div>";//panel-body
                    _ret += "   </div>";//panel

                    #endregion Курсоры

                    #region Футер с кнопками

                    _ret += "   <div id=\"button_panel\" >";
                    _ret += "       <div id=\"left_block\" style=\"float:left;width:" + (this.EditFormWidth > _maxwidth ? "445" : "187") + "px;\">";
                    _ret += "           <button type=\"button\" id=\"btnAdd\" onclick=\"btnAddClick();\" class=\"btn btn-xs btn-primary\"  title=\"Создать новую запись\"";
                    if (MainCursor.EnableAddButton)
                    {
                        JSFunctionList.Add("btnAddClick()", "window.open((location.href.indexOf('\\?')>-1?location.href.slice(0,location.href.indexOf('\\?')):location.href.replace('#','')) + '?id=0&act=add');return false;");
                    }
                    else
                        _ret += " disabled";

                    _ret += "><span class=\"hi hi-plus\"></span>" + (this.EditFormWidth > _maxwidth ? "&nbsp;&nbsp;Новая" : "") + "</button>&nbsp;";
                    _ret += "           <button type=\"button\" id=\"btnCopy\" onclick=\"btnCopyClick();\" class=\"btn btn-xs btn-primary\"  title=\"Создать новую запись копированием текущей\"";
                    if (MainCursor.EnableCopyButton && _act == "view")
                    {
                        JSFunctionList.Add("btnCopyClick()", "window.open((location.href.indexOf('\\?')>-1?location.href.slice(0,location.href.indexOf('\\?')):location.href.replace('#','')) + '?id=0&from=' + " + _id + " + '&act=copy');return false;");
                    }
                    else
                        _ret += " disabled";
                    _ret += "><span class=\"gi gi-more_items\"></span>" + (this.EditFormWidth > _maxwidth ? "&nbsp;&nbsp;Копировать" : "") + "</button>&nbsp;";
                    _ret += "           <button type=\"button\" id=\"btnDel\" class=\"btn btn-xs btn-primary\" title=\"Удалить запись\"";
                    if (MainCursor.EnableDelButton && _act == "view")
                    {
                        _ret += " onclick=\"if (confirm('Подтвердите удаление')){window.onbeforeunload = null;$('form').find('#oper').val('delete');$('form').submit();}\" ";
                        //JSFunctionList.Add("btnDelClick()", "window.open((location.href.indexOf('\\?')>-1?location.href.slice(0,location.href.indexOf('\\?')):location.href.replace('#','')) + '?id=' + " + _id + " + '&act=del');return false;");
                    }
                    else
                        _ret += " disabled";
                    _ret += "><span class=\"hi hi-trash\"></span>" + (this.EditFormWidth > _maxwidth ? "&nbsp;&nbsp;Удалить" : "") + "</button>&nbsp;";

                    if (ActionMenuItems.Count > 0)
                    {
                        _ret += "       <div class=\"btn-group dropup\">";
                        _ret += "          <button type=\"button\" class=\"btn btn-primary btn-xs dropdown-toggle\" data-toggle=\"dropdown\">";
                        _ret += "              <span class=\"gi gi-justify\"></span>" + (this.EditFormWidth > _maxwidth ? "&nbsp;&nbsp;Действия" : "") + "&nbsp;&nbsp;";
                        _ret += "          </button>";
                        _ret += "          <ul class=\"dropdown-menu\" role=\"menu\">";
                        List<string> list = new List<string>(ActionMenuItems.Keys);
                        foreach (string k in list)
                        {
                            _ret += k != "-" ? " <li><a href=\"#\" onclick=\"" + ActionMenuItems[k] + "\">" + k + "</a></li>" : "<li class=\"divider\"></li>";
                        }
                        _ret += "           </ul>";
                        _ret += "       </div>";
                    }

                    #region Стрелки

                    if (ShowArrows && _act == "view")
                    {
                        _ret += "           <button type=\"button\" id=\"btn_prev\" class=\"btn btn-xs btn-primary\" style=\"margin-left: " + (this.EditFormWidth > _maxwidth ? "50" : "20") + "px;\" onclick=\"window.onbeforeunload = null;$('form').find('#oper').val('prev');$('form').submit();\" title=\"Предыдущая запись выборки\n(CTRL + Стрелка влево)\"><span class=\"gi gi-left_arrow\"></span></button>&nbsp;";
                        _ret += "           <button type=\"button\" id=\"btn_next\" class=\"btn btn-xs btn-primary\" onclick=\"window.onbeforeunload = null;$('form').find('#oper').val('next');$('form').submit();\" title=\"Следующая запись выборки\n(CTRL + Стрелка вправо)\"><span class=\"gi gi-right_arrow\"></span></button>";
                        JSReadyList.Add("hotkey",
                            "$(document).keyup(function(evt)" +
                            "{" +
                            "if (evt.keyCode == 37 && (evt.ctrlKey)){$('#btn_prev').click();}" +
                            "if (evt.keyCode == 39 && (evt.ctrlKey)) {$('#btn_next').click();}" +
                            "return false;" +
                            "});");
                    }

                    #endregion Стрелки

                    _ret += "       </div>";
                    _ret += "       <div id=\"right_block\" style=\"float:right;width:" + (this.EditFormWidth > _maxwidth ? "165" : "80") + "px;\">";
                    _ret += "           <div class=\"btn-group\">";
                    _ret += "               <button id=\"savebutton\" type=\"button\" class=\"btn btn-xs btn-primary\" style=\"width: 80px;\" ";
                    _ret += (MainCursor.EnableSaveButton) ? " onclick=\"window.onbeforeunload = null;$('form').find('#oper').val('save');$('form').submit();\"" : " disabled ";
                    _ret += "               >Сохранить</button>";
                    _ret += "           </div>&nbsp;";
                    _ret += this.EditFormWidth > _maxwidth ? "           <button type=\"button\" class=\"btn btn-xs btn-default\" style=\"width: 80px;\" onclick=\"window.onbeforeunload = null;$('form').find('#oper').val('close');$('form').submit();\">Закрыть</button>" : "";
                    //_ret += "           <input type=\"submit\" name=\"cph_btnSave\" value=\"Сохранить\" id=\"btnSave\" class=\"btn btn-xs btn-primary\" style=\"width: 100px;height:25px;\"" + (MainCursor.EnableSaveButton ? "" : " disabled") + ">&nbsp;";
                    //_ret += "           <input type=\"submit\" name=\"cph_btnCancel\" value=\"Закрыть\" id=\"btnCancel\" class=\"btn btn-xs btn-default\" style=\"width: 100px;height:25px;\">";
                    _ret += "       </div>";
                    _ret += "   </div>";
                    if (RouteName == "complectnew")
                    {
                        _ret += "<iframe id=\"iframe_multiple\" src=\"/ajax/MultipleUploadfile.aspx?cur=" + BaseName + "_complectnew_list_cursor_" + _id + "\" style=\"display: none;\"></iframe>";
                        //_ret += "<input type=\"hidden\" id=\"" + _n + "\" name=\"" + _n + "\"/>";
                    }

                    #endregion Футер с кнопками
                }
                else
                    _ret += faFunc.Alert(faAlert.RowNotFoundOrAccessDenied);
            }
            else
                _ret += faFunc.Alert(faAlert.BadParam);

            _ret += "</div>";
            return System.Text.RegularExpressions.Regex.Replace(_ret, "  +", "");
        }

        public bool Save(out string result)
        {
            string changes = "";
            int new_id = 0;
            DataRow row;
            DataTable dt;
            string user_id = HttpContext.Current.Session["user_id"].ToString();
            int _cid = _act == "copy" ? 0 : Int32.Parse(_id);
            string _buf = "";

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);

            #region From POST

            foreach (var fld in MainCursor.Fields)
            {
                if (fld.Edit.Visible)
                {
                    switch (fld.Edit.Auto)
                    {
                        case faAutoType.None:
                            fld.Edit.Value = (RequestPost[MainCursor.Alias + "_" + fld.Data.FieldName] ?? "").Trim();
                            fld.Edit.ValueText = (RequestPost["cph_" + MainCursor.Alias + "_" + fld.Data.FieldName] ?? "").Trim();
                            break;

                        case faAutoType.NowDate:
                            fld.Edit.Value = DateTime.Now.ToShortDateString();
                            break;

                        case faAutoType.NowDateTime:
                            if (!fld.Edit.AddOnly || _cid == 0)
                                fld.Edit.Value = DateTime.Now.ToString();
                            else
                                fld.Edit.Value = (RequestPost[MainCursor.Alias + "_" + fld.Data.FieldName] ?? "").Trim();
                            break;

                        case faAutoType.UserID:
                            if (!fld.Edit.AddOnly || _cid == 0)
                                fld.Edit.Value = HttpContext.Current.Session["user_id"].ToString();
                            else
                            {
                                fld.Edit.Value = (RequestPost[MainCursor.Alias + "_" + fld.Data.FieldName] ?? "").Trim();
                                fld.Edit.ValueText = (RequestPost["cph_" + MainCursor.Alias + "_" + fld.Data.FieldName] ?? "").Trim();
                            }

                            break;

                        default:
                            break;
                    }
                }
            }

            #endregion From POST

            #region Required

            bool showerror = false;
            foreach (var fld in MainCursor.Fields)
            {
                if (fld.Edit.Visible && fld.Edit.Required)
                {
                    switch (fld.Edit.Control)
                    {
                        case faControl.TextBox:
                        case faControl.TextBoxNumber:
                        case faControl.TextBoxInteger:
                        case faControl.DatePicker:
                        case faControl.DateTimePicker:
                        case faControl.File:
                        case faControl.NewWindowArchiveID:
                            showerror = fld.Edit.Value == "";
                            break;

                        case faControl.DropDown:
                        case faControl.AutoComplete:
                        case faControl.TreeGrid:
                        case faControl.NewWindowArchive:
                            showerror = (fld.Edit.Value == "0" || fld.Edit.Value == "-1" || fld.Edit.ValueText == "");
                            break;
                    }
                }
                if (showerror)
                {
                    result = String.Format(faEdit.MsgMustFill, fld.View.CaptionShort);
                    return false;
                }
            }

            #endregion Required

            #region Unique

            showerror = false;
            foreach (var fld in MainCursor.Fields)
            {
                if (fld.Edit.Visible && fld.Edit.Unique)
                {
                    conn.Open();
                    SqlCommand cmd_u = new SqlCommand("SELECT id FROM [dbo].[" + MainCursor.Alias + "] WHERE del=0 AND id<>" + _cid + " AND " + fld.Data.FieldName + " = '" + fld.Edit.Value + "'", conn);
                    var res_u = cmd_u.ExecuteScalar();
                    cmd_u.Dispose();
                    conn.Close();
                    if (!(res_u is DBNull || res_u == null))
                    {
                        result = String.Format(faEdit.MsgDublicate, fld.View.CaptionShort, (int)res_u);
                        return false;
                    }
                }
            }

            #endregion Unique

            #region Check

            string error = "";
            decimal _decimal;
            int _int;
            DateTime _datetime;
            Regex r;
            foreach (var fld in MainCursor.Fields)
            {
                if (fld.Edit.Visible)
                {
                    switch (fld.Edit.Control)
                    {
                        case faControl.TextBox:
                            if (fld.Edit.Max > 0 && fld.Edit.Value.Length > fld.Edit.Max)
                                error = String.Format(faEdit.MsgMaxText, fld.View.CaptionShort, fld.Edit.Max);
                            else if (fld.Edit.Min > 0 && fld.Edit.Value.Length < fld.Edit.Min)
                                error = String.Format(faEdit.MsgMinText, fld.View.CaptionShort, fld.Edit.Min);
                            break;

                        case faControl.TextBoxNumber:
                            r = new Regex("[^-?0-9.]*");
                            fld.Edit.Value = r.Replace(fld.Edit.Value.Replace(",", "."), "");
                            if (Decimal.TryParse(fld.Edit.Value == "" ? "0" : fld.Edit.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out _decimal))
                            {
                                fld.Edit.Value = _decimal.ToString(CultureInfo.InvariantCulture);
                                if (fld.Edit.Max > 0 && _decimal > fld.Edit.Max)
                                    error = String.Format(faEdit.MsgMaxInt, fld.View.CaptionShort, fld.Edit.Max);
                                else if (fld.Edit.Min > 0 && _decimal > 0 && _decimal < fld.Edit.Min)
                                    error = String.Format(faEdit.MsgMinInt, fld.View.CaptionShort, fld.Edit.Min);
                            }
                            else
                                error = String.Format(faEdit.MsgBadFormat, fld.View.CaptionShort);
                            break;

                        case faControl.TextBoxInteger:
                        case faControl.NewWindowArchiveID:
                            r = new Regex("[^-?0-9]*");
                            if (Int32.TryParse(fld.Edit.Value == "" ? "0" : fld.Edit.Value, out _int))
                            {
                                if (fld.Edit.Max > 0 && _int > fld.Edit.Max)
                                    error = String.Format(faEdit.MsgMaxInt, fld.View.CaptionShort, fld.Edit.Max);
                                else if (fld.Edit.Min > 0 && _int > 0 && _int < fld.Edit.Min)
                                    error = String.Format(faEdit.MsgMinInt, fld.View.CaptionShort, fld.Edit.Min);
                            }
                            else
                                error = String.Format(faEdit.MsgBadFormat, fld.View.CaptionShort);
                            break;

                        case faControl.DatePicker:
                        case faControl.DateTimePicker:
                            if (fld.Edit.Value != "")
                            {
                                if (!DateTime.TryParse(fld.Edit.Value, out _datetime))
                                    error = String.Format(faEdit.MsgBadFormat, fld.View.CaptionShort);
                            }
                            break;

                        case faControl.File:
                            //showerror = fld.Edit.Value == "";
                            break;

                        case faControl.DropDown:
                        case faControl.AutoComplete:
                        case faControl.TreeGrid:
                        case faControl.NewWindowArchive:
                            //showerror = (fld.Edit.Value == "0" || fld.Edit.Value == "-1" || fld.Edit.ValueText == "");
                            break;
                    }
                }
                if (error != "")
                {
                    result = error;
                    return false;
                }
            }

            #endregion Check

            #region Исполнитель и источник

            if (MainCursor.Alias == "_complectnew")
            {
                faField fld1 = null, fld2 = null;
                foreach (faField fld in MainCursor.Fields)
                {
                    if (fld.Data.FieldName == "inet") fld1 = fld;
                    if (fld.Data.FieldName == "id_perf") fld2 = fld;
                }
                if ((fld1.Edit.Value == "2" || fld1.Edit.Value == "8") && fld2.Edit.Value == "0")
                {
                    result = String.Format(faEdit.MsgMustFill, fld2.View.CaptionShort);
                    return false;
                }
            }

            #endregion Исполнитель и источник

            byte _all_score = 0, _sc = 0;
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction(MainCursor.Alias + "_tr");
            try
            {
                SqlCommand cmd = new SqlCommand("", conn, trans);

                #region INSERT

                if (_cid == 0)
                {
                    string _tmp = "";
                    cmd.CommandText = "INSERT INTO [dbo].[" + MainCursor.Alias + "](";
                    foreach (var fld in MainCursor.Fields.Skip(1))
                    {
                        if (fld.Edit.Visible)
                        {
                            cmd.CommandText += "[" + fld.Data.FieldName + "],";
                            _tmp += "@p_" + fld.Data.FieldName + ",";
                            cmd.Parameters.AddWithValue("@p_" + fld.Data.FieldName, fld.Edit.Value);
                            changes += faFunc.GetChange(fld.Data.FieldName, "", fld.Edit.Control != faControl.TextArea ? fld.Edit.Value : "", fld, out _sc, Page, _act);
                            _all_score += _sc;
                        }
                    }
                    cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 1);
                    _tmp = _tmp.Substring(0, _tmp.Length - 1);
                    cmd.CommandText += ") VALUES(";
                    cmd.CommandText += _tmp + ");SELECT SCOPE_IDENTITY();";
                    int.TryParse(cmd.ExecuteScalar().ToString(), out new_id);
                }

                #endregion INSERT

                #region UPDATE

                else if (_cid > 0)
                {
                    string _cond = MainCursor.Fields[0].Filter.Condition;
                    MainCursor.Fields[0].Filter.Condition = "=";

                    string[] _oldvalue = new string[MainCursor.FieldCount];
                    string[] _oldvalue2 = new string[MainCursor.FieldCount];

                    for (int i = 0; i < MainCursor.FieldCount; i++)
                    {
                        _oldvalue[i] = MainCursor.Fields[i].Filter.Value;
                        MainCursor.Fields[i].Filter.Value = "";
                        _oldvalue2[i] = MainCursor.Fields[i].Filter.Value2;
                        MainCursor.Fields[i].Filter.Value2 = "";
                    }

                    MainCursor.Fields[0].Filter.Value = _cid.ToString();
                    int _tr = 0;
                    dt = GetData(MainCursor, 0, 1, "id", "Asc", out _tr);
                    MainCursor.Fields[0].Filter.Condition = _cond;

                    for (int i = 0; i < MainCursor.FieldCount; i++)
                    {
                        MainCursor.Fields[i].Filter.Value = _oldvalue[i];
                        MainCursor.Fields[i].Filter.Value2 = _oldvalue2[i];
                    }

                    if (dt.Rows.Count == 0)
                        throw new System.Exception("Записи c ID = " + _cid + " не существует");
                    row = dt.Rows[0];
                    cmd.CommandText = "UPDATE [dbo].[" + MainCursor.Alias + "] SET ";
                    foreach (var fld in MainCursor.Fields.Skip(1))
                    {
                        if (fld.Edit.Visible && (fld.Edit.Enable || fld.Edit.Auto != faAutoType.None))
                        {
                            cmd.CommandText += "[" + fld.Data.FieldName + "]=@p_" + fld.Data.FieldName + ",";
                            cmd.Parameters.AddWithValue("@p_" + fld.Data.FieldName, fld.Edit.Value);
                            changes += faFunc.GetChange(fld.Data.FieldName, row, fld, out _sc, Page, _act);
                            _all_score += _sc;
                        }
                    }
                    cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 1);
                    cmd.CommandText += " WHERE id = @p_id";
                    cmd.Parameters.AddWithValue("@p_id", _cid);
                    cmd.ExecuteNonQuery();
                }

                #endregion UPDATE

                faFunc.ToJournal(cmd, user_id, (_cid > 0 ? 2 : 1), (_cid > 0 ? _cid : new_id), IDBase, MainCursor.TableID, changes, _all_score);

                // если курсоры есть
                if (CursorCount > 0)
                {
                    DataTable dt_old, dt_new;

                    List<string> list = new List<string>(Cursors.Keys);
                    foreach (string k in list)
                    {
                        int _tr;
                        dt_old = GetData(Cursors[k], 0, 10000, "id", "Asc", out _tr);
                        dt_new = GetDataList(Cursors[k]);

                        #region Next Ver

                        int next_ver = 0;
                        foreach (var fld in Cursors[k].Fields)
                            if (fld.Edit.Auto == faAutoType.Version)
                                next_ver = 1;

                        if (next_ver == 1)
                            using (SqlCommand cmd1 = new SqlCommand("", conn, trans))
                            {
                                cmd1.CommandText = "SELECT MAX([nn]) FROM [" + k + "] where " + Cursors[k].Fields[1].Data.FieldName + " = " + _cid;
                                var nv = cmd1.ExecuteScalar();
                                next_ver = nv is DBNull ? 0 : (int)nv;
                            }

                        #endregion Next Ver

                        #region Main

                        int main = 0;
                        foreach (var fld in Cursors[k].Fields)
                            if (fld.Edit.Auto == faAutoType.Main)
                                main = 1;
                        if (main == 1 && dt_new.Rows.Count > 0)
                        {
                            foreach (DataRow row_new in dt_new.Rows)
                                main += (row_new["main"].ToString() == "1" && row_new["main"].ToString() != "3" ? 1 : 0);
                            if (main > 2)
                            {
                                result = "Основной может быть только одна из версий";
                                conn.Close();
                                return false;
                            }
                            if (main == 1)
                            {
                                result = "Не указана основная версия";
                                conn.Close();
                                return false;
                            }
                        }

                        #endregion Main

                        if (_act == "copy")
                        {
                            int i = 0;
                            foreach (DataRow row_new in dt_new.Rows)
                            {
                                row_new["id"] = --i;
                                row_new["status"] = 1;
                            }
                        }
                        foreach (DataRow row_new in dt_new.Rows)
                        {
                            _all_score = 0;
                            int rid = (int)row_new["id"];
                            string status = row_new["status"].ToString();
                            // Delete
                            if (status == "3" && rid > 0)
                            {
                                cmd.CommandText = "UPDATE [dbo].[" + Cursors[k].Alias + "] SET [del] = 1 WHERE id = @p_id";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@p_id", rid);
                                cmd.ExecuteNonQuery();
                                faFunc.ToJournal(cmd, user_id, 3, rid, IDBase, Cursors[k].TableID, "", 0);
                            }
                            // Add-Edit
                            if (status == "1" || status == "2")
                            {
                                cmd.Parameters.Clear();
                                changes = "";
                                if (rid > 0)
                                {
                                    cmd.CommandText = "UPDATE [dbo].[" + Cursors[k].Alias + "] SET ";
                                    DataRow row_old = dt_old.Rows.Find(rid);
                                    foreach (var fld in Cursors[k].Fields)
                                    {
                                        if (fld.Edit.Control == faControl.File && row_new[fld.Data.FieldName].ToString() != row_old[fld.Data.FieldName].ToString())
                                        {
                                            DateTime _date = DateTime.Now;
                                            string _ext = System.IO.Path.GetExtension(row_new[fld.Data.FieldName].ToString());
                                            string _rootpath = "";

                                            _rootpath = System.IO.Path.Combine(Properties.Settings.Default.filepath, "complectfiles");

                                            string _path = System.IO.Path.Combine(_date.Year.ToString(), _date.Month.ToString(), _date.Day.ToString());
                                            if (!Directory.Exists(System.IO.Path.Combine(_rootpath, _path)))
                                                Directory.CreateDirectory(System.IO.Path.Combine(_rootpath, _path));
                                            string _relativepath = "";
                                            string _fullpath = "";
                                            _relativepath = System.IO.Path.Combine(_path, System.IO.Path.GetFileName(row_new[fld.Data.FieldName].ToString()));
                                            _fullpath = System.IO.Path.Combine(_rootpath, _path, System.IO.Path.GetFileName(row_new[fld.Data.FieldName].ToString()));
                                            if (File.Exists(_fullpath))
                                                File.Delete(_fullpath);
                                            long length = new System.IO.FileInfo(row_new[fld.Data.FieldName].ToString()).Length;
                                            File.Copy(row_new[fld.Data.FieldName].ToString(), _fullpath);
                                            row_new[fld.Data.FieldName] = _relativepath;
                                        }

                                        if (fld.Edit.Visible && fld.Edit.Enable)
                                        {
                                            cmd.Parameters.AddWithValue("@p_" + fld.Data.FieldName, row_new[fld.Data.FieldName]);
                                            cmd.CommandText += "[" + fld.Data.FieldName + "]=@p_" + fld.Data.FieldName + ",";
                                            changes += faFunc.GetChange(fld.Data.FieldName, row_old[fld.Data.FieldName].ToString(), row_new[fld.Data.FieldName].ToString(), fld, out _sc, Page, _act);
                                            _all_score += _sc;
                                        }
                                        else if (fld.Edit.Auto != faAutoType.None && !fld.Edit.AddOnly)
                                        {
                                            cmd.CommandText += "[" + fld.Data.FieldName + "]=@p_" + fld.Data.FieldName + ",";
                                            _buf = "";
                                            switch (fld.Edit.Auto)
                                            {
                                                case faAutoType.NowDate:
                                                    _buf = DateTime.Now.ToShortDateString();
                                                    break;

                                                case faAutoType.NowDateTime:
                                                    _buf = DateTime.Now.ToString();
                                                    break;

                                                case faAutoType.UserID:
                                                    _buf = HttpContext.Current.Session["user_id"].ToString();
                                                    break;
                                            }
                                            cmd.Parameters.AddWithValue("@p_" + fld.Data.FieldName, _buf);
                                            changes += faFunc.GetChange(fld.Data.FieldName, row_old[fld.Data.FieldName].ToString(), _buf, fld, out _sc, Page, _act);
                                            _all_score += _sc;
                                        }
                                    }
                                    //
                                    cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 1);
                                    cmd.CommandText += " WHERE id = @p_id";
                                    cmd.Parameters.AddWithValue("@p_id", rid);
                                    cmd.ExecuteNonQuery();
                                    faFunc.ToJournal(cmd, user_id, 2, rid, IDBase, Cursors[k].TableID, changes, _all_score);
                                }
                                else
                                {
                                    string _tmp = "";
                                    cmd.CommandText = "INSERT INTO [dbo].[" + Cursors[k].Alias + "] (";
                                    long length = 0;
                                    foreach (var fld in Cursors[k].Fields.Skip(1))
                                    {
                                        if (fld.Edit.Control == faControl.File)
                                        {
                                            DateTime _date = DateTime.Now;
                                            string _ext = System.IO.Path.GetExtension(row_new[fld.Data.FieldName].ToString());
                                            string _rootpath = "";

                                            _rootpath = System.IO.Path.Combine(Properties.Settings.Default.filepath, "complectfiles");

                                            string _path = System.IO.Path.Combine(_date.Year.ToString(), _date.Month.ToString(), _date.Day.ToString());
                                            if (!Directory.Exists(System.IO.Path.Combine(_rootpath, _path)))
                                                Directory.CreateDirectory(System.IO.Path.Combine(_rootpath, _path));
                                            string _relativepath = "";
                                            string _fullpath = "";
                                            _relativepath = System.IO.Path.Combine(_path, System.IO.Path.GetFileName(row_new[fld.Data.FieldName].ToString()));
                                            _fullpath = System.IO.Path.Combine(_rootpath, _path, System.IO.Path.GetFileName(row_new[fld.Data.FieldName].ToString()));
                                            if (File.Exists(_fullpath))
                                                File.Delete(_fullpath);
                                            length = new System.IO.FileInfo(row_new[fld.Data.FieldName].ToString()).Length;
                                            File.Copy(row_new[fld.Data.FieldName].ToString(), _fullpath);
                                            row_new[fld.Data.FieldName] = _relativepath;
                                        }

                                        if (fld.Edit.Visible)
                                        {
                                            cmd.Parameters.AddWithValue("@p_" + fld.Data.FieldName, row_new[fld.Data.FieldName]);
                                            cmd.CommandText += "[" + fld.Data.FieldName + "],";
                                            _tmp += "@p_" + fld.Data.FieldName + ",";
                                            changes += faFunc.GetChange(fld.Data.FieldName, "", row_new[fld.Data.FieldName].ToString(), fld, out _sc, Page, _act);
                                            _all_score += _sc;
                                        }
                                        switch (fld.Edit.Auto)
                                        {
                                            case faAutoType.None:
                                                break;

                                            case faAutoType.NowDate:
                                                cmd.CommandText += "[" + fld.Data.FieldName + "],";
                                                _tmp += "@p_" + fld.Data.FieldName + ",";
                                                cmd.Parameters.AddWithValue("@p_" + fld.Data.FieldName, DateTime.Now.ToShortDateString());
                                                changes += faFunc.GetChange(fld.Data.FieldName, "", DateTime.Now.ToShortDateString(), fld, out _sc, Page, _act);
                                                break;

                                            case faAutoType.NowDateTime:
                                                cmd.CommandText += "[" + fld.Data.FieldName + "],";
                                                _tmp += "@p_" + fld.Data.FieldName + ",";
                                                cmd.Parameters.AddWithValue("@p_" + fld.Data.FieldName, DateTime.Now.ToString());
                                                changes += faFunc.GetChange(fld.Data.FieldName, "", DateTime.Now.ToString(), fld, out _sc, Page, _act);
                                                break;

                                            case faAutoType.Version:
                                                next_ver++;
                                                cmd.CommandText += "[" + fld.Data.FieldName + "],";
                                                _tmp += "@p_" + fld.Data.FieldName + ",";
                                                cmd.Parameters.AddWithValue("@p_" + fld.Data.FieldName, next_ver);
                                                changes += faFunc.GetChange(fld.Data.FieldName, "", next_ver.ToString(), fld, out _sc, Page, _act);
                                                break;

                                            case faAutoType.UserID:
                                                cmd.CommandText += "[" + fld.Data.FieldName + "],";
                                                _tmp += "@p_" + fld.Data.FieldName + ",";
                                                cmd.Parameters.AddWithValue("@p_" + fld.Data.FieldName, HttpContext.Current.Session["user_id"].ToString());
                                                changes += faFunc.GetChange(fld.Data.FieldName, "", HttpContext.Current.Session["user_id"].ToString(), fld, out _sc, Page, _act);
                                                break;

                                            case faAutoType.FileSize:
                                                cmd.CommandText += "[" + fld.Data.FieldName + "],";
                                                _tmp += "@p_" + fld.Data.FieldName + ",";
                                                cmd.Parameters.AddWithValue("@p_" + fld.Data.FieldName, length);
                                                changes += faFunc.GetChange(fld.Data.FieldName, "", length.ToString(), fld, out _sc, Page, _act);
                                                break;

                                            default:
                                                break;
                                        }
                                    }
                                    cmd.CommandText += "[" + Cursors[k].Fields[1].Data.FieldName + "]) VALUES(";
                                    _tmp += "@p_" + Cursors[k].Fields[1].Data.FieldName;
                                    cmd.CommandText += _tmp + ");SELECT SCOPE_IDENTITY();";
                                    cmd.Parameters.AddWithValue("@p_" + Cursors[k].Fields[1].Data.FieldName, (_cid > 0 ? _cid : new_id));
                                    int nid = 0;
                                    int.TryParse(cmd.ExecuteScalar().ToString(), out nid);

                                    // Ебучие комплекты
                                    if (_mode == "complect" && row_new["from"] != null)
                                    {
                                        cmd.CommandText = "UPDATE [dbo].[" + BaseName + "_complect_list] SET [id_archive] = " + (_cid > 0 ? _cid : new_id) +
                                            ", [filepath] ='" + row_new["file"].ToString() + "' WHERE id_sp = " + row_new["from"].ToString();
                                        cmd.Parameters.Clear();
                                        cmd.ExecuteScalar();
                                    }
                                    else if (_mode == "complectnew" && row_new["from"] != null)
                                    {
                                        cmd.CommandText = "UPDATE [dbo].[" + BaseName + "_complectnew_list] SET [id_archive] = " + (_cid > 0 ? _cid : new_id) +
                                            ", [file_archive] ='" + row_new["file"].ToString() + "' WHERE id = " + row_new["from"].ToString();
                                        cmd.Parameters.Clear();
                                        cmd.ExecuteScalar();
                                    }

                                    //
                                    changes += (_cid == 0 ? faFunc.GetChange(Cursors[k].Fields[1].Data.FieldName, "", new_id.ToString(), Cursors[k].Fields[1], out _sc, Page, _act) : "");
                                    _all_score += _sc;
                                    faFunc.ToJournal(cmd, user_id, 1, nid, IDBase, Cursors[k].TableID, changes, _all_score);
                                }
                            }
                        }
                        ClearData(Cursors[k]);
                    }
                }

                // Считаем количество документов для комплектов
                if (RouteName == "complectnew")
                {
                    cmd.CommandText = "UPDATE [dbo].[_complectnew] SET [doccount] = (select count(*) from _complectnew_list where id_complectnew=" + (_cid > 0 ? _cid : new_id) + " and del=0) WHERE id=" + (_cid > 0 ? _cid : new_id);
                    cmd.Parameters.Clear();
                    cmd.ExecuteNonQuery();
                }

                trans.Commit();
                conn.Close();
                ClearData(MainCursor);
                result = (_cid > 0 ? _cid : new_id).ToString();
                //!!!
                if (MainCursor.Alias == "_doctree")
                    HttpContext.Current.Cache.Remove("data_tree__doctree");
                //!!!
                return true;
            }
            catch (Exception ex)
            {
                result = faFunc.GetExceptionMessage(ex);
                try
                {
                    trans.Rollback();
                    conn.Close();
                }
                catch (Exception ex2)
                {
                    result += ex2.GetType() + ":" + ex2.Message;
                }
                return false;
            }
        }

        #endregion EDIT
    }
}