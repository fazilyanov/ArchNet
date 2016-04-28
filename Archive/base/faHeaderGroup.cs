// ***********************************************************************
// Assembly         : ArchNet
// Author           : Artur Fazilyanov (a.fazilyanov@gmail.com)
// Created          : 03-24-2016
//
// Last Modified By : a.fazilyanov
// Last Modified On : 03-24-2016
// ***********************************************************************
// <copyright file="faHeaderGroup.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ArchNet
{
    /// <summary>
    /// Группировка заголовков таблицы
    /// </summary>
    public class faHeaderGroup
    {
        /// <summary>
        /// Имя столбца с которого нужно начинать группировку
        /// </summary>
        public string StartColumnName { get; set; }

        /// <summary>
        /// Сколько столбцов нужно сгруппировать
        /// </summary>
        public int NumberOfColumns { get; set; }

        /// <summary>
        /// Заголовок группировки
        /// </summary>
        public string TitleText { get; set; }

        /// <summary>
        /// Конструктор для <see cref="faHeaderGroup"/>
        /// </summary>
        public faHeaderGroup()
        {
        }
    }
}