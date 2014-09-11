using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplicationTesting
{
    public class Problem1
    {
                
        
        public void Validate(List<Manager> manager)
        {
            
            foreach(IValidate mgr in manager)
            { 
                mgr.Validate(); 
            }

            //// City == null -> display "City must not be empty"
            //// Zip Code == null -> display "Zip Code must not be empty"
             
            //foreach (Manager mgr in manager)
            //{ 
            //    foreach (Address a in mgr.Address)
            //    { 
            //        if (a.City == null) {
            //            //"City must not be empty"
            //        }

            //        if (a.Zip == null)
            //        {
            //            //"Zip Code must not be empty"
            //        }
            //    }

            //    foreach (Person assoc in mgr.Associate)
            //    {
            //        foreach (Address address in assoc.Address)
            //        {
            //            if (address.City == null)
            //            {
            //                //"City must not be empty"
            //            }
            //            if (address.Zip == null)
            //            {
            //                //"Zip Code must not be empty"
            //            }
            //        }
            //    } 
            //} 
        } 
    }

 
    public class Manager:Person,IValidate 
    {
        string TeamName;
        public List<Person> Associate;
         
        void IValidate.Validate()
        {
            if (TeamName == null)
            { 
            //Team name must not be empty
            }

            foreach (IValidate assoc in Associate)
            {
                assoc.Validate();
            }
        }
    }

    public class Person: IValidate
    {
        public string LastName;
        public string FirstName;
        public List<Address> Address;
         
        public void Validate()
        {
            if (LastName == null)
            { 
            //Last name must not be null.
            }

            foreach (IValidate a in Address)
            {
                a.Validate();
            }
        }
    }
      
    public class Address:IValidate
    {
        public string City;
        public string Zip;
         
        public void Validate()
        {
            if (City == null)
            {
                //"City must not be empty"
            }
            if (Zip == null)
            {
                //"Zip Code must not be empty"
            }
        } 
    } 

    public interface IValidate
    {
        void Validate();
    }
}
