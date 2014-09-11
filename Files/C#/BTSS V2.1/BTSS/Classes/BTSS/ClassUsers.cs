using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;

namespace Biztech.Classes 
{
    class ClassUsers:Datasource,IDisposable 

    {
        BTSSContext ctx;

        #region "Properties"

        private string _Id;
        public string Id { get { return _Id; } set { _Id = value; } }

        private string _LastName;
        public string LastName { get { return _LastName; } set { _LastName = value; } }

        private string _FirstName;
        public string FirstName { get { return _FirstName; } set { _FirstName = value; } }

        private string _MI;
        public string MI { get { return _MI; } set { _MI = value; } }

        private string _UserName;
        public string userName { get { return _UserName; } set { _UserName = value; } }

        private string _Password;
        public string Password { get { return _Password; } set { _Password = value; } }

        private DataTable _DataUserProject;
        public DataTable DataUserProject { get { return _DataUserProject; } set { _DataUserProject = value; } }

        private DataTable _DataUserGroup;
        public DataTable DataUserGroup { get { return _DataUserGroup; } set { _DataUserGroup = value; } }

        private Global.Operation _Operation;
        public Global.Operation Operation { get { return _Operation; } set { _Operation = value; } }
       
        #endregion

        #region "Constructor"
        public ClassUsers(string connectionString) : base(connectionString, Global.DataProvider.SQL) {
            ctx = new BTSSContext(Global.ConnectionString);
        }
        #endregion
        
        #region "Private Methods"
        private string GetUsersProjectListSQL(string UserId)
        { 
            string sql;
            sql = "EXEC GetUserProjectList '"+ UserId +"'";
            return sql;
        }

        private string GetUserGroupSQL(string UserId)
        {
            string sql;
            sql = "EXECUTE GetUserGroup '"+ UserId +"'";
            return sql;
        }
        #endregion

        #region "Public Methods"

        public void Dispose() { }

        public void Save()
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    BTSSContext ctx = new BTSSContext(Global.ConnectionString);

                    ctx.SaveUser(ref _Id, _LastName, _FirstName, _MI, _UserName, _Password, Global.UserName, _Operation.ToString());

                    if (Operation != Global.Operation.DELETE)
                    { 
                        if (_DataUserGroup.GetChanges() != null)
                        {
                            foreach (DataRow r in _DataUserGroup.GetChanges(DataRowState.Modified).Rows)
                            {
                                ctx.SaveUserGroup(r.Field<long?>("Id"), _Id, r.Field<string>("GroupId"));
                            }
                        } 
                        
                        if (_DataUserProject.GetChanges() != null)
                        { 
                            foreach(DataRow r in _DataUserProject.GetChanges(DataRowState.Modified).Rows)
                            {
                            ctx.SaveUserProject(r.Field<long?>("Id"), _Id, r.Field<string>("ProjId"));
                            }
                        }
                    }

                    transaction.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetUserListResult> GetUserList()
        {
            try
            {
                return ctx.GetUserList().ToList<GetUserListResult>();
            }
            catch (Exception)
            {
                throw;
            }
        }
               
        public GetUserDetailsResult GetUserDetails(string UserId)
        {
            try
            {
                return ctx.GetUserDetails(UserId).ToList<GetUserDetailsResult>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetUsersProjectList(string UserId)
        {
            try
            {
                return ExecuteQuery(GetUsersProjectListSQL(UserId)).Tables[0]; 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetUserProjectsResult> GetUserProjects(string UserId)
        {
            try
            {
                return ctx.GetUserProjects(UserId).ToList<GetUserProjectsResult>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public DataTable GetUserGroup(string UserId)
        {
            try
            {
                return ExecuteQuery(GetUserGroupSQL(UserId)).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsUserValid(string userName, string password)
        {
            try
            {
                return ctx.IsUserValid(userName, password).Value;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GetUserDetailsByUserNameResult GetUserDetailsByUserName(string userName)
        {
            try
            {
                return ctx.GetUserDetailsByUserName(userName).ToList<GetUserDetailsByUserNameResult>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public GetAccessRightsResult GetAccessRights(string userId, Global.Module module)
        {
            try
            {
                return ctx.GetAccessRights(userId, module.ToString()).ToList<GetAccessRightsResult>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        #endregion

    }
}
