using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
   partial class FormTesting
    {

       private void Method()
       {
           Class1.Method1();

           Class3 c = new Class3();
           c.Method1();
          
            

       }
       
    }

   static class Class1
   {

    public static void Method1()
       { 
        
       }

    static void Method2()
    { }


   }

   public class Class3:Class2 
   {

       public new void Method1()
       {
           base.Method1();
           Method1();
       }

   }

   public abstract class Class2 
   {
       public void Method1()
       { }
 
   }
     
}
