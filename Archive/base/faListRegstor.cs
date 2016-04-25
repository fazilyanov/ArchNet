using System;
using System.Data;
using System.Web;
using Trirand.Web.UI.WebControls;

namespace WebArchiveR6
{
    /// <summary>
    /// Расширение
    /// </summary>
    public class faListRegstor : faList
    {
        public faListRegstor()
        {
        }

        public override DataTable AlterData(DataTable dt)
        {
            if (RouteName == "regstorlist")
            {
                // Добавляем столбец с базой
                string cb = "";
                string _sql1 = "";
                string _sql2 = "";
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["id_archive"].ToString() != "0" || row["barcode"].ToString() != "0")
                    {
                        i++;
                        cb = row["id_base_name_text"].ToString();
                        _sql1 = "SELECT a.id_archive,a.[file], b.[id_doctree], d.name as id_doctree_name_text, b.id_frm_contr, f.name as id_frm_contr_name_text, b.[date_doc], a.id_quality,q.name as id_quality_name_text FROM [dbo].[" + cb + "_docversion]  as a " +
                            " INNER JOIN [dbo].[" + cb + "_archive] as b ON a.id_archive=b.id" +
                            " LEFT JOIN [dbo].[_frm] as f ON b.id_frm_contr=f.id" +
                            " LEFT JOIN [dbo].[_doctree] as d ON b.id_doctree=d.id" +
                            " LEFT JOIN [dbo].[_quality] as q ON a.id_quality=q.id" +
                            " where a.barcode=" + row["barcode"].ToString() + " and a.del=0 and a.main=1";
                        _sql2 = "SELECT a.id_archive,a.[file], b.[id_doctree], d.name as id_doctree_name_text, b.id_frm_contr, f.name as id_frm_contr_name_text, b.[date_doc], a.id_quality,q.name as id_quality_name_text FROM [dbo].[" + cb + "_docversion] as a " +
                            " INNER JOIN [dbo].[" + cb + "_archive] as b ON a.id_archive=b.id " +
                            " LEFT JOIN [dbo].[_frm] as f ON b.id_frm_contr=f.id" +
                            " LEFT JOIN [dbo].[_doctree] as d ON b.id_doctree=d.id" +
                            " LEFT JOIN [dbo].[_quality] as q ON a.id_quality=q.id" +
                            " where a.id_archive=" + row["id_archive"].ToString() + " and a.del=0 and a.main=1";

                        DataTable res = faFunc.GetData(row["id_archive"].ToString() != "0" ? _sql2 : _sql1);
                        if (res.Rows.Count > 0)
                        {
                            row["id_archive"] = res.Rows[0]["id_archive"];
                            row["file"] = res.Rows[0]["file"];
                            row["id_doctree_name_text"] = res.Rows[0]["id_doctree_name_text"];
                            row["id_frm_contr_name_text"] = res.Rows[0]["id_frm_contr_name_text"];
                            row["id_quality_name_text"] = res.Rows[0]["id_quality_name_text"];
                            row["date_doc"] = res.Rows[0]["date_doc"];
                        }
                    }
                }
            }
            return dt;
        }

        public override void AddCustomButtonsToListJQGrid(JQGrid jqGrid, faCursor cur)
        {
            if (RouteName == "regstorlist")
            {
                JQGridToolBarButton jqBtnViewRegstor = new JQGridToolBarButton();
                jqBtnViewRegstor.OnClick = "GoToViewRegstor_" + cur.Alias;
                jqBtnViewRegstor.ButtonIcon = "ui-icon-document";
                jqBtnViewRegstor.Text = "Дело&nbsp;&nbsp;";
                jqBtnViewRegstor.ToolTip = "Открыть соответствующий регистр";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnViewRegstor);
                JSFunctionList.Add("GoToViewRegstor_" + cur.Alias + "()",
                    "var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                    "var id = grid.jqGrid('getGridParam', 'selrow');" +
                    "if (id>0) {" +
                    "   var row = grid.getRowData(id);" +
                    "   var f = row.id_regstor;" +
                    "   window.open('" + "/Regstor/?id='+f+'&act=view');" +
                    "}else alert('Выберите запись');  return false;");

                JQGridToolBarButton jqBtnViewCard = new JQGridToolBarButton();
                jqBtnViewCard.OnClick = "ViewCard_" + cur.Alias;
                jqBtnViewCard.ButtonIcon = "ui-icon-newwin";
                jqBtnViewCard.Text = "Карточка&nbsp;&nbsp;";
                jqBtnViewCard.ToolTip = "Открывает соответствующую карточку (если указана)";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnViewCard);
                JSFunctionList.Add("ViewCard_" + cur.Alias + "()",
                " var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                   " var id = grid.jqGrid('getGridParam', 'selrow');" +
                   " if (id>0) { " +
                   "    var row = grid.getRowData(id);" +

                   "   if (row.id_archive>0) window.open('/archive/'+row.id_base_name_text+'/srch?id='+row.id_archive+'&act=view');else alert('Код ЭА не указан.');" +
                   "}else alert('Выберите запись');  return false;");

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
                    "   if (f!='') window.open('/Archive/GetFile.aspx?id='+id +'&b=' + row.id_base_name_text+'&f=' + f + '&k='+$.md5(id));" +
                    "   else alert('Файл не указан');" +
                    "}" +
                    "else alert('Выберите запись');  return false;");
            }
        }

        public override void AddCustomButtonsToCursorJQGrid(JQGrid jqGrid, faCursor cur)
        {
            #region CustomButtonFile

            JQGridToolBarButton jqBtnFile = new JQGridToolBarButton();
            jqBtnFile.OnClick = "FileRow" + cur.Alias;
            jqBtnFile.ButtonIcon = "ui-icon-arrowthickstop-1-s";
            jqBtnFile.Text = "Файл&nbsp;&nbsp;";
            jqBtnFile.ToolTip = "Открыть файл";
            jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnFile);
            string _ret = "";
            _ret += "   var grid = jQuery('#cph_" + jqGrid.ClientID + "');";
            _ret += "   var id = grid.jqGrid('getGridParam', 'selrow');";
            _ret += "   if (id) { ";
            _ret += "       var row = grid.getRowData(id);";
            _ret += "       var f = row.file;";
            _ret += "       var b = row.id_base;";
            _ret += "       if (f!='') window.open('/Archive/GetFile.aspx?id='+id +'&b='+b+'&f=' + f + '&k='+$.md5(id));";
            _ret += "       else alert('Файл не указан');";
            _ret += "   }";
            _ret += "   else alert('Выберите запись');  return false;";
            JSFunctionList.Add("FileRow" + cur.Alias + "()", _ret);

            #endregion CustomButtonFile

            #region CustomButtonViewCard

            JQGridToolBarButton jqBtnViewCard = new JQGridToolBarButton();
            jqBtnViewCard.OnClick = "ViewCard_" + cur.Alias;
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
            JSFunctionList.Add("ViewCard_" + cur.Alias + "()", _ret);

            #endregion CustomButtonViewCard

            #region CustomButtonClipboard1

            if (cur.EnableSaveButton)
            {
                if (!String.IsNullOrEmpty(RequestGet["sidx"]))
                {
                    //для вставки из буфера по текущей сортировке
                    HttpContext.Current.Session["_regstor_list_cursor_sidx"] = RequestGet["sidx"];
                    HttpContext.Current.Session["_regstor_list_cursor_sord"] = RequestGet["sord"];
                }

                JQGridToolBarButton jqBtnClipboard1 = new JQGridToolBarButton();
                jqBtnClipboard1.OnClick = "InsertFromCB_" + cur.Alias;
                jqBtnClipboard1.ButtonIcon = "ui-icon-newwin";
                jqBtnClipboard1.Text = "Вставить ШК&nbsp;&nbsp;";
                jqBtnClipboard1.ToolTip = "Позволяет вставить из буфера обмена список состоящий из штрихкодов";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnClipboard1);
                _ret =
                    " var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                    " var id = grid.jqGrid('getGridParam', 'selrow');" +
                    " var buf = window.clipboardData.getData('Text').trim();" +
                    // " if (id && buf.length>0) { " +
                    "   var row = grid.getRowData(id);" +
                    "   $.ajax({" +
                    "   url: '/ajax/InsertFromCBRegstor.aspx?id='+id+'&cur=" + cur.Alias + "_cursor_" + _id + "'," +
                    "   type: 'POST'," +
                    "       data: buf," +
                    "       cache: false," +
                    "       contentType: false," +
                    "       processData: false," +
                    "       success: function (html) {jQuery('#cph_" + jqGrid.ID + "').trigger('reloadGrid');if (html.length>0)alert(html)}," +
                    "       error: function (request, status, error) {  alert(request.responseText); }" +
                    "   });" +
                    "  return false;"
                    ;

                JSFunctionList.Add("InsertFromCB_" + cur.Alias + "()", _ret);
            }

            #endregion CustomButtonClipboard1

            #region CustomButtonClipboard2

            if (cur.EnableSaveButton)
            {
                if (!String.IsNullOrEmpty(RequestGet["sidx"]))
                {
                    //для вставки из буфера по текущей сортировке
                    HttpContext.Current.Session["_regstor_list_cursor_sidx"] = RequestGet["sidx"];
                    HttpContext.Current.Session["_regstor_list_cursor_sord"] = RequestGet["sord"];
                }

                JQGridToolBarButton jqBtnClipboard2 = new JQGridToolBarButton();
                jqBtnClipboard2.OnClick = "InsertFromCB2_" + cur.Alias;
                jqBtnClipboard2.ButtonIcon = "ui-icon-newwin";
                jqBtnClipboard2.Text = "Вставить КЭА&nbsp;&nbsp;";
                jqBtnClipboard2.ToolTip = "Позволяет вставить из буфера обмена список состоящий из Кодов ЭА";
                jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnClipboard2);
                _ret =
                    " var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                    " var id = grid.jqGrid('getGridParam', 'selrow');" +
                    " var buf = window.clipboardData.getData('Text').trim();" +
                    "   var row = grid.getRowData(id);" +
                    "   $.ajax({" +
                    "   url: '/ajax/InsertFromCBRegstor2.aspx?id='+id+'&cur=" + cur.Alias + "_cursor_" + _id + "'," +
                    "   type: 'POST'," +
                    "       data: buf," +
                    "       cache: false," +
                    "       contentType: false," +
                    "       processData: false," +
                    "       success: function (html) {jQuery('#cph_" + jqGrid.ID + "').trigger('reloadGrid');if (html.length>0)alert(html)}," +
                    "       error: function (request, status, error) {  alert(request.responseText); }" +
                    "   });" +
                    "  return false;"
                    ;

                JSFunctionList.Add("InsertFromCB2_" + cur.Alias + "()", _ret);
            }

            #endregion CustomButtonClipboard2

            #region CustomDel

            JQGridToolBarButton jqBtnSep = new JQGridToolBarButton();
            jqBtnSep.ButtonIcon = "none";
            jqBtnSep.Text = "&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;";
            jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnSep);

            JQGridToolBarButton jqBtnDel = new JQGridToolBarButton();
            jqBtnDel.OnClick = "DelRow" + cur.Alias;
            jqBtnDel.ButtonIcon = "ui-icon-trash";
            jqBtnDel.Text = "Удалить&nbsp;&nbsp;";
            jqBtnDel.ToolTip = "Удалить запись";
            jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnDel);
            _ret =
               " var grid = jQuery('#cph_" + jqGrid.ID + "');" +
               " var id = grid.jqGrid('getGridParam', 'selrow');" +
               " var form = $('#form" + cur.Alias + "_edit');" +
               " if (id!=null && id!=0) {" +
               "   form.find('#id').val(id);" +
               "   form.find('#oper').val('del');" +
               "   var res = form.find(':input').serializeArray();" +
               "   $.ajax({ type: 'POST',url: location.href+'&jqGridID=cph_jqGrid" + cur.Alias + "&editMode=1',data : res ,success: function(data) {grid.trigger('reloadGrid');}});" +
               "}else alert('Выберите запись');  return false;";
            JSFunctionList.Add("DelRow" + cur.Alias + "()", _ret);

            #endregion CustomDel
        }

        public override string SetListJQGridRowDoubleClick(faCursor cur)
        {
            return (RouteName == "regstorlist") ? "GoToViewRegstor_" + cur.Alias : "GoToView_" + cur.Alias;
        }
    }
}