<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Rating.aspx.cs" Inherits="WebArchiveR6.Rating" %>

<%@ Register Assembly="Trirand.Web, Version=4.5.4.0, Culture=neutral, PublicKeyToken=e2819dc449af3295"
    Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc1" %>
<%@ Register assembly="Trirand.Web" namespace="Trirand.Web.UI.WebControls" tagprefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
    <form id="form1" runat="server" method="get">
        <div id="panel_search" class="panel panel-default" style="margin-bottom: 2px;">
            <div class="panel-body" style="padding: 11px;">
                <div class="row">

                    <%--<div class="col-md-3" style="width: 271px; margin-top: 3px; margin-bottom: 1px;">
                        <div class="input-group">
                            <div class="input-group-btn">
                                <button type="button" id="clear_year" class="btn btn-xs btn-default" style="width: 100px;" onclick="$('#year' ).val('');$('#year_cond' ).val('=');cld('#clear_year');" title="Балл">Балл</button>
                            </div>
                            <input id="year" name="cph_year" class="form-control" style="width: 107px;" value="" onchange="if ($('#year').val()!='')cls('#clear_year'); else cld('#clear_year');">
                        </div>
                    </div>

                    <div class="col-md-3" style="width: 271px; margin-top: 3px; margin-bottom: 1px;">
                        <div class="input-group">
                            <div class="input-group-btn">
                                <button type="button" id="clear__monthmonth" class="btn btn-xs btn-default" style="width: 100px;" onclick="$('#_monthmonth').val('0');$('#cph__monthmonth').val('');cld('#clear__monthmonth');" title="Месяц">Месяц</button>
                            </div>
                            <input id="cph__monthmonth" name="cph__monthmonth" class="form-control ui-autocomplete-input" style="width: 154px;" value="" onclick="$('#cph__monthmonth').autocomplete('search', ' ');" onchange="if ($('#cph__monthmonth').val()!='')cls('#clear__monthmonth'); else cld('#clear__monthmonth');" autocomplete="off">
                            <input type="hidden" name="_monthmonth" id="_monthmonth" value="0">
                        </div>
                    </div>--%>

                    <div class="col-md-3" style="width: 400px; margin-top: 3px; margin-bottom: 1px;">
                        <div class="input-group">
                            <div class="input-group-btn">
                                <button type="button" id="clear_when" class="btn btn-xs btn-default" style="width: 120px;" onclick="$('#cph_when_begin' ).val(''); $('#cph_when_end' ).val('');cld('#clear_when');" title="Время">Период</button>
                                <button type="button" class="btn btn-xs btn-default dropdown-toggle" data-toggle="dropdown"><span class="caret"></span><span class="sr-only">Toggle Dropdown</span></button>
                                <ul class="dropdown-menu" role="menu">
                                    <li><a href="#" onclick="$('#cph_when_begin').val(formatDate(GetLastMonthBegin())+' 00:00');$('#cph_when_end').val(formatDate(GetLastMonthEnd())+' 23:59');cls('#clear_when');return false;">Прошлый месяц</a></li>
                                    <li><a href="#" onclick="$('#cph_when_begin').val(formatDate(GetLastWeekBegin())+' 00:00');$('#cph_when_end').val(formatDate(GetLastWeekEnd())+' 23:59');cls('#clear_when');return false;">Прошлая неделя</a></li>
                                    <li><a href="#" onclick="$('#cph_when_begin').val(formatDate(GetYesterday())+' 00:00');$('#cph_when_end' ).val(formatDate(GetYesterday())+' 23:59');cls('#clear_when');return false;">Вчера</a></li>
                                    <li><a href="#" onclick="var _date = new Date();$('#cph_when_begin').val(formatDate(_date)+' 00:00');$('#cph_when_end' ).val(formatDate(_date)+' 23:59');cls('#clear_when');return false;">Сегодня</a></li>
                                    <li><a href="#" onclick="$('#cph_when_begin').val(formatDate(GetCurrentWeekBegin())+' 00:00');$('#cph_when_end' ).val(formatDate(GetCurrentWeekEnd())+' 23:59');cls('#clear_when');return false;">Текущая неделя</a></li>
                                    <li><a href="#" onclick="$('#cph_when_begin').val(formatDate(GetCurrentMonthBegin())+' 00:00');$('#cph_when_end' ).val(formatDate(GetCurrentMonthEnd())+' 23:59');cls('#clear_when');return false;">Текущий месяц</a></li>
                                    <li><a href="#" onclick="$('#cph_when_begin').val(formatDate(GetCurrentYearBegin())+' 00:00');$('#cph_when_end' ).val(formatDate(GetCurrentYearEnd())+' 23:59');cls('#clear_when');return false;">Текущий год</a></li>
                                    <li class="divider"></li>
                                    <li><a href="#" onclick="$('#cph_when_begin' ).val('');$('#cph_when_end' ).val('');cld('#clear_when');return false;">Очистить</a></li>
                                </ul>
                            </div>
                            <input id="cph_when_begin" name="cph_when_begin" class="form-control" placeholder="с" style="width: 110px;" value="<%Response.Write(_from); %>" onchange="if ($('#cph_when_begin').val()!='' || $('#cph_when_end').val()!='')cls('#clear_when'); else cld('#clear_when');">
                            <input id="cph_when_end" name="cph_when_end" class="form-control" placeholder="по" style="width: 110px;" value="<%=_to %>" onchange="if ($('#cph_when_begin').val()!='' || $('#cph_when_end').val()!='')cls('#clear_when'); else cld('#clear_when');">
                        </div>
                    </div>

                    <div class="col-md-3" style="width: 271px;margin-top:3px;margin-bottom:1px;">
                        <input type="submit" id="btn_apply_filter" name="cph_btn_apply_filter" value="Применить" class="btn btn-xs btn-primary" style="width: 133px;">
                        <%--<button type="button" id="btn_clear_filter" name="cph_btn_clear_filter" class="btn btn-xs btn-default" style="width: 132px;" onclick="$('button[id^=\'clear_\']').click();">Сбросить все</button>--%>
                    </div>

                    <%--<div class="col-md-3" style="width: 271px;margin-top:3px;margin-bottom:1px;">
                        <% Response.Write("Последнее обновление :" + DateTime.Now.ToShortTimeString()+" (МСК)"); %>
                    </div>--%>

                </div>
            </div>
        </div>
        <table>
            <tr>
                <td style="vertical-align: top;">
                    <cc1:JQGrid ID="jqUserScore" runat="server" Width="900px" ColumnReordering="True"
                        Height="600px" EnableViewState="False" OnDataRequesting="jqUserScore_DataRequesting">
                        <Columns>
                            <cc1:JQGridColumn DataField="id" Width="58" Editable="False" PrimaryKey="True" DataType="Int" HeaderText="№ п/п" Visible="false" />
                            <cc1:JQGridColumn DataField="name" Width="300" DataType="System.String" HeaderText="ФИО" />
                            <cc1:JQGridColumn DataField="score1" Width="150" DataType="String" HeaderText="Основные баллы" />
                            <cc1:JQGridColumn DataField="score2" Width="150" DataType="String" HeaderText="Дополнительные баллы" />
                            <cc1:JQGridColumn DataField="score3" Width="150" DataType="String" HeaderText="Штрафные баллы" />
                            <cc1:JQGridColumn DataField="score_all" Width="150" DataType="String" HeaderText="Итоговые баллы"  />
                            <cc1:JQGridColumn DataField="workhour" Width="150" DataType="String" HeaderText="Часов отработано" />
                            <cc1:JQGridColumn DataField="min" Width="150" DataType="String" HeaderText="Порог" />
                            <cc1:JQGridColumn DataField="max" Width="150" DataType="String" HeaderText="Макс.часов" />
                            <cc1:JQGridColumn DataField="max_score" Width="150" DataType="String" HeaderText="Макс.баллов" />
                        </Columns>
                        <PagerSettings PageSize="5000" PageSizeOptions="[5000]" />
                        <SortSettings InitialSortDirection="Desc" InitialSortColumn="score_all" />
                        <AppearanceSettings Caption="Админ / Эффективность" ShowRowNumbers="True" ShrinkToFit="False"  />
                        <ExportSettings ExportDataRange="Filtered" /> 
                    </cc1:JQGrid>
                    <%--<asp:Button runat="server" ID="ExportToExcelButton" Text="Export to Excel" OnClick="ExportToExcel"
                    CssClass="hidebutton" />
                <asp:Button runat="server" ID="ClearGridButton" Text="Clear Grid Button" OnClick="ClearGrid"
                    CssClass="hidebutton" />
                <asp:Button runat="server" ID="SetAcceptButton" Text="Set Accept" OnClick="SetAccept"
                    CssClass="hidebutton" />--%>
                </td>
            </tr>
        </table>
    </form>
    <script type='text/javascript'>

        $(window).bind('resize', function () {
            var grid = jQuery('#cph_jqUserScore');
            grid.setGridWidth($('.container-fluid').width() - 8);
            var h = $(window).height() - ($('#panel_hide').is(':visible') ? $('#panel_hide').height() : 0) - ($('#panel_search').is(':visible') ? $('#panel_search').height() : 0) - 120;
            grid.setGridHeight(h);
            grid.setGridWidth($('.container-fluid').width() - 8);
        }).trigger('resize');


        function cls(selector) {
            $(selector).removeClass('btn-default');
            $(selector).addClass('btn-success');
            $(selector).attr('onmouseover', '$(this).text("Очистить")');
            $(selector).attr('onmouseout', "$(this).text('" + $(selector).text().trim() + "')");
        }

        function cld(selector) {
            $(selector).removeClass('btn-success');
            $(selector).addClass('btn-default');
            $(selector).attr('onmouseover', '');
        }

        

        jQuery(document).ready(function () {

            //setInterval(function () { location.reload(); }, 600000);

            //setInterval(function () {
            //    jQuery('#cph_jqUserScore').trigger('reloadGrid');
            //    var l = new Date();
                
            //    jQuery('#cph_jqUserScore').jqGrid('setCaption', 'Админ / Эффективность (Последнее обновление: ' + l.toLocaleString()+')');
            //}, 600000);

            if ($('#_monthmonth').val() != '0') cls('#clear__monthmonth');

            if ($('#cph_when_begin').val() != '' || $('#cph_when_end').val() != '') cls('#clear_when');

            $('#cph_when_begin').datetimepicker();
            $('#cph_when_end').datetimepicker();


            $('#cph__monthmonth').autocomplete({
                source: '/ajax/dd.aspx?table=_month',
                minLength: 1,
                delay: 10,
                select: function (event, ui) {
                    $("#_monthmonth").val(ui.item.id);
                    $("#cph__monthmonth").val(ui.item.name);
                    if ($('#_monthmonth').val() != '0') cls('#clear__monthmonth');
                    else cld('#clear__monthmonth');
                    return false;
                }
            });

            $(window).resize();
        });

        <%--function ExportToExcel(id) {
            var grid = jQuery("#<%= jqArchivePerform.ClientID %>");
            var exp_btn = jQuery("#<%= ExportToExcelButton.ClientID %>");
            exp_btn.click();
            return true;
        }
        function ClearGrid() {
            var _btn = jQuery("#<%= ClearGridButton.ClientID %>");
            _btn.click();
            return true;
        }
        function SetAccept() {
            var _btn = jQuery("#<%= SetAcceptButton.ClientID %>");
            _btn.click();
            return true;
        }--%>
    </script>
</asp:Content>
