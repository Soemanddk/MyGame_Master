<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.Master" AutoEventWireup="true" CodeBehind="Charecters.aspx.cs" Inherits="MyGame.admin.Charecters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="admin_head" runat="server">
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
        <asp:Repeater ID="Repeater_Show_All" runat="server" ItemType="MyGame.charecter">
            <HeaderTemplate>
                <table class="table table-responsive">
                    <thead>
                        <tr>
                            <th>#id</th>
                            <th>Name</th>
                            <th>Race</th>
                            <th>Alignment</th>
                            <th>User</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>#<%# Item.id %>
                    </td>
                    <td>
                        <%# Item.name %>
                    </td>
                    <td>
                        <%# Item.race.name %>
                    </td>
                    <td>
                        <%# Item.race.alignment.name %>
                    </td>
                    <td>
                        <%# Item.user.email %>
                    </td>
                    <td>
                        <a href="Charecters.aspx?action=edit&id=<%# Item.id %>" title="Edit" class="btn btn-sm btn-primary">
                            <span class="glyphicon glyphicon-edit"></span>
                        </a>
                        <a href="Charecters.aspx?action=stats&id=<%# Item.id %>" title="Edit Stats" class="btn btn-sm btn-info">
                            <span class="glyphicon glyphicon-stats"></span>
                        </a>
                        <a href="Charecters.aspx?action=delete&id=<%# Item.id %>" title="Delete" class="btn btn-sm btn-danger" onclick="return confirm('Are you sure?');">
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
                <label class="control-label" for="TextBox_UserAccountEmail">User Account</label>
                <asp:TextBox ID="TextBox_UserAccountEmail" runat="server" CssClass="form-control" ClientIDMode="Static" ReadOnly></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="control-label" for="TextBox_Name">Name</label>
                <asp:TextBox ID="TextBox_Name" runat="server" CssClass="form-control" placeholder="Enter Name" ClientIDMode="Static" data-regex=""></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="control-label" for="DropDownList_Race">Race</label>
                <asp:DropDownList ID="DropDownList_Race" runat="server" CssClass="form-control" DataValueField="id" DataTextField="name" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Race_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="radio admin-radio-list">

                <asp:RadioButtonList ID="RadioButtonList_RaceImage" runat="server" RepeatLayout="UnorderedList"></asp:RadioButtonList>

            </div>
            <asp:LinkButton ID="LinkButton_Form" runat="server" OnClick="LinkButton_Form_Click">
                <asp:Literal ID="Literal_LinkButton_Form" runat="server"></asp:Literal>
            </asp:LinkButton>
            <asp:LinkButton ID="LinkButton_Form_Cancel" runat="server" OnClick="LinkButton_Search_Reload_Click" CssClass="btn btn-default">
                Cancel
            </asp:LinkButton>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_Stats" runat="server" Visible="false">
        <table class="table table-responsive">
            <thead>
                <tr>
                    <th>
                        Name
                    </th>
                    <th>
                        Amount
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater_Stats" runat="server" ItemType="MyGame.charecter_stat">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Item.stat.name %>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox_Stat" TextMode="Number" runat="server" value="<%# Item.amount %>"></asp:TextBox>
                                <asp:HiddenField ID="TextBox_StatId" runat="server" value="<%# Item.id %>"></asp:HiddenField>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="2">
                        <asp:LinkButton ID="LinkButton_Save_Stats" runat="server" CssClass="btn btn-success" OnClick="LinkButton_Save_Stats_Click">
                            Save
                        </asp:LinkButton>
                        <asp:LinkButton ID="LinkButton_Reset" runat="server" CssClass="btn btn-default" OnClick="LinkButton_Search_Reload_Click">
                            Cancel
                        </asp:LinkButton>
                    </td>
                </tr>
            </tfoot>
        </table>

        <asp:RequiredFieldValidator 
            ID="RequiredFieldValidator1" 
            runat="server" 
            ErrorMessage=""
            ControlToValidate="DropDownList_AddaStat"
            ValidationGroup="addastat">
        </asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator 
            ID="RequiredFieldValidator2" 
            runat="server" 
            ErrorMessage=""
            ControlToValidate="TextBox_AddaStat_Amount"
            ValidationGroup="addastat">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator 
            ID="RegularExpressionValidator1" 
            runat="server" 
            ErrorMessage=""
            ControlToValidate="TextBox_AddaStat_Amount"
            ValidationExpression="^\d*(?:\.\d{1,2})?$"
            ValidationGroup="addastat">
        </asp:RegularExpressionValidator>
        <div class="form form-inline">
            <div class="form-group">
                <label class="control-label" for="DropDownList_AddaStat">Add a stat</label>
                <asp:DropDownList ID="DropDownList_AddaStat" runat="server" CssClass="form-control" DataValueField="id" DataTextField="name" ValidationGroup="addastat"></asp:DropDownList>                
            </div>
            <div class="form-group">
                <label class="control-label" for="TextBox_AddaStat_Amount">Amout</label>
                <asp:TextBox ID="TextBox_AddaStat_Amount" runat="server" CssClass="form-control" TextMode="Number" data-regex="" ClientIDMode="Static" ValidationGroup="addastat"></asp:TextBox>              
            </div>
            <asp:LinkButton ID="LinkButton_Add_A_Stat" runat="server" CssClass="validate btn btn-info" OnClick="LinkButton_Add_A_Stat_Click" ClientIDMode="Static" ValidationGroup="addastat">Add</asp:LinkButton>
        </div>
    </asp:Panel>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder_ContentFooter" runat="server">
    <a id="btnBack" href="Charecters.aspx" runat="server" title="Back" class="btn btn-sm btn-default" visible="false">
        <span class="glyphicon glyphicon-arrow-left"></span>
    </a>

    <a id="btnAdd" href="Charecters.aspx?action=create" runat="server" title="Create New" class="btn btn-sm btn-success" visible="false">
        <span class="glyphicon glyphicon-plus"></span>
    </a>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="admin_scripts" runat="server">
    <asp:Literal ID="Literal_Form" runat="server" Visible="false">
        <script src="../js/bootstrap_validate.js"></script>
    </asp:Literal>
</asp:Content>
