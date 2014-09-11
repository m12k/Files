using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTSS.Logic.Interfaces.Projects
{
    public interface IAudit
    { 
        System.Data.DataTable GetTablesList();
        System.Data.DataTable GetUsersList();
        System.Data.DataTable GetAuditListByTable(string tableName);
        System.Data.DataTable GetAuditListByUser(string userName); 
    }
}
