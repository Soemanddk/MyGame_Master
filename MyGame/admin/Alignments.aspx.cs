using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyGame.admin
{
    public partial class Alignments : System.Web.UI.Page
    {
        DataClassesDataContext db = new DataClassesDataContext();

        public string PageUrl = "/admin/Alignments.aspx";

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
            Literal_LinkButton_Form.Text = "Create";
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Alignments", "Create");
            Literal_ContentTitle.Text = "Create a new alignment";
        }
        private void Edit()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                VisibleTrue("edit");
                LinkButton_Form.CssClass = "validate btn btn-primary";
                Literal_LinkButton_Form.Text = "Edit";

                alignment EditAlignment = (from a in db.alignments
                                   where a.id.Equals(Request.QueryString["id"])
                                   select a).FirstOrDefault();

                if (EditAlignment != null)
                {
                    TextBox_Name.Text = EditAlignment.name;
                    Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Alignments", "Edit");
                    Literal_ContentTitle.Text = "Edit " + EditAlignment.name;
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Alignment does not exsists");
                    Response.Redirect(this.PageUrl);
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Alignment does not exsists");
                Response.Redirect(this.PageUrl);
            }
        }
        private void Delete()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                alignment DeleteRight = (from a in db.alignments
                                     where a.id.Equals(Request.QueryString["id"])
                                     select a).FirstOrDefault();
                if (DeleteRight != null)
                {
                    db.alignments.DeleteOnSubmit(DeleteRight);
                    db.SubmitChanges();
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Alignment does not exsists");
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Alignment does not exsists");
            }

            Response.Redirect(this.PageUrl);
        }
        private void Show_All()
        {
            VisibleTrue("all");
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Alignments", "All");
            Literal_ContentTitle.Text = "All alignments";
            Repeater_Show_All.DataSource = db.alignments.ToList();
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
            string url = Request.RawUrl;

            switch (Request.QueryString["action"])
            {
                case "create":
                    alignment NewAlignment = new alignment();
                    NewAlignment.name = Name;
                    db.alignments.InsertOnSubmit(NewAlignment);
                    db.SubmitChanges();
                    MsgHandler.InsertMsg(1, "New alignment called " + NewAlignment.name + " created");
                    url = this.PageUrl + "?action=edit&id=" + NewAlignment.id;
                    break;
                case "edit":
                    if (MyGame.Helper.IsQueryStringInt("id"))
                    {
                        alignment OldAlignment = (from a in db.alignments
                                          where a.id.Equals(Request.QueryString["id"])
                                          select a).FirstOrDefault();
                        if (OldAlignment != null)
                        {
                            OldAlignment.name = Name;
                            MsgHandler.InsertMsg(4, OldAlignment.name + " edited corretly");
                            db.SubmitChanges();                         
                        }
                        else
                        {
                            MsgHandler.InsertMsg(2, "Alignment does not exsists");
                        }
                    }
                    else
                    {
                        MsgHandler.InsertMsg(2, "Alignment does not exsists");
                    }
                    break;
            }

            Response.Redirect(url);
        }
    }
}