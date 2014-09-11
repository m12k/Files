using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;

namespace BTSS.Logic
{
    partial class Group
    {
        
        BTSSContext ctx;
        Data.Group group;

#region "Constructor"
        
     public Group(string connectionString) { 
         ctx = new BTSSContext(connectionString);
         group = new BTSS.Data.Group(connectionString);
     }

#endregion
        
#region "Properties"
         
     private DataTable _DataGroupModule;
     public DataTable DataGroupModule { get { return _DataGroupModule; } set { _DataGroupModule = value; } }

     private BTSS.Common.Core.Operation _Operation;
     public BTSS.Common.Core.Operation Operation { get { return _Operation; } set { _Operation = value; } }
         
#endregion
                 
#region "Methods"
    public void Save()
    {
         try
         { 
             using (var transaction = new TransactionScope())
             {
                 ctx.SaveGroup(ref _Id, _Name, _Desc, BTSS.Common.Core.UserName, _Operation.ToString());

                 if (Operation != BTSS.Common.Core.Operation.DELETE)
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
            return group.GetGroupModule(GroupId);
        }
        catch (Exception)
        {
            throw;
        }
    }


#endregion

    }
}
