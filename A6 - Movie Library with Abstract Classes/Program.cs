using System;
using System.IO;
using System.Collections.Generic;
namespace A6___Movie_Library_with_Abstract_Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = -1;
            while (choice!=4){
                try{
                    Console.WriteLine("Choose what type of media you want options for.");
                    Console.WriteLine("Type 1 for Movies Type 2 for Shows Type 3 for Videos Type 4 to Exit: ");
                    choice = int.Parse(Console.ReadLine());
                }
                catch(Exception e){
                    Console.WriteLine($"{e} error please try again.");
                    Console.WriteLine("Choose what type of media you want options for.");
                    Console.WriteLine("Type 1 for Movies Type 2 for Shows Type 3 for Videos Type 4 to Exit: ");
                    choice = int.Parse(Console.ReadLine());
                }
                if(choice==1){
                    List<movies> movieList = new List<movies>();
                    movies.read(movieList);
                    movies.movieOptions(movieList);
                }
                else if(choice==2){
                    List<shows> showList = new List<shows>();
                    shows.read(showList);
                    shows.showOptions(showList);
                }
                else if(choice==3){
                    List<videos> videosList = new List<videos>();
                    videos.read(videosList);
                    videos.videoOptions(videosList);
                }
            }
            Console.WriteLine("Thank you Goodbye!");
            
            
            

        }
    }
}
