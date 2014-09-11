using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Transactions;
using BTSS.Common;

namespace BTSS.Data
{

    internal class Datasource
    {
        private SqlConnection sqlConnection;
        private OleDbConnection oledbConnection;
        private Core.DataProvider dataProvider;

        protected internal Datasource(string connectionString, Core.DataProvider provider)
        {
            try 
            {
                dataProvider = provider;

                if (dataProvider ==  Core.DataProvider.SQL)
                { sqlConnection = new SqlConnection(connectionString); }
                else 
                { oledbConnection = new OleDbConnection(connectionString); }
            }
            catch (Exception)
            {
                throw;
            }          
        }

        protected internal DataSet ExecuteQuery(string sql)
        {
            try
            {
                DataSet ds = new DataSet();

                if (dataProvider == Core.DataProvider.SQL)
                {
                    SqlDataAdapter adap = new SqlDataAdapter(sql, sqlConnection);
                    adap.Fill(ds);
                }
                else
                {
                    OleDbDataAdapter adap = new OleDbDataAdapter(sql, oledbConnection);
                    adap.Fill(ds);
                }

                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected internal void ExecuteScalar(string sql)
        {
            if (dataProvider == Core.DataProvider.SQL)
            {
                try
                {
                    using (TransactionScope transaction = new TransactionScope())
                    {
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand(sql, sqlConnection);
                        command.ExecuteScalar();
                        transaction.Complete();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            else if (dataProvider == Core.DataProvider.OLEDB)
            {
                oledbConnection.Open();
                OleDbTransaction trans;
                trans = oledbConnection.BeginTransaction();

                try
                {
                    OleDbCommand command = new OleDbCommand(sql, oledbConnection, trans);
                    command.ExecuteScalar();
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
                finally
                {
                    oledbConnection.Close();
                }
            }
        }

        protected internal void ExecuteScalar(List<string> sql)
        {
            if (dataProvider == Core.DataProvider.SQL)
            {
                try
                {
                    using (TransactionScope transaction = new TransactionScope())
                    {
                        sqlConnection.Open();
                        foreach (string s in sql)
                        {
                            SqlCommand command = new SqlCommand(s, sqlConnection);
                            command.ExecuteScalar();
                        }

                        transaction.Complete();
                    }

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            else if (dataProvider == Core.DataProvider.OLEDB)
            {
                oledbConnection.Open();
                OleDbTransaction trans;
                trans = oledbConnection.BeginTransaction();

                try
                {
                    foreach (string s in sql)
                    {
                        using (OleDbCommand command = new OleDbCommand(s, oledbConnection, trans))
                        {
                            command.ExecuteScalar();
                        }
                    }

                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
                finally
                {
                    oledbConnection.Close();
                }
            }
        }

        protected internal string GetNextCode(string tableName, string fieldName, string sequence)
        {
            try
            {
                string nextCode;
                string sql;
                string prefix;
                DataTable dt;

                int strLenght;
                strLenght = sequence.Length;

                switch (tableName)
                {
                    case "set_user": prefix = "USER"; break;
                    case "set_project": prefix = "PRO"; break;
                    case "set_module": prefix = "MOD"; break;
                    case "set_group": prefix = "GRP"; break;
                    default: prefix = ""; break;
                }

                if (Core.ProjectDataProvider == Core.DataProvider.OLEDB)
                {
                    sql = "SELECT TOP 1 " +
                          "Right(" + fieldName + " , " + strLenght + ") AS a_" + fieldName + " " +
                          "From " +
                          "" + tableName + " " +
                          "WHERE created_date = Date() " +
                          "ORDER BY created_date DESC ";
                }
                else //SQL
                {
                    sql = "SELECT TOP 1 " +
                          "Right(" + fieldName + " , " + strLenght + ") AS a_" + fieldName + " " +
                          "From " +
                          "" + tableName + " " +
                          "WHERE (CONVERT(varchar, created_date, 103) = CONVERT(varchar, GETDATE(), 103)) " +
                          "ORDER BY a_" + fieldName + " DESC ";
                }


                dt = ExecuteQuery(sql).Tables[0];

                if (dt.Rows.Count == 0)
                {
                    nextCode = prefix + "-" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "-" + sequence;
                }
                else
                {
                    string formattedCode = "";
                    int newSeries;

                    //convert string to integer.
                    newSeries = Convert.ToInt32(dt.Rows[0]["a_" + fieldName]);

                    //increment 1
                    newSeries++;

                    StringBuilder sb = new StringBuilder();

                    //formula: sequence no. of characters - newSeries no. of characters.
                    sequence = sequence.Substring(0, sequence.Length - newSeries.ToString().Length);

                    //append incremented value to sequence
                    sb.Append(sequence).Append(newSeries.ToString());

                    //get the new code as string
                    formattedCode = sb.ToString();

                    //format the new code.
                    nextCode = prefix + "-" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "-" + formattedCode;
                }

                return nextCode;

            }
            catch (Exception)
            {
                throw;
            }
        }

        protected internal bool IsValueAlreadyExist(string tableName, string fieldName, string originalvalue, string currentValue, Core.Operation operation)
        {
            try
            {
                string sql = "";


                if (Core.ProjectDataProvider == Core.DataProvider.OLEDB)
                {

                    if (operation == Core.Operation.INSERT)
                    {
                        sql += "SELECT " +
                             " IIF(COUNT(" + fieldName + ") > 0 ,1,0)" +
                             " FROM " +
                             " " + tableName + " " +
                             " WHERE " +
                             " " + fieldName + " = '" + currentValue + "'";
                    }
                    else
                    {
                        sql += "SELECT " +
                             " IIF(COUNT(" + fieldName + ") > 0 ,1,0)" +
                             " FROM " +
                             " " + tableName + " " +
                             " WHERE " +
                             " " + fieldName + " = '" + currentValue + "' AND " + fieldName + " <> '" + originalvalue + "'";
                    }
                }
                else if (Core.ProjectDataProvider == Core.DataProvider.SQL)
                {
                    if (operation == Core.Operation.INSERT)
                    {
                        sql += "SELECT " +
                              " CASE WHEN COUNT(" + fieldName + ") > 0 THEN " +
                              "     1 " +
                              " ELSE " +
                              "     0 " +
                              " END " +
                              " FROM " +
                              " " + tableName + " " +
                              " WHERE " +
                              " " + fieldName + " = '" + currentValue + "'";
                    }
                    else if (operation == Core.Operation.UPDATE)
                    {
                        sql = "SELECT " +
                            " CASE WHEN COUNT(" + fieldName + ") > 0 THEN " +
                            "   1" +
                            " ELSE " +
                            "   0" +
                            " END  " +
                            " FROM " +
                            " " + tableName + "" +
                            " WHERE " +
                            " " + fieldName + " = '" + currentValue + "' AND " + fieldName + " <> '" + originalvalue + "'";
                    }
                }

                DataTable dt;

                dt = ExecuteQuery(sql).Tables[0];


                if (dt.Rows[0].Field<int?>(0) == 1) return true;
                return false;

            }
            catch (Exception)
            {
                throw;
            }
        }
         
    }
}
