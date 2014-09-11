using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BTSS.Data.Projects
{
    public class Group
    {

        #region "Declarations"

        private Datasource datasource;

        #endregion

        #region "Constructor"
        
        public Group(string connectionString, BTSS.Common.Core.DataProvider provider) 
        {
            datasource = new Datasource(connectionString,provider); 
        }

        #endregion

        #region "Private Methods"

        private List<string> SaveSQL(Logic.Projects.Group group)
        {
            List<string> sql = new List<string>();

            switch (group.Operation)
            {
                case BTSS.Common.Core.Operation.INSERT:
                    group.Id = datasource.GetNextCode("set_group", "grp_id", "001");
                    sql.Add("INSERT INTO set_group(grp_id, grp_name,grp_desc)VALUES(" +
                        "'" + group.Id + "'," +
                        "'" + group.Name + "'," +
                        "'" + group.Desc + "'" +
                        ")");
                    break;
                case BTSS.Common.Core.Operation.UPDATE:
                    sql.Add("UPDATE set_group " +
                        "SET " +
                        "grp_name = '" + group.Name + "', " +
                        "grp_desc = '" + group.Desc + "' " +
                        "WHERE grp_id = '" + group.Id + "'");
                    break;
                case BTSS.Common.Core.Operation.DELETE:
                    sql.Add("DELETE FROM set_group WHERE grp_id = '" + group.Id + "'");
                    sql.Add("DELETE FROM set_group_access WHERE grp_id = '" + group.Id + "'");
                    break;
            }


            if (group.Operation != BTSS.Common.Core.Operation.DELETE)
            {
                foreach (DataRow row in group.Data.Select("", "", DataViewRowState.ModifiedOriginal))
                {
                    if (IsModuleIdExist(row["mod_id"].ToString(), group.Id))
                    {
                        if (
                            ((row.IsNull("can_view")) ? 0 : (int)row["can_view"]) == 0 &&
                            ((row.IsNull("can_add")) ? 0 : (int)row["can_add"]) == 0 &&
                            ((row.IsNull("can_edit")) ? 0 : (int)row["can_edit"]) == 0 &&
                            ((row.IsNull("can_delete")) ? 0 : (int)row["can_delete"]) == 0
                            )
                        {
                            sql.Add("DELETE FROM set_group_access WHERE " +
                                    "mod_id = '" + row["mod_id"] + "' " +
                                    "AND grp_id = '" + group.Id + "'");
                        }
                        else
                        {
                            sql.Add("UPDATE set_group_access " +
                                "SET " +
                                "can_view = '" + ((row.IsNull("can_view")) ? 0 : row["can_view"]) + "', " +
                                "can_add = '" + ((row.IsNull("can_add")) ? 0 : row["can_add"]) + "', " +
                                "can_edit = '" + ((row.IsNull("can_edit")) ? 0 : row["can_edit"]) + "', " +
                                "can_delete = '" + ((row.IsNull("can_delete")) ? 0 : row["can_delete"]) + "' " +
                                "WHERE " +
                                "mod_id = '" + row["mod_id"] + "' " +
                                "AND grp_id = '" + group.Id + "'");
                        }

                    }
                    else
                    {
                        sql.Add("INSERT INTO set_group_access(grp_id,mod_id,can_view,can_add,can_edit,can_delete)" +
                            "VALUES(" +
                            "'" + group.Id + "'," +
                            "'" + row["mod_id"] + "'," +
                            "'" + row["can_view"] + "'," +
                            "'" + row["can_add"] + "'," +
                            "'" + row["can_edit"] + "'," +
                            "'" + row["can_delete"] + "'" +
                            ")");
                    }
                }
            }

            return sql;
        }

        private string GetGroupListSQL()
        {
            string sql;
            sql = "SELECT grp_id, grp_name, grp_desc FROM set_group";
            return sql;
        }

        private string GetGroupDetailsSQL(string id)
        {
            string sql;
            sql = "SELECT grp_name,grp_desc FROM set_group WHERE grp_id = '" + id + "'";
            return sql;
        }

        private string GetModuleAccessSQL()
        {
            string sql;
            sql = "SELECT mod_id, mod_name, mod_desc, 0 as can_view, 0 as can_add, 0 as can_edit, 0 as can_delete FROM set_module";
            return sql;
        }

        private string GetGroupModuleSQL(string id)
        {
            string sql;
            sql = "SELECT acc.mod_id, acc.grp_id, acc.can_view, acc.can_add, acc.can_edit, acc.can_delete " +
                "FROM set_group_access acc WHERE acc.grp_id = '" + id + "'";
            return sql;
        }

        private bool IsModuleIdExist(string modId, string groupId)
        {
            try
            {
                string sql;
                sql = "SELECT TOP 1 mod_id FROM set_group_access WHERE mod_id = '" + modId + "' AND grp_id = '" + groupId + "'";
                if (datasource.ExecuteQuery(sql).Tables[0].Rows.Count > 0) return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string IsGroupAllowToDeleteSQL(string id)
        {
            string sql;
            sql = "SELECT COUNT(grp_id) AS [count] FROM set_user_access WHERE grp_id = '" + id + "'";
            return sql;
        }

        #endregion

        #region "Public Methods"

        public void Save(Logic.Projects.Group group)
        {
            try
            {
                datasource.ExecuteScalar(SaveSQL(group));
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
                return datasource.ExecuteQuery(GetGroupListSQL()).Tables[0];
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
                return datasource.ExecuteQuery(GetGroupDetailsSQL(id)).Tables[0].Rows[0];
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
                return datasource.ExecuteQuery(GetModuleAccessSQL()).Tables[0];
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
                return datasource.ExecuteQuery(GetGroupModuleSQL(id)).Tables[0];
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
                if ((int)(datasource.ExecuteQuery(IsGroupAllowToDeleteSQL(id)).Tables[0].Rows[0].Field<int>("count")) == 0)
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
