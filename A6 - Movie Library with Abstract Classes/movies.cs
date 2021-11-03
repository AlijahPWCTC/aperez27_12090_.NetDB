using System;
using System.Collections.Generic;
using System.IO;

namespace A6___Movie_Library_with_Abstract_Classes
{
    public class movies : media
    {
        int movieID{get;set;}
        string title {get;set;}
        string[] genres {get;set;}

        public movies(){
            
        }
        public movies(int id, string Title, string[] array)
        {
            movieID=id;
            title=Title;
            genres=array;
        }

        public static void read(List<movies> movieList){
            using(StreamReader sr = new StreamReader("movies2.csv")){
                sr.ReadLine();
                while(!sr.EndOfStream){
                    string line = sr.ReadLine();
                    int inx = line.IndexOf('"');            
                    if(inx == -1){
                        string[] temp =line.Split(',');
                        temp[2]=temp[2].Replace("|",",.");
                        string[] tempTwo = temp[2].Split('.');
                        movies test = new movies(int.Parse(temp[0]),temp[1],tempTwo);
                        movieList.Add(test);
                    }
                    else{
                        int tempID=(int.Parse(line.Substring(0, inx-1)));
                        line = line.Substring(inx+1);
                        inx = line.IndexOf('"');
                        string tempTitle = (line.Substring(0, inx));
                        line = line.Substring(inx+2);
                        string[] tempArray= (line.Replace("|",",")).Split(',');
                        movies test = new movies(tempID,tempTitle,tempArray);
                    }
                }
            }
        }

        public override void display()
        {
            string holder = genres[0];
            for(int i=1; i<genres.Length; i++){
               holder+= $" {genres[i]}";
            }
            Console.WriteLine($"ID: {this.movieID} Title: {this.title} Genres: {holder}");
        }

        public static void displayAll(List<movies> movieList)
        {
            for (int i=0; i<movieList.Count; i++){
                movieList[i].display();
            }
        }
        public static void addMovie(List<movies> movieList){
            String newTitle = "";
            try{
                Console.WriteLine("Enter Movie Title and Year: ");
                newTitle = Console.ReadLine();
            }
            catch(Exception e){
                Console.WriteLine($"{e} error please try again.");
                Console.WriteLine("Enter Movie Title and Year: ");
                newTitle = Console.ReadLine();
            }
                for (int i=0; i<movieList.Count; i++){
                    if(movieList[i].title.Equals(newTitle)==true){
                        i=-1;
                        Console.WriteLine("This is not a suitable entry.");
                        try{
                            Console.WriteLine("Enter Different Movie Title and Year: ");
                            newTitle = Console.ReadLine();
                        }
                        catch(Exception e){
                            Console.WriteLine($"{e} error please try again.");
                            Console.WriteLine("Enter Different Movie Title and Year: ");
                            newTitle = Console.ReadLine();
                        }
                        
                    }
                }
            List<String> newGenres= new List<string>();
            String tempHold="";
            for(int j=0; j>-1; j++){
                
                try{
                    Console.WriteLine("Please Enter Movie Genres type END to stop.");
                    tempHold = Console.ReadLine();
                }
                catch(Exception e){
                    Console.WriteLine($"{e} error please try again.");
                    Console.WriteLine("Please Enter Movie Genres type END to stop.");
                    tempHold = Console.ReadLine();
                }
                        
                if (tempHold.Equals("END", StringComparison.OrdinalIgnoreCase)){
                    break;
                }
                else {
                    newGenres.Add(tempHold);
                }
            }
            String genereFile =newGenres[0];
            for(int i=1; i<newGenres.Count; i++){
                genereFile+=$"|{newGenres[i]}";
            }
            string[] genreArray = newGenres.ToArray();
            int newIds = (movieList[movieList.Count-1].movieID+1);
            movies tempMovie = new movies(newIds, newTitle, genreArray);
            movieList.Add(tempMovie);
            String newEntry = $"{newIds},{newTitle},{genereFile}";
            using (StreamWriter sw =File.AppendText("movies2.csv")){
                            sw.WriteLine(newEntry);
            }
            
        }
        public static void movieOptions(List<movies> movieList){
            int choice=-1;
            while (choice!=3){
                try{
                    Console.WriteLine("Type 1 for Display options Type 2 to add a new entry Type 3 to exit: ");
                    choice = int.Parse(Console.ReadLine());
                }
                catch(Exception e){
                    Console.WriteLine($"{e} error please try again.");
                    Console.WriteLine("Type 1 for Display options Type 2 to add a new entry: ");
                    choice = int.Parse(Console.ReadLine());
                }
                if(choice==1){
                    int choice1;
                    try{
                        Console.WriteLine($"There are a total of {movieList.Count} entries.");
                        Console.WriteLine($"Type 0 to display all or type anywhere from 1 to {movieList.Count} to display a specific entry: ");
                        choice1= int.Parse(Console.ReadLine());
                    }
                    catch(Exception e){
                        Console.WriteLine($"{e} error please try again.");
                        Console.WriteLine($"There are a total of {movieList.Count} entries.");
                        Console.WriteLine($"Type 0 to display all or type anywhere from 1 to {movieList.Count} to display a specific entry: ");
                        choice1= int.Parse(Console.ReadLine());
                    }
                    if(choice1==0){
                        movies.displayAll(movieList);
                    }
                    else{
                        movieList[choice1-1].display();
                    }
                }
                else if (choice==2){
                    movies.addMovie(movieList);
                }
            }
        }
    }
}