using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class Test:Datasource 
    {

        public Test(string connectionString) : base(connectionString, Global.DataProvider.SQL) { }

        public void LoadDates()
        {
            try
            {

                string sql;
                sql = "SELECT StartDate,EndDate From tbl_ArchiveHistory ORDER BY StartDate";

                System.Data.DataTable dt;
                dt = ExecuteQuery(sql).Tables[0];




            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
