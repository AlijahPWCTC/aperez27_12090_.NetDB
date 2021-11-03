using System.Collections.Generic;
using System.IO;

namespace A7_Implemnting_Interfaces
{
    public interface media
    {
        int ID{get;set;}
        string title {get;set;}
        string[] genres {get;set;}
        void display();
                
    }
}