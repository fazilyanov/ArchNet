// ***********************************************************************
// Assembly         : ArchNet
// Author           : Artur Fazilyanov (a.fazilyanov@stg.ru | a.fazilyanov@gmail.com)
// Created          : 02-25-2016
//
// Last Modified By : a.fazilyanov
// Last Modified On : 03-18-2016
// ***********************************************************************
// <copyright file="faListDel.cs" company="CJSC Stroytransgaz">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using Trirand.Web.UI.WebControls;

namespace ArchNet
{
    /// <summary>
    /// Расширение класса для показа удаленных документов
    /// </summary>
    /// <seealso cref="ArchNet.faList" />
    public class faListDel : faList
    {
        public override void AddCustomButtonsToListJQGrid(JQGrid jqGrid, faCursor cur)
        {
            JQGridToolBarButton jqBtnRestore = new JQGridToolBarButton();
            jqBtnRestore.OnClick = "GoToRestore_" + cur.Alias;
            jqBtnRestore.ButtonIcon = "ui-icon-arrowthickstop-1-s";
            jqBtnRestore.Text = "Восстановить&nbsp;&nbsp;";
            jqBtnRestore.ToolTip = "Восстановить документ";
            jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnRestore);
            JSFunctionList.Add("GoToRestore_" + cur.Alias + "()",
                "var grid = jQuery('#cph_" + jqGrid.ID + "');" +
                "var id = grid.jqGrid('getGridParam', 'selrow');" +
                "if (id>0) { " +
                "   window.open('/Archive/Restore.aspx?table=" + cur.Alias + "&id='+id);" +
                "}" +
                "else alert('Выберите запись');  return false;");
        }
    }
}