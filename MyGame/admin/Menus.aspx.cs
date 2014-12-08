using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyGame.admin
{
    public partial class Menus : System.Web.UI.Page
    {
        DataClassesDataContext db = new DataClassesDataContext();
        public string PageUrl = "/admin/Menus.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginHandler.HasAccess)
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["action"] != null)
                    {
                        switch (Request.QueryString["action"])
                        {
                            case "links":
                                Links();
                                break;
                            case "create":
                                Create();
                                break;
                            case "edit":
                                Edit();
                                break;
                            case "delete":
                                Delete();
                                break;
                            case "delete_link":
                                Delete_Link();
                                break;
                            default:
                                Show_All();
                                break;
                        }
                    }
                    else
                    {
                        Show_All();
                    }
                }
            }
            else
            {
                Response.Redirect("~/admin/Control_Panel.aspx");
            }
        }       
        private void Links()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                VisibleTrue("menu_links");
                Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Menus", "Links");
                menus Menu = (from m in db.menus
                              where m.id.Equals(Request.QueryString["id"])
                              select m).FirstOrDefault();
                Literal_ContentTitle.Text = "Link management for " + Menu.name;
                btnEdit.HRef = this.PageUrl + "?action=edit&id=" + Menu.id;

                Repeater_Menu_Links.DataSource = (from m in db.menu_links
                                                  where m.menu_id.Equals(Request.QueryString["id"])
                                                  select m).ToList();
                Repeater_Menu_Links.DataBind();

                DropDownList_Menu_Links.DataSource = db.links.ToList();
                DropDownList_Menu_Links.DataBind();
                
            }
            else
            {
                MsgHandler.InsertMsg(2, "Menu does not exsists");
                Response.Redirect(this.PageUrl);
            }
        }
        public static bool Checked(object primary)
        {
            if(Convert.ToInt32(primary) == 1)
            {
                return true;
            }

            return false;
        }
        private void Create()
        {
            VisibleTrue("add");
            LinkButton_Form.CssClass = "validate btn btn-success";
            Literal_LinkButton_Form.Text = "Create";
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Menus", "Create");
        }
        private void Edit()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                VisibleTrue("edit");
                LinkButton_Form.CssClass = "validate btn btn-primary";
                Literal_LinkButton_Form.Text = "Edit";
                Literal_BreadCrumbs.Text = Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Menus", "Edit");

                menus EditMenu = (from m in db.menus
                                  where m.id.Equals(Request.QueryString["id"])
                                  select m).FirstOrDefault();

                if (EditMenu != null)
                {
                    TextBox_Name.Text = EditMenu.name;
                    Literal_ContentTitle.Text = "Edit " + EditMenu.name;
                    btnLinks.HRef = this.PageUrl + "?action=links&id=" + EditMenu.id;
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Menu does not exsists");
                    Response.Redirect(this.PageUrl);
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Menu does not exsists");
                Response.Redirect(this.PageUrl);
            }
        }
        private void Delete()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                menus DeleteMenu = (from m in db.menus
                                    where m.id.Equals(Request.QueryString["id"])
                                    select m).First();
                MsgHandler.InsertMsg(3, "Menu " + DeleteMenu.name + " deleted");
                db.menus.DeleteOnSubmit(DeleteMenu);
                db.SubmitChanges();               
            }
            else
            {
                MsgHandler.InsertMsg(2, "Wrong id");
            }

            Response.Redirect(this.PageUrl);
        }
        private void Delete_Link()
        {
            if (MyGame.Helper.IsQueryStringInt("mid") && MyGame.Helper.IsQueryStringInt("lid"))
            {
                menu_link DeleteMenuLink = (from m in db.menu_links
                                            where m.link_id.Equals(Request.QueryString["lid"]) && m.menu_id.Equals(Request.QueryString["mid"])
                                            select m).FirstOrDefault();
                if (DeleteMenuLink != null)
                {
                    MsgHandler.InsertMsg(4, "Link " + DeleteMenuLink.link.name + " deleted from menu");
                    db.menu_links.DeleteOnSubmit(DeleteMenuLink);
                    db.SubmitChanges();
                    Response.Redirect(this.PageUrl + "?action=links&id=" + Request.QueryString["mid"]);
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Menu or link could not be found!");
                    Response.Redirect(this.PageUrl);
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Menu link could not be found!");
                Response.Redirect(this.PageUrl);
            }           
        }
        private void Show_All()
        {
            VisibleTrue("all");
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Menus", "All");
            Literal_ContentTitle.Text = "All menus";
            Repeater_Show_All.DataSource = db.menus.ToList();
            Repeater_Show_All.DataBind();
        }
        private void VisibleTrue(string Panel)
        {
            switch (Panel)
            {
                case "all":
                    Panel_Show_All.Visible = true;
                    btnAdd.Visible = true;
                    break;
                case "edit":
                    Literal_Form.Visible = true;
                    Panel_Form.Visible = true;
                    btnBack.Visible = true;
                    btnAdd.Visible = true;
                    btnLinks.Visible = true;
                    break;
                case "add":
                    Literal_Form.Visible = true;
                    Panel_Form.Visible = true;
                    btnBack.Visible = true;
                    break;
                case "menu_links":
                    Panel_Menu_Links.Visible = true;
                    btnBack.Visible = true;
                    btnAdd.Visible = true;
                    btnEdit.Visible = true;
                    break;
            }
        }
        protected void LinkButton_Form_Click(object sender, EventArgs e)
        {
            string url = Request.RawUrl;
            string Name = TextBox_Name.Text;

            switch (Request.QueryString["action"])
            {
                case "create":
                    menus NewMenu = new menus();
                    NewMenu.name = Name;
                    db.menus.InsertOnSubmit(NewMenu);
                    db.SubmitChanges();
                    MsgHandler.InsertMsg(1, "New menu " + NewMenu.name + " created");
                    url = this.PageUrl + "?action=edit&id=" + NewMenu.id;
                    break;
                case "edit":
                    if (MyGame.Helper.IsQueryStringInt("id"))
                    {
                        menus OldMenu = (from m in db.menus
                                         where m.id.Equals(Request.QueryString["id"])
                                         select m).FirstOrDefault();

                        if (OldMenu != null)
                        {
                            OldMenu.name = Name;
                            db.SubmitChanges();
                            MsgHandler.InsertMsg(4, "Menu " + Name + " updated");
                        }
                        else
                        {
                            MsgHandler.InsertMsg(2, "Menu does not exsists");
                        }
                    }
                    else
                    {
                        MsgHandler.InsertMsg(2, "Menu does not exsists");
                    }
                    break;
            }

            Response.Redirect(url);
        }
        protected void LinkButton_Menu_Links_Click(object sender, EventArgs e)
        {
            if(MyGame.Helper.IsQueryStringInt("id"))
            {
                menu_link NewMenuLink = new menu_link();
                NewMenuLink.menu_id = Convert.ToInt32(Request.QueryString["id"]);
                NewMenuLink.link_id = Convert.ToInt32(DropDownList_Menu_Links.SelectedValue);
                NewMenuLink.is_primary = false;
                db.menu_links.InsertOnSubmit(NewMenuLink);
                db.SubmitChanges();
                MsgHandler.InsertMsg(1, "Link " + NewMenuLink.link.name + " added");
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                MsgHandler.InsertMsg(2, "Menu does not exsists");
                Response.Redirect(this.PageUrl);
            }        
        }
        public static string Disabled(object Primary)
        {
            if (Convert.ToInt32(Primary) == 1)
            {
                return "btn btn-sm btn-primary disabled";
            }
            return "btn btn-sm btn-default";
        }
        public static string PrimaryName(object Primary)
        {
            if (Convert.ToInt32(Primary) == 1)
            {
                return "Primary";
            }
            return "Set as Primary";
        }

        protected void Repeater_Menu_Links_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            menu_link UpdateMenuLink = (from m in db.menu_links
                                        where m.link_id.Equals(e.CommandName) && m.menu_id.Equals(e.CommandArgument)
                                        select m).FirstOrDefault();
            if (UpdateMenuLink != null)
            {
                if (!Convert.ToBoolean(UpdateMenuLink.is_primary))
                {
                    List<menu_link> UpdateAllMenuLinks = (from m in db.menu_links
                                                          where m.menu_id.Equals(e.CommandArgument)
                                                          select m).ToList();
                    foreach (menu_link menulink in UpdateAllMenuLinks)
                    {
                        menulink.is_primary = false;
                    }

                    UpdateMenuLink.is_primary = true;
                    db.SubmitChanges();

                    Response.Redirect(Request.RawUrl);
                }

                Response.Redirect(Request.RawUrl);
            }
        }
    }
}