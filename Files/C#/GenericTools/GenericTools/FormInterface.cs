using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FormInterface : Form
    {
        public FormInterface()
        {
            InitializeComponent();
        }

        private void FormInterface_Load(object sender, EventArgs e)
        {
            IOperation operation;
            Person p = new Person();

            IPerson person = p as IPerson;
            operation = p as IOperation;

            person.Lastname = "Cajandig";
            person.Firstname = "Mikel";
            //operation.Save();



            Customer c = new Customer();
            Icustomer customer = c as Icustomer;
            operation = c as IOperation;
            customer.LastName = "tininin";
            operation.Save();

             
           
        }
    }
      
    public interface IOperation
    {
        void Save();
        DataSet GetList();
        DataRow GetDetails();
    }

    public class Source
    {

        public void ExecuteNonScalar(string sql)
        {
            throw new NotImplementedException();
        }

        public void ExecuteNonScalar(List<string> sql)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteQuery(string sql)
        {
            throw new NotImplementedException();
        }

        public string GetNextCode(string tableName, string fieldName, string sequence)
        {
            throw new NotImplementedException();
        }

    }

    public interface IPerson 
    {
        int Id { get; set; }
        string Lastname { get; set; }
        string Firstname { get; set; }
        string Middlename { get; set; }
        string Address { get; set; }
        string Email { get; set; }
    }

    public class Person : IPerson, IOperation
    {
        Source source = new Source();

        public void Save()
        {
            source.ExecuteNonScalar("");
        }

        public DataSet GetList()
        {
            throw new NotImplementedException();
        }

        public DataRow GetDetails()
        {
            throw new NotImplementedException();
        }

        public int Id
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        private string _lastname;
        public string Lastname
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value;
            }
        }

        private string _FirstName;
        public string Firstname
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
            }
        }

        public string Middlename
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Address
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Email
        {
            get
            { throw new NotImplementedException(); }
            set
            { }
        }
    }

    public interface Icustomer
    {
        string LastName { get; set; }
        string GetStore();
    }

    public class Customer : Source, Icustomer, IOperation
    {
        Source source = new Source();

        private string _LastName;
        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
            }
        }

        public string GetStore()
        {
            return "";
        }


        public string GetName(string name) { return ""; }

        public void Save()
        {
            source.ExecuteNonScalar("");
        }

        public DataSet GetList()
        {
            throw new NotImplementedException();
        }

        public DataRow GetDetails()
        {
            throw new NotImplementedException();
        }


    }

     

}
