// ***********************************************************************
// Assembly         : ArchNet
// Author           : Artur Fazilyanov (a.fazilyanov@gmail.com)
// Created          : 02-25-2016
//
// Last Modified By : a.fazilyanov
// Last Modified On : 02-25-2016
// ***********************************************************************
// <copyright file="faPage.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ArchNet
{
    /// <summary>
    /// Перечисление типов документов / типа станицы 
    /// </summary>
    public enum faPage
    {
        /// <summary>
        /// Нет
        /// </summary>
        none = 0,
        /// <summary>
        /// Бухгалтерские
        /// </summary>
        acc = 7,
        /// <summary>
        /// Договоры
        /// </summary>
        dog = 15,
        /// <summary>
        /// ОРД
        /// </summary>
        ord = 2,
        /// <summary>
        /// Прочие
        /// </summary>
        oth = 56,
        /// <summary>
        /// По личному составу
        /// </summary>
        empl = 50,
        /// <summary>
        /// Охрана труда
        /// </summary>
        ohs = 60,
        /// <summary>
        /// Технические 
        /// </summary>
        tech = 11,
        /// <summary>
        /// Страница поиска, включает все доступные типы документов
        /// </summary>
        srch = 1000,
        /// <summary>
        /// Страница выбора
        /// </summary>
        select = 1001,
        /// <summary>
        /// Банковские документы
        /// </summary>
        bank = 5574,
        /// <summary>
        /// Нормативные документы
        /// </summary>
        norm = 5596
    }
}