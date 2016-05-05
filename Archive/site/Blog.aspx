<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Blog.aspx.cs" Inherits="ArchNet.Blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph" runat="server">
    <div class="row">
        <div class="col-md-3">
        </div>
        <div class="col-md-6">
            <br />
            <br />

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Последние</h3>
                </div>
                <div class="panel-body" style="line-height: 1.7;">
                    
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="1 марта 2016"></span>&nbsp; В карточку добавлены кнопки навигации "стрелки". Быстрый вызов (CTRL + стрелка влево/вправо)<span id="n70"></span>
                        <br />
                    </div>

                    <div class="alert alert-info" role="alert">
                        <span class="gi gi-ok_2" title="26 января 2016"></span>&nbsp; Структура справочника "Подразделения" изменена с линейной на древовидную <span id="n69"></span>
                        <br />
                    </div>

                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="17 декабря 2015"></span>&nbsp; Добавлена база ООО «Стройтрансгаз Трубопроводстрой» <span id="n68"></span>
                        <br />
                    </div>

                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="15 декабря 2015"></span>&nbsp; Добавлена возможность поиска документов комплекта в докуметационном фонде (Кнопка «Действия») <span id="n67"></span>
                        <br />
                    </div>

                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="08 декабря 2015"></span>&nbsp; Для связи с соответствующим справочником, в справочник контрагентов добавлено поле "Код 1С" <span id="n66"></span>
                        <br />
                    </div>
                                        
                    <div class="alert alert-info" role="alert">
                        <span class="gi gi-ok_2" title="07 декабря 2015"></span>&nbsp; Теперь сессия "вечная" (При условии, что открыта хотя бы одна из страниц приложения и есть связь с сервером)<span id="n65"></span>
                        <br />
                    </div>
                    
            </div>
                </div>

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Октябрь 2015</h3>
                </div>
                <div class="panel-body" style="line-height: 1.7;">
                    
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="23 сентября 2015"></span>&nbsp;Добавлена функция восстановления удаленных документов
                        <span id="n64"></span>
                    </div>
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="23 сентября 2015"></span>&nbsp;Для тестирования добавлена функция массового редактирования
                        <span id="n63"></span>
                    </div>
                    <div class="alert alert-info" role="alert">
                        <span class="gi gi-ok_2" title="28 сентября 2015"></span>&nbsp; В справочники пользователей добавлено поле - ФИО краткое. В списках поле "ФИО" заменено на "ФИО краткое" <span id="n62"></span>
                        <br />
                    </div>
                    
            </div>
                </div>

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Сентябрь 2015</h3>
                </div>
                <div class="panel-body" style="line-height: 1.7;">
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="23 сентября 2015"></span>&nbsp;В полях с поиском по пользователям системы (Оператор, исполнитель, создал и т.д) добавлен постоянный элемент списка "Я" (Текущий пользователь системы).<br /><br />
                        <center><img style="border-width: 1px; border-style: solid; border-color: #428bca;" src="/site/img/me.png" /></center><span id="n60"></span><br />
                    </div>
                    <div class="alert alert-info" role="alert">
                        <span class="gi gi-ok_2" title="25 сентября 2015"></span>&nbsp; В карточке документа, версия с "галочкой" на одном из полей, помечается красным. <span id="n61"></span>
                        <br />
                    </div>
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="23 сентября 2015"></span>&nbsp;В полях с автодополнением, добавлено втоматическое исправление неправильной раскладки запроса.<br /><br />
                        <center><img style="border-width: 1px; border-style: solid; border-color: #428bca;" src="/site/img/engrus.png" /></center><span id="n59"></span><br />
                    </div>
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="22 сентября 2015"></span>&nbsp;Для упрощения редактирования текстовых полей, добавлен многострочный элемент ввода. Открывается/закрывается по двойному клику по соответствующему полю.<br /><br />
                        <center><img style="border-width: 1px; border-style: solid; border-color: #428bca;" src="/site/img/textarea.png" /></center><span id="n58"></span><br />
                    </div>
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="11 сентября 2015"></span>&nbsp; Добавлена маска ввода для полей с датой<br /><br />
                        <center><img style="border-width: 1px; border-style: solid; border-color: #428bca;" src="/site/img/inputmask.png" /></center><span id="n57"></span><br />
                    </div>
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="8 сентября 2015"></span>&nbsp; Добавлены подсказки, для просмотра всего содержимого тектового поля<br /><br />
                        <center><img style="border-width: 1px; border-style: solid; border-color: #428bca;" src="/site/img/hint.png" /></center><span id="n53"></span><br />
                    </div>
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="8 сентября 2015"></span>&nbsp; Добавлено служебное поле - "Комментарий к документу"<span id="n54"></span><br />
                    </div>
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="10 сентября 2015"></span>&nbsp; Добавлен пункт меню кнопки "Действия", позволяющий найти документ в комплектах по коду ЭА<br /><br />
                        <center><img style="border-width: 1px; border-style: solid; border-color: #428bca;" src="/site/img/menu.png" /></center><span id="n56"></span><br />
                    </div>
                    <div class="alert alert-info" role="alert">
                        <span class="gi gi-ok_2" title="8 сентября 2015"></span>&nbsp; Обновлена страница "<a href="/site/help.aspx" target="_blank">Справка</a>" <span id="n52"></span>
                        <br />
                    </div>
                    <div class="alert alert-info" role="alert">
                        <span class="gi gi-ok_2" title="10 сентября 2015"></span>&nbsp; Убрана кнопка "Найти в SP" (Табличная часть документа)<span id="n55"></span><br />
                    </div>
                    <%--<div class="alert alert-danger" role="alert">
                        <span class="gi gi-bug" title="23 июля 2015"></span>&nbsp;Имправлена редкая ошибка возникающая при полнотекстовом поиске<span id="n50"></span><br />
                    </div>--%>
                </div>
            </div>

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Июль 2015</h3>
                </div>
                <div class="panel-body" style="line-height: 1.7;">

                    <div class="alert alert-success" role="alert"><span class="gi gi-plus" title="30 июля 2015"></span>&nbsp;Добавлена кнопка для перехода к журналу изменений для выбранной версии документа. <span id="n51"></span></div>
                    <div class="alert alert-success" role="alert"><span class="gi gi-plus" title="23 июля 2015"></span>&nbsp;Для тестирования добавлена функция копирования документов из одной базы в другую<span id="n49"></span></div>
                    <div class="alert alert-success" role="alert"><span class="gi gi-plus" title="14 июля 2015"></span>&nbsp;Сервис - "Анализ документов по штрихкоду"<span id="n48"></span>   </div>
                    <div class="alert alert-success" role="alert"><span class="gi gi-plus" title="18 июля 2015"></span>&nbsp;Спецификации старых комплектов<span id="n46"></span>                                         </div>
                    <div class="alert alert-info" role="alert"><span class="gi gi-ok_2" title="10 июля 2015"></span>&nbsp;Переработана страница просмотра документов из 1С УСО <a href="/test/letgetcardsalt/756035">Пример</a> <span id="n47"></span></div>
                    <div class="alert alert-danger" role="alert"><span class="gi gi-bug" title="23 июля 2015"></span>&nbsp;Иcправлена редкая ошибка, возникающая при полнотекстовом поиске<span id="n50"></span>                    </div>
                </div>
            </div>

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Июнь 2015</h3>
                </div>
                <div class="panel-body" style="line-height: 1.7;">
                    <div class="alert alert-success" role="alert"><span class="gi gi-plus" title="18 июня 2015"></span>&nbsp;Новый блок "Локальные нормативные документы"<span id="n43"></span></div>
                    <div class="alert alert-success" role="alert"><span class="gi gi-plus" title="18 июня 2015"></span>&nbsp;Документы: Регламент, Инструкция, Положение, Политика, Руководство, Требования, Процедура. <span id="n44"></span></div>
                    <div class="alert alert-success" role="alert"><span class="gi gi-plus" title="18 июня 2015"></span>&nbsp;В модуле "Регистр хранения дел" добавлена возможность выбора организации <span id="n42"></span></div>
                    <div class="alert alert-success" role="alert"><span class="gi gi-plus" title="16 июня 2015"></span>&nbsp;В Модуле "Комплекты" добалено поле "Количество документов"<span id="n40"></span></div>
                    <div class="alert alert-success" role="alert"><span class="gi gi-plus" title="1 июня 2015"></span>&nbsp;Добалена функция выгрузки в файл CSV <span id="n34"></span></div>
                    <div class="alert alert-success" role="alert"><span class="gi gi-plus" title="10 июня 2015"></span>&nbsp;Для тестирования добалена функция поиска по структуре документов (Действия -> Поиск по структуре) <span id="n37"></span></div>

                    <div class="alert alert-info" role="alert"><span class="gi gi-ok_2" title="23 июня 2015"></span>&nbsp;Поле "Дата создания" в модуле "Комплекты" подсвечивается красным цветом, если комплект небыл обработан в течении 3-х дней и более<span id="n45"></span></div>
                    <div class="alert alert-info" role="alert"><span class="gi gi-ok_2" title="16 июня 2015"></span>&nbsp;В бухгалтерских документах скрыто поле "Содержание документа" <span id="n41"></span></div>
                    <div class="alert alert-info" role="alert"><span class="gi gi-ok_2" title="11 июня 2015"></span>&nbsp;Модуль "Комплекты на исправление" переименован в "Комплекты движения документов"<span id="n38"></span></div>
                    <div class="alert alert-info" role="alert"><span class="gi gi-ok_2" title="11 июня 2015"></span>&nbsp;В модуле "Комплекты" атрибут "Электронные каналы связи" заменен на атрибут "Источник" (Регламентное сканирование, электронные копии, выгрузка<span id="n39"></span></div>

                    <div class="alert alert-danger" role="alert"><span class="gi gi-bug" title="3 июня 2015"></span>&nbsp;Исправлено подтверждение удаления записи из табличной части документа<span id="n35"></span></div>
                    <div class="alert alert-danger" role="alert"><span class="gi gi-bug" title="3 июня 2015"></span>&nbsp;Устранена ошибка функции скрытия документа<span id="n36"></span></div>
                    <div class="alert alert-danger" role="alert"><span class="gi gi-bug" title="4 июня 2015"></span>&nbsp;Исправлен баг, при котором, иногда не работала кнопка "Игнорировать"</div>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Май 2015</h3>
                </div>
                <div class="panel-body" style="line-height: 1.7;">
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="29 мая 2015 9:26"></span>&nbsp;Добавлена функция отражения ошибок в полях карточки документа с помощью пометок -  "галочек". 
                        Супервайзер, обнаружив ошибку в любом поле карточки документа, устанавливает "Галочку супервайзера" (одним кликом устанавливаются две галочки, в т.ч. и в "галочка оператора"). 
                        Оператор ОЦ при исправлении ошибки, самостоятельно снимает "Галочку оператора", после чего супервайзер проверяет исправление и снимает "галочку супервайзера". 
                        Все ошибки заносятся в "Отчет ошибок операторов ОЦ"<span id="n30"></span><br />
                        <span class="gi gi-plus" title="28 мая 2015 9:26"></span>&nbsp;Новый статус для версии документа - "Электронный документ".<span id="n32"></span><br />
                        <span class="gi gi-plus" title="7 мая 2015 9:26"></span>&nbsp;Добавлен раздел "Регистр хранения дел", справочники: "Места хранения", "Группы"<span id="n29"></span><br />
                    </div>
                    <div class="alert alert-info" role="alert">
                        <span class="gi gi-ok_2" title="28 мая 2015 15:10"></span>&nbsp;Запрет на добавление версии без статуса и источника<span id="n31"></span><br />
                        <span class="gi gi-ok_2" title="27 мая 2015 15:10"></span>&nbsp;Настроена автоматическая смена источника на "Электронные каналы связи" при выборе статуса "Электронный документ"<span id="n33"></span><br />
                    </div>
                </div>
            </div>

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Апрель 2015</h3>
                </div>
                <div class="panel-body">
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="14 апреля 2015 11:26"></span>&nbsp;В карточку документа - пункт меню "Журнал", для перехода к поиску записей в журнале по текущему документу<span id="n27"></span><br />
                        <span class="gi gi-plus" title="10 апреля 2015 10:42"></span>&nbsp;Система оценки эффективности операторов.<span id="n24"></span><br />
                        <span class="gi gi-plus" title="2 апреля 2015 09:38"></span>&nbsp;Поле "Тип документа" в разделе "Комплекты", для разграничения доступа к документам.<span id="n22"></span><br />
                        <span class="gi gi-blank"></span>&nbsp;Группы пользователей:
                        <ul>
                            <li>Большинство - пользователи имеют доступ к комплектам, которые они сами создали или являются в нем исполнителем.</li>
                            <li>Доступ ко всем документам, кроме банковских</li>
                            <li>Полный доступ</li>
                        </ul>
                        <span class="gi gi-plus" title="2 апреля 2015 09:38"></span>&nbsp;Раздел "Спецификации комплектов на исправление"<span id="n20"></span><br />
                    </div>
                    <div class="alert alert-info" role="alert">
                        <span class="gi gi-ok_2" title="13 апреля 2015 15:10"></span>&nbsp;Обратная сортировка по столбцу "Код ЭА" в списке архивных документов<span id="n26"></span><br />
                        <span class="gi gi-ok_2" title="13 апреля 2015 15:10"></span>&nbsp;При открытии или копировании карточки документа со страницы "Поиск документов", учитывается тип документа (открывается страница соответствующего раздела). <span id="n25"></span>
                        <br />
                        <span class="gi gi-ok_2" title="2 апреля 2015 15:10"></span>&nbsp;При создании нового комплекта, в поле "Имя" устанавливается префикс формата "ддммгггг"<span id="n21"></span><br />
                        <span class="gi gi-ok_2" title="2 апреля 2015 09:38"></span>&nbsp;Запрет редактирования комплекта на исправление после подписания исполнителем<span id="n19"></span>
                    </div>
                    <div class="alert alert-danger" role="alert">
                        <span class="gi gi-bug" title="6 апреля 2015 9:40"></span>&nbsp;При загрузке проведенных исключены удаленные записи<span id="n23"></span><br />
                        <span class="gi gi-bug" title="14 апреля 2015 11:52"></span>&nbsp;В комплектах на исправление устранена ошибка, возникающая при ручном добавлении штрихкода.<span id="n28"></span><br />
                    </div>
                </div>
            </div>


            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Март 2015</h3>
                </div>
                <div class="panel-body">
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="18 фебраля 2015 13:30"></span>&nbsp;Выбор типа документа, при создании карточки из страницы поиска<br />
                        <span class="gi gi-plus"></span>&nbsp;Выгрузка файлов из комплектов<br />
                        <span class="gi gi-plus" title="16 фебраля 2015 9:49"></span>&nbsp;Поля: оператор ОЦ, примечание и важность в новых комплектах<br />
                        <span class="gi gi-plus" title="10 марта 2015 11:20"></span>&nbsp;Открытие карточек в том же окне. Сохранение пареметров страницы (направление сортировки, страница, выбранная строка)<br />
                        <span class="gi gi-plus" title="11 марта 2015 11:20"></span>&nbsp;Кнопка "Комплект" в спецификациях комплектов, открывающая комплект соответствующий выбранной записи<br />
                        <span class="gi gi-plus" title="30 марта 2015 11:20"></span>&nbsp;Комплекты на исправление<br />
                        <span class="gi gi-plus" title="30 марта 2015 11:20"></span>&nbsp;Кнопка "Перезагрузить сеанс"<br />
                    </div>
                    <div class="alert alert-info" role="alert">
                        <span class="gi gi-ok_2" title="18 марта 2015 12:00"></span>&nbsp;В бухгалтерских документах, при выборе старшего документа, в поле "Код проекта" автоматически подставлятся код проекта старшего документа
                        <br />
                        <span class="gi gi-ok_2" title="30 марта 2015 12:00"></span>&nbsp;Раздел комплектов и общие справочники выведены за рамки баз<br />
                    </div>
                </div>
            </div>

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Февраль 2015</h3>
                </div>
                <div class="panel-body">
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="6 фебраля 2015 15:36"></span>&nbsp;Страница просмотра
                        документов архива по подразделениям<br />
                        <span class="gi gi-plus" title="16 фебраля 2015 9:49"></span>&nbsp;Проверка на задвоение
                        записей в архиве<br />
                        <span class="gi gi-plus" title="16 фебраля 2015 9:49"></span>&nbsp;Функция массовой
                        выгрузки файлов<br />
                        <span class="gi gi-plus" title="16 фебраля 2015 9:49"></span>&nbsp;Функционал импорта
                        информации о проведенных документах из 1С
                        <br />
                        <span class="gi gi-plus" title="24 фебраля 2015 17:29"></span>&nbsp;Проверка заполнения полей при статусе "Завершен"
                        <br />
                    </div>
                    <div class="alert alert-info" role="alert">
                        <span class="gi gi-ok_2" title="16 фебраля 2015 9:49"></span>&nbsp;Переписан раздел
                        "Комплекты"<br />
                    </div>
                    <div class="alert alert-danger" role="alert">
                        <span class="gi gi-bug" title="12 января 2015 15:14"></span>&nbsp;Исправлено положение
                        всплывающих окон для систем с двумя мониторами<br />
                    </div>
                </div>
            </div>

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Январь 2015</h3>
                </div>
                <div class="panel-body">
                    <a name="settings"></a>
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="26 января 2015 09:36"></span>&nbsp;Добавлена вкладка
                        "Бухгалтерские документы" в карточку технических документов<br />
                        <span class="gi gi-plus" title="22 января 2015 09:06"></span>&nbsp;Добавлены атрибуты
                        "Скрытый", "Состояние" и "Принят к учету"<br />
                        <span class="gi gi-plus" title="16 января 2015 09:57"></span>&nbsp;К вкладке версии
                        документа добавлена кнопка "Найти в SP", позволяющая перейти к поиску документа
                        по штрихкоду на портале Share Point.
                    </div>
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="13 января 2015 17:10"></span>&nbsp;В раздел "Сервис"
                        добавлен пункт "Настройка".
                    </div>
                    <div class="alert alert-default" role="alert">
                        <ul>
                            <li>На странице настройки будут находиться личные настройки пользователя. </li>
                            <li>На данный момент, присутствует лишь пункт настройки двойного клика в списке, позволяющий
                                выбрать соответствующее действие.</li>
                            <li>Настройки сохраняются автоматически</li>
                        </ul>
                    </div>
                    <div class="alert alert-info" role="alert">
                        <span class="gi gi-ok_2" title="21 января 2015 08:10"></span>&nbsp;Основной цвет
                        элементов изменен на рекомендуемый для портала<br />
                        <span class="gi gi-ok_2" title="21 января 2015 08:10"></span>&nbsp;Обновлен справочник
                        "Коды проектов"<br />
                        <span class="gi gi-ok_2" title="12 января 2015 12:10"></span>&nbsp;Новая версия
                        автоматически помечается основной, пометки с остальных версий снимаются.<br />
                        <span class="gi gi-ok_2" title="13 января 2015 17:19"></span>&nbsp;По умолчанию,
                        выбор из списка при редактировании полей таких как: "Пакет" и "Старший документ",
                        происходит двойным кликом по строке списка.<br />
                        <span class="gi gi-ok_2" title="13 января 2015 17:23"></span>&nbsp;Вызов диалогов
                        при редактировании карточки происходит только по клику по полю, либо по нажатию
                        пробела.<br />
                    </div>
                    <div class="alert alert-danger" role="alert">
                        <span class="gi gi-bug" title="12 января 2015 15:14"></span>&nbsp;Устранена ошибка,
                        возникающая при проверке удаляемой карточки.<br />
                        <span class="gi gi-bug" title="21 января 2015 08:14"></span>&nbsp;Исправлено отображение
                        кавычек в текстовых полях на форме редактирования<br />
                    </div>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Декабрь 2014</h3>
                </div>
                <div class="panel-body">
                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus" title="26 декабря 2014 15:14"></span>&nbsp;Добавлен отчет
                        о доступах, настройках и посещениях пользователей.
                    </div>
                    <div class="alert alert-info" role="alert">
                        <span class="gi gi-ok_2" title="26 декабря 2014 11:00"></span>&nbsp; Диалоговые
                        окна выбора из справочников (Старший документ, пакет и т.д), теперь можно вызвать
                        нажатием пробела.<br />
                        <span class="gi gi-ok_2" title="26 декабря 2014 11:00"></span>&nbsp; Убраны лишние
                        поля в разделе "Техническая документация"<br />
                        <span class="gi gi-ok_2" title="25 декабря 2014 11:00"></span>&nbsp; При создании
                        карточки или версии из комплекта, источником по умолчанию установливается "Бумажный
                        экземпляр".<br />
                        <span class="gi gi-ok_2"></span>&nbsp;Сумма в карточке отображается с разделением
                        разрядов пробелами.
                    </div>
                    <div class="alert alert-danger" role="alert" title="25 декабря 2014 10:44">
                        <span class="gi gi-bug"></span>&nbsp;Исправлена ошибка открытия файла при ручном
                        редактировании комплекта.
                    </div>
                    <a name="columnchooser"></a>
                    <div class="alert alert-success" role="alert" title="25 декабря 2014 00:14">
                        <span class="gi gi-plus"></span>&nbsp;Добавлены на тестирование функции настройки
                        видимости, размера и положения столбцов таблиц.
                    </div>
                    <div class="alert alert-default" role="alert">
                        <ul>
                            <li>Настройка ширины столбца: перетащите правую границу его заголовка до нужной ширины.
                            </li>
                            <li>Перемещение столбца: наведите указатель мыши на заголовок, перетащите в нужное место</li>
                            <li>Для скрытия ненужных столбцов, необходимо воспользоваться соответствующей кнопкой
                                <img src="/site/img/13.png" alt="">
                                в правой части заголовка таблицы. Откроется диалоговое окно:<br />
                                <a class="thumbnail" href="/site/img/15.png" target="_blank">
                                    <img src="/site/img/15.png" alt=""></a> Для выделения нескольких столбцов, щелкайте
                                поочередно каждый объект, удерживая нажатой клавишу CTRL.Поля с уникальным идентификатором
                                срыть нельзя.</li>
                            <li>Все проделанные изменения сохраняются только для текущей сессии. Чтобы сохранить
                                настроки, для восстановления их при следующем запуске, воспользуйтесь кнопкой
                                <img src="/site/img/14.png" alt="">
                                в правой части заголовка таблицы. </li>
                        </ul>
                    </div>

                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus"></span>&nbsp;Добавлена возможность оповещения пользователей
                        и раздел "Последние изменения"
                    </div>

                    <div class="alert alert-success" role="alert">
                        <span class="gi gi-plus"></span>&nbsp;Добавлен раздел "Комплекты"
                    </div>

                    <div class="alert alert-info" role="alert">
                        <span class="gi gi-ok_2"></span>&nbsp;Запуск на портале, предоставлен доступ операторам
                    </div>
                </div>
            </div>
            <a name="first"></a>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Октябрь 2014</h3>
                </div>
                <div class="panel-body">
                    <div class="alert alert-info" role="alert">
                        <span class="gi gi-ok_2"></span>&nbsp;Начало разработки основного функционала
                    </div>
                </div>
            </div>
    </div>
    <div class="col-md-3">
        </div>
    </div>
    <script type='text/javascript'>
        <%Response.Write(_js);%>
    </script>
</asp:Content>
