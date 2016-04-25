// ***********************************************************************
// Assembly         : ArchNet
// Author           : Artur Fazilyanov (a.fazilyanov@stg.ru | a.fazilyanov@gmail.com)
// Created          : 02-25-2016
//
// Last Modified By : a.fazilyanov
// Last Modified On : 03-23-2016
// ***********************************************************************
// <copyright file="faView.cs" company="CJSC Stroytransgaz">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ArchNet
{
    /// <summary>
    /// Описывает внешний вид поля
    /// </summary>
    public class faView
    {
        /// <summary>
        /// Конструктор для <see cref="faView"/>
        /// </summary>
        public faView()
        {
            Visible = true;
            Hint = string.Empty;
            CaptionShort = string.Empty;
            Caption = string.Empty;
            Width = 50;
            FormatString = string.Empty;
            TextAlign = "left";
        }

        /// <summary>
        /// Заголовок короткий.. для заголовков таблицы или фильтров
        /// </summary>
        public string CaptionShort { get; set; }

        /// <summary>
        /// Обычный заголовок, для карточки
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Формат вывода данных
        /// </summary>
        public string FormatString { get; set; }

        /// <summary>
        /// Подсказка / описание для поля
        /// </summary>
        public string Hint { get; set; }

        /// <summary>
        /// Выравнивание текста
        /// </summary>
        public string TextAlign { get; set; }

        /// <summary>
        /// Видимость поля
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Ширина столбца для отображения данных
        /// </summary>
        public int Width { get; set; }
    }
}