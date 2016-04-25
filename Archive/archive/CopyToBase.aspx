<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CopyToBase.aspx.cs" Inherits="WebArchiveR6.CopyToBase" %>
<%@ Register Assembly="Trirand.Web, Version=4.5.4.0, Culture=neutral, PublicKeyToken=e2819dc449af3295"
    Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<%@ Register assembly="Trirand.Web" namespace="Trirand.Web.UI.WebControls" tagprefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
    <div class="panel-body">
        <form id="form1" runat="server">
            <div class="row">
                &nbsp;&nbsp;
            </div>
            <div class="row">
                &nbsp;
            </div>
            <div class="row">
                <div class="col-sm-2"></div>
                <div class="col-sm-6">

                    <div class="input-group">
                        <div class="input-group-btn">
                            <button type="button" id="clear__baseid_base" class="btn btn-xs btn-success" style="width: 150px;" >Целевая база</button>
                        </div>
                        <input id="cph__baseid_base" name="cph__baseid_base" class="form-control ui-autocomplete-input" style="width: 300px;" value="<%Response.Write(dest_rus);%>" onclick="$('#cph__baseid_base').autocomplete('search', ' ');" onchange="if ($('#cph__baseid_base').val()!='')cls('#clear__baseid_base'); else cld('#clear__baseid_base');" autocomplete="off">
                        <input type="hidden" name="_baseid_base" id="_baseid_base" value="<%Response.Write(id_dest);%>">
                    </div>
                    <cc1:JQGrid ID="jqArchiveCheck" runat="server"  ColumnReordering="True"
                         EnableViewState="False" OnDataRequesting="jqArchiveCheck_DataRequesting">
                        <Columns>
                            <cc1:JQGridColumn DataField="id" Width="58" Editable="False" PrimaryKey="True" DataType="Int" HeaderText="Код ЭА" />
                            <cc1:JQGridColumn DataField="docpack" Width="58" DataType="String" HeaderText="Пакет" />
                            <cc1:JQGridColumn DataField="doctree" Width="230" DataType="System.String" HeaderText="Документ" />
                            <cc1:JQGridColumn DataField="date_doc" Width="63" DataType="System.String" HeaderText="Дата док." />
                            <cc1:JQGridColumn DataField="num_doc" Width="108" DataType="System.String" HeaderText="Номер докум." />
                            <cc1:JQGridColumn DataField="frm" Width="230" DataType="System.String" HeaderText="Контрагент" />
                            <cc1:JQGridColumn DataField="summ" Width="100" DataType="System.String" HeaderText="Сумма" DataFormatString="{0:n2}" />
                        </Columns>
                        <ToolBarSettings >
                           <CustomButtons>
                                <cc1:JQGridToolBarButton OnClick="ToCard" Text="Открыть карточку &nbsp;&nbsp;" ToolTip="Перейти к соотвествующей карточке документа" />
                               
                            </CustomButtons>
                        </ToolBarSettings>
                        <AppearanceSettings Caption="Итоги проверки" ShowRowNumbers="True" ShrinkToFit="False" />
                       <ClientSideEvents RowDoubleClick="ToCard"  RowSelect="rowsel" LoadComplete="selectfirst"/>
                    </cc1:JQGrid>
                    <br />
                    <button type="button" id="manualcheck" class="btn btn-xs btn-primary" style="width: 150px;" onclick="window.open('/archive/<% Response.Write(dest);%>/srch');" title="Проверить вручную в целевой базе на наличие документа"  <% Response.Write(id_dest > 0 ? "" : " disabled");%> >Проверить вручную</button>
                    <asp:Button ID="add_to_card" runat="server" Text="Добавить новую карточку" class="btn btn-xs btn-primary" style="width: 160px;" OnClick="add_to_card_Click"  />
                    <asp:Button ID="add_as_ver" runat="server" Text="Добавить в карточку" class="btn btn-xs btn-primary" style="width: 133px;" OnClick="add_as_ver_Click"  />
                    <asp:TextBox ID="id_archive" class="form-control" style="width: 43px;display: inline;" value="0" runat="server" TextMode="Number"></asp:TextBox>
                </div>
                <div class="col-sm-3">
                </div>

            </div>
        </form>
         <script type='text/javascript'>

             $(window).bind('resize', function () {
                 //var grid = jQuery('#cph_jqArchiveStructur');
                 //grid.setGridWidth($(window).width() - 20);
                 //grid.setGridHeight($(window).height() - 160);
                 
             }).trigger('resize');


             $('#cph__baseid_base').autocomplete({
                 source: '/ajax/dd.aspx?table=_base&where=name<>%27<%Response.Write(cb);%>%27',
                 minLength: 1,
                 delay: 10,
                 select: function (event, ui) {
                     $("#_baseid_base").val(ui.item.id);
                     $("#cph__baseid_base").val(ui.item.name);
                     if ($('#_baseid_base').val() != '0')
                     {
                         location.href = (location.href.indexOf('\?') > -1 ? location.href.slice(0, location.href.indexOf('\?')) : location.href.replace('#', '')) + '?dest=' + ui.item.id;
                     }
                     return false;
                 }
             });

             function ToCard() {
                 var grid = jQuery('#cph_jqArchiveCheck');
                 var id = grid.jqGrid('getGridParam', 'selrow');
                 if (id > 0) {
                     window.open('/archive/' + '<% Response.Write(dest);%>' + '/srch?id=' + id + '&act=view');
                 }
                 else alert('Выберите запись');
                 return false;
             }

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

             function rowsel() {
                 var grid = jQuery('#cph_jqArchiveCheck');
                 var id = grid.jqGrid('getGridParam','selrow');
                 $('#cph_id_archive').val(id);
             }

             function selectfirst() {
                 var grid = jQuery("#cph_jqArchiveCheck");
                 var ids = grid.jqGrid("getDataIDs");
                 if (ids && ids.length > 0)
                     grid.jqGrid("setSelection", ids[0]);
             }

             jQuery(document).ready(function () {
                 $('#cph_jqArchiveCheck_pager_center').hide();
                 $(window).resize();
             });

    </script>
    </div>
</asp:Content>

