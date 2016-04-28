// ***********************************************************************
// Assembly         : ArchNet
// Author           : Artur Fazilyanov (a.fazilyanov@gmail.com)
// Created          : 02-25-2016
//
// Last Modified By : a.fazilyanov
// Last Modified On : 03-22-2016
// ***********************************************************************
// <copyright file="faEdit.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ArchNet
{
    /// <summary>
    /// Типы автозаполняемых полей
    /// </summary>
    public enum faAutoType
    {
        /// <summary>
        /// Нет
        /// </summary>
        None,

        /// <summary>
        /// Дата
        /// </summary>
        NowDate,

        /// <summary>
        /// Дата Время
        /// </summary>
        NowDateTime,

        /// <summary>
        /// Размер загруженного файла
        /// </summary>
        FileSize,

#warning Ниже частности - избавиться

        /// <summary>
        /// Новая версия
        /// </summary>

        Version,

        /// <summary>
        /// Текущий пользователь
        /// </summary>
        UserID,

        /// <summary>
        /// Основная версия
        /// </summary>
        Main
    }

    /// <summary>
    /// Описывает поведение при редактировании
    /// </summary>
    public class faEdit
    {
        #region Шаблоны сообщений при редактировании

        /// <summary>
        /// The MSG must fill
        /// </summary>
        public const string
            MsgMustFill = "Поле <b><q>{0}</q></b> должно быть заполнено.",
            MsgMustFillForStatus = "Для статуса <b><q>Завершен</q></b>, обязательны к заполнению поля: <b> Код проекта, Исполнитель </b>",
            MsgDublicate = "Выбранное значение поля <b><q>{0}</q></b> уже существует (id = <b>{1}</b>)",
            MsgMaxText = "Слишком длинное значение поля <b><q>{0}</q></b>. Максимальное: <b><q>{1}</q></b>",
            MsgMinText = "Слишком короткое значение поля <b><q>{0}</q></b>. Минимальное: <b><q>{1}</q></b>",
            MsgBadFormat = "Значение поля <b><q>{0}</q></b> имеет недопустимый формат.",
            MsgMaxInt = "Значение поля <b><q>{0}</q></b> больше, максимально допустимого. Максимальное: <b><q>{1}</q></b>",
            MsgMinInt = "Значение поля <b><q>{0}</q></b> меньше, минимально допустимого. Минимальное: <b><q>{1}</q></b>",
            MsgCantDel = "Удаление невозможно, запись используется в таблице <b><q>{0}</q></b> (id = <b>{1}</b>)",
            MsgDubArchive = "Для контрагента <b><q>{0}</q></b>, документ <b><q>{1}</q></b> №<b><q>{2}</q></b> уже существует!" +
            " Арх.№ <b><q>{3}</q></b>. {4}";

        #endregion Шаблоны сообщений при редактировании

        /// <summary>
        /// Конструктор для <see cref="faEdit"/>
        /// </summary>
        public faEdit()
        {
            Visible = true;
            Enable = true;
            Required = false;
            Unique = false;
            Copied = true;
            BaseCopied = true;
            Control = faControl.TextBox;
            Value = null;
            ValueText = string.Empty;
            DefaultValue = string.Empty;
            DefaultText = string.Empty;
            Chk1 = false;
            Chk2 = false;
            Auto = faAutoType.None;
            Max = 0;
            Min = 0;
            AddOnly = false;
        }

        /// <summary>
        /// Данные заносятся один раз при добавлении, не редактируется
        /// </summary>
        public bool AddOnly { get; set; }

        /// <summary>
        /// Тип автозаполнения
        /// </summary>
        public faAutoType Auto { get; set; }

        /// <summary>
        /// Копируется ли при копировании из одной базы в другую
        /// </summary>
        public bool BaseCopied { get; set; }

        /// <summary>
        /// Галочка редактора
        /// </summary>
        /// <value><c>true</c> если есть ошибка, иначе <c>false</c>.</value>
        public bool Chk1 { get; set; }

        /// <summary>
        /// Галочка супервайзера
        /// </summary>
        /// <value><c>true</c> если есть неисправленная ошибка редактора, иначе <c>false</c>.</value>
        public bool Chk2 { get; set; }

        /// <summary>
        /// Вид контрола для поля
        /// </summary>
        public faControl Control { get; set; }

        /// <summary>
        /// Копируется ли при копировании
        /// </summary>
        public bool Copied { get; set; }

        /// <summary>
        /// Видимое значение по умолчанию, например, при добавлении новой записи (Наименование контрагента)
        /// </summary>
        public string DefaultText { get; set; }

        /// <summary>
        /// Значение по умолчанию, например, при добавлении новой записи (id контрагента)
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Устанавливает доступность редактирования
        /// </summary>
        /// <value><c>true</c> если доступно; иначе, <c>false</c>.</value>
        public bool Enable { get; set; }

        /// <summary>
        /// Максимальное возможное значение (текст - длинна)
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// Минимальное возможное значение
        /// </summary>
        /// <value>The minimum.</value>
        /// <remarks>Для текстовых полей указывается минимальная длинна, для числовых - минимально возможное значение</remarks>
        public int Min { get; set; }

        /// <summary>
        /// Обязательно для заполнения
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Значение должно быть уникальным в текущем контексте
        /// </summary>
        public bool Unique { get; set; }

        /// <summary>
        /// Хранит текущее значение
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Хранит текущее видимое значение, при использовании справочника
        /// </summary>
        public string ValueText { get; set; }

        /// <summary>
        /// Устанавливает видимость поля для редактирования
        /// </summary>
        public bool Visible { get; set; }
    }
}