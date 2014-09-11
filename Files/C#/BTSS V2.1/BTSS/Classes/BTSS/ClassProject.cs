using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;

namespace Biztech.Classes
{
    class ClassProject : Datasource  
    {
        BTSSContext ctx; 

        #region Properties

        private string _Id;
        public string Id { get { return _Id; } set { _Id = value; } }

        private string _Name;
        public string Name { get { return _Name; } set { _Name = value; } }

        private string _Description;
        public string Description { get { return _Name; } set { _Description = value; } }

        private bool _IsActive;
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }

        private string _Version;
        public string Version { get { return _Version; } set { _Version = value; } }

        private string _SharepointURL;
        public string SharepointURL { get { return _SharepointURL;} set {_SharepointURL =value;} }

        private string _ErrorLogPath;
        public string ErrorLogPath { get { return _ErrorLogPath; } set { _ErrorLogPath = value; } }

        private string _BusinessOwner;
        public string BusinessOwner { get { return _BusinessOwner;}set{ _BusinessOwner = value;} }

        private string _Tester;
        public string Tester { get { return _Tester; } set { _Tester = value; } }
         
        private string _OtherContact;
        public string OtherContact { get { return _OtherContact; } set  {_OtherContact =value; } }

        private Global.DataProvider _DataProvider;
        public Global.DataProvider DataProvider { get { return _DataProvider; } set { _DataProvider = value; } }
        
        private string _FileMDB;
        public string FileMBD { get { return _FileMDB; } set { _FileMDB = value; } }

        private bool _EnableByPassKey;
        public bool EnableByPassKey { get { return _EnableByPassKey; } set { _EnableByPassKey = value; } }

        private bool _HasOtherDetails;
        public bool HasOtherDetails { get { return _HasOtherDetails; } set { _HasOtherDetails = value; } }

        private string _FileMDW;
        public string FileMDW { get { return _FileMDB; } set { _FileMDW = value; } }

        private string _AccessUserId;
        public string AccessUserId { get { return _AccessUserId; } set { _AccessUserId = value; } }

        private string _AccessPassword;
        public string AccessPassword { get { return _AccessPassword; } set { _AccessPassword = value; } }

        private string _Datasource;
        public string Datasource { get { return _Datasource; } set { _Datasource = value; } }
         
        private string _SQLDatabaseName;
        public string SQLDatabaseName { get { return _SQLDatabaseName; } set { _SQLDatabaseName = value; } }

        private string _SQLUserId;
        public string SQLUserId { get { return _SQLUserId; } set { _SQLUserId = value; } }

        private string _SQLPassword;
        public string SQLPassword { get { return _SQLPassword; } set { _SQLPassword = value; } }

        private Global.Operation _Operation;
        public Global.Operation Operation { get { return _Operation; } set { _Operation = value; } }
         
        #endregion

        #region Constructor
        public ClassProject(string connectionString) : base(connectionString, Global.DataProvider.SQL) 
        {
            ctx = new BTSSContext(connectionString); 
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
                    BTSSContext ctx = new BTSSContext(Global.ConnectionString);

                    if (_DataProvider == Global.DataProvider.OLEDB)
                        ctx.SaveProject(ref _Id, _Name, _Description, _Version, _IsActive, _BusinessOwner, _Tester, _OtherContact, _DataProvider.ToString(),_FileMDB, _EnableByPassKey, _HasOtherDetails , _FileMDW, _Datasource, _SQLDatabaseName, _AccessUserId, _AccessPassword, _SharepointURL, _ErrorLogPath, Global.UserName, _Operation.ToString());
                    else
                        ctx.SaveProject(ref _Id, _Name, _Description, _Version, _IsActive, _BusinessOwner, _Tester, _OtherContact,_DataProvider.ToString(), _FileMDB, _EnableByPassKey, _HasOtherDetails, _FileMDW ,_Datasource, _SQLDatabaseName, _SQLUserId, _SQLPassword,_SharepointURL, _ErrorLogPath, Global.UserName, _Operation.ToString());
                    
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
                return ExecuteQuery(GetProjectListForReportSQL()).Tables[0];
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
         
        #endregion

    }
}
