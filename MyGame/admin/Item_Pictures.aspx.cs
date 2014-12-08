using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyGame.admin
{
    public partial class Item_Pictures : System.Web.UI.Page
    {
        DataClassesDataContext db = new DataClassesDataContext();

        public string PageUrl = "/admin/Item_Pictures.aspx";

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
                            case "upload":
                                Upload();
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

        private void Upload()
        {
            if (Request.QueryString["id"] == null)
            {
                int SlotType = (from st in db.slot_types
                                select st.id).FirstOrDefault();

                Response.Redirect(this.PageUrl + "?id=" + SlotType + "&action=upload");
            }
            else
            {
                if (!Helper.IsQueryStringInt("id"))
                {
                    MsgHandler.InsertMsg(2, "Item picture does not exists");
                    Response.Redirect(this.PageUrl);
                }
            }
          
            slot_type AnotherSlotType = (from st in db.slot_types
                                         where st.id.Equals(Request.QueryString["id"])
                                         select st).FirstOrDefault();

            if (AnotherSlotType != null)
            {
                VisibleTrue("upload");
                Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Item Pictures", "Upload");
                Literal_ContentTitle.Text = AnotherSlotType.name + " Upload";
                DropDownList_ChooseSlotType.DataSource = db.slot_types.ToList();
                DropDownList_ChooseSlotType.DataBind();
                DropDownList_ChooseSlotType.SelectedValue = AnotherSlotType.id.ToString();
                Repeater_Upload_Images.DataSource = (from ip in db.item_pictures
                                                     where ip.slot_type_id.Equals(Request.QueryString["id"])
                                                     select ip).ToList();
                Repeater_Upload_Images.DataBind();
            }
            else
            {
                MsgHandler.InsertMsg(2, "Slot type does not exists");
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

                item_picture ItemPicture = (from ip in db.item_pictures
                                         where ip.id.Equals(Request.QueryString["id"])
                                         select ip).FirstOrDefault();

                if (ItemPicture != null)
                {
                    imgItem.Src = "/img/item/" + ItemPicture.id + ItemPicture.img_type;
                    imgItem.Alt = ItemPicture.name;
                    TextBox_Name.Text = ItemPicture.name;
                    DropDownList_SlotType.DataSource = db.slot_types.ToList();
                    DropDownList_SlotType.DataBind();
                    DropDownList_SlotType.SelectedValue = ItemPicture.slot_type_id.ToString();
                    Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Item Pictures", "Edit");
                    Literal_ContentTitle.Text = "Edit " + ItemPicture.name;
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Item picture does not exists");
                    Response.Redirect(this.PageUrl);
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Item picture does not exists");
                Response.Redirect(this.PageUrl);
            }
        }
        private void Delete()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                item_picture ItemPicture = (from ip in db.item_pictures
                                            where ip.id.Equals(Request.QueryString["id"])
                                            select ip).FirstOrDefault();
                if (ItemPicture != null)
                {

                    string Img = Server.MapPath("~/img/item/" + ItemPicture.id + ItemPicture.img_type);
                    if (File.Exists(Img))
                    {
                        File.Delete(Img);
                    }
                    db.item_pictures.DeleteOnSubmit(ItemPicture);
                    db.SubmitChanges();
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Item picture does not exists");
                }
            }
            else
            {
                MsgHandler.InsertMsg(2, "Item picture does not exists");
            }

            Response.Redirect(this.PageUrl);
        }
        private void Show_All()
        {
            VisibleTrue("all");
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Item Pictures", "All");
            Literal_ContentTitle.Text = "All item pictures";
            Pagination();
        }
        #region Pagination
        private void Pagination()
        {

            /* PAGINATION START */

            // Vælg hvor mange pagination knapper der højst må være synlige hele tiden. !!! (Ullige tal er bedst) !!!
            int max_pag_btn = 11; // Kan ændres
            // Vælg antal af items der skal vises per pagination knap.
            int item_per_page = 10; // Kan ændres..
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
            int amount_items = db.item_pictures.Count(); // Kan ændres

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
            Repeater_Show_All.DataSource = (from ip in db.item_pictures.Skip(offset).Take(item_per_page)
                                            orderby ip.id
                                            select ip).ToList(); // Kan ændres
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
                    btnImage.Visible = true;
                    break;
                case "edit":
                    Panel_ContentTitle.Visible = true;
                    Literal_Form.Visible = true;
                    Panel_Form.Visible = true;
                    btnBack.Visible = true;
                    btnImage.Visible = true;
                    break;
                case "upload":
                    Panel_ContentTitle.Visible = true;
                    Literal_PlupLoad_Css.Visible = true;
                    Literal_PlupLoad_Js.Visible = true;
                    Panel_Upload.Visible = true;
                    btnBack.Visible = true;
                    break;
            }
        }
        protected void LinkButton_Form_Click(object sender, EventArgs e)
        {
            string Name = TextBox_Name.Text;
            int SlotType = Convert.ToInt32(DropDownList_SlotType.SelectedValue);
            string url = Request.RawUrl;

            switch (Request.QueryString["action"])
            {
                case "edit":
                    if (MyGame.Helper.IsQueryStringInt("id"))
                    {
                        item_picture ItemPicture = (from ip in db.item_pictures
                                                    where ip.id.Equals(Request.QueryString["id"])
                                                    select ip).FirstOrDefault();
                        if (ItemPicture != null)
                        {
                            ItemPicture.name = Name;
                            ItemPicture.slot_type_id = SlotType;
                            MsgHandler.InsertMsg(4, ItemPicture.name + " edited corretly");
                            db.SubmitChanges();
                        }
                        else
                        {
                            MsgHandler.InsertMsg(2, "Item picture does not exists");
                        }
                    }
                    else
                    {
                        MsgHandler.InsertMsg(2, "Item picture does not exists");
                    }
                    break;
            }

            Response.Redirect(url);
        }

        protected void DropDownList_ChooseSlotType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(this.PageUrl + "?id=" + DropDownList_ChooseSlotType.SelectedValue + "&action=upload");
        }

        protected void LinkButton_Search_Click(object sender, EventArgs e)
        {
            Repeater_Show_All.DataSource = (from ip in db.item_pictures
                                            where ip.name.Contains(TextBox_Search.Text) || ip.slot_type.name.Contains(TextBox_Search.Text)
                                            select ip).ToList();
            Repeater_Show_All.DataBind();
        }

        protected void LinkButton_Search_Reload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}