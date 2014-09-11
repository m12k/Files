using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Biztech.UserControls.Generic
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

         


        // The code for this user control declares a public property of type DimensionData with a DesignerSerializationVisibility 
        // attribute set to DesignerSerializationVisibility.Content, indicating that the properties of the object should be serialized.

        // The public, not hidden properties of the object that are set at design time will be persisted in the initialization code
        // for the class object. Content persistence will not work for structs without a custom TypeConverter.		

         
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DimensionData Dimensions
        {
            get
            {
                return new DimensionData(this);
            }
        }

        private String[] stringsValue = new String[1];

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public String[] Strings
        {
            get
            {
                return this.stringsValue;
            }
            set
            {

                this.stringsValue = value;

                // Populate the contained TextBox with the values
                // in the stringsValue array.
                StringBuilder sb =
                    new StringBuilder(this.stringsValue.Length);

                for (int i = 0; i < this.stringsValue.Length; i++)
                {
                    sb.Append(this.stringsValue[i]);
                    sb.Append("\r\n");
                }

                label1.Text = sb.ToString();
            }
        }

        
    }



    [TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
    // This attribute indicates that the public properties of this object should be listed in the property grid.
    public class DimensionData
    {
        private Control owner;

        // This class reads and writes the Location and Size properties from the Control which it is initialized to.
        internal DimensionData(Control owner)
        {
            this.owner = owner;
        }

        public Point Location
        {
            get
            {
                return owner.Location;
            }
            set
            {
                owner.Location = value;
            }
        }

        public Size FormSize
        {
            get
            {
                return owner.Size;
            }
            set
            {
                owner.Size = value;
            }
        }
    }










}
