<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ArchiveBulkEdit.aspx.cs" Inherits="ArchNet.ArchiveBulkEdit" %>

<%@ Register Assembly="Trirand.Web, Version=4.5.4.0, Culture=neutral, PublicKeyToken=e2819dc449af3295"
    Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Site.master" %>
<%@ Register Assembly="Trirand.Web" Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
    <form id="form1" runat="server">
        <h3 class="page-header-custom"></h3>
        
        <table>
            <tr>
                <td width="30%" style="vertical-align: top;">
                    <form method="post" action="acc?id=862614&amp;act=view" id="form2" enctype="multipart/form-data">
                        <input id="oper" name="oper" type="hidden">
                        
                        <div class="input-group" style="margin-top: 8px;">
                            <label style="width: 110px; float: left;">Документ</label>
                            <input id="cph_archive_id_doctree" name="cph_archive_id_doctree" class="form-control ui-autocomplete-input" style="width: 250px;" value="" onclick="$(this).select(); $(this).autocomplete('search', ' ');" onblur="if ($('#archive_id_doctree').val()=='0') $(this).val('');if ($(this).val().trim()=='')$('#archive_id_doctree').val('0');">
                            <input type="hidden" name="archive_id_doctree" id="archive_id_doctree" value="0">
                        </div>

                        <%if (_page != "dog" && _page != "empl" && _page != "tech")
                          { %>
                        <div class="input-group" style="margin-top: 8px;">
                            <label style="width: 110px; float: left;">Вид док.</label>
                            <input id="cph_archive_id_doctype" name="cph_archive_id_doctype" class="form-control ui-autocomplete-input" style="width: 250px;" value="" onclick="$(this).select(); $(this).autocomplete('search', ' ');" onblur="if ($('#archive_id_doctype').val()=='0') $(this).val('');if ($(this).val().trim()=='')$('#archive_id_doctype').val('0');">
                            <input type="hidden" name="archive_id_doctype" id="archive_id_doctype" value="0">
                        </div>
                        <%}
                          if (_page != "norm" && _page != "empl")
                          {%>
                        <div class="input-group" style="margin-top: 8px;">
                            <label style="width: 110px; float: left;">Контрагент</label>
                            <input id="cph_archive_id_frm_contr" name="cph_archive_id_frm_contr" class="form-control ui-autocomplete-input" style="width: 250px;" value="" onclick="$(this).select(); $(this).autocomplete('search', ' ');" onblur="if ($('#archive_id_frm_contr').val()=='0') $(this).val('');if ($(this).val().trim()=='')$('#archive_id_frm_contr').val('0');">
                            <input type="hidden" name="archive_id_frm_contr" id="archive_id_frm_contr" value="0">
                        </div>
                        
                        <%}
                          if (_page != "dog")
                          {
                        %>
                        <div class="input-group" style="margin-top: 8px;">
                            <label style="width: 110px; float: left;">Пакет</label>
                            <input id="archive_docpack" name="archive_docpack" class="form-control" style="width: 250px;" value="">
                        </div>
                        <%}
                      if (_page != "ord" && _page != "empl" && _page != "tech" && _page != "oth" && _page != "norm")
                      {
                        %>
                        <div class="input-group" style="margin-top: 8px;">
                            <label style="width: 110px; float: left;">Код проекта</label>
                            <input id="cph_archive_id_prjcode" name="cph_archive_id_prjcode" class="form-control ui-autocomplete-input" style="width: 250px;" value="" onclick="$(this).select(); $(this).autocomplete('search', ' ');" onblur="if ($('#archive_id_prjcode').val()=='0') $(this).val('');if ($(this).val().trim()=='')$('#archive_id_prjcode').val('0');">
                            <input type="hidden" name="archive_id_prjcode" id="archive_id_prjcode" value="0">
                        </div>
                        <%}
                      if (_page != "ord" && _page != "tech" && _page != "norm")
                      {%>
                        <div class="input-group" style="margin-top: 8px;">
                            <label style="width: 110px; float: left;">Старший док.</label>
                            <input id="cph_archive_id_parent" name="cph_archive_id_parent" class="form-control" style="width: 250px;" value="" onblur="if ($(this).val().trim()=='')$('#archive_id_parent').val('0');">
                            <input type="hidden" name="archive_id_parent" id="archive_id_parent" value="0">
                        </div>
                        <%}
                      if (_page != "empl" && _page != "tech")
                      {%>
                        <div class="input-group" style="margin-top: 8px;">
                            <label style="width: 110px; float: left;">Исполнитель</label>
                            <input id="cph_archive_id_perf" name="cph_archive_id_perf" class="form-control ui-autocomplete-input" style="width: 250px;" value="" onclick="$(this).select(); $(this).autocomplete('search', ' ');" onblur="if ($('#archive_id_perf').val()=='0') $(this).val('');if ($(this).val().trim()=='')$('#archive_id_perf').val('0');">
                            <input type="hidden" name="archive_id_perf" id="archive_id_perf" value="0">
                        </div>
                        <%  }
                      if (_page != "tech")
                      {
                        %>
                        <div class="input-group" style="margin-top: 8px;">
                            <label style="width: 110px; float: left;">Получатель</label>
                            <input id="cph_archive_id_depart" name="cph_archive_id_depart" class="form-control ui-autocomplete-input" style="width: 250px;" value="" onclick="$(this).select(); $(this).autocomplete('search', ' ');" onblur="if ($('#archive_id_depart').val()=='0') $(this).val('');if ($(this).val().trim()=='')$('#archive_id_depart').val('0');">
                            <input type="hidden" name="archive_id_depart" id="archive_id_depart" value="0">
                        </div>
                        <%} %>
                        <div class="input-group" style="margin-top: 8px;">
                            <label style="width: 110px; float: left;">Скрытый</label>
                            <input id="cph_archive_hidden" name="cph_archive_hidden" class="form-control ui-autocomplete-input" style="width: 250px;" value="" onclick="$(this).select(); $(this).autocomplete('search', ' ');" onblur="if ($('#archive_hidden').val()=='0') $(this).val('');if ($(this).val().trim()=='')$('#archive_hidden').val('0');">
                            <input type="hidden" name="archive_hidden" id="archive_hidden" value="0">
                        </div>
                        <div class="btn-group" style="margin-top: 20px;">
                            <button type="button" class="btn btn-xs btn-primary" style="width: 150px;margin-left: 110px;" onclick="window.onbeforeunload = null;$('form').find('#oper').val('presave');$('form').submit();">Сохранить</button>
                        </div>
                    </form>
                </td>
                <td width="70%">
                    <cc1:JQGrid ID="jqArchiveBulkEdit" runat="server" Width="900px" ColumnReordering="True"
                        Height="600px" EnableViewState="False" OnDataRequesting="jqArchiveBulkEdit_DataRequesting"
                        OnRowDeleting="jqArchiveBulkEdit_RowDeleting" >
                        <Columns>
                            <%--
                            <cc1:JQGridColumn DataField="id" Width="58" Editable="False" PrimaryKey="True" DataType="Int"
                                HeaderText="Код ЭА" />
                            <cc1:JQGridColumn DataField="id_doctype_name_text" Width="108" DataType="System.String" HeaderText="Вид докум." />
                            <cc1:JQGridColumn DataField="id_frm_contr_name_text" Width="108" DataType="System.String" HeaderText="Контрагент" />
                            <cc1:JQGridColumn DataField="docpack" Width="58" DataType="String" HeaderText="Пакет" />
                            
                            <cc1:JQGridColumn DataField="id_prjcode_code_new_text" Width="58" DataType="String" HeaderText="Код проекта" />
                            
                            <cc1:JQGridColumn DataField="id_parent_num_doc_text" Width="63" DataType="System.String" HeaderText="Старший документ" />
                            <cc1:JQGridColumn DataField="id_perf_name_text" Width="63" DataType="System.String" HeaderText="Исполнитель" />
                            <cc1:JQGridColumn DataField="id_depart_name_text" Width="63" DataType="System.String" HeaderText="Получатель" />

                            ---%>


                            <%--<cc1:JQGridColumn DataField="doctree" Width="230" DataType="System.String" HeaderText="Документ" />
                            <cc1:JQGridColumn DataField="frm" Width="230" DataType="System.String" HeaderText="Контрагент" />
                            <cc1:JQGridColumn DataField="summ" Width="100" DataType="System.String" HeaderText="Сумма" />
                            <cc1:JQGridColumn DataField="file" Width="100" DataType="System.String" HeaderText="Файл" />--%>
                        </Columns>
                        <PagerSettings PageSize="1000" PageSizeOptions="[1000]" />
                        <ToolBarSettings ShowDeleteButton="True"  >

                           <%-- <CustomButtons>
                                <cc1:JQGridToolBarButton OnClick="ExportToExcel" Text="Экспорт &nbsp;&nbsp;" ToolTip="Выгрузка данных в Excel" />
                            </CustomButtons>--%>
                           <%-- <CustomButtons>
                                <cc1:JQGridToolBarButton OnClick="ClearGrid" Text="Очистить &nbsp;&nbsp;" ToolTip="Полностью очистить список" />
                            </CustomButtons>--%>
                           <%-- <CustomButtons>
                                <cc1:JQGridToolBarButton OnClick="SetAccept" Text="Установить атрибут &nbsp; &nbsp;" ToolTip="Установить атрибут 'Принят к учету' для всего списка" />
                            </CustomButtons>--%>
                        </ToolBarSettings>
                        <AppearanceSettings Caption="Результаты поиска" ShowRowNumbers="True" ShrinkToFit="False"  />
                       
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
        jQuery(document).ready(function () {
            $('#<%= jqArchiveBulkEdit.ClientID+"_pager_center" %>').hide();
            $('.ui-icon-trash').after(' Удалить из списка');

            $('#cph_archive_id_doctree').autocomplete({
                source: '/ajax/ac.aspx?table=_doctree',
                minLength: 1,
                delay: 10,
                select: function (event, ui) {
                    $("#archive_id_doctree").val(ui.item.id);
                    $("#cph_archive_id_doctree").val(ui.item.name);
                    return false;
                }
            });


            $('#cph_archive_id_doctype').autocomplete({
                source: '/ajax/dd.aspx?table=_doctype',
                minLength: 1,
                delay: 10,
                select: function (event, ui) {
                    $("#archive_id_doctype").val(ui.item.id);
                    $("#cph_archive_id_doctype").val(ui.item.name);
                    return false;
                }
            });

            $('#cph_archive_hidden').autocomplete({
                source: '/ajax/dd.aspx?table=_yesno',
                minLength: 1,
                delay: 10,
                select: function (event, ui) {
                    $("#archive_hidden").val(ui.item.id);
                    $("#cph_archive_hidden").val(ui.item.name);
                    return false;
                }
            });

            $('#cph_archive_id_frm_contr').autocomplete({
                source: '/ajax/ac.aspx?table=_frm',
                minLength: 1,
                delay: 10,
                select: function (event, ui) {
                    $("#archive_id_frm_contr").val(ui.item.id);
                    $("#cph_archive_id_frm_contr").val(ui.item.name);
                    return false;
                }
            });


            $('#cph_archive_id_frm_contr').bind('change', function (event) {
                if (this.value.trim() == '') $('#archive_id_frm_contr').val('0');
            });


            $('#cph_archive_id_prjcode').autocomplete({
                source: '/ajax/ac.aspx?table=_prjcode',
                minLength: 1,
                delay: 10,
                select: function (event, ui) {
                    $("#archive_id_prjcode").val(ui.item.id);
                    $("#cph_archive_id_prjcode").val(ui.item.name);
                    return false;
                }
            });
            $('#cph_archive_id_prjcode').bind('change', function (event) {
                if (this.value.trim() == '') $('#archive_id_prjcode').val('0');
            });

            $('#archive_docpack').bind('keydown click', function (event) {
                if (event.keyCode == 32 || event.keyCode == null) OpenNewWindow2_archive();
            });
            $('#cph_archive_id_parent').bind('keydown click', function (event) {
                if (event.keyCode == 32 || event.keyCode == null) OpenNewWindow_archive();
            });

            $('#cph_archive_id_perf').autocomplete({
                source: '/ajax/ac.aspx?table=<%= Master.cur_basename%>_person',
                minLength: 1,
                delay: 10,
                select: function (event, ui) {
                    $("#archive_id_perf").val(ui.item.id);
                    $("#cph_archive_id_perf").val(ui.item.name);
                    return false;
                }
            });
            $('#cph_archive_id_perf').bind('change', function (event) {
                if (this.value.trim() == '') $('#archive_id_perf').val('0');
            });
            $('#cph_archive_id_depart').autocomplete({
                source: '/ajax/ac.aspx?table=<%= Master.cur_basename%>_department',
                minLength: 1,
                delay: 10,
                select: function (event, ui) {
                    $("#archive_id_depart").val(ui.item.id);
                    $("#cph_archive_id_depart").val(ui.item.name);
                    return false;
                }
            });
            $('#cph_archive_id_depart').bind('change', function (event) {
                if (this.value.trim() == '') $('#archive_id_depart').val('0');
            });

        });

        function OpenNewWindow_archive() {
            var id_frm = $('#archive_id_frm_contr').val();
            var id_frm_text = $('#cph_archive_id_frm_contr').val();
            $.ajax({
                url: '/ajax/setses.aspx?key=archive_select_id_frm_contr_filter&value=' + escape(id_frm) + '&key1=archive_select_id_frm_contr_filter_text&value1=' + escape(id_frm_text),
                type: 'POST',
                success: function (html) {
                    window.open('/archive/<%= Master.cur_basename%>/select', 'modal', 'width=' + (document.body.clientWidth - 50) + ',height=' + (document.body.clientHeight - 50) + ',top=10,left=10');
                }
            });
            }

            function OpenNewWindow2_archive() {
                var id_frm = $('#archive_id_frm_contr').val();
                var id_frm_text = $('#cph_archive_id_frm_contr').val();
                $.ajax({
                    url: '/ajax/setses.aspx?key=archive_select_id_frm_contr_filter&value=' + escape(id_frm) + '&key1=archive_select_id_frm_contr_filter_text&value1=' + escape(id_frm_text),
                    type: 'POST',
                    success: function (html) {
                        window.open('/archive/<%= Master.cur_basename%>/select?m=2', 'modal', 'width=' + (document.body.clientWidth - 50) + ',height=' + (document.body.clientHeight - 50) + ',left=10,top=10');
                }
            });
            }

            function changeBut2(id, name) {
                $('#archive_docpack').val(id);
                return false;
            }

            function changeBut(id, name) {
                $('#archive_id_parent').val(id);
                $('#cph_archive_id_parent').val(name);
                $.ajax({
                    url: '/ajax/get_prjcode_by_id_archive.aspx?cb=<%= Master.cur_basename%>&id=' + escape(id),
                type: 'POST',
                success: function (html) {
                    arr = html.split('|');
                    $('#archive_id_prjcode').val(arr[0]);
                    $('#cph_archive_id_prjcode').val(arr[1]);
                }
            });
            return false;
        }

    </script>
</asp:Content>
