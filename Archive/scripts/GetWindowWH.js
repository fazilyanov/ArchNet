//window.onresize = function (event) {
//   // SetWidthHeight();
//}
//function SetWidthHeight() {
//    var height = $(window).height();
//    var width = $(window).width();
//    $.ajax({
//        url: "/Util/GetWindowWH.ashx",
//        data: {
//            'Height': height,
//            'Width': width
//        },
//        contentType: "application/json; charset=utf-8",
//        dataType: "json"
//    }).done(function (data) {
//        if (data.isFirst) {
//            window.location.reload();
//        };
//    }).fail(function (xhr) {
//        alert("Эпик фэйл чо..");
//    });

//}
//$(function () {
//    SetWidthHeight();
//});