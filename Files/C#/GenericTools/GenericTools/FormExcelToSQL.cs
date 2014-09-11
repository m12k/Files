using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Data.OleDb;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class FormExcelToSQL : Form
    {
        public FormExcelToSQL()
        {
            InitializeComponent();
        }

        private void FormExcelToSQL_Load(object sender, EventArgs e)
        {
            try
            {

                List<string> fileNames = new List<string>();

                fileNames.Add(@"P:\Project Documents\BCU Aging Report\BCU Aging Report 2_4_2013.xls");
                fileNames.Add(@"C:\BCU Aging Report 2_4_2013.xls");


                foreach (string fileName in fileNames)
                {
                    if (fileName.Substring(0, 3).Equals("BCU"))
                    {
                        

                    }
                    else if (fileName.Substring(0, 3).Equals("Benefits"))
                    {

                    } 
                }




                //string cString;
                //cString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\BCU Aging Report 2_4_2013.xls;Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1;"";";
              
                //// Create Connection to Excel Workbook
                //using (OleDbConnection connection = new OleDbConnection(cString))
                //{
                //    OleDbCommand command = new OleDbCommand("Select * FROM [Pended$]", connection);

                //    connection.Open();
                //    OleDbDataReader dr;
                //    dr = command.ExecuteReader();

                //    string sqlConnectionString = @"Data Source=VMANNMBPS1\SQLEXPRESS;Initial Catalog=BTSSSQL;Integrated Security=True";

                //    // Bulk Copy to SQL Server
                //    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnectionString))
                //    {
                //        bulkCopy.DestinationTableName = "tblTempPended";
                //        bulkCopy.WriteToServer(dr);
                //    }
                //}

                 


            }
            catch (Exception)
            { 
                throw;
            }
        }
         
    }
}
