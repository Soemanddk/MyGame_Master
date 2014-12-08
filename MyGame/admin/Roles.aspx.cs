using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MyGame.admin
{
    public partial class Roles : System.Web.UI.Page
    {
        DataClassesDataContext db = new DataClassesDataContext();

        public string PageUrl = "/admin/Roles.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginHandler.HasAccess)
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
                        case "role_rights":
                            Role_Rights();
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
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Roles", "Create");
            Literal_ContentTitle.Text = "Create a new role";
        }
        private void Edit()
        {
            if (!IsPostBack)
            {
                if (MyGame.Helper.IsQueryStringInt("id"))
                {                                                                         
                    role EditRole = (from r in db.roles
                                     where r.id.Equals(Request.QueryString["id"])
                                     select r).FirstOrDefault();

                    if (EditRole != null)
                    {
                        VisibleTrue("edit");
                        LinkButton_Form.CssClass = "validate btn btn-primary";
                        Literal_LinkButton_Form.Text = "Edit"; 
                        TextBox_Name.Text = EditRole.name;
                        Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Roles", "Edit"); 
                        Literal_ContentTitle.Text = "Edit role " + EditRole.name;
                    }
                    else
                    {
                        MsgHandler.InsertMsg(2, "Role does not exsists");
                        Response.Redirect(this.PageUrl);
                    }
                }
                else
                {
                    MsgHandler.InsertMsg(2, "Role does not exsists");
                    Response.Redirect(this.PageUrl);
                }
            }
        }
        private void Delete()
        {
            if (MyGame.Helper.IsQueryStringInt("id"))
            {
                role EditRole = (from r in db.roles
                                 where r.id.Equals(Request.QueryString["id"])
                                 select r).First();
                db.roles.DeleteOnSubmit(EditRole);
                db.SubmitChanges();
            }
            else
            {
                MsgHandler.InsertMsg(2, "Role does not exsists");
            }

            Response.Redirect(this.PageUrl);
        }
        private void Show_All()
        {
            VisibleTrue("all");
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Roles", "All");
            Literal_ContentTitle.Text = "All Roles";
            Repeater_Show_All.DataSource = db.roles.ToList();
            Repeater_Show_All.DataBind();
        }
        private void Role_Rights()
        {
            VisibleTrue("rr");
            Literal_ContentTitle.Text = "Role Rights";
            Literal_BreadCrumbs.Text = Helper.BreadCrumb(this.PageUrl, "Roles", "Role rights");
            Repeater_Role_Rights_Roles.DataSource = db.roles.ToList();
            Repeater_Role_Rights_Roles.DataBind();
            Repeater_Role_Rights_Rights.DataSource = db.rights.ToList();
            Repeater_Role_Rights_Rights.DataBind();
        }
        private void VisibleTrue(string Panel)
        {
            switch (Panel)
            {
                case "all":
                    Panel_Show_All.Visible = true;
                    btnAdd.Visible = true;
                    btnRoleRights.Visible = true;
                    break;
                case "edit":
                    Literal_Form.Visible = true;
                    Panel_Form.Visible = true;
                    btnAdd.Visible = true;
                    btnBack.Visible = true;
                    btnRoleRights.Visible = true;
                    break;
                case "add":
                    Literal_Form.Visible = true;
                    Panel_Form.Visible = true;
                    btnBack.Visible = true;
                    btnRoleRights.Visible = true;
                    break;
                case "rr":
                    Panel_Role_Rights.Visible = true;
                    btnAdd.Visible = true;
                    btnBack.Visible = true;
                    break;
            }
        }

        protected void LinkButton_Form_Click(object sender, EventArgs e)
        {
            string RoleName = TextBox_Name.Text;
            string url = Request.RawUrl;

            switch (Request.QueryString["action"])
            {
                case "create":
                    role NewRole = new role();
                    NewRole.name = RoleName;
                    db.roles.InsertOnSubmit(NewRole);
                    db.SubmitChanges();
                    MsgHandler.InsertMsg(1, "New role called " + NewRole.name + " created");
                    url = "~/admin/Roles.aspx?action=edit&id=" + NewRole.id;
                    break;
                case "edit":
                    if (MyGame.Helper.IsQueryStringInt("id"))
                    {
                        role OldRole = (from r in db.roles
                                        where r.id.Equals(Request.QueryString["id"])
                                        select r).FirstOrDefault();
                        if (OldRole != null)
                        {
                            OldRole.name = RoleName;
                            db.SubmitChanges();
                            MsgHandler.InsertMsg(4, OldRole.name + " edited corretly");
                        }
                        else
                        {
                            MsgHandler.InsertMsg(2, "Role does not exsists");
                        }                        
                    }
                    else
                    {
                        MsgHandler.InsertMsg(2, "Role does not exsists");
                    }
                    break;
            }

            Response.Redirect(url);
        }
        

        protected void Repeater_Role_Rights_Rights_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Prøver med LinQ
                right thisRight = (right)e.Item.DataItem;

                HtmlGenericControl tr = new HtmlGenericControl("tr");
                HtmlGenericControl td = new HtmlGenericControl("td");
                td.InnerText = thisRight.name.ToString(); // Rights.name
                tr.Controls.Add(td);

                int i = 1;
                List<role> Roles = db.roles.ToList();

                foreach (role Role in Roles)
                {
                    HtmlGenericControl td_row = new HtmlGenericControl("td");

                    HiddenField hf_rights_id = new HiddenField();
                    hf_rights_id.ID = "rights" + i;
                    hf_rights_id.ClientIDMode = ClientIDMode.Static;
                    hf_rights_id.Value = thisRight.id.ToString();
                    td_row.Controls.Add(hf_rights_id);

                    HiddenField hf_role_id = new HiddenField();
                    hf_role_id.ID = "role" + i;
                    hf_role_id.ClientIDMode = ClientIDMode.Static;
                    hf_role_id.Value = Role.id.ToString();
                    td_row.Controls.Add(hf_role_id);

                    CheckBox cb = new CheckBox();
                    cb.ID = "checkbox" + i;
                    cb.ClientIDMode = ClientIDMode.Static;
                    cb.Checked = IsChecked(thisRight.id, Role.id);
                    td_row.Controls.Add(cb);
                    tr.Controls.Add(td_row);
                    i++;
                }

                e.Item.Controls.Add(tr);

                /* Uden LinQ
                DataRowView eRow = (DataRowView)e.Item.DataItem;
                
                HtmlGenericControl tr = new HtmlGenericControl("tr");
                HtmlGenericControl td = new HtmlGenericControl("td");
                td.InnerText = eRow["rights_name"].ToString();
                tr.Controls.Add(td);

                int i = 1;
                DataTable dt = MyGame.SqlConn.Select("SELECT role_id FROM roles");

                foreach (DataRow row in dt.Rows)
                {
                    HtmlGenericControl td_row = new HtmlGenericControl("td");

                    HiddenField hf_rights_id = new HiddenField();
                    hf_rights_id.ID = "rights" + i;
                    hf_rights_id.ClientIDMode = ClientIDMode.Static;
                    hf_rights_id.Value = eRow["rights_id"].ToString();
                    td_row.Controls.Add(hf_rights_id);

                    HiddenField hf_role_id = new HiddenField();
                    hf_role_id.ID = "role" + i;
                    hf_role_id.ClientIDMode = ClientIDMode.Static;
                    hf_role_id.Value = row["role_id"].ToString();
                    td_row.Controls.Add(hf_role_id);

                    CheckBox cb = new CheckBox();
                    cb.ID = "checkbox" + i;
                    cb.ClientIDMode = ClientIDMode.Static;
                    cb.Checked = IsChecked(eRow["rights_id"], row["role_id"]);
                    td_row.Controls.Add(cb);
                    tr.Controls.Add(td_row);
                    i++;
                }

                e.Item.Controls.Add(tr); */
            }
        }
        private static bool IsChecked(object rights_id, object role_id)
        {
            DataClassesDataContext db = new DataClassesDataContext();

            role_right RR = (from rr in db.role_rights
                             where rr.role_id.Equals(role_id) && rr.rights_id.Equals(rights_id)
                             select rr).FirstOrDefault();
            if (RR != null)
            {
                return true;
            }

            return false;
        }

        protected void LinkButton_Role_Rights_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item_Rettigheder in Repeater_Role_Rights_Rights.Items)
            {
                int Count = db.roles.Count();

                for (int i = 1; i <= Count; i++)
                {
                    bool IsChecked = ((CheckBox)item_Rettigheder.FindControl("checkbox" + i)).Checked;
                    int hiddenRole = Convert.ToInt32(((HiddenField)item_Rettigheder.FindControl("role" + i)).Value);
                    int hiddenRight = Convert.ToInt32(((HiddenField)item_Rettigheder.FindControl("rights" + i)).Value);
                    role_right RR = (from rr in db.role_rights
                                     where rr.rights_id.Equals(hiddenRight) && rr.role_id.Equals(hiddenRole)
                                     select rr).FirstOrDefault();

                    if (RR != null)
                    {
                        if (!IsChecked)
                        {
                            db.role_rights.DeleteOnSubmit(RR);
                            db.SubmitChanges();
                        }
                    }
                    else
                    {
                        if (IsChecked)
                        {
                            role_right NewRR = new role_right();
                            NewRR.rights_id = hiddenRight;
                            NewRR.role_id = hiddenRole;
                            db.role_rights.InsertOnSubmit(NewRR);
                            db.SubmitChanges();
                        }
                    }                 
                }
            }

            MsgHandler.InsertMsg(1, "Role rights saved");
            Response.Redirect(this.PageUrl + "?action=role_rights");
        }
    }
}