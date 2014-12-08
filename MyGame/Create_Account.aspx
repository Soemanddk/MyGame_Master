<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Create_Account.aspx.cs" Inherits="MyGame.Create_Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main_content" runat="server">
    <article class="create_account">
        <h1>Create an account for free</h1>
        <h3>Playing the game requires an active subscription.</h3>
    </article>

    <div class="form col-lg-6 col-md-6 col-sm-6 col-xs-6">
        <div class="form-group">
            <label for="TextBox_Email" class="control-label">
                Email
                <asp:Literal ID="Literal_duplicateUser" runat="server" Visible="false"><span class="text-danger glyphicon glyphicon-exclamation-sign"></span><span class="text-danger"> email already exists</span></asp:Literal></label>
            <asp:TextBox ID="TextBox_Email" runat="server" TextMode="Email" ClientIDMode="Static" ValidationGroup="create" data-regex="" CssClass="form-control form-dashed-color-style" placeholder="Enter your email"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Email"
                ControlToValidate="TextBox_Email"
                runat="server"
                Display="Dynamic"
                ErrorMessage=""
                ValidationGroup="create">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator_Email"
                SetFocusOnError="true" Text="" ControlToValidate="TextBox_Email"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"
                ValidationGroup="create" />
        </div>
        <div class="form-group">
            <label for="TextBox_Name" class="control-label">Real Name</label>
            <asp:TextBox ID="TextBox_Name" runat="server" ClientIDMode="Static" ValidationGroup="create" data-regex="" CssClass="form-control form-dashed-color-style" placeholder="Enter your real name"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Name"
                ControlToValidate="TextBox_Name"
                runat="server"
                Display="Dynamic"
                ErrorMessage=""
                ValidationGroup="create">
            </asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <label for="TextBox_Password" class="control-label">Desired Password (6 Charecters. 1 UpperCase & 1 Number required)</label>
            <asp:TextBox ID="TextBox_Password" runat="server" TextMode="password" ClientIDMode="Static" ValidationGroup="create" data-regex="" CssClass="form-control form-dashed-color-style" placeholder="Enter your desired passwrod"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Password"
                ControlToValidate="TextBox_Password"
                runat="server"
                Display="Dynamic"
                ErrorMessage=""
                ValidationGroup="create">
            </asp:RequiredFieldValidator>
        </div>
        <asp:LinkButton ID="LinkButton_Create_Account" runat="server" ClientIDMode="Static" CssClass="validate btn btn-default form-dashed-color-style form-dashed-color-style-btn" OnClick="LinkButton_Create_Account_Click" ValidationGroup="create"><span class="glyphicon glyphicon-user"></span> Create Account</asp:LinkButton>
        <asp:LinkButton ID="LinkButton_Reset" runat="server" CssClass="btn btn-default form-dashed-color-style form-dashed-color-style-btn" OnClick="LinkButton_Reset_Click"><span class="glyphicon glyphicon-retweet"></span> Reset</asp:LinkButton>
    </div>
    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 push-top-20">
        <p class="lead">Already Have an <span class="text-success">Account?</span></p>
        <p><a href="Login.aspx" class="btn btn-default form-dashed-color-style form-dashed-color-style-btn btn-block">Yes, let me log in!</a></p>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot_js" runat="server">
    <script src="js/bootstrap_validate.js"></script>
</asp:Content>
