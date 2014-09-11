using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentNHibernateTesting.Model
{
    public class Employee
    {

        public virtual int Id { get; set; } 
        public virtual string FirstName { get; set; } 
        public virtual string LastName { get; set; } 
        public virtual string Address { get; set; } 
        public virtual Post Post { get; set; } 
        public virtual DateTime DateEmployed { get; set; }

    }
}
