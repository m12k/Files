using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;

namespace BTSS.Presentation
{
    public partial class FormActiveDirectoryResultsView : Form
    {

        #region Constructor

        public FormActiveDirectoryResultsView()
        {
            InitializeComponent();
        }

        #endregion

        #region Declarations

        IEnumerable<Logic.DomainUser> users;
        IEnumerable<Logic.DomainGroup> groups; 
        private DataTable dtUser; 

        public enum SearchTypes
        {
            Group,
            User
        }

        public enum TransactionTypes
        { 
            MemberOfGroup,
            GroupsOfUser,
            SearchGroupOrUser        
        }

        #endregion

        #region Properties

        public string Caption
        { 
            set {
                labelCaption.Text = value;
            }
        }

        private string _Domain;
        public string Domain { set { _Domain = value; } }
        
        private string _Name;
        public new string Name { set { _Name = value; } }

        private SearchTypes _SearchType;
        public SearchTypes SearchType { set {_SearchType = value;} }

        private TransactionTypes _TransactionType;
        public TransactionTypes TransactionType { set { _TransactionType = value; } }

        #endregion

        #region EventHandlers 

        private void FormActiveDirectoryResultsView_Load(object sender, EventArgs e)
        {
            try
            {
                Initialize(); 
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void FormActiveDirectoryResultsView_Shown(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void userControlListingData_OnTextSearchChanged(string textSearch)
        {
            try
            { 

                switch (_TransactionType)
                {
                    case TransactionTypes.MemberOfGroup:
                        var result1 = from s in users
                                      where s.DisplayName.ToLower().Contains(textSearch.ToLower()) || s.UserName.ToLower().Contains(textSearch.ToLower())
                                      select s;
                        
                        userControlListingData.DataSource = result1; 
                        break;
                    case TransactionTypes.GroupsOfUser: 
                        if (groups == null) break;
                        var result2 = from s in groups 
                                      where s.Name.ToLower().Contains(textSearch.ToLower())
                                      select s;
                         
                        userControlListingData.DataSource = result2; 
                        break;
                    case TransactionTypes.SearchGroupOrUser: 
                        if (_SearchType == SearchTypes.User)
                        {
                            if (users.Count() == 0) break;
                            var result3 = from s in users
                                      where s.DisplayName.ToLower().Contains(textSearch.ToLower()) || s.UserName.ToLower().Contains(textSearch.ToLower())
                                      select s;

                            userControlListingData.DataSource = result3;
                        }
                        else//Group
                        {

                            if (groups == null) break;
                            var result4 = from s in groups 
                                          where s.Name.ToLower().Contains(textSearch.ToLower())
                                          select s;
                             
                            userControlListingData.DataSource = result4; 
                        }
                        break;
                } 

            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void userControlListingData_OnPrint(object sender, EventArgs e)
        {
            try
            { 
                switch (_TransactionType)
                {
                    case TransactionTypes.MemberOfGroup:                      
                        using (var reportViewer = new Common.ReportViewer())
                        {
                            reportViewer.ReportTitle = "Member of Group: " + _Domain;
                            reportViewer.XMLName = "MemberOfGroup";
                            reportViewer.ReportSource = (new Reports.ReportMemberOfGroup());
                            ////Uncomment this code for purpose to write XML schema
                            //reportViewer.SetDatasource(dtUser);    
                            reportViewer.SetDatasource(users.ToList());
                            reportViewer.ShowReport(); 

                             
                            //List<Reports.GroupOfUsers> header = new List<BTSS.Presentation.Reports.GroupOfUsers>();
                            //List<Reports.GroupOfUsersDetails> details = new List<BTSS.Presentation.Reports.GroupOfUsersDetails>();

                            //header.Add(new BTSS.Presentation.Reports.GroupOfUsers() { DisplayName = "mike", Domain= "watat", UserName = "cajandm" });
                            //details.Add(new BTSS.Presentation.Reports.GroupOfUsersDetails() { ComputerName="wagrh", Email = "sdff@adf.c", UserName="cajandm" });

                            //List<Object> listGroupOfUsers = new List<object>();
                            //listGroupOfUsers.Add(header);
                            //listGroupOfUsers.Add(details);



                            //reportViewer.ReportTitle = "Member of Group: " + _Domain;
                            //reportViewer.XMLName = "MemberOfGroup";
                            //reportViewer.ReportSource = (new Reports.ReportMemberOfGroup());
                            //reportViewer.SetDatasource(listGroupOfUsers);

                            //reportViewer.ShowReport(); 
                        }
                         
                        break;
                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
         

        #endregion

        #region Methods

        private void Initialize()
        {
            try
            {
                InitializeGrid();
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

                if (_SearchType == SearchTypes.Group)
                {
                    DataTable dtGroup = new DataTable();
                    dtGroup.Columns.Add("Name");
                    dtGroup.Columns.Add("Domain");
                    userControlListingData.DataSource = dtGroup;
                }
                else  //User
                {
                    dtUser = new DataTable("Users");
                    dtUser.Columns.Add("DisplayName");
                    dtUser.Columns.Add("UserName");
                    dtUser.Columns.Add("Domain"); 
                    userControlListingData.DataSource = dtUser;  
                }
                  
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
                foreach (DataGridViewColumn col in userControlListingData.dataGridViewList.Columns)
                {
                    switch (col.HeaderText)
                    {
                        case "Name": col.HeaderText = "Name"; break;
                        case "Domain": col.HeaderText = "Domain"; break;
                        case "DisplayName": col.HeaderText = "Display Name"; break;
                        case "UserName": col.HeaderText = "UserName"; break;  
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

        private void LoadData()
        {
            try
            {

                PrincipalContext context = new PrincipalContext(ContextType.Domain, _Domain);
                                
                switch (_TransactionType)
                { 
                    case TransactionTypes.MemberOfGroup: 
                        
                        using (var loader = new Common.Loader<string>(o =>
                        {
                            GroupPrincipal gp = GroupPrincipal.FindByIdentity(context, IdentityType.Name, _Name);

                            users = (from u in gp.Members
                                     select new Logic.DomainUser { DisplayName = u.DisplayName == null ? "":u.DisplayName, UserName = u.SamAccountName, Domain = u.Context.Name }).OrderBy(a => a.DisplayName).ToList();
                        }))
                        {
                            loader.RunWorker();
                        };
  
                        userControlListingData.DataSource = users; 
                        break;
                    case TransactionTypes.GroupsOfUser: 

                        UserPrincipal up = UserPrincipal.FindByIdentity(context, _Name);

                        if (up == null) break;  

                       
                        using (var loader = new Common.Loader<string>(o =>
                        {
                            PrincipalSearchResult<Principal> principalSearchResult = up.GetGroups();

                            groups = (from u in principalSearchResult.ToList()
                                      select new Logic.DomainGroup { Name = u.Name, Domain = u.Context.Name }).OrderBy(a => a.Name);                        
                        }))
                        {
                            loader.RunWorker();
                        };

                        userControlListingData.DataSource = groups;
                        break;                         
                    case TransactionTypes.SearchGroupOrUser:  
                        if (_SearchType == SearchTypes.User)
                        {
                         
                            using (var loader = new Common.Loader<string>(o =>
                            {
                                UserPrincipal userPrincipal = new UserPrincipal(context);
                                StringBuilder sbUser = new StringBuilder();
                                sbUser.Append("*").Append(_Name).Append("*");
                                userPrincipal.DisplayName = sbUser.ToString(); 

                                PrincipalSearcher psUser = new PrincipalSearcher(userPrincipal);
                                  
                                users = (from u in psUser.FindAll().ToList()
                                         select new Logic.DomainUser { DisplayName = u.DisplayName == null ? "" : u.DisplayName, UserName = u.SamAccountName, Domain = u.Context.Name }).OrderBy(a => a.DisplayName).ToList();
                                  
                            }))
                            {
                                loader.RunWorker();  
                            };
                            userControlListingData.DataSource = users;

                        }
                        else //Group
                        { 
                            using (var loader = new Common.Loader<string>(o =>
                            {
                                GroupPrincipal groupPrincipal = new GroupPrincipal(context);
                                StringBuilder sbGroup = new StringBuilder();
                                sbGroup.Append("*").Append(_Name).Append("*");
                                groupPrincipal.Name = sbGroup.ToString();

                                PrincipalSearcher psGroup = new PrincipalSearcher(groupPrincipal);


                                groups = (from u in psGroup.FindAll().ToList()
                                      select new Logic.DomainGroup { Name = u.Name, Domain = u.Context.Name }).OrderBy(a => a.Name);
                            }))
                            {
                                loader.RunWorker();                            
                            };
                            
                            userControlListingData.DataSource = groups; 
                        } 
                        break;
                }
                  
            }
            catch (Exception)
            {

                throw;
            }
        }

        ////invoke a delegate method.
        //delegate void dLoadData(IEnumerable<Logic.DomainUser> data);

        //private void LoadData(IEnumerable<Logic.DomainUser> data)
        //{
        //    try
        //    {
        //        if (userControlListingData.InvokeRequired)
        //        {
        //            dLoadData d = new dLoadData(LoadData);
        //            Invoke(d);
        //        }
        //        else
        //        {
        //            userControlListingData.DataSource = data;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        #endregion
        
    }
}
