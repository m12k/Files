using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BTSS.Logic.Projects
{
    public class Module: Interfaces.Projects.IModule 
    {

        #region "Declarations"

        private BTSS.Data.Projects.Module module;

        #endregion

        #region "Constructor"

        public Module(string connectionString, BTSS.Common.Core.DataProvider provider)
        {
            module = new BTSS.Data.Projects.Module(connectionString, provider);
        }

        #endregion 

        #region "Properties"

        private string _Id;
        public string Id { get { return _Id; } set { _Id = value; } }

        private string _Name;
        public string Name { get { return _Name; } set { _Name = value; } }

        private string _Desc;
        public string Desc { get { return _Desc; } set { _Desc = value; } }

        private DataTable _Table;
        public DataTable Table { get; set; }

        private BTSS.Common.Core.Operation _Operation;
        public BTSS.Common.Core.Operation Operation { get { return _Operation; } set { _Operation = value; } }
        
        #endregion 
  
        #region "Public Methods"

        public void Save()
        {
            try
            {
                module.Save(this);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetProjectModuleList()
        {
            try
            {
                return module.GetProjectModuleList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataRow GetProjectModuleDetails(string id)
        {
            try
            {
                return module.GetProjectModuleDetails(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
                
        public bool IsModuleAllowToDelete(string id)
        {
            try
            {
                if (module.IsModuleAllowToDelete(id))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetModuleTableMap(string id)
        {
            try
            {
                return module.GetModuleTableMap(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
