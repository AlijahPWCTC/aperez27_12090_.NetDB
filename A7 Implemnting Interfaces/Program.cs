using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using Json.Net;
using Newtonsoft.Json;

namespace A7_Implemnting_Interfaces
{
    class Program
    {

        static void Main(string[] args)
        {
            List<movies> movieList = new List<movies>();
            using (StreamReader r = new StreamReader("movies.json"))
                {
                    string json = r.ReadToEnd();
                    movieList = JsonConvert.DeserializeObject<List<movies>>(json);
                }
            movies.jsonMovieOptions(movieList);  
                   
        }
    }
}
