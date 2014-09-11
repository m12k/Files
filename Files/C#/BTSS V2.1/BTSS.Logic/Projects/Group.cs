using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BTSS.Logic.Projects
{
    public class Group : Interfaces.Projects.IGroup 
    {

        #region "Declarations" 

        Data.Projects.Group group;

        #endregion

        #region "Constructor"

        public Group(string connectionString, BTSS.Common.Core.DataProvider provider) 
        {
            group = new BTSS.Data.Projects.Group(connectionString,provider);

        }

        #endregion

        #region "Properties"

        private string _Id;
        public string Id { get { return _Id; } set { _Id = value; } }

        private string _Name;
        public string Name { get { return _Name; } set { _Name = value; } }

        private string _Desc;
        public string Desc { get { return _Desc; } set { _Desc = value; } }

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
                group.Save(this);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetGroupList()
        {
            try
            {
                return group.GetGroupList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataRow GetGroupDetails(string id)
        {
            try
            {
                return group.GetGroupDetails(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetModuleAccess()
        {
            try
            {
                return group.GetModuleAccess();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetGroupModule(string id)
        {
            try
            {
                return group.GetGroupModule(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsGroupAllowToDelete(string id)
        {
            try
            {
                return group.IsGroupAllowToDelete(id); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
        
    }
}
