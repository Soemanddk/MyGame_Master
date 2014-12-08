<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Create_Character.aspx.cs" Inherits="MyGame.Create_Character" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main_content" runat="server">
    <div id="create_character" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <div id="character_good" class="col-lg-4 col-md-4 col-sm-4 col-xs-4 col-lg-offset-3 col-md-offset-3 col-sm-offset-3 col-xs-offset-3 col-xs-pull-2 text-success">
            <h1>GOOD</h1>
        </div>

        <div id="character_evil" class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-danger">
            <h1>EVIL</h1>
        </div>

        <asp:Panel ID="Panel_Race" runat="server">
            <div id="character_race" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 push-right-14 col-lg-offset-1 text-success">
                    <h3>Human</h3>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 push-right-14 text-success">
                    <h3>Elf</h3>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 push-right-14 text-success">
                    <h3>Dwarf</h3>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel_Race_Image" runat="server">
            <div id="character_image" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 push-right-14">
                    <img class="" src="img/race/37.jpg" />
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 push-right-14">
                    <img class="" src="img/race/37.jpg" />
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 push-right-14">
                    <img class="" src="img/race/37.jpg" />
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel_Character_Name" runat="server">
            <div id="character_name_column" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 col-lg-offset-4">
                    <asp:TextBox ID="TextBox_Character_Name" CssClass="textbox_style push-top-10" runat="server" placeholder="Character name" Height="30px" Width="240px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Character_Name"
                        ControlToValidate="TextBox_Character_Name"
                        runat="server"
                        Display="Dynamic"
                        ErrorMessage=""
                        ValidationGroup="create">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="Panel_TalentTree" runat="server">
            <div id="character_talent_tree" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    sdadasd                
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:LinkButton ID="LinkButton_Create_Character" CssClass="btn btn-success" ValidationGroup="create" runat="server">Create Character</asp:LinkButton>
                </div>

            </div>

        </asp:Panel>


    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot_js" runat="server">
</asp:Content>
