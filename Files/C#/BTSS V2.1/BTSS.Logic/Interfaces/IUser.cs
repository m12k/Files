using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTSS.Logic.Interfaces 
{
    public interface IUser
    {

        string Id { get; set; }
        string LastName { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string UserName { get; set; }
        System.Data.DataTable DataUserProject { get; set; }
        System.Data.DataTable DataUserGroup { get; set; }
        BTSS.Common.Core.Operation Operation { get; set; }


        void Save(); 
        List<GetUserListResult> GetUserList();
        GetUserDetailsResult GetUserDetails(string UserId);
        System.Data.DataTable GetUsersProjectList(string UserId);
        List<GetUserProjectsResult> GetUserProjects(string UserId);
        System.Data.DataTable GetUserGroup(string UserId);
        bool IsUserValid(string userName, string password, string authentication);
        GetUserDetailsByUserNameResult GetUserDetailsByUserName(string userName);
        GetAccessRightsResult GetAccessRights(string userId, BTSS.Common.Core.Module module);
         
    }
}
