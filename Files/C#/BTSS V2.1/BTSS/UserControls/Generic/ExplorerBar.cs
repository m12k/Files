using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections;

 

namespace Biztech.UserControls.Generic
{
     
    public class MyType:Label 
    {
        private string _str = "myString";
        
        public string str 
        { 
            get 
            {
                return _str;
            } 
            set 
            {
                _str = value;
            } 
        } 
    }


    
    public class StrCollection : CollectionBase
    { 
        public delegate void OnAddEvent(Label label);
        public event OnAddEvent OnAdd;
          
        public MyType this[int index]
        {
            get
            {
                return (MyType)List[index];
            }
        }
         
        public void Add(MyType s)
        {
            List.Add(s);             
            OnAdd(s);
        }
         
        public void Remove(MyType s)
        {
            List.Remove(s); 
        }
        
        public void Insert(int index, MyType s)
        {
            List.Insert(index, s); 
        } 
    } 
}
