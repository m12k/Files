using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTSS.Logic.Interfaces
{
    interface IGroup
    {

        string Id { get; set; }
        string Name { get; set; }
        string Desc { get; set; }
        System.Data.DataTable DataGroupModule { get; set; }
        BTSS.Common.Core.Operation Operation { get; set; }


        void Save();


    }
}
