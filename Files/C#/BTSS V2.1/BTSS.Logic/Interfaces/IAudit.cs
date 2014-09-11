using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTSS.Logic.Interfaces
{
    public interface IAudit
    {
        List<Audit> GetListByUser(string userName);
        List<string> GetUsers();
        List<string> GetTables();
        List<GetAuditListResult> GetAuditList(string userName, string tableName);

    }
}
