using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;

namespace BTSS.Logic
{
    partial class User :IDisposable, Interfaces.IUser 
    {
        BTSSContext ctx;
        Data.User user;

        #region "Properties"
          
        private DataTable _DataUserProject;
        public DataTable DataUserProject { get { return _DataUserProject; } set { _DataUserProject = value; } }

        private DataTable _DataUserGroup;
        public DataTable DataUserGroup { get { return _DataUserGroup; } set { _DataUserGroup = value; } }

        private BTSS.Common.Core.Operation _Operation;
        public BTSS.Common.Core.Operation Operation { get { return _Operation; } set { _Operation = value; } }
       
        #endregion

        #region "Constructor"
        public User(string connectionString) {
            ctx = new BTSSContext(connectionString);
            user = new BTSS.Data.User(connectionString);
        }
        #endregion
          
        #region Methods
     
        public void Save()
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    BTSSContext ctx = new BTSSContext(BTSS.Common.Core.ConnectionString);
                                        
                    ctx.SaveUser(ref _Id, _LastName, _FirstName, _MiddleName, _UserName, BTSS.Common.Core.UserName, _Operation.ToString());
                                   
                    if (Operation != BTSS.Common.Core.Operation.DELETE)
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
                return user.GetUsersProjectList(UserId);
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
                return user.GetUserGroup(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsUserValid(string userName, string password, string authentication)
        {
            try
            {

                if (authentication == "Application")
                { 
                    Data.Authentication auth = new BTSS.Data.Authentication(BTSS.Common.Core.ConnectionString);

                    string encryptedPassword;
                    encryptedPassword = auth.GetPassword(userName);
                    
                    if (encryptedPassword == "") return false;

                    if (password == AESencryption.Decrypt(encryptedPassword)) return true;
                    
                    return false;
                }
                else
                {
                    return ctx.IsUserValid(userName).Value;
                }  
                
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

        public GetAccessRightsResult GetAccessRights(string userId, BTSS.Common.Core.Module module)
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
        
        #region IDisposable Members

        public void Dispose()
        {         
        }

        #endregion
         
    }
}
