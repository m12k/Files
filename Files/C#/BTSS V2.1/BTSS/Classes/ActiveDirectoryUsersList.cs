using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Biztech.Classes
{
    class ActiveDirectoryUsersList
    {

        #region Declarations

        BTSSContext ctx;

        #endregion

        #region Constructor

        public ActiveDirectoryUsersList(string connectionString) { ctx = new BTSSContext(connectionString); }

        #endregion

        #region Properties
        
        private int _Id;
        public int Id { get{return _Id;} set{_Id = value;} }

        private string _Name;
        public string Name { get { return _Name; } set { _Name = value; } }

        private string _Domain;
        public string Domain { get { return _Domain; } set { _Domain = value; } }

        private Global.Operation _Operation;
        public Global.Operation Operation { get { return _Operation; } set { _Operation = value; } }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        public void Save()
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    BTSSContext ctx = new BTSSContext(Global.ConnectionString);

                    ctx.SaveActiveDirectoryGroup(_Id, _Name, _Domain, _Operation.ToString());

                    transaction.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetActiveDirectoryGroupListResult> GetActiveDirectoryGroupList()
        {
            try
            {
                return ctx.GetActiveDirectoryGroupList().ToList<GetActiveDirectoryGroupListResult>();

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
