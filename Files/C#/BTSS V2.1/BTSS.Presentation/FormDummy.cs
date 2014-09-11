using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BTSS.Presentation
{
    public partial class FormDummy : Form
    {
        public FormDummy()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var loader = new Common.Loader<string>(o =>
                                                    {
                                                        System.Threading.Thread.Sleep(10000); 
                                                    }))
                {
                    loader.RunWorker();
                };  
            }
            catch (Exception)
            {
                throw;
            }
        }
         
         
    }




}


















 
