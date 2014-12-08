<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.Master" AutoEventWireup="true" CodeBehind="Item_Categories.aspx.cs" Inherits="MyGame.admin.Item_Categories" %>
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
        <asp:Repeater ID="Repeater_Show_All" runat="server" ItemType="MyGame.item_category">
            <HeaderTemplate>                               
                <table class="table table-responsive">
                    <thead>
                        <tr>                           
                            <th>#id</th>
                            <th>Name</th>
                            <th>Equipable?</th>
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
                        <%# (Item.equipable ? "Yes" : "No") %>
                    <td>
                        <a href="Item_Categories.aspx?action=edit&id=<%# Item.id %>" title="Edit" class="btn btn-sm btn-primary">
                            <span class="glyphicon glyphicon-edit"></span>
                        </a>
                        <a href="Item_Categories.aspx?action=delete&id=<%# Item.id %>" title="Delete" class="btn btn-sm btn-danger" onclick="return confirm('Are you sure?');">
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
            <div class="checkboxp">
                <label>
                    <asp:CheckBox ID="CheckBox_EquipAble" runat="server" /> Equipable? (check for yes)
                </label>
            </div>
            <asp:LinkButton ID="LinkButton_Form" runat="server" OnClick="LinkButton_Form_Click">
                <asp:Literal ID="Literal_LinkButton_Form" runat="server"></asp:Literal>
            </asp:LinkButton>
        </div>       
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder_ContentFooter" runat="server">
    <a id="btnBack" href="Item_Categories.aspx" runat="server" title="Back" class="btn btn-sm btn-default" visible="false">
        <span class="glyphicon glyphicon-arrow-left"></span>
    </a>

    <a id="btnAdd" href="Item_Categories.aspx?action=create" runat="server" title="Create New" class="btn btn-sm btn-success" visible="false">
        <span class="glyphicon glyphicon-plus"></span>
    </a>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="admin_scripts" runat="server">
    <asp:Literal ID="Literal_Form" runat="server" Visible="false">
        <script src="../js/bootstrap_validate.js"></script>
    </asp:Literal>
</asp:Content>