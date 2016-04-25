// ***********************************************************************
// Assembly         : ArchNet
// Author           : Artur Fazilyanov (a.fazilyanov@stg.ru | a.fazilyanov@gmail.com)
// Created          : 02-25-2016
//
// Last Modified By : a.fazilyanov
// Last Modified On : 03-22-2016
// ***********************************************************************
// <copyright file="faField.cs" company="CJSC Stroytransgaz">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ArchNet
{
    /// <summary>
    /// Описывает поле
    /// </summary>
    public class faField
    {
        /// <summary>
        /// Описывает свойства данных
        /// </summary>
        /// <value>The data.</value>
        public faData Data { get; set; }

        /// <summary>
        /// Описывает фильтр
        /// </summary>
        /// <value>The filter.</value>
        public faFilter Filter { get; set; }

        /// <summary>
        /// Связь для поля
        /// </summary>
        public faLookUp LookUp { get; set; }

        /// <summary>
        /// Внешний вид поля
        /// </summary>
        public faView View { get; set; }

        /// <summary>
        /// Описывает поведение поля при редактировании
        /// </summary>
        public faEdit Edit { get; set; }

        /// <summary>
        /// Конструктор для <see cref="faField"/>
        /// </summary>
        public faField()
        {
            Data = new faData();
            Filter = new faFilter();
            LookUp = new faLookUp();
            View = new faView();
            Edit = new faEdit();
        }
    }
}