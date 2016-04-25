namespace ArchNet
{
    /// <summary>
    /// Перечисление возможных видов контролов
    /// </summary>
    public enum faControl
    {
        /// <summary>
        /// Строка текста
        /// </summary>
        TextBox,

        /// <summary>
        /// Целочисленные данных
        /// </summary>
        TextBoxInteger,

        /// <summary>
        /// С плавающей точкой (сумма, размер файла)
        /// </summary>
        TextBoxNumber,

        /// <summary>
        /// Полнотекстовый поиск
        /// </summary>
        TextBoxFullSearch,

        /// <summary>
        /// "Галочка" Да/Нет
        /// </summary>
        CheckBox,

        /// <summary>
        /// Выпадающий короткий список
        /// </summary>
        DropDown,

        /// <summary>
        /// Многострочное поле ввода
        /// </summary>
        TextArea,

        /// <summary>
        /// Дата / Календарь
        /// </summary>
        DatePicker,

        /// <summary>
        /// Дата / Календарь + Время
        /// </summary>
        DateTimePicker,

        /// <summary>
        /// Выпадющий список, с автозавершением
        /// </summary>
        AutoComplete,

        /// <summary>
        /// Выбор из дерева элементов
        /// </summary>
        TreeGrid,

        /// <summary>
        /// Загрузка файла
        /// </summary>
        File,

        /// <summary>
        /// Гиперссылка
        /// </summary>
        URL,

#warning 22.03.2016 ниже.. убрать надо эти частности из главного класса

        /// <summary>
        ///
        /// </summary>
        NewWindowArchive,

        NewWindowArchiveID
    }
}