﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.Master" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="MyGame.admin.Items" %>
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
        <asp:Repeater ID="Repeater_Show_All" runat="server" ItemType="MyGame.item">
            <HeaderTemplate>                               
                <table class="table table-responsive">
                    <thead>
                        <tr>                           
                            <th>#img</th>
                            <th>Name</th>
                            <th>Slot Type</th>
                            <th>Stats</th>
                            <th>Max Enchants</th>
                            <th>Actions</th>
                        </tr>
                    </thead>              
            </HeaderTemplate>
            <ItemTemplate>
                <tr>                   
                    <td>
                        <img class="admin-item-img" src="/img/item/<%# Item.picture_id + Item.item_picture.img_type %>" alt="<%# Item.name %>" />
                    </td>
                    <td>
                        <%# Item.name %>
                    </td>
                    <td>
                        <%# Item.slot_type.name %>
                    </td>
                    <td>
                        <%# CountItemStats(Item.id) %>
                    </td>                    
                    <td>
                        <%# Item.max_enchantments %>
                    </td>
                    <td>
                        <a href="Items.aspx?action=edit&id=<%# Item.id %>" title="Edit" class="btn btn-sm btn-primary">
                            <span class="glyphicon glyphicon-edit"></span>
                        </a>
                        <a href="Items.aspx?action=delete&id=<%# Item.id %>" title="Delete" class="btn btn-sm btn-danger" onclick="return confirm('Are you sure?');">
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
            <div class="admin-item-img-holder">
                <div class="radio admin-radio-list">
                    <asp:RadioButtonList ID="RadioButtonList_ItemPictures" runat="server" RepeatLayout="UnorderedList"></asp:RadioButtonList>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label" for="TextBox_Name">Name</label>
                <asp:TextBox ID="TextBox_Name" runat="server" CssClass="form-control" placeholder="Enter Name" ClientIDMode="Static" data-regex=""></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="control-label" for="DropDownList_SlotType">Slot Type</label>
                <asp:DropDownList ID="DropDownList_SlotType" runat="server" CssClass="form-control" DataValueField="id" DataTextField="name" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_SlotType_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <table class="table">
                <thead>
                    <tr>
                        <th>
                            Stat Name
                        </th>
                        <th>
                            Amount
                        </th>
                    </tr>
                </thead>
                <tbody>               
            <asp:Repeater ID="Repeater_Stats" runat="server" ItemType="MyGame.stat">
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="Label_StatName" runat="server" CssClass="control-label" Text="<%# Item.name %>"></asp:Label>
                        </td>
                        <td>
                            <asp:HiddenField ID="HiddenField_StatId" runat="server" Value="<%# Item.id %>" />                          
                            <asp:TextBox ID="TextBox_StatAmount" runat="server" CssClass="form-control" Text="<%# StatValue(Item.id) %>"></asp:TextBox>
                        </td>
                    </tr>                         
                </ItemTemplate>
            </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td>
                            Max Enchantments
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_MaxEnchants" runat="server" CssClass="form-control" placeholder="Enter number" ClientIDMode="Static" Text="0"></asp:TextBox>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <asp:LinkButton ID="LinkButton_Form" runat="server" OnClick="LinkButton_Form_Click">
                <asp:Literal ID="Literal_LinkButton_Form" runat="server"></asp:Literal>
            </asp:LinkButton>
        </div>       
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder_ContentFooter" runat="server">
    <a id="btnBack" href="Items.aspx" runat="server" title="Back" class="btn btn-sm btn-default" visible="false">
        <span class="glyphicon glyphicon-arrow-left"></span>
    </a>

    <a id="btnAdd" href="Items.aspx?action=create" runat="server" title="Create New" class="btn btn-sm btn-success" visible="false">
        <span class="glyphicon glyphicon-plus"></span>
    </a>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="admin_scripts" runat="server">
    <asp:Literal ID="Literal_Form" runat="server" Visible="false">
        <script src="../js/bootstrap_validate.js"></script>
    </asp:Literal>
</asp:Content>
