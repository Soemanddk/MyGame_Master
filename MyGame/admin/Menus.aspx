<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.Master" AutoEventWireup="true" CodeBehind="Menus.aspx.cs" Inherits="MyGame.admin.Menus" %>
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
        <asp:Repeater ID="Repeater_Show_All" runat="server" ItemType="MyGame.menus">
            <HeaderTemplate>             
                <table class="table table-responsive">
                    <thead>
                        <tr>                            
                            <th>#id</th>
                            <th>Name</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>             
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
                        <a href="Menus.aspx?action=edit&id=<%# Item.id %>" title="Edit" class="btn btn-sm btn-primary">
                            <span class="glyphicon glyphicon-edit"></span>
                        </a>
                        <a href="Menus.aspx?action=links&id=<%# Item.id %>" title="Edit Links" class="btn btn-sm btn-warning">
                            <span class="glyphicon glyphicon-align-left"></span>
                        </a>
                        <a href="Menus.aspx?action=delete&id=<%# Item.id %>" title="Delete" class="btn btn-sm btn-danger" onclick="return confirm('Are you sure?');">
                            <span class="glyphicon glyphicon-trash"></span>
                        </a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </tbody> 
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>

    <asp:Panel ID="Panel_Form" runat="server" Visible="false">
        <div class="form">
            <div class="form-group">
                <label class="control-label" for="TextBox_Name">Menu name</label>
                <asp:TextBox ID="TextBox_Name" runat="server" CssClass="form-control" placeholder="Enter Menu Name" ClientIDMode="Static" data-regex=""></asp:TextBox>
            </div>
            <asp:LinkButton ID="LinkButton_Form" runat="server" OnClick="LinkButton_Form_Click">
                <asp:Literal ID="Literal_LinkButton_Form" runat="server"></asp:Literal>
            </asp:LinkButton>
        </div>       
    </asp:Panel>

    <asp:Panel ID="Panel_Menu_Links" runat="server" Visible="false">
        <table class="table table-responsive">            
            <thead>
                <tr>                   
                    <th>
                        Link Name
                    </th>
                    <th>
                        Action
                    </th>
                </tr>               
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater_Menu_Links" runat="server" OnItemCommand="Repeater_Menu_Links_ItemCommand" ItemType="MyGame.menu_link">
                    <ItemTemplate>
                        <tr>                          
                            <td>
                                <%# Item.link.glyphicon.tag %> <%# Item.link.name %>
                            </td>
                             <td>
                                <a href="Menus.aspx?action=delete_link&mid=<%# Item.menu_id %>&lid=<%# Item.link_id %>" title="Delete" class="btn btn-sm btn-danger" onclick="return confirm('Are you sure?');"><span class="glyphicon glyphicon-trash"></span></a>
                                <asp:LinkButton 
                                    ID="LinkButton_Set_Primary" 
                                    runat="server" 
                                    CssClass='<%# Disabled(Item.is_primary) %>'
                                    CommandName='<%# Item.link_id %>'
                                    CommandArgument='<%# Item.menu_id %>'>
                                    <%# PrimaryName(Item.is_primary) %>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tbody>
                <tr>
                    <td colspan="2">              
                        <div class="form-inline">
                            <asp:DropDownList ID="DropDownList_Menu_Links" runat="server" CssClass="form-control" DataValueField="id" DataTextField="name"></asp:DropDownList>
                            <asp:LinkButton ID="LinkButton_Menu_Links" runat="server" CssClass="btn btn-success" OnClick="LinkButton_Menu_Links_Click">Add</asp:LinkButton>            
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>        
    </asp:Panel>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder_ContentFooter" runat="server">
    <a id="btnBack" href="Menus.aspx" runat="server" title="Back" class="btn btn-sm btn-default" visible="false">
        <span class="glyphicon glyphicon-arrow-left"></span>
    </a>

    <a id="btnAdd" href="Menus.aspx?action=create" runat="server" title="Create New" class="btn btn-sm btn-success" visible="false">
        <span class="glyphicon glyphicon-plus"></span>
    </a>

    <a id="btnEdit" runat="server" href="#" title="Edit Menu" class="btn btn-sm btn-primary" visible="false">
        <span class="glyphicon glyphicon-edit"></span>
    </a>

    <a id="btnLinks" runat="server" href="#" title="Edit Links" class="btn btn-sm btn-warning" visible="false">
        <span class="glyphicon glyphicon-align-left"></span>
    </a>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="admin_scripts" runat="server">
    <asp:Literal ID="Literal_Form" runat="server" Visible="false">
        <script src="../js/bootstrap_validate.js"></script>
    </asp:Literal>
</asp:Content>
