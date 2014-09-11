using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTSS.Logic.Interfaces
{
    public interface IActiveDirectoryGroup
    {

        int Id { get; set; }
        string Name { get; set; }
        string Domain { get; set; }
        BTSS.Common.Core.Operation Operation{ get; set; }
        void Save();
        List<GetActiveDirectoryGroupListResult> GetActiveDirectoryGroupList(string domain);
        bool IsDomainGroupExist(string name, string domain);

    }
}
