using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyGame.admin
{
    public partial class Items : System.Web.UI.Page
    {
        DataClassesDataContext db = new DataClassesDataContext();

        public string PageUrl = "/admin/Items.aspx";

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
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Items", "Create");
            Literal_ContentTitle.Text = "Create a new item";
            FillDropDownListAndRepeater();
            GenerateRadioButtonListImages(DropDownList_SlotType.SelectedValue);
        }
        private void Edit()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                VisibleTrue("edit");
                LinkButton_Form.CssClass = "validate btn btn-primary";
                Literal_LinkButton_Form.Text = "Edit";

                item Item = (from i in db.items
                                   where i.id.Equals(Request.QueryString["id"])
                                   select i).FirstOrDefault();

                if (Item != null)
                {
                    TextBox_Name.Text = Item.name;
                    FillDropDownListAndRepeater();
                    DropDownList_SlotType.SelectedValue = Item.slot_type_id.ToString();
                    GenerateRadioButtonListImages(DropDownList_SlotType.SelectedValue);
                    RadioButtonList_ItemPictures.SelectedValue = Item.picture_id.ToString();
                    TextBox_MaxEnchants.Text = Item.max_enchantments.ToString();
                    Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Items", "Edit");
                    Literal_ContentTitle.Text = "Edit " + Item.name;
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Item does not exists");
                    Response.Redirect(this.PageUrl);
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Item does not exists");
                Response.Redirect(this.PageUrl);
            }
        }
        private void GenerateRadioButtonListImages(string value)
        {
            RadioButtonList_ItemPictures.Items.Clear();

            List<item_picture> Pictures = (from ip in db.item_pictures
                                           where ip.slot_type_id.Equals(Convert.ToInt32(value))
                                           select ip).ToList();

            foreach (item_picture Picture in Pictures)
            {
                RadioButtonList_ItemPictures.Items.Add(
                    new ListItem("<img class='admin-radio-img thumbnail admin-img-80' src='../img/item/" + Picture.id + Picture.img_type + "' alt='" + Picture.name + "' />", Picture.id.ToString()));
                RadioButtonList_ItemPictures.SelectedValue = Picture.id.ToString();
            }
        }
        private void Delete()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                item Item = (from i in db.items
                             where i.id.Equals(Request.QueryString["id"])
                             select i).FirstOrDefault();
                if (Item != null)
                {
                    db.items.DeleteOnSubmit(Item);
                    db.SubmitChanges();
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Item does not exists");
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Item does not exists");
            }

            Response.Redirect(this.PageUrl);
        }
        private void Show_All()
        {
            VisibleTrue("all");
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Rights", "All");
            Pagination();
        }

        #region Pagination
        private void Pagination()
        {

            /* PAGINATION START */

            // Vælg hvor mange pagination knapper der højst må være synlige hele tiden. !!! (Ullige tal er bedst) !!!
            int max_pag_btn = 11; // Kan ændres
            // Vælg antal af items der skal vises per pagination knap.
            int item_per_page = 11; // Kan ændres..
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
            int amount_items = db.items.Count(); // Kan ændres

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
            Repeater_Show_All.DataSource = (from i in db.items.Skip(offset).Take(item_per_page)
                                            orderby i.id
                                            select i).ToList(); // Kan ændres
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
                case "add":
                    Literal_Form.Visible = true;
                    Panel_Form.Visible = true;
                    btnBack.Visible = true;
                    Panel_ContentTitle.Visible = true;
                    break;
            }
        }
        protected void LinkButton_Form_Click(object sender, EventArgs e)
        {
            string Name = TextBox_Name.Text;
            int SlotType = Convert.ToInt32(DropDownList_SlotType.SelectedValue);
            int MaxEnchants = Convert.ToInt32(TextBox_MaxEnchants.Text);
            int PictureId = Convert.ToInt32(RadioButtonList_ItemPictures.SelectedValue);
            string url = Request.RawUrl;

            switch (Request.QueryString["action"])
            {
                case "create":
                    item NewItem = new item();
                    NewItem.name = Name;
                    NewItem.slot_type_id = SlotType;
                    NewItem.max_enchantments = MaxEnchants;
                    NewItem.picture_id = PictureId;
                    db.items.InsertOnSubmit(NewItem);
                    db.SubmitChanges();

                    foreach(RepeaterItem Item in Repeater_Stats.Items)
                    {
                        int Amount = Convert.ToInt32(((TextBox)Item.FindControl("TextBox_StatAmount")).Text);
                        if (Amount > 0)
                        {
                            int HiddenStatId = Convert.ToInt32(((HiddenField)Item.FindControl("HiddenField_StatId")).Value);
                            item_stat NewItemStat = new item_stat();
                            NewItemStat.item_id = NewItem.id;
                            NewItemStat.stat_id = HiddenStatId;
                            NewItemStat.amount = Amount;
                            db.item_stats.InsertOnSubmit(NewItemStat);
                            db.SubmitChanges();
                        }
                    }

                    MsgHandler.InsertMsg(1, "New item called " + NewItem.name + " created");
                    url = this.PageUrl + "?action=edit&id=" + NewItem.id;
                    break;
                case "edit":
                    if (MyGame.Helper.IsQueryStringInt("id"))
                    {
                        item OldItem = (from i in db.items
                                     where i.id.Equals(Request.QueryString["id"])
                                     select i).FirstOrDefault();
                        if (OldItem != null)
                        {
                            int OldItemId = Convert.ToInt32(OldItem.id);
                            OldItem.name = Name;
                            OldItem.slot_type_id = SlotType;
                            OldItem.max_enchantments = MaxEnchants;
                            OldItem.picture_id = PictureId;
                            db.SubmitChanges();

                            List<item_stat> OldItemStats = (from iss in db.item_stats
                                                            where iss.item_id.Equals(OldItem.id)
                                                            select iss).ToList();

                            foreach (RepeaterItem Item in Repeater_Stats.Items)
                            {
                                int Amount = Convert.ToInt32(((TextBox)Item.FindControl("TextBox_StatAmount")).Text);
                                int HiddenStatId = Convert.ToInt32(((HiddenField)Item.FindControl("HiddenField_StatId")).Value);
                                bool exists = false;

                                foreach (item_stat OldItemStat in OldItemStats)
                                {
                                    if (OldItemStat.stat_id == HiddenStatId)
                                    {
                                        if (Amount != OldItemStat.amount)
                                        {
                                            item_stat ExistingStat = (from iss in db.item_stats
                                                                      where iss.id.Equals(OldItemStat.id)
                                                                      select iss).FirstOrDefault();
                                            if (Amount > 0)
                                            {
                                                ExistingStat.amount = Amount;
                                            }
                                            else
                                            {
                                                db.item_stats.DeleteOnSubmit(ExistingStat);
                                            }

                                            db.SubmitChanges();
                                        }
                                        exists = true;
                                    }
                                }

                                if (exists == false)
                                {
                                    if (Amount > 0)
                                    {
                                        item_stat NewItemStat = new item_stat();
                                        NewItemStat.item_id = OldItemId;
                                        NewItemStat.stat_id = HiddenStatId;
                                        NewItemStat.amount = Amount;
                                        db.item_stats.InsertOnSubmit(NewItemStat);
                                        db.SubmitChanges();
                                    }
                                }
                            }
                            MsgHandler.InsertMsg(4, OldItem.name + " edited corretly");
                        }
                        else
                        {
                            MsgHandler.InsertMsg(2, "Item does not exists");
                        }
                    }
                    else
                    {
                        MsgHandler.InsertMsg(2, "Item does not exists");
                    }
                    break;
            }

            Response.Redirect(url);
        }

        protected void LinkButton_Search_Click(object sender, EventArgs e)
        {
            Repeater_Show_All.DataSource = (from i in db.items
                                            where i.name.Contains(TextBox_Search.Text) || i.slot_type.name.Contains(TextBox_Search.Text)
                                            orderby i.id
                                            select i).ToList(); // Kan ændres
            Repeater_Show_All.DataBind();
        }

        protected void LinkButton_Search_Reload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        public string CountItemStats(object id)
        {
            int ItemStats = (from iss in db.item_stats
                             where iss.item_id.Equals(id)
                             select iss).Count();

            return ItemStats.ToString();
        }

        public string StatValue(object id)
        {
            if(Request.QueryString["action"] == "edit")
            {
                item_stat Stat = (from iss in db.item_stats
                                  where iss.item_id.Equals(Request.QueryString["id"]) && iss.stat_id.Equals(id)
                                  select iss).FirstOrDefault();
                if (Stat != null)
                {
                    return Stat.amount.ToString();
                }
                else
                {
                    return "0";
                }
            }

            return "0";
        }

        private void FillDropDownListAndRepeater()
        {
            DropDownList_SlotType.DataSource = db.slot_types.ToList();
            DropDownList_SlotType.DataBind();

            Repeater_Stats.DataSource = db.stats.ToList();
            Repeater_Stats.DataBind();
        }

        protected void DropDownList_SlotType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateRadioButtonListImages(DropDownList_SlotType.SelectedValue.ToString());
        }
    }
}