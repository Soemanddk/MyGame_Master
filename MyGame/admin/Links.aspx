<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.Master" AutoEventWireup="true" CodeBehind="Links.aspx.cs" Inherits="MyGame.admin.Links" %>
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
            <asp:TextBox ID="TextBox_Search" runat="server" CssClass="form-control" ValidationGroup="search"></asp:TextBox>
            <span class="input-group-btn">
                <asp:LinkButton ID="LinkButton_Search" runat="server" CssClass="btn btn-default" OnClick="LinkButton_Search_Click" ValidationGroup="search">
                    <span class="glyphicon glyphicon-search"></span>
                </asp:LinkButton>
            </span>
        </div>
        <asp:Repeater ID="Repeater_Show_All" runat="server" ItemType="MyGame.link">
            <HeaderTemplate>              
                <table class="table table-responsive">
                    <thead>
                        <tr>                         
                            <th>Name</th>
                            <th>Rights Codename</th>
                            <th>Table</th>
                            <th>Actions</th>
                        </tr>
                    </thead>              
            </HeaderTemplate>
            <ItemTemplate>
                <tr>                  
                    <td>
                        <%# Item.glyphicon.tag %> <%# Item.name %>
                    </td>
                    <td>
                        <%# Item.right.codename %>
                    </td>
                    <td>
                        <%# Item.urlpage %>
                    </td>
                    <td>
                        <a href="Links.aspx?action=edit&id=<%# Item.id %>" title="Edit" class="btn btn-sm btn-primary">
                            <span class="glyphicon glyphicon-edit"></span>
                        </a>
                        <a href="Links.aspx?action=delete&id=<%# Item.id %>" title="Delete" class="btn btn-sm btn-danger" onclick="return confirm('Are you sure?');">
                            <span class="glyphicon glyphicon-trash"></span>
                        </a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <%# Eval("TABLE_NAME") %>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>

    <asp:Panel ID="Panel_Form" runat="server" Visible="false">
        <div class="form">
            <div class="form-group">
                <label class="control-label" for="TextBox_Name">Name</label>
                <asp:TextBox ID="TextBox_Name" runat="server" CssClass="form-control" placeholder="Enter Name" ClientIDMode="Static" data-regex="empty"></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="control-label" for="TextBox_Codename">Page Url</label>
                <asp:TextBox ID="TextBox_Pageurl" runat="server" CssClass="form-control" placeholder="Enter Page Url" ClientIDMode="Static" data-regex=""></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="control-label" for="DropDownList_Rights">Rights</label>
                <asp:DropDownList ID="DropDownList_Rights" runat="server" CssClass="form-control" DataTextField="name" DataValueField="id" ClientIDMode="Static"></asp:DropDownList>
            </div>
            <div class="form-group">
                <div>
                    <label class="control-label" for="RadioButtonList_Glyphicons">Glyphicon <asp:Literal ID="Literal_RadioButtonList_Glyphicons" runat="server" Visible="false"></asp:Literal></label>
                </div>            
                <asp:RadioButtonList ID="RadioButtonList_Glyphicons" runat="server" RepeatLayout="Table" RepeatColumns="40" RepeatDirection="Horizontal" ClientIDMode="Static" DataTextField="tag" DataValueField="id"></asp:RadioButtonList>
            </div>
            <div class="form-group">
                <label class="control-label" for="TextBox_Codename">Table to count from (Optional)</label>
                <asp:DropDownList ID="DropDownList_Badget_Table" runat="server" CssClass="form-control" DataTextField="TABLE_NAME" DataValueField="TABLE_NAME" ClientIDMode="Static"></asp:DropDownList>
            </div>
            <asp:LinkButton ID="LinkButton_Form" runat="server" OnClick="LinkButton_Form_Click">
                <asp:Literal ID="Literal_LinkButton_Form" runat="server"></asp:Literal>
            </asp:LinkButton>
        </div>       
    </asp:Panel>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder_ContentFooter" runat="server">
    <a id="btnBack" href="Links.aspx" runat="server" title="Back" class="btn btn-sm btn-default" visible="false">
        <span class="glyphicon glyphicon-arrow-left"></span>
    </a>

    <a id="btnAdd" href="Links.aspx?action=create" runat="server" title="Create New" class="btn btn-sm btn-success" visible="false">
        <span class="glyphicon glyphicon-plus"></span>
    </a>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="admin_scripts" runat="server">
    <asp:Literal ID="Literal_Form" runat="server" Visible="false">
        <script src="../js/bootstrap_validate.js"></script>
    </asp:Literal>
</asp:Content>
