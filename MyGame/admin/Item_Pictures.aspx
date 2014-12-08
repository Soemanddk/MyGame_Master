<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.Master" AutoEventWireup="true" CodeBehind="Item_Pictures.aspx.cs" Inherits="MyGame.admin.Item_Pictures" %>
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
    <asp:Panel ID="Panel_Pagination" runat="server" Visible="false" ClientIDMode="Static">
        <asp:Literal ID="Literal_Pagination" runat="server"></asp:Literal>
    </asp:Panel>
    <asp:Panel ID="Panel_ContentTitle" runat="server" Visible="false" ClientIDMode="Static">
        <asp:Literal ID="Literal_ContentTitle" runat="server"></asp:Literal>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Content" runat="server">

    <asp:Panel ID="Panel_Show_All" runat="server" Visible="false">
        <div class="input-group search">
            <span class="input-group-btn">
                <asp:LinkButton ID="LinkButton_Search_Reload" runat="server" CssClass="btn btn-default" OnClick="LinkButton_Search_Reload_Click">
                    <span class="glyphicon glyphicon-remove"></span>
                </asp:LinkButton>
            </span>
            <asp:TextBox ID="TextBox_Search" runat="server" CssClass="form-control" ValidationGroup="search" placeholder="Search"></asp:TextBox>
            <span class="input-group-btn">
                <asp:LinkButton ID="LinkButton_Search" runat="server" CssClass="btn btn-default" OnClick="LinkButton_Search_Click" ValidationGroup="search">
                    <span class="glyphicon glyphicon-search"></span>
                </asp:LinkButton>
            </span>
        </div>
        <asp:Repeater ID="Repeater_Show_All" runat="server" ItemType="MyGame.item_picture">
            <HeaderTemplate>                               
                <table class="table table-responsive">
                    <thead>
                        <tr>                           
                            <th>Image</th>
                            <th>Name</th>
                            <th>Slot type</th>
                            <th>Actions</th>
                        </tr>
                    </thead>              
            </HeaderTemplate>
            <ItemTemplate>
                <tr>                  
                    <td>
                        <img class="img-rounded admin-item-img" src="/img/item/<%# Item.id + Item.img_type %>" alt="<%# Item.name %>" />
                    </td>
                    <td>
                        <%# Item.name %>
                    </td>
                    <td>
                        <%# Item.slot_type.name %>
                    </td>
                    <td>
                        <a href="Item_Pictures.aspx?action=edit&id=<%# Item.id %>" title="Edit" class="btn btn-sm btn-primary">
                            <span class="glyphicon glyphicon-edit"></span>
                        </a>                       
                        <a href="Item_Pictures.aspx?action=delete&id=<%# Item.id %>" title="Delete" class="btn btn-sm btn-danger" onclick="return confirm('Are you sure?');">
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
            <img id="imgItem" class="admin-item-img-edit" src="#" runat="server" alt="#" />       
            <div class="form-group">
                <label class="control-label" for="TextBox_Name">Name</label>
                <asp:TextBox ID="TextBox_Name" runat="server" CssClass="form-control" placeholder="Enter Name" ClientIDMode="Static" data-regex=""></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="control-label" for="DropDownList_SlotType">Slot Type</label>
                <asp:DropDownList ID="DropDownList_SlotType" runat="server" CssClass="form-control" DataValueField="id" DataTextField="name"></asp:DropDownList>
            </div>
            <asp:LinkButton ID="LinkButton_Form" runat="server" OnClick="LinkButton_Form_Click">
                <asp:Literal ID="Literal_LinkButton_Form" runat="server"></asp:Literal>
            </asp:LinkButton>
        </div>       
    </asp:Panel>

    <asp:Panel ID="Panel_Upload" runat="server" Visible="false">
        <div class="form">
            <div class="form-group">
                <label class="control-label" for="DropDownList_ChooseSlotType">Choose Slot Type the pictures should belong to!</label>
                <asp:DropDownList ID="DropDownList_ChooseSlotType" runat="server" CssClass="form-control" DataValueField="id" DataTextField="name" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_ChooseSlotType_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div id="uploader">
            <p>Your browser doesn't have Flash, Silverlight, Gears, BrowserPlus or HTML5 support.</p>
        </div>
        
        <asp:Repeater ID="Repeater_Upload_Images" runat="server" ItemType="MyGame.item_picture">
            <ItemTemplate>
                <img class="img-rounded admin-item-img-upload" src="/img/item/<%# Item.id + Item.img_type %>" alt="<%# Item.name %>" />
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder_ContentFooter" runat="server">
    <a id="btnBack" href="Item_Pictures.aspx" runat="server" title="Back" class="btn btn-sm btn-default" visible="false">
        <span class="glyphicon glyphicon-arrow-left"></span>
    </a>
    <a id="btnImage" runat="server" href="Item_Pictures.aspx?action=upload" title="Upload Pictures" class="btn btn-sm btn-warning" visible="false">
        <span class="glyphicon glyphicon-picture"></span>
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
                url: 'Handler.ashx?id=' + id + "&type=item",
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
