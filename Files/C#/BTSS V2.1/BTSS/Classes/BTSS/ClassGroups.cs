using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;

namespace Biztech.Classes 
{
    class ClassGroups:Datasource
    {
        
        BTSSContext ctx; 

#region "Constructor"
        
     public ClassGroups(string connectionString) : base(connectionString, Global.DataProvider.SQL) { 
         ctx = new BTSSContext(connectionString);  
     }

#endregion
        
#region "Properties"

     private string _Id;
     public string Id { get { return _Id; } set { _Id = value; } }

     private string _Name;
     public string Name { get { return _Name; } set { _Name = value; } }

     private string _Desc;
     public string Desc { get { return _Desc; } set { _Desc = value; } }

     private DataTable _DataGroupModule;
     public DataTable DataGroupModule { get { return _DataGroupModule; } set { _DataGroupModule = value; } }

     private Global.Operation _Operation;
     public Global.Operation Operation { get { return _Operation; } set { _Operation = value; } }
         
#endregion

#region "Private Method"

     private string GetGroupModuleSQL(string GroupId)
     {
         try
         {
             string sql;
             sql = "EXECUTE GetGroupModule '"+ GroupId +"'";
             return sql;
         }
         catch (Exception)
         {
             throw;
         }
     }

#endregion
        
#region "Public Region"
    public void Save()
    {
         try
         { 
             using (var transaction = new TransactionScope())
             {
                 ctx.SaveGroup(ref _Id, _Name, _Desc, Global.UserName, _Operation.ToString());

                 if (Operation != Global.Operation.DELETE)
                 {
                     if (_DataGroupModule.GetChanges() != null)
                     {
                         foreach (DataRow row in _DataGroupModule.GetChanges(DataRowState.Modified).Rows)
                         {
                             ctx.SaveGroupModule(row.Field<long?>("Id"), _Id, row.Field<int>("ModuleId"), row.Field<bool>("CanView"), row.Field<bool>("CanAdd"), row.Field<bool>("CanEdit"), row.Field<bool>("CanDelete"));
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

    public List<GetGroupListResult> GetGroupList()
    { 
         try 
        {
            return ctx.GetGroupList().ToList<GetGroupListResult>();
        }
        catch (Exception)
        {
	        throw;
        }
     }

    public GetGroupDetailsResult GetGroupDetails(string groupId)
    { 
        try 
	    {
            return ctx.GetGroupDetails(groupId).ToList<GetGroupDetailsResult>().FirstOrDefault();
	    }
	    catch (Exception)
	    { 
		throw;
	    }
    }
        
    public bool IsGroupAllowToDelete(string groupId)
    {
        try
        {
            return ctx.IsGroupAllowToDelete(groupId).Value;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public DataTable GetGroupModule(string GroupId)
    {
        try
        {
            return ExecuteQuery(GetGroupModuleSQL(GroupId)).Tables[0];
        }
        catch (Exception)
        {
        throw;
        }
    }


#endregion

    }
}
