<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LetGetCardsAlt.aspx.cs" Inherits="WebArchiveR6.LetGetCardsAlt" %>

<%@ Register Assembly="Trirand.Web, Version=4.5.4.0, Culture=neutral, PublicKeyToken=e2819dc449af3295"
    Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc1" %>

<%@ Register Assembly="Trirand.Web" Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc2" %>


<!DOCTYPE html>
<html lang="ru">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Архив документов</title>
    <link href="/styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="/styles/jquery-ui-1.10.4.custom.css" rel="stylesheet" type="text/css" />
    <link href="/styles/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="/styles/site.css?t=2" rel="stylesheet" type="text/css" />
    <link href="/styles/icons.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="favicon.ico" type="image/ico" />
    <script src="/scripts/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="/scripts/trirand/i18n/grid.locale-ru.js" type="text/javascript"></script>
    <script src="/scripts/trirand/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery.md5.js" type="text/javascript"></script>
    <script src="/scripts/jquery-ui.min.js" type="text/javascript"></script>
</head>
<body style="padding-top: 0px;">
    <div class="" style="position: fixed; right: 15px; top: 3px; z-index: 1100; color: #fff; padding: 0px 5px;">Была изменена форма просмотра документа <a style="color: #fff; background-color: red;" href="\letgetcardsalthelp<%=justdog%>">Подробнее</a></div>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td style="vertical-align: top;">
                    <%--<div class="alert alert-success" role="alert">Была изменена форма просмотра документа <a target="_blank" href="\letgetcardsalthelp">Подробнее</a></div>--%>
                    <cc1:JQGrid ID="jqArchiveStructur" runat="server" ColumnReordering="True"
                        EnableViewState="False" OnDataRequesting="jqArchiveStructur_DataRequesting">
                        <Columns>
                            <cc1:JQGridColumn DataField="id" Width="100" Editable="False" PrimaryKey="True" DataType="Int"
                                HeaderText="Код ЭА" />
                            <cc1:JQGridColumn DataField="treetext" Width="200" DataType="System.String" HeaderText="Номер документа" />
                            <cc1:JQGridColumn DataField="doctree" Width="200" DataType="System.String" HeaderText="Документ" />
                            <cc1:JQGridColumn DataField="docpack" Width="80" DataType="System.String" HeaderText="Пакет" />
                            <cc1:JQGridColumn DataField="content" Width="300" DataType="System.String" HeaderText="Содержание" />
                            <cc1:JQGridColumn DataField="file" Width="1" DataType="System.String" Visible="false" />
                        </Columns>
                        <AppearanceSettings Caption="Структура договора" ShrinkToFit="False" />
                        <TreeGridSettings Enabled="true" />
                        <ToolBarSettings>
                            <CustomButtons>
                                <cc1:JQGridToolBarButton OnClick="ToFile1" Text="Открыть файл &nbsp;&nbsp;" ToolTip="Открыть файл документа" />
                                <cc1:JQGridToolBarButton OnClick="ToCard1" Text="Открыть карточку &nbsp;&nbsp;" ToolTip="Перейти к соотвествующей карточке документа" />
                                <cc1:JQGridToolBarButton OnClick="ToFull" Text="Полная / Сокращенная структура &nbsp;&nbsp;" />
                                <cc1:JQGridToolBarButton OnClick="JustDog" />
                                <cc1:JQGridToolBarButton
                                    OnClick="GoToFileDown1" Text="Скачать все файлы &nbsp;&nbsp;"
                                    ToolTip="Выгрузить файлы соответствующие документам в списке"
                                    ButtonIcon="ui-icon-arrowthickstop-1-s" />
                            </CustomButtons>
                        </ToolBarSettings>
                        <ClientSideEvents RowDoubleClick="ToFile1" RowSelect="rowsel" />
                    </cc1:JQGrid>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    <div id="tabs">
                        <ul>
                            <li><a href="#tabs-1">Пакет документов</a></li>
                            <li><a href="#tabs-2">Технические документы</a></li>
                        </ul>
                        <div id="tabs-1">
                            <cc1:JQGrid ID="jqArchiveDocpack" runat="server" ColumnReordering="True"
                                EnableViewState="False" OnDataRequesting="jqArchiveDocpack_DataRequesting">
                                <Columns>
                                    <cc1:JQGridColumn DataField="id" Width="58" Editable="False" PrimaryKey="True" DataType="Int" HeaderText="Код ЭА" />
                                    <cc1:JQGridColumn DataField="docpack" Width="58" DataType="String" HeaderText="Пакет" />
                                    <cc1:JQGridColumn DataField="doctree" Width="230" DataType="System.String" HeaderText="Документ" />
                                    <cc1:JQGridColumn DataField="date_doc" Width="63" DataType="System.String" HeaderText="Дата док." />
                                    <cc1:JQGridColumn DataField="num_doc" Width="108" DataType="System.String" HeaderText="Номер докум." />
                                    <cc1:JQGridColumn DataField="frm" Width="230" DataType="System.String" HeaderText="Контрагент" />
                                    <cc1:JQGridColumn DataField="summ" Width="100" DataType="System.String" HeaderText="Сумма" DataFormatString="{0:n2}" />
                                    <cc1:JQGridColumn DataField="code_new" Width="100" DataType="System.String" HeaderText="Код проекта" />
                                    <cc1:JQGridColumn DataField="name" Width="100" DataType="System.String" HeaderText="Исполнитель" />
                                    <cc1:JQGridColumn DataField="file" Width="1" DataType="System.String" Visible="false" />
                                </Columns>
                                <PagerSettings PageSize="5000" PageSizeOptions="[5000]" />
                                <ToolBarSettings>
                                    <CustomButtons>
                                        <cc1:JQGridToolBarButton OnClick="ToFile2" Text="Открыть файл &nbsp;&nbsp;" ToolTip="Открыть файл документа" />
                                        <cc1:JQGridToolBarButton OnClick="ToCard2" Text="Открыть карточку &nbsp;&nbsp;" ToolTip="Перейти к соотвествующей карточке документа" />
                                        <cc1:JQGridToolBarButton
                                            OnClick="GoToFileDown2" Text="Скачать все файлы &nbsp;&nbsp;"
                                            ToolTip="Выгрузить файлы соответствующие документам в списке"
                                            ButtonIcon="ui-icon-arrowthickstop-1-s" />
                                    </CustomButtons>
                                </ToolBarSettings>
                                <AppearanceSettings Caption="" ShowRowNumbers="True" ShrinkToFit="False" />
                                <ClientSideEvents RowDoubleClick="ToFile2" />
                            </cc1:JQGrid>
                        </div>
                        <div id="tabs-2">
                            <cc1:JQGrid ID="jqArchiveBuhTech" runat="server" ColumnReordering="True"
                                EnableViewState="False" OnDataRequesting="jqArchiveBuhTech_DataRequesting">
                                <Columns>
                                    <cc1:JQGridColumn DataField="id" Width="58" Editable="False" PrimaryKey="True" DataType="Int" HeaderText="Код ЭА" />
                                    <cc1:JQGridColumn DataField="date_doc" Width="70" DataType="System.String" HeaderText="Дата док." />
                                    <cc1:JQGridColumn DataField="doctree" Width="250" DataType="System.String" HeaderText="Документ" />
                                    <cc1:JQGridColumn DataField="num_doc" Width="200" DataType="System.String" HeaderText="Номер докум." />
                                    <cc1:JQGridColumn DataField="frm" Width="300" DataType="System.String" HeaderText="Контрагент" />
                                    <cc1:JQGridColumn DataField="docpack" Width="80" DataType="String" HeaderText="Пакет" />
                                    <cc1:JQGridColumn DataField="file" Width="1" DataType="System.String" Visible="false" />
                                </Columns>
                                <PagerSettings PageSize="5000" PageSizeOptions="[5000]" />
                                <ToolBarSettings>
                                    <CustomButtons>
                                        <cc1:JQGridToolBarButton OnClick="ToFile3" Text="Открыть файл &nbsp;&nbsp;" ToolTip="Открыть файл документа" />
                                        <cc1:JQGridToolBarButton OnClick="ToCard3" Text="Открыть карточку &nbsp;&nbsp;" ToolTip="Перейти к соотвествующей карточке документа" />
                                        <cc1:JQGridToolBarButton
                                            OnClick="GoToFileDown3" Text="Скачать все файлы &nbsp;&nbsp;"
                                            ToolTip="Выгрузить файлы соответствующие документам в списке"
                                            ButtonIcon="ui-icon-arrowthickstop-1-s" />
                                    </CustomButtons>
                                </ToolBarSettings>
                                <AppearanceSettings Caption="" ShowRowNumbers="True" ShrinkToFit="False" />
                                <ClientSideEvents RowDoubleClick="ToFile3" />
                            </cc1:JQGrid>
                        </div>
                    </div>
                </td>
            </tr>
        </table>

        <div id="dialog" title="Карточка документа">
            <div class="input-group" style="margin-top: 8px;">
                <label style="width: 110px; float: left;">Код ЭА</label>
                <input class="form-control" style="width: 250px;" value="">
            </div>
            <%----%>
        </div>
    </form>
    <script type='text/javascript'>

        jQuery(document).ready(function () {
            $(window).resize();
            $('#jqArchiveStructur_pager_left').width(800);
            $('#jqArchiveDocpack_pager_center').hide();
            $('#jqArchiveBuhTech_pager_center').hide();
            $("#tabs").tabs();
            $("#dialog").dialog({
                autoOpen: false,
                height: 600,
                width: 700
            });
        });

        $(window).bind('resize', function () {
            var grid = jQuery('#jqArchiveStructur');
            grid.setGridWidth($(window).width() - 15);
            var t = $(window).height() / 2;
            grid.setGridHeight(t);
            var grid2 = jQuery('#jqArchiveDocpack');
            var grid3 = jQuery('#jqArchiveBuhTech');
            grid2.setGridWidth($(window).width() - 25);
            grid3.setGridWidth($(window).width() - 25);
            var h = $(window).height() - t - 180;
            grid2.setGridHeight(h);
            grid3.setGridHeight(h);
        }).trigger('resize');

        function ToFull() {
            var grid = jQuery('#jqArchiveStructur');
            $.ajax({
                url: '/ajax/setses.aspx?key=LetGetCardAltFull&value=<%Response.Write((Session["LetGetCardAltFull"] ?? "").ToString() == "full" ? "0" : "full");%>',
                type: 'POST',
                success: function (html) {
                    location.reload();
                }
            });
        return false;
    }

    function JustDog() {
        var grid = jQuery('#jqArchiveStructur');
        $.ajax({
            url: '/ajax/setses.aspx?key=JustDog&value=<%Response.Write((Session["JustDog"] ?? "").ToString() == "1" ? "0" : "1");%>',
        type: 'POST',
        success: function (html) {
            location.reload();
        }
    });
    return false;
}

function rowsel() {
    var grid = jQuery('#jqArchiveStructur');
    var id = grid.jqGrid('getGridParam', 'selrow');
    //alert(id);
    $.ajax({
        url: '/ajax/setses.aspx?key=LetGetCardDocpackId&value=' + escape(id),
        type: 'POST',
        success: function (html) {
            jQuery('#jqArchiveDocpack').trigger('reloadGrid');
            jQuery('#jqArchiveBuhTech').trigger('reloadGrid');
        }
    });

}

function ToCard1() {
    ToCard('#jqArchiveStructur');
}

function ToCard2() {
    ToCard('#jqArchiveDocpack');
}

function ToCard3() {
    ToCard('#jqArchiveBuhTech');
}

function ToCard(selector) {
    var grid = jQuery(selector);
    var id = grid.jqGrid('getGridParam', 'selrow');
    if (id > 0) {

        $.ajax({
            url: '/<%=cb%>/letgetcardsalt/0/' + id + '?id=' + id,
                type: 'POST',
                success: function (html) {
                    $("#dialog").html(html);

                }
            });

            $("#dialog").dialog("open");

        } else alert('Выберите запись');
        return false;
    }

    function GoToFileDown1() {
        GoToFileDown('#jqArchiveStructur');
    }

    function GoToFileDown2() {
        GoToFileDown('#jqArchiveDocpack');
    }

    function GoToFileDown3() {
        GoToFileDown('#jqArchiveBuhTech');
    }

    function GoToFileDown(selector) {

        var grid = jQuery(selector);
        var rc = grid.jqGrid('getGridParam', 'reccount');
        var v = grid.getRowData();
        var f = '';
        if (rc > 0) {
            for (var i = 0; i < v.length; i++) {
                f += v[i]['id'] + '=' + v[i]['file'] + '|';
            }
            f = f.substring(0, f.length - 1);

            $.ajax({
                url: '/Archive/GetFileMultiList.aspx?b=<%Response.Write(cb);%>',
                type: 'POST',
                data: f,
                cache: false,
                contentType: false,
                processData: false,
                success: function (html) {
                    document.location.href = '/Archive/GetFileMultiList.aspx?b=<%Response.Write(cb);%>';
                },
                error: function (request, status, error) {
                    alert(request.responseText);
                }
            });
            } else alert('Нет записей для выгрузки');
            return false;
        }

        function ToFile1() {
            ToFile('#jqArchiveStructur');
        }

        function ToFile2() {
            ToFile('#jqArchiveDocpack');
        }

        function ToFile3() {
            ToFile('#jqArchiveBuhTech');
        }

        function ToFile(selector) {
            var grid = jQuery(selector);
            var id = grid.jqGrid('getGridParam', 'selrow');
            if (id > 0) {
                var row = grid.getRowData(id);
                var f = row.file;
                if (f != '') document.location.href = '/Archive/GetFile.aspx?id=' + id + '&b=<%Response.Write(cb);%>' + '&f=' + f + '&k=' + $.md5(id);
                else alert('Файл не указан');
            } else alert('Выберите запись');
            return false;
        }
    </script>
</body>
</html>
