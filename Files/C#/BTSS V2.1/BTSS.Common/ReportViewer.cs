using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace BTSS.Common
{
    public class ReportViewer:IDisposable 
    { 
        #region "Declarations"

        object _Data;

        #endregion

        #region "Properties"

        private CrystalDecisions.CrystalReports.Engine.ReportDocument _ReportSource;
        public CrystalDecisions.CrystalReports.Engine.ReportDocument ReportSource { set { _ReportSource = value; } }

        private string _ReportTitle;
        public string ReportTitle { set { _ReportTitle = value; } }

        private string _XMLName;
        public string XMLName { set { _XMLName = value; } }

        private string _Param1 = "";
        public string Param1 { set { _Param1 = value; } }

        private string _Param2 = "";
        public string Param2 { set { _Param2 = value; } }
        
        private string _Param3 = "";
        public string Param3 { set { _Param3 = value; } }
         
        #endregion
 
        #region "Public Methods"

        public void ShowReport()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)).Append(@"\Report File");

                if (!Directory.Exists(sb.ToString())) Directory.CreateDirectory(sb.ToString());
  
                sb.Append(@"\").Append(_XMLName).Append(".xml");

                if (_Data.GetType().Name == "DataTable")
                {
                    ((DataTable)_Data).WriteXmlSchema(sb.ToString()); 
                    _ReportSource.SetDataSource(_Data);
                }
                else if (_Data.GetType().Name == "DataSet")
                {
                    ((DataSet)_Data).WriteXmlSchema(sb.ToString());
                    _ReportSource.SetDataSource(_Data);
                    ((DataSet)_Data).WriteXmlSchema(sb.ToString());
                }
                else if (_Data.GetType().Name == "List`1")
                {
                    _ReportSource.SetDataSource(_Data);
                    //int i = 0;
                    //foreach (var data in ((List<object>)_Data))
                    //{
                    //    _ReportSource.Database.Tables[i].SetDataSource(data);
                    //    i++;
                    //}
                }
                                 
                _ReportSource.SetParameterValue("company_name", "John Hancock");
                _ReportSource.SetParameterValue("address", "27 Drydock Ave STE 3");
                _ReportSource.SetParameterValue("state", "MA");
                _ReportSource.SetParameterValue("city", "Boston");
                _ReportSource.SetParameterValue("zip", "02210-2382");
                _ReportSource.SetParameterValue("phone_number", "");
                _ReportSource.SetParameterValue("report_title", _ReportTitle);
                _ReportSource.SetParameterValue("param_1", _Param1);
                _ReportSource.SetParameterValue("param_2", _Param2);
                _ReportSource.SetParameterValue("param_3", _Param3);

                using (var formReportViewer = new FormReportViewer())
                {
                    formReportViewer.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    formReportViewer.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    formReportViewer.crystalReportViewer.ReportSource = _ReportSource;
                    formReportViewer.ShowDialog();
                }
                  
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SetDatasource(DataTable dt)
        {
            try
            {
                _Data = new DataSet();
                ((DataSet)_Data).Tables.Add(dt.Copy());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SetDatasource(DataSet ds)
        {
            try
            {
                _Data = new DataSet();
                _Data = ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SetDatasource<T>(IEnumerable<T> data)
        {
            try
            {                 
                _Data = data;

                 
            }
            catch (Exception)
            {
                throw;
            }
        } 
                
        #endregion 
            
        #region IDisposable Members

        public void Dispose(){ }

        #endregion
    }
}
