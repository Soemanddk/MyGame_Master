$(document).ready(function () {

    $('a[href="#myModal"]').click(function (e) {       
        AjaxGet($(this).data("url"));
    });
});

function AjaxGet(URL)
{
    $.ajax({
        type: "GET",
        url: URL,
        async: true,
        success: function () {
           
        },
        error: function () {
            console.log("Der er en fejl");
        }
    }).done(function (data) {
        if (data != "" || data != null) {
            $("#myModal").html(data);
            $('#myModal').modal('show');
            Init_Click();
        }
    });
}

function Init_Click()
{
    $('button[data-action="save"]').click(function () {
        
            switch ($(this).data("function"))
            {
                case "RaceImage":
                    RaceImage("save");
                    break;
            }  
    });

    $('button[data-action="delete"]').click(function () {
             
            switch ($(this).data("function"))
            {
                case "RaceImage":
                    RaceImage("delete");
                    break;
            }
    });
}

// Ajax Functions
function RaceImage(Action)
{
    var ImageName  = $("#TextBox_RaceImageName").val();
    var ImageID = $("#HiddenField_RaceImageId").val();

    if (ImageName != "")
    {
        $('button[data-action="save"]').prop("disabled", true);
        $('button[data-action="delete"]').prop("disabled", true);
        $('button[data-action="close"]').html('Loading...  <img src="../img/design/image-loader.gif" />').prop("disabled", true);

        var Function = "";

        if (Action == "save") {
            Function = "SaveFunction";
        }
        else if (Action == "delete") {
            Function = "DeleteFunction";
        }

        $.ajax({
            type: "POST",
            url: "../ajax/adminraceimage.aspx/" + Function,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ id: ImageID, name: ImageName }),
            dataType: 'json',
            async: false,
            error: function (xhr, status, error) {
                console.log(error + " " + status + " " + xhr);
            }
        }).done(function () {
            if (Action == "save") {
                setTimeout(function () {
                    AjaxGet("../ajax/adminraceimage.aspx?id=" + ImageID);
                }, 1200);
            }
            else if (Action == "delete") {
                setTimeout(function () {
                    $('#myModal').modal('hide');
                    $('#myModal').on('hidden.bs.modal', function (e) {
                        location.reload();
                    });
                }, 1200);
            }
        });
    }
}

