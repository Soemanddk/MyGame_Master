using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyGame.admin
{
    public partial class Stats : System.Web.UI.Page
    {
        DataClassesDataContext db = new DataClassesDataContext();

        public string PageUrl = "/admin/Stats.aspx";

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
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Stats", "Create");
            Literal_ContentTitle.Text = "Create a new stat";
            FillDropDownList();
        }
        public void FillDropDownList()
        {
            DropDownList_Type.DataSource = db.stat_types.ToList();
            DropDownList_Type.DataBind();
        }
        private void Edit()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                VisibleTrue("edit");
                LinkButton_Form.CssClass = "validate btn btn-primary";
                Literal_LinkButton_Form.Text = "Edit";

                stat Stat = (from s in db.stats
                             where s.id.Equals(Request.QueryString["id"])
                             select s).FirstOrDefault();

                if (Stat != null)
                {
                    FillDropDownList();

                    TextBox_Name.Text = Stat.name;
                    TextBox_Tooltip.Text = Stat.tooltip;
                    DropDownList_Type.SelectedValue = Stat.stat_type_id.ToString();
                    Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Stats", "Edit");
                    Literal_ContentTitle.Text = "Edit " + Stat.name;
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Stat does not exsists");
                    Response.Redirect(this.PageUrl);
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Stat does not exsists");
                Response.Redirect(this.PageUrl);
            }
        }
        private void Delete()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                stat Stat = (from s in db.stats
                             where s.id.Equals(Request.QueryString["id"])
                             select s).FirstOrDefault();
                if (Stat != null)
                {
                    db.stats.DeleteOnSubmit(Stat);
                    db.SubmitChanges();
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Stat does not exsists");
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Stat does not exsists");
            }

            Response.Redirect(this.PageUrl);
        }
        private void Show_All()
        {
            VisibleTrue("all");
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Stats", "All");
            Literal_ContentTitle.Text = "All stats";
            Repeater_Show_All.DataSource = db.stats.ToList();
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
            string Tooltop = TextBox_Tooltip.Text;
            int Type = Convert.ToInt32(DropDownList_Type.SelectedValue);
            string url = Request.RawUrl;

            switch (Request.QueryString["action"])
            {
                case "create":
                    stat NewStat = new stat();
                    NewStat.name = Name;
                    NewStat.tooltip = Tooltop;
                    NewStat.stat_type_id = Type;
                    db.stats.InsertOnSubmit(NewStat);
                    db.SubmitChanges();
                    MsgHandler.InsertMsg(1, "New stat called " + NewStat.name + " created");
                    url = this.PageUrl + "?action=edit&id=" + NewStat.id;
                    break;
                case "edit":
                    if (MyGame.Helper.IsQueryStringInt("id"))
                    {
                        stat OldStat = (from s in db.stats
                                          where s.id.Equals(Request.QueryString["id"])
                                          select s).FirstOrDefault();
                        if (OldStat != null)
                        {
                            OldStat.name = Name;
                            OldStat.tooltip = Tooltop;
                            OldStat.stat_type_id = Type;
                            db.SubmitChanges();
                            MsgHandler.InsertMsg(4, OldStat.name + " edited corretly");
                        }
                        else
                        {
                            MsgHandler.InsertMsg(2, "Stat does not exsists");
                        }
                    }
                    else
                    {
                        MsgHandler.InsertMsg(2, "Stat does not exsists");
                    }
                    break;
            }

            Response.Redirect(url);
        }
    }
}