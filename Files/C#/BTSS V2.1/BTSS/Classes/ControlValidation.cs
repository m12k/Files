using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Biztech.Classes
{
    class Validation
    {

        #region "Declarations"

        private static List<TextBox> requiredTextBox;

        #endregion

        #region "Constructor"

        public Validation()
        {
            requiredTextBox = new List<TextBox>();
        }

        #endregion
        
        #region "Public Methods"

        public void Validate(Control control)
        {
            try
            {               
                var e = control.Controls.GetEnumerator();
                while (e.MoveNext())
                {
                    if ((e.Current as Control).GetType().Name == "TextBox")
                    {
                        FormatTextBox(e.Current as TextBox);
                    } 
                    Validate(e.Current as Control);
                }                 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsTextBoxesValid()
        {
            try
            {
                foreach(TextBox t in requiredTextBox) if (t.Text == "" && t.Enabled) return false; 
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region "Private Methods"

        private static void FormatTextBox(TextBox textbox)
        {
            if (textbox.Tag != null) 
            {
                if (textbox.Tag.ToString().Length > 0)
                { 
                    requiredTextBox.Add(textbox);
                    textbox.BackColor = System.Drawing.Color.Cornsilk;
                } 
            } 

            //textbox.BorderStyle = BorderStyle.FixedSingle;
        }


        #endregion


       
         

       


    }
}
