using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JRO;

namespace WindowsFormsApplication1
{
    public partial class FormTesting : Form
    {
        public FormTesting()
        {
            InitializeComponent();
        }
         
        private void FormTesting_Load(object sender, EventArgs e)
        {
            try
            {

                char[] ch;
                string str = "ppormvieabd";
                //ch = str.ToCharArray().OrderBy(x=>x).ToArray();

                ch = str.ToCharArray();                
                Array.Sort(ch);


                string ss = new string(ch);



                //= str.ToCharArray(); 
                 //ActiveDirectory a = new ActiveDirectory();
                //string s;
                //s = a.GetNetbiosDomainName("MLISDC1.americas.manulife.net");

                //Test t = new Test(@"Server=B04SQLD04\JHUSAFDEV08;Database=dbbtVTGTp1;Trusted_Connection=True;");
                
                //t.LoadDates();


               // string s;
               //// s = @"Data Source=P:\Test\ProjectNameUI.accdb;Provider=Microsoft.Jet.OLEDB.12.0;";
               
               // //mdb
               // //s = @"Data Source=P:\TINMatchingDatabaseToCompact.mdb;Provider=Microsoft.Jet.OLEDB.4.0;";
                
               // //accdb
               // s = @"Data Source=P:\ProjectNameToCompact.accdb;Provider=Microsoft.ACE.OLEDB.12.0;Persist Security Info=False;";

               // CompactAccessDB(s, @"P:\ProjectNameToCompact.accdb");
                  
            }
            catch (Exception)
            {
                throw;
            }
        }
         
        public static void CompactAccessDB(string connectionString, string mdbfilename)
        {
            object[] oParams;

            //create an inctance of a Jet Replication Object
            //object objJRO =
            //  Activator.CreateInstance(Type.GetTypeFromProgID("JRO.JetEngine"));

            object objJRO =
              Activator.CreateInstance(Type.GetTypeFromProgID("Dao.DBEngine.120"));

            

            //filling Parameters array
            //cnahge "Jet OLEDB:Engine Type=5" to an appropriate value
            // or leave it as is if you db is JET4X format (access 2000,2002)
            //(yes, jetengine5 is for JET4X, no misprint here)
            
            //mdb
            //oParams = new object[] {
            //            connectionString,
            //            "Provider=Microsoft.Jet.OLEDB.4.0;Data" + 
            //            " Source=C:\\tempdb.mdb;Jet OLEDB:Engine Type=5"};


            oParams = new object[] {
                        connectionString,
                        "Provider=Microsoft.ACE.OLEDB.12.0;Data" + 
                        " Source=C:\\tempdb.accdb;Persist Security Info=False;"};
             



            //invoke a CompactDatabase method of a JRO object
            //pass Parameters array
            objJRO.GetType().InvokeMember("CompactDatabase",
                System.Reflection.BindingFlags.InvokeMethod,
                null,
                objJRO,
                oParams);

            //database is compacted now
            //to a new file C:\\tempdb.mdw
            //let's copy it over an old one and delete it

            System.IO.File.Delete(mdbfilename);
            System.IO.File.Move("C:\\tempdb.mdb", mdbfilename);

            //clean up (just in case)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(objJRO);
            objJRO = null;
        }
         

    }
}
