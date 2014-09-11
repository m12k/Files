using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;

namespace Biztech.Classes
{
    class Preference:Datasource 
    { 
        BTSSContext ctx;

        #region "Properties"

        private int _Id;
        public int Id { get { return _Id; } set { _Id = value; } }

        private string _Tag;
        public string Tag { get { return _Tag; } set { _Tag = value; } }

        private string _Value;
        public string Value { get { return _Value; } set { _Value = value; } }
   
        private Global.Operation _Operation;
        public Global.Operation Operation { get { return _Operation; } set { _Operation = value; } }

        #endregion

        #region "Constructor"

        public Preference(string connectionString)
             : base(connectionString, Global.DataProvider.SQL)
                { 
                    ctx = new BTSSContext(connectionString);  
                }

        #endregion

        #region "Properties"

        #endregion

        #region "Private Methods"

        #endregion

        #region "Public Methods"

        public void Save()
        {
             try
             {
                 using (var transaction = new TransactionScope())
                 {
                     BTSSContext ctx = new BTSSContext(Global.ConnectionString);
                      
                     ctx.SavePreferences(_Id, _Tag, _Value, _Operation.ToString());
                     
                     transaction.Complete();
                 }
             }
             catch (Exception)
             {
                 throw;
             }
        }
         

        #endregion


    }
}
