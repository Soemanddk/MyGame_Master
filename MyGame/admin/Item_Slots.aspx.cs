using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyGame.admin
{
    public partial class Item_Slots : System.Web.UI.Page
    {
        DataClassesDataContext db = new DataClassesDataContext();

        public string PageUrl = "/admin/Item_Slots.aspx";

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
        public void FillDropDownList()
        {
            DropDownList_Category.DataSource = db.item_categories.ToList();
            DropDownList_Category.DataBind();
        }
        private void Create()
        {
            VisibleTrue("add");
            LinkButton_Form.CssClass = "validate btn btn-success";
            Literal_LinkButton_Form.Text = "Create";
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Item Slots", "Create");
            Literal_ContentTitle.Text = "Create a new item slot";
            FillDropDownList();
        }
        private void Edit()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                VisibleTrue("edit");
                LinkButton_Form.CssClass = "validate btn btn-primary";
                Literal_LinkButton_Form.Text = "Edit";

                slot_type SlotType = (from st in db.slot_types
                                          where st.id.Equals(Request.QueryString["id"])
                                          select st).FirstOrDefault();

                if (SlotType != null)
                {
                    FillDropDownList();
                    TextBox_Name.Text = SlotType.name;
                    DropDownList_Category.SelectedValue = SlotType.item_category_id.ToString();
                    Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Item Slots", "Edit");
                    Literal_ContentTitle.Text = "Edit " + SlotType.name;
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Item slot does not exists");
                    Response.Redirect(this.PageUrl);
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Item slot does not exists");
                Response.Redirect(this.PageUrl);
            }
        }
        private void Delete()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                slot_type SlotType = (from st in db.slot_types
                                      where st.id.Equals(Request.QueryString["id"])
                                      select st).FirstOrDefault();
                if (SlotType != null)
                {
                    db.slot_types.DeleteOnSubmit(SlotType);
                    db.SubmitChanges();
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Item slot does not exists");
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Item slot does not exists");
            }

            Response.Redirect(this.PageUrl);
        }
        private void Show_All()
        {
            VisibleTrue("all");
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Item Slots", "All");
            Literal_ContentTitle.Text = "All Item Slots";
            Repeater_Show_All.DataSource = db.slot_types.ToList();
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
            int Category = Convert.ToInt32(DropDownList_Category.SelectedValue);
            string url = Request.RawUrl;

            switch (Request.QueryString["action"])
            {
                case "create":
                    slot_type NewSlotType = new slot_type();
                    NewSlotType.name = Name;
                    NewSlotType.item_category_id = Category;
                    db.slot_types.InsertOnSubmit(NewSlotType);
                    db.SubmitChanges();
                    MsgHandler.InsertMsg(1, "New item slot called " + NewSlotType.name + " created");
                    url = this.PageUrl + "?action=edit&id=" + NewSlotType.id;
                    break;
                case "edit":
                    if (MyGame.Helper.IsQueryStringInt("id"))
                    {
                        slot_type OldSlotType = (from st in db.slot_types
                                                 where st.id.Equals(Request.QueryString["id"])
                                                 select st).FirstOrDefault();
                        if (OldSlotType != null)
                        {
                            OldSlotType.name = Name;
                            OldSlotType.item_category_id = Category;
                            db.SubmitChanges();
                            MsgHandler.InsertMsg(4, OldSlotType.name + " edited corretly");
                        }
                        else
                        {
                            MsgHandler.InsertMsg(2, "Item slot does not exists");
                        }
                    }
                    else
                    {
                        MsgHandler.InsertMsg(2, "Item slot does not exists");
                    }
                    break;
            }

            Response.Redirect(url);
        }
    }
}