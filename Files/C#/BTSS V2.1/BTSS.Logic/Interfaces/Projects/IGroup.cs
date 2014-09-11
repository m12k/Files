using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTSS.Logic.Interfaces.Projects
{
    public interface IGroup
    {

        string Id { get; set; }
        string Name { get; set; }
        string Desc { get; set; }
        System.Data.DataTable Data { get; set; }
        BTSS.Common.Core.Operation  Operation { get; set; }

        void Save();
        System.Data.DataTable GetGroupList();
        System.Data.DataRow GetGroupDetails(string id);
        System.Data.DataTable GetModuleAccess();
        System.Data.DataTable GetGroupModule(string id);
        bool IsGroupAllowToDelete(string id);

    }
}
