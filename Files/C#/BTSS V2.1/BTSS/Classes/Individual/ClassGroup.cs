using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Biztech.Classes 
{
    class ClassGroup:Datasource 
    {

        #region "Declarations"
        private Global.DataProvider _provider;
        #endregion

        #region "Constructor"
        public ClassGroup(string connectionString, Global.DataProvider provider) : base(connectionString, provider) { _provider = provider; }
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

        private Global.Operation _Operation;
        public Global.Operation Operation { get { return _Operation; } set { _Operation = value; } }
        
        #endregion

        #region "Private Methods"

        private List<string> SaveSQL()
        {
            List<string> sql = new List<string>();  
            
            switch (_Operation)
            {
                case Global.Operation.INSERT: 
                    _Id = GetNextCode("set_group", "grp_id", "001");
                    sql.Add("INSERT INTO set_group(grp_id, grp_name,grp_desc)VALUES(" +
                        "'"+ _Id  +"'," +
                        "'"+ _Name +"'," +
                        "'" + _Desc + "'" +
                        ")");
                    break;
                case Global.Operation.UPDATE:
                    sql.Add("UPDATE set_group " +
                        "SET " +
                        "grp_name = '" + _Name + "', " +
                        "grp_desc = '" + _Desc + "' " +
                        "WHERE grp_id = '"+ _Id +"'");
                    break;
                case Global.Operation.DELETE: 
                    sql.Add("DELETE FROM set_group WHERE grp_id = '" + _Id + "'");
                    sql.Add("DELETE FROM set_group_access WHERE grp_id = '" + _Id + "'");
                    break;
            }


            if (Operation != Global.Operation.DELETE)
            {  
                foreach (DataRow row in _Data.Select("","", DataViewRowState.ModifiedOriginal))
                {
                    if (IsModuleIdExist(row["mod_id"].ToString()))
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
                                    "AND grp_id = '" + _Id + "'"); 
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
                                "AND grp_id = '" + _Id + "'"); 
                        }
 
                    }
                    else
                    {
                        sql.Add("INSERT INTO set_group_access(grp_id,mod_id,can_view,can_add,can_edit,can_delete)" +
                            "VALUES(" +
                            "'" + _Id + "'," +
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
                "FROM set_group_access acc WHERE acc.grp_id = '"+ id +"'";
            return sql;
        }

        private bool IsModuleIdExist(string modId)
        {
            try
            {
                string sql;
                sql = "SELECT TOP 1 mod_id FROM set_group_access WHERE mod_id = '"+ modId +"' AND grp_id = '"+ _Id +"'";
                if (ExecuteQuery(sql).Tables[0].Rows.Count > 0) return true;
                return false ;                 
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

        public DataTable GetGroupList()
        {
            try
            {
                return ExecuteQuery(GetGroupListSQL()).Tables[0];
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
                return ExecuteQuery(GetGroupDetailsSQL(id)).Tables[0].Rows[0];
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
                return ExecuteQuery(GetModuleAccessSQL()).Tables[0];
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
                return ExecuteQuery(GetGroupModuleSQL(id)).Tables[0];
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
                if ((int)(ExecuteQuery(IsGroupAllowToDeleteSQL(id)).Tables[0].Rows[0].Field<int>("count")) == 0)
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
