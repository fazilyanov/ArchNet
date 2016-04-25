// ***********************************************************************
// Assembly         : WebArchiveR6
// Author           : Artur Fazilyanov (a.fazilyanov@stg.ru | a.fazilyanov@gmail.com)
// Created          : 03-17-2016
//
// Last Modified By : a.fazilyanov
// Last Modified On : 03-17-2016
// ***********************************************************************
// <copyright file="faListNtd.cs" company="CJSC Stroytransgaz">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using Trirand.Web.UI.WebControls;

namespace WebArchiveR6
{
    /// <summary>
    /// Расширение класса для НТД
    /// </summary>
    /// <seealso cref="WebArchiveR6.faList" />
    public class faListNtd : faList
    {
        public faListNtd()
        {
        }

        /// <summary>
        /// Устанавливает действие по двойному клику в списке
        /// </summary>
        /// <param name="cur">The current.</param>
        /// <returns>System.String.</returns>
        public override string SetListJQGridRowDoubleClick(faCursor cur)
        {
            return "GoToView_" + cur.Alias;
        }

        public override string SetCursorJQGridRowDoubleClick(string _cn)
        {
            return "FileRow" + _cn;
        }

        public override void RecolorGrid(JQGrid jqGrid)
        {
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
                    "  if (cm[i].name === 'id_status_name_text') {" +
                       "     iCol1 = i;" +
                       " }" +
                    //" if (cm[i].name === 'date_reg') {" +
                    //"     iCol2 = i; " +
                    //" }" +
                    "}" +
                    "var cRows = this.rows.length;" +
                    "var iRow;" +
                    "var row;" +
                    "var className;" +
                    "for (iRow = 0; iRow < cRows; iRow++) {" +
                      "  row = this.rows[iRow];" +
                      "  var x1 = $(row.cells[iCol1]);" +
                      //  "  var x2 = $(row.cells[iCol2]);" +
                      "  if (x1[0].innerText.trim() == 'Отменен') {" +
                      "      x1[0].className = 'redbgcell';" +
                      "  }" +
                    "}" +
                "});");
        }
    }
}