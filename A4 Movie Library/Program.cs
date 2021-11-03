using System.IO;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using NLog;
namespace A4_Movie_Library
{
    class Program
    {
        public static Logger log = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            List<UInt64> movieIds = new List<UInt64>();
            List<String> movieTitles = new List<String>();
            List<String> movieGenres = new List<String>();
            String holding ="";
            try{
            Console.WriteLine("Press 1 to Add New Movie, 2 to List All Movies, or Enter to exit");
            holding = Console.ReadLine();
            }
            catch (Exception e){
                log.Error(new Exception(), $"This is a {e} please try again");
                Console.WriteLine("Press 1 to Add New Movie, 2 to List All Movies, or Enter to exit");
                holding = Console.ReadLine();
            }
            if (holding=="1"||holding=="2"){
                do{
                    using(StreamReader sr = new StreamReader("movies.csv")){
                        sr.ReadLine();
                        while(!sr.EndOfStream){
                            string line = sr.ReadLine();
                            int inx = line.IndexOf('"');
                            
                            if(inx == -1){
                                string[] temp =line.Split(',');
                                movieIds.Add(UInt64.Parse(temp[0]));
                                movieTitles.Add(temp[1]);
                                movieGenres.Add(temp[2].Replace("|",","));
                            }
                            
                            else{
                                movieIds.Add(UInt64.Parse(line.Substring(0, inx-1)));
                                line = line.Substring(inx+1);
                                inx = line.IndexOf('"');
                                movieTitles.Add(line.Substring(0, inx));
                                line = line.Substring(inx+2);
                                movieGenres.Add(line.Replace("|",","));
                            }
                        }
                    }
                    if (holding=="1"){
                        String newMovie = addMovie(movieIds,movieTitles,movieGenres);
                        using (StreamWriter sw =File.AppendText("movies.csv")){
                            sw.WriteLine(newMovie);
                        }
                    }
                    if (holding=="2"){
                        listMovies(movieIds, movieTitles, movieGenres);
                    }

                    try{
                        Console.WriteLine("Press 1 to Add New Movie, 2 to List All Movies, or Enter to exit");
                        holding = Console.ReadLine();
                    }
                    catch (Exception e){
                        log.Error(new Exception(), $"This is a {e} please try again");
                        Console.WriteLine("Press 1 to Add New Movie, 2 to List All Movies, or Enter to exit");
                        holding = Console.ReadLine();
                    }
                }while(holding=="1"||holding=="2");
                Console.WriteLine("Thank you!"); 
            }
            else{
                Console.WriteLine("Thank you!");
            }
            
        }
        public static void listMovies(List<UInt64> movieIds, List<String> movieTitles, List<String> movieGenres){
            bool temp = true;
            int tempID =0;

            while(temp==true){
                int onehundred =0;
                while (tempID<=movieIds.Count){
                    if (tempID==movieIds.Count){
                        break;
                    }
                    Console.WriteLine($"ID: {movieIds[tempID]} Title: {movieTitles[tempID]} Genres: {movieGenres[tempID]}");
                    onehundred++;
                    tempID++;
                    if (onehundred==93){
                        break;
                    }
                }
                if (tempID==movieIds.Count){
                    break;
                }
                String holder ="";
                try{
                    Console.WriteLine("Press Enter to Continue or N to Cancel");
                    holder = Console.ReadLine();
                }
                catch (Exception e){
                        log.Error(new Exception(), $"This is a {e} please try again");
                        Console.WriteLine("Press Enter to Continue or N to Cancel");
                        holder = Console.ReadLine();
                 }
                if(holder=="N"||holder=="n"){
                    break;
                }
            }
        }
        public static String addMovie(List<UInt64> movieIds, List<String> movieTitles, List<String> movieGenres){
            String newTitle = "";
            try{
                Console.WriteLine("Enter Movie Title and Year: ");
                newTitle = Console.ReadLine();
            }
            catch(Exception e){
                log.Error(new Exception(), $"This is a {e} please try again");
                Console.WriteLine("Enter Movie Title and Year: ");
                newTitle = Console.ReadLine();
            }
                for (int i=0; i<movieTitles.Count; i++){
                    if(movieTitles[i].Equals(newTitle)==true){
                        i=-1;
                        log.Warn("This is not a suitable entry.");
                        try{
                            Console.WriteLine("Enter Different Movie Title and Year: ");
                            newTitle = Console.ReadLine();
                        }
                        catch(Exception e){
                            log.Error(new Exception(), $"This is a {e} please try again");
                            Console.WriteLine("Enter Different Movie Title and Year: ");
                            newTitle = Console.ReadLine();
                        }
                        
                    }
                }
            String newGenres = "";
            String tempHold="";
            for(int j=0; j>-1; j++){
                
                try{
                    Console.WriteLine("Please Enter Movie Genres type END to stop.");
                    tempHold = Console.ReadLine();
                }
                catch(Exception e){
                    log.Error(new Exception(), $"This is a {e} please try again");
                    Console.WriteLine("Please Enter Movie Genres type END to stop.");
                    tempHold = Console.ReadLine();
                }
                        
                if (tempHold.Equals("END", StringComparison.OrdinalIgnoreCase)){
                    break;
                }
                else if (j==0){
                    newGenres=tempHold;
                }
                else {
                    newGenres+= "|"+tempHold;
                }
            }
            UInt64 newIds = (movieIds[movieIds.Count-1]+1);
            String newEntry = $"{newIds},{newTitle},{newGenres}";
            return newEntry;
        }
    }
}
