<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MyGame.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main_content" runat="server">
    <div class="form col-lg-6 col-md-6 col-sm-6 col-xs-6">
        <div class="input-group">
            <span class="input-group-addon form-dashed-color-style"><span class="glyphicon glyphicon-envelope"></span></span>
            <asp:TextBox ID="TextBox_Email" runat="server" CssClass="form-control form-dashed-color-style" TextMode="Email" placeholder="Enter Email" data-regex=""></asp:TextBox>
        </div>
        <div class="input-group">
            <span class="input-group-addon form-dashed-color-style"><span class="glyphicon glyphicon-eye-close"></span></span>
            <asp:TextBox ID="TextBox_Password" runat="server" CssClass="form-control form-dashed-color-style" TextMode="Password" placeholder="Enter Password" data-regex="empty"></asp:TextBox>
        </div>
        <asp:LinkButton ID="LinkButton_Login" runat="server" CssClass="validate btn btn-default form-dashed-color-style form-dashed-color-style-btn" OnClick="LinkButton_Login_Click"><span class="glyphicon glyphicon-log-in"></span> Login</asp:LinkButton>
        <asp:LinkButton ID="LinkButton_Reset" runat="server" CssClass="btn btn-default form-dashed-color-style form-dashed-color-style-btn" OnClick="LinkButton_Reset_Click"><span class="glyphicon glyphicon-retweet"></span> Reset</asp:LinkButton>
        <asp:Literal ID="Literal_Error_Message" runat="server" Visible="false"></asp:Literal>
    </div>
    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
        <p class="lead">Register now for <span class="text-success">FREE</span></p>
        <ul class="list-unstyled" style="line-height: 2">
            <li><span class="glyphicon glyphicon-ok text-success"></span> Indsæt text her</li>
            <li><span class="glyphicon glyphicon-ok text-success"></span> Indsæt text her</li>
            <li><span class="glyphicon glyphicon-ok text-success"></span> Indsæt text her</li>
            <li><span class="glyphicon glyphicon-ok text-success"></span> Indsæt text her</li>
            <li><span class="glyphicon glyphicon-ok text-success"></span> Indsæt text her</li>
        </ul>
        <p><a href="Create_Account.aspx" class="btn btn-default form-dashed-color-style form-dashed-color-style-btn btn-block">Yes please, register now!</a></p>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot_js" runat="server">
    <script src="js/bootstrap_validate.js"></script>
</asp:Content>
