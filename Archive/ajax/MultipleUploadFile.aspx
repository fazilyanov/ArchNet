<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="MultipleUploadFile.aspx.cs" Inherits="WebArchiveR6.MultipleUploadFile"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>File Upload Control</title>
     <script src="/scripts/jquery-1.11.0.min.js" type="text/javascript"></script>
    <link href="/styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
       
        body {overflow:hidden}
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <label id="label_file" class="label" style="padding: 0.6em 1.3em; width: 190px; display: inline-block;">
        <input id="input_file" name="input_file" type="file" multiple onchange="upload()"/>
        <input id="temp_file" name="temp_file" type="hidden" value="<%=_path%>"/>
    </label>
    <progress style="display:none;width: 216px;height: 22px;-webkit-appearance: none;-moz-appearance: none;appearance: none;"></progress>
    </form>
    <script type="text/javascript">
    function progressHandlingFunction(e) {
        if (e.lengthComputable) {
            // $('progress').attr({ value: e.loaded, max: e.total });
            //alert(e.loaded+' / '+ e.total);
            //window.parent.RefreshAfterMOF();
        }
    }
    function upload()
    {

            var formData = new FormData($('form')[0]);
            $.ajax({
                url: '/ajax/MultipleUploadFile.aspx?cur=<%=cur%>',
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
                success: function (html) { window.parent.RefreshAfterMOF();alert(html)},
                error: function (request, status, error) { $('#label_file').removeClass('label-default').removeClass('label-success').addClass('label-danger'); $('#label_file').find('span').html('Файл не загружен! '); $('#label_file').show(); $('progress').hide(); alert(request.responseText); }
            });
    }
    
    </script>
</body>
</html>
