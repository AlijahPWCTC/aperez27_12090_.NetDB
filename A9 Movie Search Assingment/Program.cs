using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using Json.Net;
using Newtonsoft.Json;
using System.Linq;

namespace A9_Movie_Search_Assingment
{
    class Program
    {
        static void Main(string[] args)
        {
            mediaOptions();
        }
        public static void mediaOptions(){
            int ch;
            try{
                Console.WriteLine("Choose CSV Type 1, Choose Json Type 2");
                ch = int.Parse(Console.ReadLine());
            }
            catch(Exception e){
                Console.WriteLine($"{e} error please try again.");
                Console.WriteLine("Choose CSV Type 1, Choose Json Type 2");
                ch = int.Parse(Console.ReadLine());
            }
            if(ch==1){
                List<movies> movieList = new List<movies>();
                movies.read(movieList);
                List<shows> showList = new List<shows>();
                shows.read(showList);
                List<videos> videosList = new List<videos>();
                videos.read(videosList);
                int choice = -1;
            while (choice!=5){
                try{
                    Console.WriteLine("Choose what type of media you want options for.");
                    Console.WriteLine("Type 1 for Movies Type 2 for Shows Type 3 for Videos Type 4 to Search Type 5 to Exit: ");
                    choice = int.Parse(Console.ReadLine());
                }
                catch(Exception e){
                    Console.WriteLine($"{e} error please try again.");
                    Console.WriteLine("Choose what type of media you want options for.");
                    Console.WriteLine("Type 1 for Movies Type 2 for Shows Type 3 for Videos Type 4 to Search Type 5 to Exit: ");
                    choice = int.Parse(Console.ReadLine());
                }
                if(choice==1){
                    movies.movieOptions(movieList);
                }
                else if(choice==2){
                    shows.showOptions(showList);
                }
                else if(choice==3){
                    videos.videoOptions(videosList);
                }
                else if(choice==4){
                    search(movieList, showList, videosList);
                }
            }
            Console.WriteLine("Thank you Goodbye!");
            }

            if(ch==2){
                List<movies> movieList = new List<movies>();
                using (StreamReader r = new StreamReader("movies.json"))
                    {
                        string json = r.ReadToEnd();
                        movieList = JsonConvert.DeserializeObject<List<movies>>(json);
                    }
                List<shows> showList = new List<shows>();
                using (StreamReader r = new StreamReader("shows.json"))
                    {
                        string json = r.ReadToEnd();
                        showList = JsonConvert.DeserializeObject<List<shows>>(json);
                    }
                List<videos> videoList = new List<videos>();
                using (StreamReader r = new StreamReader("videos.json"))
                    {
                        string json = r.ReadToEnd();
                        videoList = JsonConvert.DeserializeObject<List<videos>>(json);
                    }  
                int choice = -1;
                while (choice!=4){
                try{
                    Console.WriteLine("Choose what type of media you want options for.");
                    Console.WriteLine("Type 1 for Movies Type 2 for Shows Type 3 for Videos Type 4 to Search Type 5 to Exit: ");
                    choice = int.Parse(Console.ReadLine());
                }
                catch(Exception e){
                    Console.WriteLine($"{e} error please try again.");
                    Console.WriteLine("Choose what type of media you want options for.");
                    Console.WriteLine("Type 1 for Movies Type 2 for Shows Type 3 for Videos Type 4 to Search Type 5 to Exit: ");
                    choice = int.Parse(Console.ReadLine());
                }
                if(choice==1){
                    
                    movies.jsonMovieOptions(movieList);
                }
                else if(choice==2){
                    shows.jsonShowsOptions(showList);
                }
                else if(choice==3){  
                    videos.jsonVideosOptions(videoList);
                }
                else if(choice==4){
                    search(movieList, showList, videoList);
                }
            }
                Console.WriteLine("Thank you Goodbye!");
            }
        }
        public static void search(List<movies> movieList, List<shows> showList, List<videos> videosList){
            Console.WriteLine("Enter Search Term: ");
            string searchTerm = Console.ReadLine();
            
            IEnumerable<movies> movieQuery =
            from mov in movieList
            where mov.title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            select mov;

            IEnumerable<shows> showQuery =
            from show in showList
            where show.title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            select show;

            IEnumerable<videos> videoQuery =
            from vid in videosList
            where vid.title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            select vid;
            Console.WriteLine("Movie Results: ");
            foreach (movies movie in movieQuery){
                movie.display();
            }
            Console.WriteLine("Show Results: ");
            foreach (shows show in showQuery){
                show.display();
            }
            Console.WriteLine("Video Results: ");
            foreach (videos vid in videoQuery){
                vid.display();
            }
        }

    }
}
