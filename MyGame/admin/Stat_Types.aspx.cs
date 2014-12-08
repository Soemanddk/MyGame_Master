using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyGame.admin
{
    public partial class Stat_Types : System.Web.UI.Page
    {
        DataClassesDataContext db = new DataClassesDataContext();

        public string PageUrl = "/admin/Stat_Types.aspx";

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
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Stat Types", "Create");
            Literal_ContentTitle.Text = "Create a new stat type";
        }
        private void Edit()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                VisibleTrue("edit");
                LinkButton_Form.CssClass = "validate btn btn-primary";
                Literal_LinkButton_Form.Text = "Edit";

                stat_type StatType = (from st in db.stat_types
                                   where st.id.Equals(Request.QueryString["id"])
                                   select st).FirstOrDefault();

                if (StatType != null)
                {
                    TextBox_Name.Text = StatType.name;
                    TextBox_Codename.Text = StatType.codename;
                    Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Stat Types", "Edit");
                    Literal_ContentTitle.Text = "Edit " + StatType.name;
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Stat type does not exsists");
                    Response.Redirect(this.PageUrl);
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Stat type does not exsists");
                Response.Redirect(this.PageUrl);
            }
        }
        private void Delete()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                stat_type StatType = (from st in db.stat_types
                                      where st.id.Equals(Request.QueryString["id"])
                                      select st).FirstOrDefault();
                if (StatType != null)
                {
                    db.stat_types.DeleteOnSubmit(StatType);
                    db.SubmitChanges();
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Stat type does not exsists");
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Stat type does not exsists");
            }

            Response.Redirect(this.PageUrl);
        }
        private void Show_All()
        {
            VisibleTrue("all");
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Stat Types", "All");
            Literal_ContentTitle.Text = "All stat types";
            Repeater_Show_All.DataSource = db.stat_types.ToList();
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
            string CodeName = TextBox_Codename.Text;
            string url = Request.RawUrl;

            switch (Request.QueryString["action"])
            {
                case "create":
                    stat_type NewType = new stat_type();
                    NewType.name = Name;
                    NewType.codename = CodeName;
                    db.stat_types.InsertOnSubmit(NewType);
                    db.SubmitChanges();
                    MsgHandler.InsertMsg(1, "New stat type called " + NewType.name + " created");
                    url = this.PageUrl + "?action=edit&id=" + NewType.id;
                    break;
                case "edit":
                    if (MyGame.Helper.IsQueryStringInt("id"))
                    {
                        stat_type OldType = (from st in db.stat_types
                                          where st.id.Equals(Request.QueryString["id"])
                                          select st).FirstOrDefault();
                        if (OldType != null)
                        {
                            OldType.name = Name;
                            OldType.codename = CodeName;
                            db.SubmitChanges();
                            MsgHandler.InsertMsg(4, OldType.name + " edited corretly");
                        }
                        else
                        {
                            MsgHandler.InsertMsg(2, "Stat type does not exsists");
                        }
                    }
                    else
                    {
                        MsgHandler.InsertMsg(2, "Stat type does not exsists");
                    }
                    break;
            }

            Response.Redirect(url);
        }
    }
}