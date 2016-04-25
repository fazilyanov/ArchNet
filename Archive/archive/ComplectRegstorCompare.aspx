<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ComplectRegstorCompare.aspx.cs" Inherits="WebArchiveR6.ComplectRegstorCompare" %>

<%@ Register Assembly="Trirand.Web, Version=4.5.4.0, Culture=neutral, PublicKeyToken=e2819dc449af3295"
    Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Site.master" %>
<%@ Register Assembly="Trirand.Web" Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
    <form id="form1" runat="server">

        <table>
            <tr>

                <td style="vertical-align: top; padding: 20px;">
                    <cc1:JQGrid ID="jqArchivePerform" runat="server" Width="1200px"
                        Height="600px" EnableViewState="False" OnDataRequesting="jqArchivePerform_DataRequesting">
                        <Columns>
                            <cc1:JQGridColumn DataField="id" Width="58" Editable="False" PrimaryKey="True" DataType="Int" HeaderText="ID" Visible="false" />
                            <cc1:JQGridColumn DataField="id_archive1" Width="58" Editable="False" PrimaryKey="True" DataType="Int" HeaderText="Код ЭА" />
                            <cc1:JQGridColumn DataField="nn1" Width="50" Editable="False" PrimaryKey="True" DataType="Int" HeaderText="Версия" />
                            <cc1:JQGridColumn DataField="barcode1" Width="66" DataType="System.String" HeaderText="Штрихкод" />
                            <cc1:JQGridColumn DataField="status1" Width="80" DataType="System.String" HeaderText="Статус" />
                           
                            
                            <cc1:JQGridColumn DataField="space" Width="20" DataType="System.String" HeaderText=" " TextAlign ="Center" />
                            
                            <cc1:JQGridColumn DataField="id_regstor_list" Width="60" DataType="System.String" HeaderText="Запись №" />
                            <cc1:JQGridColumn DataField="id_archive2" Width="50" Editable="False" PrimaryKey="True" DataType="Int" HeaderText="Код ЭА" />
                            <cc1:JQGridColumn DataField="nn2" Width="50" Editable="False" PrimaryKey="True" DataType="Int" HeaderText="Версия" />
                            <cc1:JQGridColumn DataField="barcode2" Width="66" DataType="System.String" HeaderText="Штрихкод" />
                            <cc1:JQGridColumn DataField="status2" Width="80" DataType="System.String" HeaderText="Статус" />

                            <cc1:JQGridColumn DataField="space2" Width="20" DataType="System.String" HeaderText=" " TextAlign ="Center" />

                            <cc1:JQGridColumn DataField="id_regstor" Width="58" DataType="System.String" HeaderText="Дело №" />
                            <cc1:JQGridColumn DataField="name" Width="120" DataType="System.String" HeaderText="Имя" />
                            <cc1:JQGridColumn DataField="regstor_barcode" Width="66" DataType="System.String" HeaderText="Штрихкод дела" />
                            <cc1:JQGridColumn DataField="case_index" Width="80" DataType="System.String" HeaderText="Индекс дела" />


                        </Columns>
                        <GroupSettings CollapseGroups="false">
                            <GroupFields>
                                <cc1:GroupField
                                    DataField="id_archive1"
                                    GroupSortDirection="Asc"
                                    HeaderText="&lt;b&gt;{0}&lt;/b&gt;"
                                    ShowGroupColumn="True"
                                    ShowGroupSummary="False" />
                            </GroupFields>
                        </GroupSettings>
                        <HeaderGroups>
                            <cc1:JQGridHeaderGroup StartColumnName="id" NumberOfColumns="5" TitleText="Комплект" />
                            <cc1:JQGridHeaderGroup StartColumnName="id_regstor_list" NumberOfColumns="5" TitleText="Документационный фонд" />
                            <cc1:JQGridHeaderGroup StartColumnName="id_regstor" NumberOfColumns="4" TitleText="Дело" />
                        </HeaderGroups>
                        <PagerSettings PageSize="5000" PageSizeOptions="[5000]" />
                        <ToolBarSettings>
                            <CustomButtons>
                            <cc1:JQGridToolBarButton OnClick="Update" Text="Обновить" ToolTip="Перезагрузить данные" />
                        </CustomButtons>
                        </ToolBarSettings>
                        <AppearanceSettings Caption="Результаты поиска" ShrinkToFit="False" ShowRowNumbers="true" />
                        <ExportSettings ExportDataRange="Filtered" />
                        <ClientSideEvents RowDoubleClick="ToRegstor" />
                        <%--<SortSettings InitialSortColumn="id_archive1" />--%>
                    </cc1:JQGrid>
                </td>
            </tr>
        </table>
    </form>
    <script type='text/javascript'>

        jQuery(document).ready(function () {

            $("th[title='Комплект'],th[title='Дело'],th[title='Документационный фонд']").css("font-size","14px").css("font-weight","600").css("text-align","center");


            //font-size: 14px;
            //font-weight: 600;
            //text-align: center;

            var grid = jQuery('#cph_jqArchivePerform');
            grid.bind('jqGridLoadComplete', function (e, data) {
                var iCol1 = 0,
                    iCol2 = 0,
                    iRow = 0;
                var cm = grid.jqGrid('getGridParam', 'colModel'),
                    i = 0,
                    l = cm.length;
                for (; i < l; i++) {
                    if (cm[i].name === 'space') {
                        iCol1 = i;
                    }
                }
                
                var cRows = this.rows.length;
                var iRow;
                var row;
                var className;
                for (iRow = 0; iRow < cRows; iRow++) {
                    row = this.rows[iRow];
                    var x1 = $(row.cells[iCol1]);
                    try {
                        if (x1 != null && x1[0].innerText == '!') {
                            x1[0].className = 'redbgcell';
                        }

                    } catch (e) {

                    }
                    
                }
            });
        });

        function ToRegstor() {
            var grid = jQuery('#cph_jqArchivePerform');
            var id = grid.jqGrid('getGridParam', 'selrow');

            if (id > 0) {
                var row = grid.getRowData(id);
                var f = row.id_regstor;
                var e = row.id_regstor_list;
                if (f != '') window.open('/Regstor/?id=' + f + '&act=view&id_regstor_list=' + e);
                else alert('Не указано дело!');
            }
            else alert('Выберите запись'); return false;
        }

        function Update() {
            var grid = jQuery("#<%= jqArchivePerform.ClientID %>");
            grid.trigger('reloadGrid');
            return true;
        }


    </script>
</asp:Content>
