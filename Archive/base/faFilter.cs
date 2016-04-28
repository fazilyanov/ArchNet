// ***********************************************************************
// Assembly         : ArchNet
// Author           : Artur Fazilyanov (a.fazilyanov@gmail.com)
// Created          : 02-25-2016
//
// Last Modified By : a.fazilyanov
// Last Modified On : 04-19-2016
// ***********************************************************************
// <copyright file="faField.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Web;

namespace ArchNet
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public class faFilter
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public faFilter()
        {
            Enable = true;
            Value = string.Empty;
            Value2 = string.Empty;
            DefaultValue = string.Empty;
            Text = string.Empty;
            Condition = string.Empty;
            Control = faControl.TextBox;
            Caption = string.Empty;
        }

        /// <summary>
        /// Заголовок для фильтра
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Условия сравнения (числовые и текстовые фильтры)
        /// </summary>
        public string Condition
        {
            get { return (HttpContext.Current.Session[SesValName + "_filter_cond"] ?? "").ToString(); }
            set
            {
                if (value != "")
                    HttpContext.Current.Session[SesValName + "_filter_cond"] = value;
                else
                    HttpContext.Current.Session.Remove(SesValName + "_filter_cond");
            }
        }

        /// <summary>
        /// Вид контрола
        /// </summary>
        public faControl Control { get; set; }

        /// <summary>
        /// Предустановленный фильтр
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Фильтрируется ли поле
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// Ключ сессии где храниться значение фильтра
        /// </summary>
        public string SesValName { get; set; }

        /// <summary>
        /// Текстовое представление выбранного значения, например, наименование фирмы, а не его id
        /// </summary>
        public string Text
        {
            get { return (HttpContext.Current.Session[SesValName + "_filter_text"] ?? "").ToString(); }
            set
            {
                if (value != "")
                    HttpContext.Current.Session[SesValName + "_filter_text"] = value;
                else
                    HttpContext.Current.Session.Remove(SesValName + "_filter_text");
            }
        }

        /// <summary>
        /// Значение фильтра
        /// </summary>
        public string Value
        {
            get { return (HttpContext.Current.Session[SesValName + "_filter"] ?? "").ToString(); }
            set
            {
                if (value != "")
                    HttpContext.Current.Session[SesValName + "_filter"] = value;
                else
                    HttpContext.Current.Session.Remove(SesValName + "_filter");
            }
        }

        /// <summary>
        /// Второе значение фильтра. (Дата или сумма)
        /// </summary>
        public string Value2
        {
            get { return (HttpContext.Current.Session[SesValName + "_filter2"] ?? "").ToString(); }
            set
            {
                if (value != "")
                    HttpContext.Current.Session[SesValName + "_filter2"] = value;
                else
                    HttpContext.Current.Session.Remove(SesValName + "_filter2");
            }
        }
        
    }
}