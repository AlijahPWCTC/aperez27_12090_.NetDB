using System.Collections.Generic;
using System.IO;

namespace A6___Movie_Library_with_Abstract_Classes
{
    public abstract class media
    {
        int idType{get;set;}
        string title {get;set;}
        string[] tempArray {get;set;}
        public abstract void display();
                
    }
}