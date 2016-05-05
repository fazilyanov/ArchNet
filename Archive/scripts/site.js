// 2014-05-05

////////////////////////
// datetim e templates //
////////////////////////


function formatDate(date) {
    var dd = date.getDate()
    if (dd < 10) dd = '0' + dd;
    var mm = date.getMonth() + 1
    if (mm < 10) mm = '0' + mm;
    var yy = date.getFullYear();
    if (yy < 10) yy = '0' + yy;
    return dd + '.' + mm + '.' + yy;
}

function formatDateTime(date) {
    var dd = date.getDate()
    if (dd < 10) dd = '0' + dd;
    var mm = date.getMonth() + 1
    if (mm < 10) mm = '0' + mm;
    var yy = date.getFullYear();
    if (yy < 10) yy = '0' + yy;
    var hh = date.getHours()
    if (hh < 10) hh = '0' + hh;
    var mmm = date.getMinutes()
    if (mmm < 10) mmm = '0' + mmm;
    return dd + '.' + mm + '.' + yy + ' ' + hh + ':' + mmm;
}


function GetYesterday() {
    var date = new Date();
    var dd = date.getDate() - 1;
    var mm = date.getMonth();
    var yy = date.getFullYear();
    var ret = new Date(yy, mm, dd);
    return ret;
}

function GetLastWeekBegin() {
    var date = new Date();
    var dow = date.getDay() - 1;
    var dd = date.getDate();
    var mm = date.getMonth();
    var yy = date.getFullYear();
    var ret = new Date(yy, mm, dd - dow - 7);
    return ret;
}

function GetLastWeekEnd() {
    var date = new Date();
    var dow = date.getDay() - 1;
    var dd = date.getDate();
    var mm = date.getMonth();
    var yy = date.getFullYear();
    var ret = new Date(yy, mm, dd + (6 - dow) - 7);
    return ret;
}

function GetLastMonthBegin() {
    var date = new Date();
    var dd = 1;
    var mm = date.getMonth() - 1;
    var yy = date.getFullYear();
    var ret = new Date(yy, mm, dd);
    return ret;
}
function GetLastMonthEnd() {
    var date = new Date();
    var mm = date.getMonth() - 1;
    var yy = date.getFullYear();
    var dd = new Date(yy, mm + 1, 0).getDate();
    var ret = new Date(yy, mm, dd);
    return ret;
}

function GetCurrentWeekBegin() {
    var date = new Date();
    var dow = date.getDay() - 1;
    var dd = date.getDate();
    var mm = date.getMonth();
    var yy = date.getFullYear();
    var ret = new Date(yy, mm, dd - dow);
    return ret;
}

function GetCurrentWeekEnd() {
    var date = new Date();
    var dow = date.getDay() - 1;
    var dd = date.getDate();
    var mm = date.getMonth();
    var yy = date.getFullYear();
    var ret = new Date(yy, mm, dd + (6 - dow));
    return ret;
}

function GetCurrentMonthBegin() {
    var date = new Date();
    var dd = 1;
    var mm = date.getMonth();
    var yy = date.getFullYear();
    var ret = new Date(yy, mm, dd);
    return ret;
}
function GetCurrentMonthEnd() {
    var date = new Date();
    var mm = date.getMonth();
    var yy = date.getFullYear();
    var dd = new Date(yy, mm + 1, 0).getDate();
    var ret = new Date(yy, mm, dd);
    return ret;
}
function GetCurrentYearBegin() {
    var date = new Date();
    var dd = 1;
    var mm = 0;
    var yy = date.getFullYear();
    var ret = new Date(yy, mm, dd);
    return ret;
}
function GetCurrentYearEnd() {
    var date = new Date();
    var mm = 11;
    var yy = date.getFullYear();
    var dd = new Date(yy, mm + 1, 0).getDate();
    var ret = new Date(yy, mm, dd);
    return ret;
}

////////////////////////

$.datepicker.regional['ru'] = {
    closeText: 'Закрыть',
    prevText: '<Пред',
    nextText: 'След>',
    currentText: 'Сегодня',
    monthNames: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь',
	'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
    monthNamesShort: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн',
	'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
    dayNames: ['воскресенье', 'понедельник', 'вторник', 'среда', 'четверг', 'пятница', 'суббота'],
    dayNamesShort: ['вск', 'пнд', 'втр', 'срд', 'чтв', 'птн', 'сбт'],
    dayNamesMin: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
    weekHeader: 'Не',
    dateFormat: 'dd.mm.yy',
    firstDay: 1,
    isRTL: false,
    showMonthAfterYear: false,
    yearSuffix: ''
};
$.datepicker.setDefaults($.datepicker.regional['ru']);


$.timepicker.regional['ru'] = {
    timeOnlyTitle: 'Выберите время',
    timeText: 'Время',
    hourText: 'Часы',
    minuteText: 'Минуты',
    secondText: 'Секунды',
    millisecText: 'Миллисекунды',
    timezoneText: 'Часовой пояс',
    currentText: 'Сейчас',
    closeText: 'Закрыть',
    timeFormat: 'HH:mm',
    amNames: ['AM', 'A'],
    pmNames: ['PM', 'P'],
    isRTL: false
};
$.timepicker.setDefaults($.timepicker.regional['ru']);
