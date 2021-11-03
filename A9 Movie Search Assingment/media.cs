using System;
using System.Collections.Generic;
using System.IO;
using Json.Net;
using Newtonsoft.Json;


namespace A9_Movie_Search_Assingment
{
    public interface media
    {
        int ID{get;set;}
        string title {get;set;}
        
        void display();
                
    }
}