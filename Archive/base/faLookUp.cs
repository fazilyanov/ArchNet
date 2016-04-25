// ***********************************************************************
// Assembly         : ArchNet
// Author           : Artur Fazilyanov (a.fazilyanov@stg.ru | a.fazilyanov@gmail.com)
// Created          : 02-25-2016
//
// Last Modified By : a.fazilyanov
// Last Modified On : 02-25-2016
// ***********************************************************************
// <copyright file="faLookUp.cs" company="CJSC Stroytransgaz">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ArchNet
{
    /// <summary>
    /// Описывает связь для таблиц
    /// </summary>
    public class faLookUp
    {
        /// <summary>
        /// Конструктор для <see cref="faLookUp"/>
        /// </summary>
        public faLookUp()
        {
            Key = string.Empty;
            Field = string.Empty;
            Table = string.Empty;
            TableAlias = string.Empty;
            Where = string.Empty;
            Again = false;
        }

        /// <summary>
        /// Если уже есть соединение с таблицей, и нужно использовать его еще раз
        /// </summary>
        public bool Again { get; set; }

        /// <summary>
        /// Поле таблицы для замены, при соединении
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Поле для соединения обычно "id"
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Таблица для соединения
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// Алиас для таблицы соединения.. если соединение происходит несколько раз для разных полей
        /// </summary>
        public string TableAlias { get; set; }

        /// <summary>
        /// Условие для соединяемой таблицы
        /// </summary>
        public string Where { get; set; }
    }
}