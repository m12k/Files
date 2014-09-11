using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection; 

namespace BTSS.Logic
{
    internal partial class BTSSContext
    {

        [Function(Name = "dbo.GetAuditList")]
        public ISingleResult<GetAuditListResult> GetAuditListCopy([Parameter(Name = "UserName", DbType = "VarChar(128)")] string userName, [Parameter(Name = "TableName", DbType = "VarChar(128)")] string tableName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userName, tableName);
            return ((ISingleResult<GetAuditListResult>)(result.ReturnValue));
        }

    }
}
