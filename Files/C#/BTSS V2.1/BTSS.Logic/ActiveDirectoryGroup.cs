using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BTSS.Logic
{
    public partial class ActiveDirectoryGroup: Interfaces.IActiveDirectoryGroup 
    {

        #region Declarations

        BTSSContext ctx;

        #endregion

        #region Constructor

        public ActiveDirectoryGroup(string connectionString) { ctx = new BTSSContext(connectionString); }

        #endregion

        #region Properties
         
        private BTSS.Common.Core.Operation _Operation;
        public BTSS.Common.Core.Operation Operation { get { return _Operation; } set { _Operation = value; } }

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
                    ctx.SaveActiveDirectoryGroup(_Id, _Name, _Domain, _Operation.ToString());

                    transaction.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetActiveDirectoryGroupListResult> GetActiveDirectoryGroupList(string domain)
        {
            try
            {
                return ctx.GetActiveDirectoryGroupList(domain).GetResult<GetActiveDirectoryGroupListResult>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsDomainGroupExist(string name, string domain)
        {
            try
            {
                return ctx.IsDomainGroupExist(name, domain).Value;
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

    }
}
