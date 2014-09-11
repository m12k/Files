using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BTSS.Logic.Interfaces
{
    public interface IProject 
    {         
        string Id { get; set; }
        string Name { get; set; }
        string Desc { get; set; }
        string Version { get; set; }
        bool IsActive { get; set; }
        string BusinessOwner { get; set; }
        string Tester { get; set; }
        string OtherContact { get; set; }
        string File { get; set; }
        bool EnableBypassKey { get; set; }
        bool HasOtherDetails { get; set; }
        string MDW { get; set; }
        string Datasource { get; set; } 
        string SharepointURL { get; set; }
        string PathErrorLog { get; set; }
        
        BTSS.Common.Core.DataProvider DataProvider { get; set; } 
        string AccessUserId { get; set; }
        string AccessPassword { get; set; }
        string SQLDatabaseName { get; set; }
        string SQLUserId { get; set; }
        string SQLPassword { get; set; }
        BTSS.Common.Core.Operation Operation { get; set; }
        string CreatedBy { get; set; }
         
        void Save();
        List<GetProjectListResult> GetProjectList();
        GetProjectDetailsResult GetProjectDetails(string ProjId);
        bool IsProjectAllowToDelete(string projectId);
        System.Data.DataTable GetProjectListForReport(); 
    }
}
