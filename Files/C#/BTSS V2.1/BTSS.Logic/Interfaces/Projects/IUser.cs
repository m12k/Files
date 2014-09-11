using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTSS.Logic.Interfaces.Projects
{
    public interface IUser
    { 
        string Id { get; set; }
        string LastName { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string UserName { get; set; }

        System.Data.DataTable Data { get; set; }         
        BTSS.Common.Core.Operation Operation { get; set; }

        void Save();
        System.Data.DataTable GetUserList();
        System.Data.DataRow GetUserDetails(string id);
        bool IsExist(string userName);
        System.Data.DataTable GetGroupAccess();
        System.Data.DataTable GetUserGroup(string id);
        System.Data.DataTable GetUserGroupList();
    }
}
