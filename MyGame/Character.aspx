<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Character.aspx.cs" Inherits="MyGame.Character" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main_content" runat="server">
    <asp:Panel ID="Panel_Characters" runat="server">
       <%-- Generic HTML fra Codebehind --%>
    </asp:Panel>
     <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 col-lg-offset-1">
        <h3 class="text-success bold h3">Character Information</h3>
        <ul class="list-unstyled" style="line-height: 2">
            <li class="bold"><span class="glyphicon glyphicon-info-sign text-danger"></span> Only 2 Characters</li>
            <li class="bold"><span class="glyphicon glyphicon-info-sign text-danger"></span> You can only have 1 Good and 1 Evil Character</li>
            <li class="bold"><span class="glyphicon glyphicon-info-sign text-danger"></span> Deleting your Character can't be restored</li>
            <li class="bold"><span class="glyphicon glyphicon-info-sign text-danger"></span> Join a Fellowship to fight with</li>
            <li class="bold"><span class="glyphicon glyphicon-info-sign text-danger"></span> Free to play</li>
        </ul>
    </div>
    <asp:Panel ID="Panel_Show_Character" runat="server" Visible="false">
        Vis Character
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot_js" runat="server">
</asp:Content>
