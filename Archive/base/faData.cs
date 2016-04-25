// ***********************************************************************
// Assembly         : ArchNet
// Author           : Artur Fazilyanov (a.fazilyanov@stg.ru | a.fazilyanov@gmail.com)
// Created          : 02-25-2016
//
// Last Modified By : a.fazilyanov
// Last Modified On : 03-22-2016
// ***********************************************************************
// <copyright file="faData.cs" company="">
//     Copyright ©  2014
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ArchNet
{
    /// <summary>
    /// Класс описывающий свойства поля, относящиеся к получению данных из БД
    /// </summary>
    public class faData
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public faData()
        {
            FieldName = string.Empty;
            FieldCalc = string.Empty;
            Table = string.Empty;
            RefField = string.Empty;
            RefField2 = string.Empty;
            Where = string.Empty;
            Again = false;
        }

        /// <summary>
        /// Чтобы не дублировать JOINы
        /// </summary>
        public bool Again { get; set; }

        /// <summary>
        /// Описывает вычисляемое поле
        /// </summary>
        /// <example>Вычисляется размер файла в мегабайтах<c>fld.Data.FieldCalc = "CAST(a.file_size/1024.0/1024.0 as numeric(13,2))";</c></example>
        public string FieldCalc { get; set; }

        /// <summary>
        /// Имя поля в таблице
        /// </summary>
        /// <value>The name of the field.</value>
        public string FieldName { get; set; }

        /// <summary>
        /// Указывается поле связываемой таблицы, по которому происходит связь
        /// </summary>
        /// <value>The reference field.</value>
        /// <example>Главная таблица "Архив", побочная "Версии документа", указывается поле "id_archive" таблицы "Версии документа"
        /// <c>select a.*, b.barcode from archive as a left join b on b.id_archive=a.id</c></example>
        public string RefField { get; set; }

        /// <summary>
        /// ХЗ скорее всего для обратной связи.. не использовалось еще
        /// </summary>
        /// <value>The reference field2.</value>
        public string RefField2 { get; set; }

        /// <summary>
        /// Имя таблицы в бд
        /// </summary>
        /// <value>The table.</value>
        public string Table { get; set; }

        /// <summary>
        /// Предустановленное условие для поля
        /// </summary>
        /// <value>The where.</value>
        /// <example><c>fld.Data.Where = "id_doctree in (5022,5023,5029)";</c></example>
        public string Where { get; set; }
    }
}