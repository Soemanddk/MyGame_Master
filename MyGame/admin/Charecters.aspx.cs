using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MyGame.admin
{
    public partial class Charecters : System.Web.UI.Page
    {
        DataClassesDataContext db = new DataClassesDataContext();

        public string PageUrl = "/admin/Charecters.aspx";

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
                            case "edit":
                                Edit();
                                break;
                            case "delete":
                                Delete();
                                break;
                            case "stats":
                                Stats();
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

        private void Stats()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                if (!IsPostBack)
                {
                    VisibleTrue("stats");
                    Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Charecters", "Stats");
                    charecter Character = (from c in db.charecters
                                           where c.id.Equals(Request.QueryString["id"])
                                           select c).FirstOrDefault();
                    Literal_ContentTitle.Text = Character.name + " stats";

                    List<charecter_stat> CharStats = (from s in db.charecter_stats
                                                      where s.charecter_id.Equals(Request.QueryString["id"])
                                                      select s).ToList();

                    Repeater_Stats.DataSource = CharStats;
                    Repeater_Stats.DataBind();

                    List<stat> AllStats = db.stats.ToList();
                    List<stat> ToBeRemoved = new List<stat>();

                    foreach (stat Stat in AllStats)
                    {
                        foreach (charecter_stat CharStat in CharStats)
                        {
                            if (CharStat.stat_id == Stat.id)
                            {
                                ToBeRemoved.Add(Stat);
                            }
                        }
                    }

                    foreach (stat ToReMove in ToBeRemoved)
                    {
                        AllStats.Remove(ToReMove);
                    }

                    DropDownList_AddaStat.DataSource = AllStats;
                    DropDownList_AddaStat.DataBind();
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Charecter does not exsists");
                Response.Redirect(this.PageUrl);
            }
        }
        private void Edit()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                VisibleTrue("edit");
                LinkButton_Form.CssClass = "validate btn btn-primary";
                Literal_LinkButton_Form.Text = "Edit";

                charecter Charecter = (from c in db.charecters
                                       where c.id.Equals(Request.QueryString["id"])
                                       select c).FirstOrDefault();

                if (Charecter != null)
                {
                    TextBox_UserAccountEmail.Text = Charecter.user.email;
                    TextBox_Name.Text = Charecter.name;
                    DropDownList_Race.DataSource = db.races.ToList();
                    DropDownList_Race.DataBind();
                    DropDownList_Race.SelectedValue = Charecter.race_id.ToString();

                    GenerateImages(Charecter.race_id);                  
                    RadioButtonList_RaceImage.SelectedValue = Charecter.race_picture_id.ToString();

                    Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Charecters", "Edit");
                    Literal_ContentTitle.Text = "Edit " + Charecter.name;
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Charecter does not exsists");
                    Response.Redirect(this.PageUrl);
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Charecter does not exsists");
                Response.Redirect(this.PageUrl);
            }
        }

        private void GenerateImages(int id)
        {
            RadioButtonList_RaceImage.Items.Clear();

            List<race_picture> Pictures = (from i in db.race_pictures
                                            where i.race_id.Equals(id)
                                            select i).ToList();

            foreach (race_picture Picture in Pictures)
            {
                RadioButtonList_RaceImage.Items.Add(
                    new ListItem("<img class='admin-radio-img thumbnail' src='../img/race/" + Picture.id + Picture.img_type + "' alt='" + Picture.name + "' />", Picture.id.ToString()));
                RadioButtonList_RaceImage.SelectedValue = Picture.id.ToString();
            }

        }
        private void Delete()
        {
            if (LoginHandler.UserRights.Contains("admin_charecters_delete"))
            {
                if (MyGame.Helper.IsQueryStringInt("id"))
                {
                    charecter Charecter = (from c in db.charecters
                                           where c.id.Equals(Request.QueryString["id"])
                                           select c).FirstOrDefault();
                    if (Charecter != null)
                    {
                        db.charecters.DeleteOnSubmit(Charecter);
                        db.SubmitChanges();
                    }
                    else
                    {
                        MsgHandler.InsertMsg(2, "Charecter does not exsists");
                    }
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Charecter does not exsists");
                }
            }
            else
            {
                MsgHandler.InsertMsg(3, "You dont have the right!");
            }

            Response.Redirect(this.PageUrl);
        }
        private void Show_All()
        {
            VisibleTrue("all");
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Charecters", "All");
            Pagination();
        }

        #region Pagination
        private void Pagination()
        {

            /* PAGINATION START */

            // Vælg hvor mange pagination knapper der højst må være synlige hele tiden. !!! (Ullige tal er bedst) !!!
            int max_pag_btn = 11; // Kan ændres
            // Vælg antal af items der skal vises per pagination knap.
            int item_per_page = 6; // Kan ændres..
            // Vælg navnet på din QueryString.
            string qs_name = "page"; // Kan ændres..
            // Vælg stien og den aspx sides navn som skal bruges.
            string url_page = this.PageUrl; // Kan ændres..
            // Icon styles for Prev/Next & First/Last
            string btnPrev = "<span class='glyphicon glyphicon-chevron-left mgt3'></span>";
            string btnNext = "<span class='glyphicon glyphicon-chevron-right mgt3'></span>";
            string btnFirst = "<span class='glyphicon glyphicon-fast-backward mgt3'></span>";
            string btnLast = "<span class='glyphicon glyphicon-fast-forward mgt3'></span>";
            /* Vælg classen til Pagination ul
             * Bootstrap classes
             * 'pagination pagination-sm'
             * 'pagination'
             * 'pagination pagination-lg' */
            string ul_class = "pagination"; // Kan ændres

            // Tæller hvor mange rows der er i alt fra tabellen.
            int amount_items = db.charecters.Count(); // Kan ændres

            #region Magic (don't touch)
            // !!! HERFRA SKAL INTET ÆNDRES !!! //

            // Sætter total_pages til det antal pagination knapper der skal være i alt. !!! (ikke hvor mange der bliver vist) !!!
            int total_pages = (int)Math.Ceiling((double)amount_items / (double)item_per_page);
            int shown_pag_btn = total_pages; // Sætter antal knapper vist på siden
            // Hvis antal af total pagination knapper er større end det angivet max_pag_btn
            if (shown_pag_btn > max_pag_btn)
            {
                shown_pag_btn = max_pag_btn; // Sætter antal viste pagination knapper til max_pag_btn
            }
            // Sætter shown_offset til hvor mange pagination knapper der skal vises før og efter den aktuelle pagination knap.
            int shown_offset = (int)Math.Floor((double)shown_pag_btn / 2);

            int current_page = 1; // Sætter aktuelle pagination side til 1.
            if (Request.QueryString[qs_name] != null) // Spørg om url addressen QueryString page er alt andet end null
            {
                // Hvis QueryString page ikke kan conventeres til et tal eller ikke er eksisterende skal current_page sættes til 1.
                // Ellers sæt current_page til det der står i QueryString qs_name.
                if (!int.TryParse(Request.QueryString[qs_name].ToString(), out current_page))
                {
                    current_page = 1;
                }
                // Hvis den aktuelle side er større end total antal sider
                if (current_page > total_pages)
                {
                    current_page = total_pages; // Sæt aktuelle side til sidste side
                }
            }


            // Starter links som indenholder hele ul'en til pagination metoden.
            string links = "<ul class='" + ul_class + "'>";


            // Knap til at gå til første pagination side
            links += "<li><a href='" + url_page + "?" + qs_name + "=1' title='First Page (1)' " + (current_page > (shown_offset + 1) ? "" : "class='btn disabled' role='button'") + ">" + btnFirst + "</a></li>";

            // Knap til at gå 1 side tilbage fra aktuelle pagination side
            links += "<li><a href='" + url_page + "?" + qs_name + "=" + (current_page - 1) + "' title='Prev' " + (current_page > 1 ? "" : "class='btn disabled' role='button'") + ">" + btnPrev + "</a></li>";


            // Synlige pagination knapper start
            if (current_page <= shown_offset)
            {
                for (int i = 1; i <= shown_pag_btn; i++)
                {
                    links += "<li " + (i == current_page ? "class='active'" : "") + "><a href='" + url_page + "?" + qs_name + "=" + i + "'>" + i + "</a></li>";
                }
            }
            else if (current_page >= (total_pages - shown_offset))
            {
                for (int i = (total_pages - shown_pag_btn + 1); i <= total_pages; i++)
                {
                    links += "<li " + (i == current_page ? "class='active'" : "") + "><a href='" + url_page + "?" + qs_name + "=" + i + "'>" + i + "</a></li>";
                }
            }
            else
            {
                for (int i = current_page - shown_offset; i <= current_page + shown_offset; i++)
                {
                    links += "<li " + (i == current_page ? "class='active'" : "") + "><a href='" + url_page + "?" + qs_name + "=" + i + "'>" + i + "</a></li>";
                }
            }
            // Synlige pagination knapper slut


            // Knap til at gå 1 side frem fra aktuelle pagination side
            links += "<li><a href='" + url_page + "?" + qs_name + "=" + (current_page + 1) + "' title='Next' " + (current_page < total_pages ? "" : "class='btn disabled' role='button'") + ">" + btnNext + "</a></li>";

            // Knap til at gå til sidste pagination side
            links += "<li><a href='" + url_page + "?" + qs_name + "=" + total_pages + "' title='Last Page (" + total_pages + ")' " + ((current_page + shown_offset) < total_pages ? "" : "class='btn disabled' role='button'") + ">" + btnLast + "</a></li>";

            links += "</ul>";

            // Aktuelle side minus 1 gange antal af valgte ting vist per side.
            int offset = (current_page - 1) * item_per_page;
            #endregion

            // Slutter links som indenholder hele ul'en til pagination metoden.
            Literal_Pagination.Text = links; // Kan ændres

            // Skip(find et bestemt id).Take(tag de næste rækkers antal) orderby(arranger efter order af) 
            Repeater_Show_All.DataSource = (from c in db.charecters.Skip(offset).Take(item_per_page)
                                            orderby c.id
                                            select c).ToList(); // Kan ændres
            Repeater_Show_All.DataBind();

            // Sætter session som kan bruges til en tilbage knap
            Session["whereat"] = url_page + "?" + qs_name + "=" + current_page; // Kan slettes

            /* PAGINATION SLUT */
        }
        #endregion
        private void VisibleTrue(string Panel)
        {
            if (Session["whereat"] != null)
            {
                btnBack.HRef = Session["whereat"].ToString();
            }

            switch (Panel)
            {
                case "all":
                    Panel_Pagination.Visible = true;
                    Panel_Show_All.Visible = true;
                    btnAdd.Visible = true;
                    break;
                case "edit":
                    Literal_Form.Visible = true;
                    Panel_Form.Visible = true;
                    btnBack.Visible = true;
                    Panel_ContentTitle.Visible = true;
                    btnAdd.Visible = true;
                    break;
                case "stats":
                    Literal_Form.Visible = true;
                    btnBack.Visible = true;
                    Panel_ContentTitle.Visible = true;
                    Panel_Stats.Visible = true;
                    break;
            }
        }
        protected void LinkButton_Form_Click(object sender, EventArgs e)
        {
            switch (Request.QueryString["action"])
            {
                case "edit":
                    if (MyGame.Helper.IsQueryStringInt("id"))
                    {
                        charecter NewCharecter = (from c in db.charecters
                                          where c.id.Equals(Request.QueryString["id"])
                                          select c).FirstOrDefault();
                        if (NewCharecter != null)
                        {
                            NewCharecter.name = TextBox_Name.Text;
                            NewCharecter.race_id = Convert.ToInt32(DropDownList_Race.SelectedValue);
                            NewCharecter.race_picture_id = Convert.ToInt32(RadioButtonList_RaceImage.SelectedValue);
                            db.SubmitChanges();
                            MsgHandler.InsertMsg(4, NewCharecter.name + " edited corretly");
                        }
                        else
                        {
                            MsgHandler.InsertMsg(2, "Charecter does not exsists");
                        }
                    }
                    else
                    {
                        MsgHandler.InsertMsg(2, "Charecter does not exsists");
                    }
                    break;
            }

            Response.Redirect(Request.RawUrl);
        }

        protected void LinkButton_Search_Click(object sender, EventArgs e)
        {
            Repeater_Show_All.DataSource = (from c in db.charecters
                                            where c.name.Contains(TextBox_Search.Text) 
                                            || c.race.name.Contains(TextBox_Search.Text) 
                                            || c.race.alignment.name.Contains(TextBox_Search.Text)
                                            orderby c.id
                                            select c).ToList(); // Kan ændres
            Repeater_Show_All.DataBind();
        }

        protected void LinkButton_Search_Reload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void DropDownList_Race_SelectedIndexChanged(object sender, EventArgs e)
        {
           GenerateImages(Convert.ToInt32(DropDownList_Race.SelectedValue));
        }

        protected void LinkButton_Save_Stats_Click(object sender, EventArgs e)
        {
            foreach(RepeaterItem Item in Repeater_Stats.Items)
            {
                int StatId = Convert.ToInt32(((HiddenField)Item.FindControl("TextBox_StatId")).Value);
                charecter_stat Char_Stat = (from cs in db.charecter_stats
                                            where cs.id.Equals(StatId)
                                            select cs).FirstOrDefault();
                if (Char_Stat != null)
                {
                    Char_Stat.amount = Convert.ToInt32(((TextBox)Item.FindControl("TextBox_Stat")).Text);
                    db.SubmitChanges();
                }
            }

            MsgHandler.InsertMsg(1, "Stats are saved");
            Response.Redirect(Request.RawUrl);
        }

        protected void LinkButton_Add_A_Stat_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                if (Helper.IsQueryStringInt("id"))
                {
                    int StatId = Convert.ToInt32(DropDownList_AddaStat.SelectedValue);

                    charecter_stat FindStat = (from cs in db.charecter_stats
                                               where cs.charecter_id.Equals(Request.QueryString["id"]) && cs.stat_id.Equals(StatId)
                                               select cs).FirstOrDefault();

                    if(FindStat == null)
                    {
                        charecter_stat NewCharacterStat = new charecter_stat();
                        NewCharacterStat.charecter_id = Convert.ToInt32(Request.QueryString["id"]);
                        NewCharacterStat.stat_id = StatId;
                        NewCharacterStat.amount = Convert.ToInt32(TextBox_AddaStat_Amount.Text);
                        db.charecter_stats.InsertOnSubmit(NewCharacterStat);
                        db.SubmitChanges();
                    }
                    else
                    {
                        MsgHandler.InsertMsg(3, "Stat already exists");
                    }
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Can't dins character");
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Wrong querystring");
            }

            Response.Redirect(Request.RawUrl);
        }
    }
}