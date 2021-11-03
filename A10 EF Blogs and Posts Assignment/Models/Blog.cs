using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A10_EF_Blogs_and_Posts_Assignment.Models
{
    public class Blog
    {
        public int BlogId {get;set;}
        public string Name { get; set; }
        public virtual List<Post> Posts {get;set;}
    }
}