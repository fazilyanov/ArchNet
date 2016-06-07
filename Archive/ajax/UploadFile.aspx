<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="ArchNet.UploadFile"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>File Upload Control</title>
     <script src="/scripts/jquery-1.11.0.min.js" type="text/javascript"></script>
    <link href="/styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        input[type="file"]
        {
            position: fixed;
            top: -1000px;
        }
        body {overflow:hidden}
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <label id="label_file" class="label label-<%=state%>" style="padding: 0.6em 1.3em; width: 190px; display: inline-block;">
        <input id="input_file" name="input_file" type="file" onchange="upload()"/><span><%=(state=="success"?"Файл загружен":"Выбрать  файл")%></span>
        <input id="temp_file" name="temp_file" type="hidden" value="<%=_path%>"/>
    </label>
    <progress style="display:none;width: 216px;height: 22px;-webkit-appearance: none;-moz-appearance: none;appearance: none;"></progress>
    </form>
    <script type="text/javascript">
    function progressHandlingFunction(e) {
        if (e.lengthComputable) {
            $('progress').attr({ value: e.loaded, max: e.total });
        }
    }
    function upload()
    {
        var file = $('#input_file')[0].files[0];
        if (file) {          
            if (file.size > 314572800) {
                alert('Файл слишком большой. Максимум 300МБ.');
                this.value = '';
                return false;
            }
//            var type = file.type;
//            alert(type);
//            if ((type != 'image/jpeg' && type != 'application/pdf' && type != 'image/png') || type == '') {
//                alert('Неподходящий формат файла ' + type);
//                this.value = '';
//                return false;
//            }
            $('#label_file').removeClass('label-success').removeClass('label-danger').addClass('label-default').find('span').html('Выбрать файл');
            $('#label_file').hide(); $('progress').show();
            var formData = new FormData($('form')[0]);
            $.ajax({
                url: '/ajax/UploadFile.aspx',
                type: 'POST',
                xhr: function () {
                    var myXhr = $.ajaxSettings.xhr();
                    if (myXhr.upload) {
                        myXhr.upload.addEventListener('progress', progressHandlingFunction, false);
                    }
                    return myXhr;
                },
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                success: function (html) { 

                    $('#label_file').removeClass('label-default').removeClass('label-danger').addClass('label-success'); $('#label_file').find('span').html('Файл загружен'); $('progress').hide(); $('#label_file').show();$('#temp_file').val(html); },
                error: function (request, status, error) { $('#label_file').removeClass('label-default').removeClass('label-success').addClass('label-danger'); $('#label_file').find('span').html('Файл не загружен! '); $('#label_file').show(); $('progress').hide(); alert(request.responseText); }
            });
        }
    }
    
    </script>
</body>
</html>
