using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BTSS.Data.Projects
{
    public class Module  
    {

        #region "Declarations"
        
        private Data.Datasource datasource;
        private BTSS.Common.Core.DataProvider provider;

        #endregion

        #region "Constructor"

        public Module(string connectionString, BTSS.Common.Core.DataProvider provider)
        {
            this.provider = provider;
            datasource = new BTSS.Data.Datasource(connectionString, provider);
        }

        #endregion 
         
        #region "Private Methods"

        private string SaveSQL(BTSS.Logic.Projects.Module module)
        {

            string sql = "";

            switch (module.Operation)
            {
                case BTSS.Common.Core.Operation.INSERT:
                    module.Id = datasource.GetNextCode("set_module", "mod_id", "001");
                    sql = "INSERT INTO set_module (mod_id,mod_name,mod_desc)VALUES('" + module.Id + "','" + module.Name + "','" + module.Desc + "') ";
                    break;
                case BTSS.Common.Core.Operation.UPDATE:
                    sql = "UPDATE set_module SET mod_name = '" + module.Name + "', mod_desc = '" + module.Desc + "' WHERE mod_id = '" + module.Id + "' ";
                    break;
                case BTSS.Common.Core.Operation.DELETE:
                    sql = "DELETE FROM set_module WHERE mod_id = '" + module.Id + "' ";
                    break;
            }

            if (provider == BTSS.Common.Core.DataProvider.SQL)
            { 
                if (module.Operation != BTSS.Common.Core.Operation.DELETE)
                {
                    if (module.Table.GetChanges() != null)
                    {
                        foreach (DataRow r in module.Table.GetChanges(DataRowState.Modified).Rows)
                        { 
                            sql = sql + "\n" +
                                "EXEC SaveModuleTableMap '" + r.Field<int?>("Id") + "','" + module.Id + "','" + r.Field<string>("table_name") + "'";
                        }
                    } 
                }
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

        private string GetModuleTableMapSQL(string id)
        {
            string sql;
            sql = "EXEC GetModuleTableMap '"+ id +"'";
            return sql;
        }

        #endregion

        #region "Public Methods"

        public void Save(BTSS.Logic.Projects.Module module)
        {
            try
            {
                datasource.ExecuteScalar(SaveSQL(module));
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
                return datasource.ExecuteQuery(GetProjectModuleListSQL()).Tables[0];
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
                return datasource.ExecuteQuery(GetProjectModuleDetailsSQL(id)).Tables[0].Rows[0];
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
                if ((int)(datasource.ExecuteQuery(IsModuleAllowToDeleteSQL(id)).Tables[0].Rows[0].Field<int>("count")) == 0)
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
                return datasource.ExecuteQuery(GetModuleTableMapSQL(id)).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
