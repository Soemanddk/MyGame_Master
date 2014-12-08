<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.Master" AutoEventWireup="true" CodeBehind="Races.aspx.cs" Inherits="MyGame.admin.Races" %>
<asp:Content ID="Content1" ContentPlaceHolderID="admin_head" runat="server">
    <asp:Literal ID="Literal_PlupLoad_Css" runat="server" Visible="false">
        <link href="../plugins/js_ui/jquery-ui.min.css" rel="stylesheet" />
        <link href="../plugins/plupload/js/jquery.ui.plupload/css/jquery.ui.plupload.css" rel="stylesheet" />
    </asp:Literal>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_HeaderLinks" runat="server">
    <asp:Literal ID="Literal_BreadCrumbs" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_ContentTitle" runat="server">
    <asp:Literal ID="Literal_ContentTitle" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Content" runat="server">

    <asp:Panel ID="Panel_Show_All" runat="server" Visible="false">
        <asp:Repeater ID="Repeater_Show_All" runat="server" ItemType="MyGame.race">
            <HeaderTemplate>                               
                <table class="table table-responsive">
                    <thead>
                        <tr>                           
                            <th>#id</th>
                            <th>Name</th>
                            <th>Alignment</th>
                            <th>Images</th>
                            <th>Actions</th>
                        </tr>
                    </thead>              
            </HeaderTemplate>
            <ItemTemplate>
                <tr>                  
                    <td>
                        #<%# Item.id %>
                    </td>
                    <td>
                        <%# Item.name %>
                    </td>
                    <td>
                        <%# Item.alignment.name %>
                    </td>
                    <td>
                        <%# CountImages(Item.id) %>
                    </td>
                    <td>
                        <a href="Races.aspx?action=edit&id=<%# Item.id %>" title="Edit" class="btn btn-sm btn-primary">
                            <span class="glyphicon glyphicon-edit"></span>
                        </a>
                        <a href="Races.aspx?action=images&id=<%# Item.id %>" title="Handling Image" class="btn btn-sm btn-info">
                            <span class="glyphicon glyphicon-picture"></span>
                        </a>
                        <a href="Races.aspx?action=delete&id=<%# Item.id %>" title="Delete" class="btn btn-sm btn-danger" onclick="return confirm('Are you sure?');">
                            <span class="glyphicon glyphicon-trash"></span>
                        </a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>

    <asp:Panel ID="Panel_Form" runat="server" Visible="false">
        <div class="form">
            <div class="form-group">
                <label class="control-label" for="TextBox_Name">Name</label>
                <asp:TextBox ID="TextBox_Name" runat="server" CssClass="form-control" placeholder="Enter Name" ClientIDMode="Static" data-regex=""></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="control-label" for="DropDownList_Alignment">Alignment</label>
                <asp:DropDownList ID="DropDownList_Alignment" runat="server" DataValueField="id" DataTextField="name"></asp:DropDownList>
            </div>
            <asp:LinkButton ID="LinkButton_Form" runat="server" OnClick="LinkButton_Form_Click">
                <asp:Literal ID="Literal_LinkButton_Form" runat="server"></asp:Literal>
            </asp:LinkButton>
        </div>       
    </asp:Panel>

    <asp:Panel ID="Panel_Images" runat="server" Visible="false">
        <div id="uploader">
            <p>Your browser doesn't have Flash, Silverlight, Gears, BrowserPlus or HTML5 support.</p>
        </div>
                  
        <asp:Repeater ID="Repeater_Images" runat="server" ItemType="MyGame.race_picture">
            <HeaderTemplate>
                <div class="panel panel-default admin-race-img">
                  <div class="panel-heading">Images</div>
                  <div class="panel-body">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="admin-race-img-holder">
                    <div class="thumbnail">
                        <a href="#myModal" data-url="../ajax/adminraceimage.aspx?id=<%# Item.id %>">
                            <img src="../img/race/<%# Item.id + Item.img_type %>" alt="<%# Item.name %>" />
                        </a>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
</div>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder_ContentFooter" runat="server">
    <a id="btnBack" href="Races.aspx" runat="server" title="Back" class="btn btn-sm btn-default" visible="false">
        <span class="glyphicon glyphicon-arrow-left"></span>
    </a>

    <a id="btnAdd" href="Races.aspx?action=create" runat="server" title="Create New" class="btn btn-sm btn-success" visible="false">
        <span class="glyphicon glyphicon-plus"></span>
    </a>
    <a id="btnImage" runat="server" href="#" title="Handling Image" class="btn btn-sm btn-info" visible="false">
        <span class="glyphicon glyphicon-picture"></span>
    </a>
    <a id="btnEdit" runat="server" href="#" title="Edit" class="btn btn-sm btn-primary" visible="false">
        <span class="glyphicon glyphicon-edit"></span>
    </a>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="admin_scripts" runat="server">
    <asp:Literal ID="Literal_Form" runat="server" Visible="false">
        <script src="../js/bootstrap_validate.js"></script>
    </asp:Literal>
    <asp:Literal ID="Literal_PlupLoad_Js" runat="server" Visible="false">
    <script src="../plugins/js_ui/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../plugins/plupload/js/plupload.full.min.js"></script>
    <script type="text/javascript" src="../plugins/plupload/js/jquery.ui.plupload/jquery.ui.plupload.min.js"></script>
    <script type="text/javascript">
        // Håndtere Querystringen til plupload
        var id = GetParameterValues('id');

        function GetParameterValues(param) {
            var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < url.length; i++) {
                var urlparam = url[i].split('=');
                if (urlparam[0] == param) {
                    return urlparam[1];
                }
            }
        }

        $(function () {
            $("#uploader").plupload({
                // General settings
                runtimes: 'html5,flash,silverlight,html4',
                url: 'Handler.ashx?id=' + id + "&type=race",
                max_file_size: '10mb',
                chunk_size: '500kb',
                unique_names: true,

                // Specify what files to browse for
                filters: [
                    { title: "Image files", extensions: "jpg,gif,png" }
                ],
                // Rename files by clicking on their titles
                rename: true,

                // Sort files
                sortable: true,

                // Enable ability to drag'n'drop files onto the widget (currently only HTML5 supports that)
                dragdrop: true,

                // Views to activate
                views: {
                    list: true,
                    thumbs: true, // Show thumbs
                    active: 'thumbs'
                },
                // Flash settings
                flash_swf_url: '../plugins/plupload/js/Moxie.swf',
                // Silverlight settings
                silverlight_xap_url: '../plugins/plupload/js/Moxie.xap',
                init: {
                    UploadComplete: function (up, files) {
                        location.reload();
                    }
                }
            });


            // Client side form validation
            $('form').submit(function (e) {
                var uploader = $('#uploader').plupload();

                // Validate number of uploaded files
                if (uploader.total.uploaded == 0) {
                    // Files in queue upload them first
                    if (uploader.files.length > 0) {
                        // When all files are uploaded submit form
                        uploader.bind('UploadProgress', function () {

                            if (uploader.total.uploaded == uploader.files.length) {
                                $('form').submit();
                            }
                        });

                        uploader.start();
                    }
                    else {
                        alert('You must at least upload one file.');
                    }

                    e.preventDefault();
                }
            });
        });
    </script>
    </asp:Literal>
</asp:Content>
