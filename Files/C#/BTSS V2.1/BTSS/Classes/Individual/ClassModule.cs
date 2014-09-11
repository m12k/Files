using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Biztech.Classes
{
    class ClassModule :Datasource
    {

        #region "Declarations"
        private Global.DataProvider _provider;
        #endregion

        #region "Constructor"

        public ClassModule(string connectionString, Global.DataProvider provider)
            : base(connectionString, provider) { _provider = provider; }

        #endregion 

        #region "Properties"

        private string _Id;
        public string Id { get { return _Id; } set { _Id = value; } }

        private string _Name;
        public string Name { get { return _Name; } set { _Name = value; } }

        private string _Desc;
        public string Desc { get { return _Desc; } set { _Desc = value; } }

        private Global.Operation _Operation;
        public Global.Operation Operation { get { return _Operation; } set { _Operation = value; } }
        
        #endregion 

        #region "Private Methods"

        private string SaveSQL()
        {
            string sql = "";

            switch(_Operation)
            {
                case Global.Operation.INSERT:
                    _Id = GetNextCode("set_module", "mod_id", "001");
                    sql = "INSERT INTO set_module (mod_id,mod_name,mod_desc)VALUES('" + _Id + "','" + _Name + "','" + _Desc + "')";
                    break;
                case Global.Operation.UPDATE:
                    sql = "UPDATE set_module SET mod_name = '" + _Name + "', mod_desc = '" + _Desc + "' WHERE mod_id = '" + _Id + "'";
                    break;
                case Global.Operation.DELETE:
                    sql = "DELETE FROM set_module WHERE mod_id = '" + _Id + "'";
                    break;
            }
           
            return sql;
        }

        private string GetProjectModuleListSQL()
        {
            string sql;
            sql = "SELECT mod_id, mod_name, mod_desc FROM set_module";
            return sql;
        }

        private string GetProjectModuleDetailsSQL(string id)
        {
            string sql;
            sql = "SELECT mod_name, mod_desc FROM set_module WHERE mod_id = '" + id + "'";
            return sql;
        }

        private string IsModuleAllowToDeleteSQL(string id)
        {
            string sql;
            sql = "SELECT COUNT(mod_id) AS [count] FROM set_group_access WHERE mod_id = '" + id + "'";
            return sql;
        }

        #endregion

        #region "Public Methods"

        public void Save()
        {
            try
            { 
                ExecuteScalar(SaveSQL());  
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
                return ExecuteQuery(GetProjectModuleListSQL()).Tables[0];
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
                return ExecuteQuery(GetProjectModuleDetailsSQL(id)).Tables[0].Rows[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public bool IsAlreadyExist(string tableName, string fieldName, string originalvalue, string currentValue, Global.Operation operation)
        //{
        //    try
        //    {
        //        return IsValueAlreadyExist(tableName, fieldName, originalvalue, currentValue, operation);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public bool IsModuleAllowToDelete(string id)
        {
            try
            {
                if ((int)(ExecuteQuery(IsModuleAllowToDeleteSQL(id)).Tables[0].Rows[0].Field<int>("count")) == 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
