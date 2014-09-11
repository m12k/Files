using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;

namespace BTSS.Logic
{
    partial class Preferences:Interfaces.IPreferences 
    { 
        BTSSContext ctx;

        #region "Properties"
         
        private BTSS.Common.Core.Operation _Operation;
        public BTSS.Common.Core.Operation Operation { get { return _Operation; } set { _Operation = value; } }

        #endregion

        #region "Constructor"

        public Preferences(string connectionString) 
        { 
            ctx = new BTSSContext(connectionString);  
        }

        #endregion
          
        #region "Public Methods"

        public void Save()
        {
             try
             {
                 using (var transaction = new TransactionScope())
                 { 
                     ctx.SavePreferences(_Id, _Tag, _Value, _Operation.ToString());
                     
                     transaction.Complete();
                 }
             }
             catch (Exception)
             {
                 throw;
             }
        }

        public object GetValue(BTSS.Common.Core.Preferences preferences)
        {
            try
            { 
                var row = from s in ctx.Preferences
                          where s.Tag == preferences.ToString() 
                          select new { s.Value };

                if (row.Count() > 0 )
                    return row.FirstOrDefault().Value;
                else
                    return "";
                
            }
            catch (Exception)
            {
                throw;
            }
        }
      
        #endregion
        
    }
}
