using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyGame.admin
{
    public partial class Races : System.Web.UI.Page
    {
        DataClassesDataContext db = new DataClassesDataContext();

        public string PageUrl = "/admin/Races.aspx";

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
                            case "images":
                                Images();
                                break;
                            case "delete_img":
                                Delete_Img();
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

        private void Images()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                VisibleTrue("img");
                Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Races", "Images");
                string RaceName = (from r in db.races
                                   where r.id.Equals(Request.QueryString["id"])
                                   select r.name).First().ToString();

                Literal_ContentTitle.Text = RaceName + " Image handling";

                Repeater_Images.DataSource = (from rp in db.race_pictures
                                              where rp.race_id.Equals(Request.QueryString["id"])
                                              select rp).ToList();
                Repeater_Images.DataBind();

                btnEdit.HRef = this.PageUrl + "?action=edit&id=" + Request.QueryString["id"];
            }
            else
            {
                MsgHandler.InsertMsg(2, "Race does not exsists");
                Response.Redirect(this.PageUrl);
            }          
        }

        private void Delete_Img()
        {
            string Url = "";

            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                race_picture DeleteImage = (from rp in db.race_pictures
                                   where rp.id.Equals(Request.QueryString["id"])
                                   select rp).FirstOrDefault();
                if (DeleteImage != null)
                {
                    string Img = Server.MapPath("~/img/race/" + DeleteImage.id + DeleteImage.img_type);
                    if (File.Exists(Img))
                    {
                        File.Delete(Img);
                    }
                    Url = "?action=images&id=" + DeleteImage.race_id;
                    db.race_pictures.DeleteOnSubmit(DeleteImage);
                    db.SubmitChanges();
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Image does not exsists");
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Image does not exsists");
            }

            Response.Redirect(this.PageUrl + Url);
        }
        private void Create()
        {
            VisibleTrue("add");
            LinkButton_Form.CssClass = "validate btn btn-success";
            Literal_LinkButton_Form.Text = "Create";
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Races", "Create");
            Literal_ContentTitle.Text = "Create a new race";
            FillDropDownList();
        }
        private void Edit()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                VisibleTrue("edit");
                LinkButton_Form.CssClass = "validate btn btn-primary";
                Literal_LinkButton_Form.Text = "Edit";

                race EditRace = (from r in db.races
                                 where r.id.Equals(Request.QueryString["id"])
                                 select r).FirstOrDefault();

                if (EditRace != null)
                {
                    FillDropDownList();
                    TextBox_Name.Text = EditRace.name;
                    DropDownList_Alignment.SelectedValue = EditRace.alignment_id.ToString();
                    Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Races", "Edit");
                    Literal_ContentTitle.Text = "Edit " + EditRace.name;
                    btnImage.HRef = this.PageUrl + "?action=images&id=" + EditRace.id;
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Race does not exsists");
                    Response.Redirect(this.PageUrl);
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Race does not exsists");
                Response.Redirect(this.PageUrl);
            }
        }
        private void Delete()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                race DeleteRace = (from r in db.races
                                   where r.id.Equals(Request.QueryString["id"])
                                   select r).FirstOrDefault();
                if (DeleteRace != null)
                {
                    List<race_picture> Images = (from rp in db.race_pictures
                                                 where rp.race_id.Equals(Request.QueryString["id"])
                                                 select rp).ToList();
                    foreach(race_picture Image in Images)
                    {
                        string Img = Server.MapPath("~/img/race/" + Image.id + Image.img_type);
                        if(File.Exists(Img))
                        {
                            File.Delete(Img);
                        }
                    }
                    db.races.DeleteOnSubmit(DeleteRace);
                    db.SubmitChanges();
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Race does not exsists");
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Race does not exsists");
            }

            Response.Redirect(this.PageUrl);
        }
        private void Show_All()
        {
            VisibleTrue("all");
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Races", "All");
            Literal_ContentTitle.Text = "All races";
            Repeater_Show_All.DataSource = db.races.ToList();
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
                    btnImage.Visible = true;
                    break;
                case "add":
                    Literal_Form.Visible = true;
                    Panel_Form.Visible = true;
                    btnBack.Visible = true;
                    break;
                case "img":
                    Literal_PlupLoad_Css.Visible = true;
                    Literal_PlupLoad_Js.Visible = true;
                    Panel_Images.Visible = true;
                    btnBack.Visible = true;
                    btnAdd.Visible = true;
                    btnEdit.Visible = true;
                    break;
            }
        }
        private void FillDropDownList()
        {
            DropDownList_Alignment.DataSource = db.alignments.ToList();
            DropDownList_Alignment.DataBind();
        }
        protected void LinkButton_Form_Click(object sender, EventArgs e)
        {
            string Name = TextBox_Name.Text;
            int Alignment = Convert.ToInt32(DropDownList_Alignment.SelectedValue);
            string url = Request.RawUrl;

            switch (Request.QueryString["action"])
            {
                case "create":
                    race NewRace = new race();
                    NewRace.name = Name;
                    NewRace.alignment_id = Alignment;
                    db.races.InsertOnSubmit(NewRace);
                    db.SubmitChanges();
                    MsgHandler.InsertMsg(1, "New race called " + NewRace.name + " created");
                    url = this.PageUrl + "?action=edit&id=" + NewRace.id;
                    break;
                case "edit":
                    if (MyGame.Helper.IsQueryStringInt("id"))
                    {
                        race OldRace = (from r in db.races
                                        where r.id.Equals(Request.QueryString["id"])
                                        select r).FirstOrDefault();
                        if (OldRace != null)
                        {
                            OldRace.name = Name;
                            OldRace.alignment_id = Alignment;
                            MsgHandler.InsertMsg(4, OldRace.name + " edited corretly");
                            db.SubmitChanges();
                        }
                        else
                        {
                            MsgHandler.InsertMsg(2, "Race does not exsists");
                        }
                    }
                    else
                    {
                        MsgHandler.InsertMsg(2, "Race does not exsists");
                    }
                    break;
            }

            Response.Redirect(url);
        }
        public int CountImages(int id)
        {
            return db.race_pictures.Where(p => p.race_id.Equals(id)).Count();
        }
    }
}