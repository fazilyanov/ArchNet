// ***********************************************************************
// Assembly         : WebArchiveR6
// Author           : Artur Fazilyanov (a.fazilyanov@stg.ru | a.fazilyanov@gmail.com)
// Created          : 03-29-2016
//
// Last Modified By : a.fazilyanov
// Last Modified On : 03-29-2016
// ***********************************************************************
// <copyright file="faListDublicate.cs" company="CJSC Stroytransgaz">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using Trirand.Web.UI.WebControls;

namespace WebArchiveR6
{
    /// <summary>
    /// Расширение класса для показа дубликатов документов
    /// </summary>
    /// <seealso cref="WebArchiveR6.faList" />
    public class faListDublicate : faList
    {
        /// <summary>
        /// Добавляем кнопки в грид
        /// </summary>
        public override void AddCustomButtonsToListJQGrid(JQGrid jqGrid, faCursor cur)
        {
            JQGridToolBarButton jqBtnRefresh = new JQGridToolBarButton();
            jqBtnRefresh.OnClick = "GoToRefresh_" + cur.Alias;
            jqBtnRefresh.ButtonIcon = "ui-icon-refresh";
            jqBtnRefresh.Text = "Обновить данные&nbsp;&nbsp;";
            jqGrid.ToolBarSettings.CustomButtons.Add(jqBtnRefresh);
            JSFunctionList.Add("GoToRefresh_" + cur.Alias + "()","window.location =('routine/"+BaseName+"/updatedubat/);");
        }
    }
}