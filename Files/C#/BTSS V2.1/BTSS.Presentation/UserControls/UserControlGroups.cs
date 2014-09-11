using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BTSS.Presentation.UserControls
{
    public partial class UserControlGroups : UserControl
    {

#region "Constructor"
    public UserControlGroups()
    {
        InitializeComponent();
    }
#endregion

#region "Declarations" 
    private Logic.Group group;
    private List<Logic.GetGroupListResult> data; 
    private string id = "";
    private string groupName;
    private Common.Validation Validation;
         
    public delegate void OnCloseControlEvent(TabPage tabPage);
    public event OnCloseControlEvent OnCloseControl;

    protected void OnClosed(TabPage tabPage)
    {
        if (OnCloseControl != null)
        {
            OnCloseControl(tabPage);
        }

    }
    private TabPage _tabPage = null;

    public TabPage Tab
    {
        get { return _tabPage; }
        set { _tabPage = value; }
    }

#endregion

#region "EventHandlers"

    private void UserControlGroups_Load(object sender, EventArgs e)
    {
        try
        {
            Initialize();
            InitializeGrid(); 
            RefreshListing();
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void userControlButtonsMain_OnButtonClick(EventArgs e, ref bool isOk, Common.UserControls.UserControlButtonsMain.ButtonClick button, Bitmap icon)
    {
        try
        {
            switch (button)
            {
                case BTSS.Common.UserControls.UserControlButtonsMain.ButtonClick.New: isOk = New(icon); break;
                case BTSS.Common.UserControls.UserControlButtonsMain.ButtonClick.Edit: isOk = Edit(icon); break;
                case BTSS.Common.UserControls.UserControlButtonsMain.ButtonClick.View: isOk = View(icon); break;
                case BTSS.Common.UserControls.UserControlButtonsMain.ButtonClick.Delete: isOk = Delete(); break;
                case BTSS.Common.UserControls.UserControlButtonsMain.ButtonClick.Refresh: RefreshListing(); break;
                case BTSS.Common.UserControls.UserControlButtonsMain.ButtonClick.Close: OnCloseControl(_tabPage); break;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
          
    private void userControlListing_OnSelectionChanged(DataGridViewRow row)
    {
        try
        { 
            id = row.Cells["Id"].Value.ToString();
            groupName = row.Cells["Name"].Value.ToString();
            userControlButtonsMain.ButtonsMode = BTSS.Common.UserControls.UserControlButtonsMain.Mode.Select; 
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void userControlListing_OnTextSearchChanged(string textSearch)
    {
        try
        {
            var result = from s in data
                         where s.Name.ToLower().Contains(textSearch.ToLower()) || s.Desc.ToLower().Contains(textSearch.ToLower())
                         select s;

            userControlListing.DataSource = result.ToList(); 
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void userControlListing_OnRefresh(object sender, EventArgs e)
    {
        try
        { 
            RefreshListing();
        }
        catch (Exception)
        {
            throw;
        }
    }
         
    private void userControlListing_OnRowDoubleClicked(DataGridViewRow row)
    {
        try
        { 
            View(Common.Properties.Resources.View);        
        }
        catch (Exception)
        {
            throw;
        }
    }

#endregion

#region "Methods"

    private bool New(Bitmap icon)
    {
        try
        {
            using (var frm = new BTSS.Presentation.FormGroup())
            {
                frm.Operation = Common.Core.Operation.INSERT;
                frm.Icon = Icon.FromHandle(icon.GetHicon());
                frm.Text = "Group[NEW]";
                frm.OnSave += new EventHandler(RefreshOnSave);
                if (frm.ShowDialog() == DialogResult.OK)
                    RefreshListing();
            }

            group.Operation = Common.Core.Operation.INSERT;
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private bool Edit(Bitmap icon)
    {
        try
        {
            using (var frm = new BTSS.Presentation.FormGroup())
            {
                frm.Operation = Common.Core.Operation.UPDATE;
                frm.Icon = Icon.FromHandle(icon.GetHicon());
                frm.Text = "Group[EDIT]";
                frm.GroupId = id;
                if (frm.ShowDialog() == DialogResult.OK)
                    RefreshListing();
            } 

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private bool View(Bitmap icon)
    {
        try
        {
            using (var frm = new BTSS.Presentation.FormGroup())
            {
                frm.Operation = Common.Core.Operation.VIEW;
                frm.Icon = Icon.FromHandle(icon.GetHicon());
                frm.Text = "Group[VIEW]";
                frm.GroupId = id;
                frm.ShowDialog();
            }
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private bool Delete()
    {
        try
        { 
            if (!group.IsGroupAllowToDelete(id))
            {
                Common.Core.ShowMessage("Group has reference user(s). Delete not allowed.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (Common.Core.ShowQuestion(groupName + " will be deleted. Do you want to continue?") == DialogResult.Cancel) { return false; }
             
            group.Id = id;
            group.Operation = Common.Core.Operation.DELETE;
            group.Save(); 
            Common.Core.ShowMessage("Group " + groupName + " successfully deleted!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshListing();

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }
         
    private void Initialize()
    {
        try
        {
            group = new BTSS.Logic.Group(Common.Core.ConnectionString);
            Validation = new BTSS.Common.Validation(); 
            Validation.Validate(this);
            SetControlListingAccessRights();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void InitializeGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            dt.Columns.Add("Desc");

            userControlListing.DataSource = dt; 

            InitializeGridLayout();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void InitializeGridLayout()
    {
        try
        {
            foreach (DataGridViewColumn col in userControlListing.dataGridViewList.Columns)
            {
                switch (col.HeaderText)
                {
                    case "Id": col.HeaderText = "User Id"; break;
                    case "Name": col.HeaderText = "Name"; break;
                    case "Desc": col.HeaderText = "Description"; break;                    
                    default:
                        col.Visible = false; break;
                }
            }
             
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void RefreshOnSave(object sender, EventArgs e)
    {
        try
        {
            RefreshListing();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void RefreshListing()
    {
        try
        {
            id = "";
            userControlButtonsMain.ButtonsMode = BTSS.Common.UserControls.UserControlButtonsMain.Mode.Select; 
              
            using (var loader = new Common.Loader<string>(o =>
            {
                data = group.GetGroupList();
            }))
            {
                loader.RunWorker();
            };

            userControlListing.DataSource = data;
                 
        }
        catch (Exception)
        {
            throw;
        }
    }
        
    private void SetControlListingAccessRights()
    {
        try
        {
            using (var user = new Logic.User(Common.Core.ConnectionString))
            {
                var result = user.GetAccessRights(Common.Core.UserId, BTSS.Common.Core.Module.BTSSGROUP);

                userControlListing.CanEdit = result.CanEdit.Value;
            } 
             
        }
        catch (Exception)
        {
            throw;
        }
    }
#endregion 
  
    }
}