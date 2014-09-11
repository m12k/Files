using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTSS.Logic.Interfaces.Projects
{
    public interface IModule
    {
        string Id { get; set; }
        string Name { get; set; }
        string Desc { get; set; }
        System.Data.DataTable Table { get; set; }

        BTSS.Common.Core.Operation Operation { get; set; }
         
        void Save();
        System.Data.DataTable GetProjectModuleList();
        System.Data.DataRow GetProjectModuleDetails(string id);
        bool IsModuleAllowToDelete(string id);
        System.Data.DataTable GetModuleTableMap(string id);
    }
}
