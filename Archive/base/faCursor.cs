// ***********************************************************************
// Assembly         : ArchNet
// Author           : Artur Fazilyanov (a.fazilyanov@gmail.com)
// Created          : 02-25-2016
//
// Last Modified By : a.fazilyanov
// Last Modified On : 03-24-2016
// ***********************************************************************
// <copyright file="faCursor.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Web;

namespace ArchNet
{
    /// <summary>
    /// Описывает конкретный курсор
    /// </summary>
    public class faCursor
    {
        #region Поля класса

        /// <summary>
        /// Поля для курсора
        /// </summary>
        private faField[] _fld;

        /// <summary>
        /// Количество полей
        /// </summary>
        private int _fldcnt;

        #endregion Поля класса

        /// <summary>
        /// Конструктор для <see cref="faCursor"/>
        /// </summary>
        /// <param name="a">Обычно, имя таблицы для курсора </param>
        public faCursor(string a)
        {
            Alias = a;
            AliasAlt = string.Empty;
            TableID = -1;
            SesCurName = a + "_cursor";
            Caption = string.Empty;
            _fldcnt = 0;
            EditDialogWidth = 400;
            RelTable = null;
            RelBase = string.Empty;
            ShowPager = false;
            ShowColumnViewButtons = true;
            EnableCSVButton = false;
            EnableBulkEditButton = false;
            HeaderGroupList = new List<faHeaderGroup>();
            ColumnViewString = string.Empty;
        }

        #region Свойства

        /// <summary>
        /// Имя таблицы / предоставления
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Альтернативное имя таблицы / предоставления
        /// </summary>
        public string AliasAlt { get; set; }

        /// <summary>
        /// Заголовок для Курсора
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Id клиента для JQGrid-a
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// Описывает порядок и размер столбцов курсора (список)
        /// </summary>
        public string ColumnViewString { get; set; }

        /// <summary>
        /// Номер текущей страницы курсора
        /// </summary>
        public int CurrentPageNumber
        {
            get { return (int)(HttpContext.Current.Session[Alias + "_pagenumber"] ?? 1); }
            set
            {
                if (value != 0)
                    HttpContext.Current.Session[Alias + "_pagenumber"] = value;
                else
                    HttpContext.Current.Session.Remove(Alias + "_pagenumber");
            }
        }

        /// <summary>
        /// Ширина диалогового окна редактирования (карточка)
        /// </summary>
        public int EditDialogWidth { get; set; }

        /// <summary>
        /// Доступность кнопки "Все файлы"
        /// </summary>
        public bool EnablAllFileButton { get; set; }

        /// <summary>
        /// Доступность кнопки "Добавить"
        /// </summary>
        public bool EnableAddButton { get; set; }

        /// <summary>
        /// Доступность кнопки "Множественное редактирование"
        /// </summary>
        public bool EnableBulkEditButton { get; set; }

        /// <summary>
        /// Доступность кнопки "Копировать"
        /// </summary>
        public bool EnableCopyButton { get; set; }

        /// <summary>
        /// Доступность кнопки "CSV"
        /// </summary>
        public bool EnableCSVButton { get; set; }

        /// <summary>
        /// Доступность кнопки "Удалить"
        /// </summary>
        public bool EnableDelButton { get; set; }

        /// <summary>
        /// Доступность кнопки "Редактировать"
        /// </summary>
        public bool EnableEditButton { get; set; }

        /// <summary>
        /// Доступность кнопки "Excel"
        /// </summary>
        public bool EnableExcelButton { get; set; }

        /// <summary>
        /// Доступность кнопки "Файл"
        /// </summary>
        public bool EnableFileButton { get; set; }

        /// <summary>
        /// Доступность кнопки "Сохранить", и вообще режима редактирования
        /// </summary>
        public bool EnableSaveButton { get; set; }

        /// <summary>
        /// Доступность кнопки "Просмотр"
        /// </summary>
        public bool EnableViewButton { get; set; }

        /// <summary>
        /// Количество полей
        /// </summary>
        public int FieldCount { get { return _fldcnt; } }

        /// <summary>
        /// Все поля курсора
        /// </summary>
        /// <value>The fields.</value>
        public faField[] Fields { get { return _fld; } }

        /// <summary>
        /// Устанавливает группировку заголовков таблицы
        /// </summary>
        /// <value>Список группировок на таблицу</value>
        public List<faHeaderGroup> HeaderGroupList { get; set; }

        /// <summary>
        /// Количество записей на страницу
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Указывает в какой базе проверять использование записи (например, при удалении)
        /// </summary>
        public string RelBase { get; set; }

        /// <summary>
        /// Указывает в каких таблицах проверять использование записи (например, при удалении)
        /// </summary>
        public string[] RelTable { get; set; }

        /// <summary>
        /// Хранит id последней выбранной записи, чтобы выделить эту запись снова, по возвращению
        /// </summary> 
        public string SelectedRow
        {
            get { return (HttpContext.Current.Session[Alias + "_selectedrow"] ?? "0").ToString(); }
            set
            {
                if (value != "")
                    HttpContext.Current.Session[Alias + "_selectedrow"] = value;
                else
                    HttpContext.Current.Session.Remove(Alias + "_selectedrow");
            }
        }

        /// <summary>
        /// Ключ сессии в которой храниться курсор
        /// </summary>
        public string SesCurName { get; set; }

        /// <summary>
        /// Показывать или нет кнопки настройки видимости и порядка столбцов 
        /// </summary>
        public bool ShowColumnViewButtons { get; set; }

        /// <summary>
        /// Показывать ли пагинатор 
        /// </summary>
        public bool ShowPager { get; set; }

        /// <summary>
        /// Имя столбца для сортировки
        /// </summary>
        public string SortColumn
        {
            get { return (HttpContext.Current.Session[Alias + "_sortcolumn"] ?? "id").ToString(); }
            set
            {
                if (value != "")
                    HttpContext.Current.Session[Alias + "_sortcolumn"] = value;
                else
                    HttpContext.Current.Session.Remove(Alias + "_sortcolumn");
            }
        }

        /// <summary>
        /// Направление сортировки 
        /// </summary>
        public string SortDirection
        {
            get { return (HttpContext.Current.Session[Alias + "_sortdir"] ?? "Asc").ToString(); }
            set
            {
                if (value != "")
                    HttpContext.Current.Session[Alias + "_sortdir"] = value;
                else
                    HttpContext.Current.Session.Remove(Alias + "_sortdir");
            }
        }

        /// <summary>
        /// ID таблицы в базе (таблица "dbo._table")
        /// </summary>
        /// <remarks>Нужно для правильной работы журнала</remarks>
        public int TableID { get; set; }

        #endregion Свойства

        /// <summary>
        /// Добавляет поле в список полей
        /// </summary>
        /// <param name="fld">Поле</param>
        public void AddField(faField fld)
        {
            _fldcnt++;
            Array.Resize(ref _fld, _fldcnt);
            _fld[_fldcnt - 1] = new faField();
            _fld[_fldcnt - 1] = fld;
            _fld[_fldcnt - 1].Filter.SesValName = (AliasAlt != "" ? AliasAlt : (fld.Data.Table != "" ? fld.Data.Table : Alias)) + "_" + fld.Data.FieldName;
        }
    }
}