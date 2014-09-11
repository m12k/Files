using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentNHibernateTesting.Model
{
    public class Post
    {
        public virtual int Id { get; set; } 
        public virtual string PostCode { get; set; } 
        public virtual string PostName { get; set; }
    }
}
