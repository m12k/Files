using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;

namespace BTSS.Logic
{
    partial class Project:Interfaces.IProject 
    {
        BTSSContext ctx;
        Data.Project project;

        #region Properties
         
        private BTSS.Common.Core.DataProvider _DataProvider;
        public BTSS.Common.Core.DataProvider DataProvider { get { return _DataProvider; } set { _DataProvider = value; } }
                    
        private string _AccessUserId;
        public string AccessUserId { get { return _AccessUserId; } set { _AccessUserId = value; } }
 
        private string _AccessPassword;
        public string AccessPassword { get { return _AccessPassword; } set { _AccessPassword = value; } }
         
        private string _SQLDatabaseName;
        public string SQLDatabaseName { get { return _SQLDatabaseName; } set { _SQLDatabaseName = value; } }

        private string _SQLUserId;
        public string SQLUserId { get { return _SQLUserId; } set { _SQLUserId = value; } }

        private string _SQLPassword;
        public string SQLPassword { get { return _SQLPassword; } set { _SQLPassword = value; } }

        private BTSS.Common.Core.Operation _Operation;
        public BTSS.Common.Core.Operation Operation { get { return _Operation; } set { _Operation = value; } }
         
        #endregion

        #region Constructor
        public Project(string connectionString)
        {
            ctx = new BTSSContext(connectionString);
            project = new BTSS.Data.Project(connectionString);
        }

         
        #endregion

        #region Private Methods 
        private string GetProjectListForReportSQL()
        {  
            string sql;
            sql = "EXECUTE GetProjectListForReport";
            return sql; 
        }
        #endregion

        #region Public Methods

        public void Save()
        { 
            try 
	        {                 
                using (var transaction = new TransactionScope())
                {
                    BTSSContext ctx = new BTSSContext(BTSS.Common.Core.ConnectionString);
                                            
                    if (_DataProvider == BTSS.Common.Core.DataProvider.OLEDB)
                        ctx.SaveProject(ref _Id, _Name, _Desc, _Version, _IsActive, _BusinessOwner, _Tester, _OtherContact, _DataProvider.ToString(), _File, _EnableBypassKey, _HasOtherDetails, _MDW, _Datasource, _SQLDatabaseName, _AccessUserId, _AccessPassword, _SharepointURL, _PathErrorLog, BTSS.Common.Core.UserName, _Operation.ToString());
                    else
                        ctx.SaveProject(ref _Id, _Name, _Desc, _Version, _IsActive, _BusinessOwner, _Tester, _OtherContact, _DataProvider.ToString(), _File, _EnableBypassKey, _HasOtherDetails, _MDW, _Datasource, _SQLDatabaseName, _SQLUserId, _SQLPassword, _SharepointURL, _PathErrorLog, BTSS.Common.Core.UserName, _Operation.ToString());
                    
                    transaction.Complete(); 
                }
	        }
	        catch (Exception)
	        {
		        throw;
	        }
        }

        public List<GetProjectListResult> GetProjectList()
        {
            try
            {         
                return ctx.GetProjectList().ToList<GetProjectListResult>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetProjectListForReport()
        {
            try
            {
                return project.GetProjectListForReport();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsProjectAllowToDelete(string projectId)
        {
            try
            {
                return ctx.IsProjectAllowToDelete(projectId).Value;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GetProjectDetailsResult GetProjectDetails(string ProjId)
        {
            try
            {
                return ctx.GetProjectDetails(ProjId).ToList<GetProjectDetailsResult>().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Project> GetAllProjects()
        {
            try
            {

                return ctx.Project.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
         
    }
}
