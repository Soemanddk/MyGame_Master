<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminraceimage.aspx.cs" Inherits="MyGame.ajax.adminraceimage" %>

<form id="formmodal" runat="server">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                <h4 class="modal-title" id="myModalTitle">
                    <asp:Literal ID="Literal_Title" runat="server"></asp:Literal>
                </h4>
            </div>
            <div class="modal-body">
                <img id="RaceImage" runat="server" src="#" alt="#" />
                <div class="form-group">
                    <label class="control-label" for="TextBox_RaceImageName">Image name</label>
                    <asp:TextBox ID="TextBox_RaceImageName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:HiddenField ID="HiddenField_RaceImageId" runat="server" />
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-sm btn-danger pull-left" data-action="delete" data-function="RaceImage" onclick="return confirm('Are you sure?');">
                    <span class="glyphicon glyphicon-trash"></span>
                </button>
                <button type="button" class="btn btn-default" data-action="close" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-primary" data-action="save" data-function="RaceImage">Save</button>
            </div>
        </div>
    </div>
</form>
