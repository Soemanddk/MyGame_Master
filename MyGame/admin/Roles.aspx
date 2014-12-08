<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="MyGame.admin.Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="admin_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_HeaderLinks" runat="server">
    <asp:Literal ID="Literal_BreadCrumbs" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_ContentTitle" runat="server">
    <asp:Literal ID="Literal_ContentTitle" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Content" runat="server">

    <asp:Panel ID="Panel_Show_All" runat="server" Visible="false">
        <asp:Repeater ID="Repeater_Show_All" runat="server" ItemType="MyGame.role">
            <HeaderTemplate>
                <table class="table table-responsive">
                    <thead>
                        <tr>                           
                            <th>#id</th>
                            <th>Name</th>
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
                        <a href="Roles.aspx?action=edit&id=<%# Item.id %>" title="Edit" class="btn btn-sm btn-primary"><span class="glyphicon glyphicon-edit"></span></a>
                        <a href="Roles.aspx?action=delete&id=<%# Item.id %>" title="Delete" class="btn btn-sm btn-danger" onclick="return confirm('Are you sure?');"><span class="glyphicon glyphicon-trash"></span></a>
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
                <label class="control-label" for="TextBox_Name">Rolename</label>
                <asp:TextBox ID="TextBox_Name" runat="server" CssClass="form-control" placeholder="Enter Rolename" ClientIDMode="Static" data-regex="empty"></asp:TextBox>
            </div>
            <asp:LinkButton ID="LinkButton_Form" runat="server" OnClick="LinkButton_Form_Click">
                <asp:Literal ID="Literal_LinkButton_Form" runat="server"></asp:Literal>
            </asp:LinkButton>
        </div>       
    </asp:Panel>

    <asp:Panel ID="Panel_Role_Rights" runat="server"  Visible="false">
        <table class="table table-hover">
            <thead>
                <tr>
                    <th>
                        <span class="glyphicon glyphicon-asterisk"></span> Rights
                    </th>
                    <asp:Repeater ID="Repeater_Role_Rights_Roles" runat="server" ItemType="MyGame.role">
                        <ItemTemplate>
                            <th>
                                <%# Item.name %>
                            </th>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>                
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater_Role_Rights_Rights" runat="server" OnItemDataBound="Repeater_Role_Rights_Rights_ItemDataBound"></asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkButton_Role_Rights" runat="server" CssClass="btn btn-success" OnClick="LinkButton_Role_Rights_Click">Save</asp:LinkButton>
                        <a href="/admin/Roles.aspx?action=role_rights" title="Cancel" class="btn btn-default">Cancel</a>
                    </td>
                </tr>
            </tfoot>
        </table>       
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder_ContentFooter" runat="server">
    <a id="btnBack" href="Roles.aspx" runat="server" title="Back" class="btn btn-sm btn-default" visible="false">
        <span class="glyphicon glyphicon-arrow-left"></span>
    </a>

    <a id="btnAdd" href="Roles.aspx?action=create" runat="server" title="Create New" class="btn btn-sm btn-success" visible="false">
        <span class="glyphicon glyphicon-plus"></span>
    </a>

    <a id="btnRoleRights" href="Roles.aspx?action=role_rights" runat="server" title="Role Rights" class="btn btn-sm btn-warning" visible="false">
        <span class="glyphicon glyphicon-list"></span>
    </a>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="admin_scripts" runat="server">
    <asp:Literal ID="Literal_Form" runat="server" Visible="false">
        <script src="../js/bootstrap_validate.js"></script>
    </asp:Literal>
</asp:Content>
