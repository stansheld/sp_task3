using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using UlsLogger;

namespace sp_tba_v2.VisualWebPart1
{
    public partial class VisualWebPart1UserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CreateBoundFields();
                BindData();
;            }
        }

        private void CreateBoundFields()
        {
            BoundField bf = new BoundField();
            bf.DataField = "ID";
            bf.HeaderText = "ID";
            bf.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            bf.ItemStyle.Width = Unit.Pixel(50);
            GridView1.Columns.Add(bf);

            bf = new BoundField();
            bf.DataField = "Title";
            bf.HeaderText = "News";
            bf.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
            bf.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            bf.ItemStyle.Width = Unit.Pixel(200);
            GridView1.Columns.Add(bf);

            bf = new BoundField();
            bf.DataField = "Description";
            bf.HeaderText = "Description";
            bf.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
            bf.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            bf.ItemStyle.Width = Unit.Pixel(250);
            GridView1.Columns.Add(bf);

            bf = new BoundField();
            bf.DataField = "Date Publishing";
            bf.HeaderText = "Date Publishing";
            bf.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
            bf.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            bf.ItemStyle.Width = Unit.Pixel(150);
            GridView1.Columns.Add(bf);

            bf = new BoundField();
            bf.DataField = "Assigned Person";
            bf.HeaderText = "Assigned Person";
            bf.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
            bf.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            bf.ItemStyle.Width = Unit.Pixel(200);
            GridView1.Columns.Add(bf);
        }

        private void BindData()
        {
            try
            {
                SPWeb thisWeb = SPContext.Current.Web;

                SPQuery query = new SPQuery();
                query.Query = "<Where><And><And>"
                            + "<Eq><FieldRef Name='_IsVisible'/>"
                            + "<Value Type='Boolean'>1</Value></Eq>"
                            + "<Leq><FieldRef Name='_DatePublishing'/>"
                            + "<Value IncludeTimeValue='True' Type='DateTime'><Today s/></Value></Leq></And>"
                            + "<Eq><FieldRef Name='_AssignedPerson'/>"
                            + "<Value Type='Integer'><UserID Type='Integer'/></Value></Eq>"
                            + "</And></Where>";

                SPList myList = thisWeb.Lists["News"];
                SPListItemCollection queryResults = myList.GetItems(query);

                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Title", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Date Publishing", typeof(string));
                dt.Columns.Add("Assigned Person", typeof(string));

                //if (queryResults != null)
                if (queryResults.Count != 0)
                {
                    EmptyGridViewLabel.Visible = false;
                    foreach (SPListItem item in queryResults)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ID"] = item["ID"];
                        dr["Title"] = item["Title"] != null ? item["Title"].ToString() : "";
                        dr["Description"] = item["_Description"] != null ? item["_Description"].ToString() : "";
                        dr["Date Publishing"] = item["_DatePublishing"] != null ? item["_DatePublishing"].ToString() : "";
                        dr["Assigned Person"] = item["_AssignedPerson"] != null ? item["_AssignedPerson"].ToString() : "";
                        dt.Rows.Add(dr);
                    }
                } else
                {
                    EmptyGridViewLabel.Visible = true;
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (InvalidCastException ex)
            {
                UlsLogging.LogError("бла-бла. Message: {0}, StackTrace: {1}", ex.Message, ex.StackTrace);
            }
        }

        protected void AddNewsBtn_Click(object sender, EventArgs e)
        {
            SPWeb thisWeb = SPContext.Current.Web;
            SPList myList = thisWeb.Lists["News"];

            SPListItem item = myList.AddItem();
            item["Title"] = NewsTitleTextBox.Text;
            item["_Description"] = DescriptionTextBox.Text;
            item["_IsVisible"] = IsVisibleCheckBox.Checked;

            try
            {
                item["_DatePublishing"] = Convert.ToDateTime(DatePublishingDateTimeControl.SelectedDate.ToString());
            } catch
            {
                item["_DatePublishing"] = "";
            }

            if (AssignedPersonPeoplePicker.ResolvedEntities.Count > 0)
            {
                PickerEntity picker = (PickerEntity)AssignedPersonPeoplePicker.ResolvedEntities[0];
                SPUser userInstance = thisWeb.EnsureUser(picker.Key);
                item["_AssignedPerson"] = userInstance;
            } else
            {
                item["_AssignedPerson"] = "";
            }
            
            item.Update();
            NewsTitleTextBox.Text = "";
            DescriptionTextBox.Text = "";
            DatePublishingDateTimeControl.ClearSelection();
            IsVisibleCheckBox.Checked = false;
            AssignedPersonPeoplePicker.AllEntities.Clear();

            BindData();
        }

        protected void OpenPopupBtn_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
        }
    }
}
