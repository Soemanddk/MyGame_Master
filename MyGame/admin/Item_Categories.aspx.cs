using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyGame.admin
{
    public partial class Item_Categories : System.Web.UI.Page
    {
        DataClassesDataContext db = new DataClassesDataContext();

        public string PageUrl = "/admin/Item_Categories.aspx";

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
                            case "create":
                                Create();
                                break;
                            case "edit":
                                Edit();
                                break;
                            case "delete":
                                Delete();
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
        private void Create()
        {
            VisibleTrue("add");
            LinkButton_Form.CssClass = "validate btn btn-success";
            Literal_LinkButton_Form.Text = " Create";
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Item Categories", "Create");
            Literal_ContentTitle.Text = "Create a new item category";
        }
        private void Edit()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                VisibleTrue("edit");
                LinkButton_Form.CssClass = "validate btn btn-primary";
                Literal_LinkButton_Form.Text = "Edit";

                item_category ItemCategory = (from ic in db.item_categories
                                              where ic.id.Equals(Request.QueryString["id"])
                                              select ic).FirstOrDefault();

                if (ItemCategory != null)
                {
                    TextBox_Name.Text = ItemCategory.name;
                    CheckBox_EquipAble.Checked = Convert.ToBoolean(ItemCategory.equipable);
                    Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Item Categories", "Edit");
                    Literal_ContentTitle.Text = "Edit " + ItemCategory.name;
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Item category does not exists");
                    Response.Redirect(this.PageUrl);
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Item category does not exists");
                Response.Redirect(this.PageUrl);
            }
        }
        private void Delete()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                item_category ItemCategory = (from ic in db.item_categories
                                              where ic.id.Equals(Request.QueryString["id"])
                                              select ic).FirstOrDefault();
                if (ItemCategory != null)
                {
                    db.item_categories.DeleteOnSubmit(ItemCategory);
                    db.SubmitChanges();
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Item category does not exists");
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Item category does not exists");
            }

            Response.Redirect(this.PageUrl);
        }
        private void Show_All()
        {
            VisibleTrue("all");
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Item Categories", "All");
            Literal_ContentTitle.Text = "All Item Categories";
            Repeater_Show_All.DataSource = db.item_categories.ToList();
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
                    break;
                case "add":
                    Literal_Form.Visible = true;
                    Panel_Form.Visible = true;
                    btnBack.Visible = true;
                    break;
            }
        }
        protected void LinkButton_Form_Click(object sender, EventArgs e)
        {
            string Name = TextBox_Name.Text;
            bool Checked = Convert.ToBoolean(CheckBox_EquipAble.Checked);
            string url = Request.RawUrl;

            switch (Request.QueryString["action"])
            {
                case "create":
                    item_category NewItemCategory = new item_category();
                    NewItemCategory.name = Name;
                    NewItemCategory.equipable = Checked;
                    db.item_categories.InsertOnSubmit(NewItemCategory);
                    db.SubmitChanges();
                    MsgHandler.InsertMsg(1, "New item category called " + NewItemCategory.name + " created");
                    url = this.PageUrl + "?action=edit&id=" + NewItemCategory.id;
                    break;
                case "edit":
                    if (MyGame.Helper.IsQueryStringInt("id"))
                    {
                        item_category OldItemCategory = (from ic in db.item_categories
                                                         where ic.id.Equals(Request.QueryString["id"])
                                                         select ic).FirstOrDefault();
                        if (OldItemCategory != null)
                        {
                            OldItemCategory.name = Name;
                            OldItemCategory.equipable = Checked;
                            db.SubmitChanges();
                            MsgHandler.InsertMsg(4, OldItemCategory.name + " edited corretly");
                        }
                        else
                        {
                            MsgHandler.InsertMsg(2, "Item category does not exists");
                        }
                    }
                    else
                    {
                        MsgHandler.InsertMsg(2, "Item category does not exists");
                    }
                    break;
            }

            Response.Redirect(url);
        }
    }
}