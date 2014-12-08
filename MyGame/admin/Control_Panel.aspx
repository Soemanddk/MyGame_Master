<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.Master" AutoEventWireup="true" CodeBehind="Control_Panel.aspx.cs" Inherits="MyGame.admin.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="admin_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_HeaderLinks" runat="server">
    <li>Whalecum</li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_ContentTitle" runat="server">
Control Panel
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_Content" runat="server">

    <div class="panel-holder">
        <div class="panel panel-default control-panel">
            <div class="panel-heading">
                Users <span class="glyphicon glyphicon-user pull-right"></span>
            </div>
            <div class="panel-body">
                 <ul class="list-group">
                    <asp:Literal ID="Literal_CountRoles" runat="server"></asp:Literal>
                 </ul>
            
            </div>
        </div>
    </div>

    <div class="panel-holder">
        <div class="panel panel-default control-panel">
            <div class="panel-heading">
                Characters <span class="glyphicon glyphicon-subtitles pull-right"></span>
            </div>
            <div class="panel-body">
                 <ul class="list-group">
                     <li class='list-group-item list-group-item-success'>
                         <ul>
                             <li>
                                 Good
                             </li>
                             <li>
                                 <asp:Literal ID="Literal_CountGood" runat="server"></asp:Literal>
                             </li>
                        </ul>
                    </li>
                     <li class='list-group-item list-group-item-success'>
                         <ul>
                             <li>
                                 Human
                             </li>
                             <li>
                                 <asp:Literal ID="Literal_CountHumans" runat="server"></asp:Literal>
                             </li>
                        </ul>
                    </li>
                     <li class='list-group-item list-group-item-success'>
                         <ul>
                             <li>
                                 Elf
                             </li>
                             <li>
                                 <asp:Literal ID="Literal_CountElfs" runat="server"></asp:Literal>
                             </li>
                        </ul>
                    </li>
                     <li class='list-group-item list-group-item-success'>
                         <ul>
                             <li>
                                 Dwarf
                             </li>
                             <li>
                                 <asp:Literal ID="Literal_CountDwarfs" runat="server"></asp:Literal>
                             </li>
                        </ul>
                    </li>
                     <li class='list-group-item list-group-item-danger'>
                         <ul>
                             <li>
                                 Evil
                             </li>
                             <li>
                                 <asp:Literal ID="Literal_CountEvil" runat="server"></asp:Literal>
                             </li>
                        </ul>
                    </li>
                     <li class='list-group-item list-group-item-danger'>
                         <ul>
                             <li>
                                 Orc
                             </li>
                             <li>
                                 <asp:Literal ID="Literal_CountOrcs" runat="server"></asp:Literal>
                             </li>
                        </ul>
                    </li>
                     <li class='list-group-item list-group-item-danger'>
                         <ul>
                             <li>
                                 Troll
                             </li>
                             <li>
                                 <asp:Literal ID="Literal_CountTrolls" runat="server"></asp:Literal>
                             </li>
                        </ul>
                    </li>
                     <li class='list-group-item list-group-item-danger'>
                         <ul>
                             <li>
                                 Half-Ogre
                             </li>
                             <li>
                                 <asp:Literal ID="Literal_CountHalfOgre" runat="server"></asp:Literal>
                             </li>
                        </ul>
                    </li>
                     <li class='list-group-item list-group-item-info'>
                         <ul>
                             <li>
                                 Total
                             </li>
                             <li>
                                 <asp:Literal ID="Literal_CountCharacters" runat="server"></asp:Literal>
                             </li>
                        </ul>
                    </li>                  
                 </ul>          
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder_ContentFooter" runat="server">
    copyright @ MyGame
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="admin_scripts" runat="server">
</asp:Content>
