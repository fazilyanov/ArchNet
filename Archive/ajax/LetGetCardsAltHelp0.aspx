<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LetGetCardsAltHelp0.aspx.cs" Inherits="WebArchiveR6.LetGetCardsAltHelp0" %>

<%@ Register Assembly="Trirand.Web, Version=4.5.4.0, Culture=neutral, PublicKeyToken=e2819dc449af3295"
    Namespace="Trirand.Web.UI.WebControls" TagPrefix="cc1" %>

<%@ Register assembly="Trirand.Web" namespace="Trirand.Web.UI.WebControls" tagprefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="/scripts/trirand/i18n/grid.locale-ru.js" type="text/javascript"></script>
    <script src="/scripts/trirand/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery.md5.js" type="text/javascript"></script>
    <link href="/styles/jquery-ui-1.10.4.custom.css" rel="stylesheet" type="text/css" />
    <link href="/styles/ui.jqgrid.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-md-1"></div>
            <div class="col-md-9">
                <br />
                <p style="text-indent: 25px;">
                    В 1С: УСО для удобства просмотра сканированных копий документов заменена форма перехода в электронный архив: 
                    кликнув на код ЭА документа или пакета документов в соответствующей колонке 1С: УСО, открывается форма, позволяющая
                    просматривать сразу все документы, относящиеся к спецификации, на основании которой формировались бухгалтерские документы (рисунок 1).
                </p>
                <br />
                <a class="thumbnail" href="/site/img/LetGetCardsAlt_0_1.png" target="_blank">
                    <img src="/site/img/LetGetCardsAlt_0_1.png" alt="">
                </a>
                <p style="text-align: center;">рисунок 1</p>
                <br />


                <p style="text-indent: 25px;">
                   На рисунке 1 отображена структура подчиненности документов. Кликнув в панели навигации раздела «Структура» по кнопке «Показать полную структуру договора», можно увидеть все документы, относящиеся к договору, на основании которого была сформирована спецификация (рисунок 2).  
                </p>
                <br />
                <a class="thumbnail" href="/site/img/LetGetCardsAlt_0_2.png" target="_blank">
                    <img src="/site/img/LetGetCardsAlt_0_2.png" alt="">
                </a>
                <p style="text-align: center;">рисунок 2</p>
                <br />

                

                В нижней части страницы отображается состав пакета документов в виду их принадлежности к одной хозяйственной операции (рисунок 4). 
                В состав пакета могут входить документы, не отраженные в структуре. 
                Также в данном разделе показаны технические документы связанные с выбранным документом (рисунок 5). 
                <br />
                <br />
                <a class="thumbnail" href="/site/img/LetGetCardsAlt_docpack.png" target="_blank">
                    <img src="/site/img/LetGetCardsAlt_docpack.png" alt="">
                </a>
                <p style="text-align: center;">рисунок 4</p>
                <br />

                <br />
                <a class="thumbnail" href="/site/img/LetGetCardsAlt_techbuh.png" target="_blank">
                    <img src="/site/img/LetGetCardsAlt_techbuh.png" alt="">
                </a>
                <p style="text-align: center;">рисунок 5</p>
                
                <br /><br />
                <p style="text-indent: 25px;">
                    <b>Дополнительная информация.</b>
                    <ul>
                        <li>Для открытия файла документа достаточно двойного клика левой кнопкой мыши по выбранной записи ЭА.</li>
                        <li>Во всех списках доступна функция массовой выгрузки всех файлов, для этого требуется нажать на кнопку «Скачать все файлы» в панели навигации.</li>
                        <li>Также в данной форме доступна функция открытия карточки документа. 
                            Для этого необходимо выбрать нужную запись ЭА и кликнуть по кнопке «Открыть карточку», после чего система произведет автоматическую проверку доступа сотрудника к базе данных электронного архива и, при его наличии, откроет карточку документа. Иначе будет выведено сообщение об отсутствии доступа к базе данных.</li>

                    </ul>
                </p>
                <br />
                <br />
                <br />
                <br />
            </div>
            <div class="col-md-1"></div>
        </div>
    </form>
    <script type='text/javascript'>
      
    </script>
</body>
</html>
