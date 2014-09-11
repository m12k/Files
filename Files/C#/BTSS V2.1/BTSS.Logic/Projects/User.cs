using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BTSS.Logic.Projects
{
    public class User: Interfaces.Projects.IUser 
    {
        
        #region "Declarations"

        Data.Projects.User user;

        #endregion 

        #region "Constructor"

        public User(string connectionString, BTSS.Common.Core.DataProvider provider) 
        {
            user = new BTSS.Data.Projects.User(connectionString, provider);
        }

        #endregion

        #region "Properties"
        
        private string _Id;
        public string Id { get { return _Id; } set { _Id = value; } }

        private string _LastName;
        public string LastName { get { return _LastName; } set { _LastName = value; } }

        private string _FirstName;
        public string FirstName { get { return _FirstName;}set{_FirstName = value;} }

        private string _MiddleName;
        public string MiddleName { get { return _MiddleName; } set { _MiddleName = value; } }

        private string _UserName;
        public string UserName { get { return _UserName; } set { _UserName = value; } }
          
        private DataTable _Data;
        public DataTable Data { get { return _Data; } set { _Data = value; } }

        private BTSS.Common.Core.Operation _Operation;
        public BTSS.Common.Core.Operation Operation { get { return _Operation; } set { _Operation = value; } }

        #endregion

        #region "Public Methods"

        public void Save()
        {
            try
            {
                user.Save(this);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetUserList()
        {
            try
            {
                return user.GetUserList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataRow GetUserDetails(string id)
        {
            try
            {
                return user.GetUserDetails(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsExist(string userName)
        {
            try
            {
                return user.IsExist(userName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetGroupAccess()
        {
            try
            {
                return user.GetGroupAccess();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetUserGroup(string id)
        {
            try
            {
                return user.GetUserGroup(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetUserGroupList()
        {
            try
            {
                return user.GetUserGroupList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
        
    }
}
