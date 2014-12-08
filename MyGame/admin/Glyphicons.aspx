<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.Master" AutoEventWireup="true" CodeBehind="Glyphicons.aspx.cs" Inherits="MyGame.admin.Glyphicons" %>
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
        <asp:Repeater ID="Repeater_Show_All" runat="server" ItemType="MyGame.glyphicon">
            <ItemTemplate>
                <div class="glyphicon_wrapper">
                    <div>
                        <%# Item.tag %>
                    </div>
                    <div>
                        <a href="Glyphicons.aspx?action=edit&id=<%# Item.id %>" title="Edit" class="btn btn-primary btn-xs">
                            <span class="glyphicon glyphicon-edit"></span>
                        </a>
                        <a href="Glyphicons.aspx?action=delete&id=<%# Item.id %>" title="Delete" class="btn btn-danger btn-xs" onclick="return confirm('Are you sure?');"><span class="glyphicon glyphicon-trash"></span>
                        </a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>

    <asp:Panel ID="Panel_Form" runat="server" Visible="false">
        <div class="form">
            <div class="form-group">
                <label class="control-label" for="TextBox_Name">Glyphicon Ending <asp:Literal ID="Literal_Glyphicon" runat="server" Visible="false"></asp:Literal></label>
                <asp:TextBox ID="TextBox_Name" runat="server" CssClass="form-control" placeholder="Glyphicon Ending" ClientIDMode="Static" data-regex="empty"></asp:TextBox>
            </div>
            <asp:LinkButton ID="LinkButton_Form" runat="server" OnClick="LinkButton_Form_Click">
                <asp:Literal ID="Literal_LinkButton_Form" runat="server"></asp:Literal>
            </asp:LinkButton>
        </div>       
    </asp:Panel>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder_ContentFooter" runat="server">
    <a id="btnBack" href="Glyphicons.aspx" runat="server" title="Back" class="btn btn-sm btn-default" visible="false">
        <span class="glyphicon glyphicon-arrow-left"></span>
    </a>

    <a id="btnAdd" href="Glyphicons.aspx?action=create" runat="server" title="Create New" class="btn btn-sm btn-success" visible="false">
        <span class="glyphicon glyphicon-plus"></span>
    </a>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="admin_scripts" runat="server">
    <asp:Literal ID="Literal_Form" runat="server" Visible="false">
        <script src="../js/bootstrap_validate.js"></script>
    </asp:Literal>
</asp:Content>
