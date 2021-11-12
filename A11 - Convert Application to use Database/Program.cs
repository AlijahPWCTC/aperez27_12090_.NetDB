using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System;
using System.Linq;
using A11___Convert_Application_to_use_Database.DataModels;
using A11___Convert_Application_to_use_Database.Context;

namespace A11___Convert_Application_to_use_Database

{
    class Program
    {
        static void Main(string[] args)
        {
            Options();
        }
        public static void Options(){
            bool run=true;
            Console.WriteLine("Welcome to the Movie Database");
            while(run!=false){
                Console.WriteLine("Please select an option");
                string choice;
                try{
                    Console.WriteLine("1. Create a new Movie, 2. Update a Movie, 3. Delete a Movie, 4. Search for a Movie, 5 to Exit: ");
                    choice = Console.ReadLine();
                }
                catch(Exception e){
                    Console.WriteLine("Please enter a valid option");
                    Console.WriteLine("1. Create a new Movie, 2. Update a Movie, 3. Delete a Movie, 4. Search for a Movie, 5 to Exit: ");
                    choice = Console.ReadLine();
                }
                if(choice=="1"){
                    AddMovie();
                }
                else if(choice=="2"){
                    UpdateMovie();
                }
                else if(choice=="3"){
                    DeleteMovie();
                }
                else if(choice=="4"){
                    SearchMovie();
                }
                else if(choice=="5"){
                    run = false;
                }
            }
            System.Console.WriteLine("Goodbye!");
        }
        public static void AddMovie(){
            Console.WriteLine("Please enter the movie title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Please enter the release year: ");
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the release month");
            int month = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the release day");
            int day = int.Parse(Console.ReadLine());
            int id;
            using (var db = new MovieContext()){
                id = db.Movies.Count()+1;
                Movie movie = new Movie(){
                    Title = title,
                    ReleaseDate = new DateTime(year,month,day)
                };
                db.Movies.Add(movie);
                List<Genre>  genres = db.Genres.ToList();
                Console.WriteLine("\nPlease enter the genres of the movie");
                Console.WriteLine("Genres are: ");
                foreach(Genre genre in genres){
                    Console.Write($"{genre.Id} {genre.Name}, ");
                }
                List<long> newGenres= new List<long>();
                long tempHold;
                for(int j=0; j>-1; j++){
                    try{
                        Console.WriteLine("Please Enter Movie Genres type 0 to stop.");
                        tempHold = long.Parse(Console.ReadLine());
                    }
                    catch(Exception e){
                        Console.WriteLine($"{e} error please try again.");
                        Console.WriteLine("Please Enter Movie Genres type 0 to stop.");
                        tempHold = long.Parse(Console.ReadLine());
                    }
                            
                    if (tempHold==0){
                        break;
                    }
                    else {
                        newGenres.Add(tempHold);
                    }
                }
                foreach(long genre in newGenres){
                    Genre GENRES = db.Genres.Find(genre);
                    MovieGenre mGenres = new MovieGenre(){
                        Movie = movie,
                        Genre = GENRES
                    };
                    db.MovieGenres.Add(mGenres);
                }
                db.SaveChanges();
                Console.WriteLine($"Please enter your user id from 1 to {db.Users.Count()}: ");
                long userId = long.Parse(Console.ReadLine());
                User tempUser = db.Users.Find(userId);
                Console.WriteLine($"Please enter the rating you would like to give the movie from 1 to 5: ");
                long rating = long.Parse(Console.ReadLine());
                UserMovie mRating = new UserMovie(){
                    Movie = movie,
                    User = tempUser,
                    Rating = rating,
                    RatedAt = DateTime.Now
                };
                db.UserMovies.Add(mRating);
                db.SaveChanges();
            }

        }
        public static void DeleteMovie(){
            using (var db = new MovieContext()){
                Console.WriteLine($"Please enter the name of the movie you would like to search for: ");
                string title = Console.ReadLine();
                List<Movie> movies = db.Movies.ToList();
                List<Movie> moviesToDelete = new List<Movie>();
                foreach(Movie MOVIE in movies){
                    if(MOVIE.Title.ToLower().Contains(title.ToLower())){
                        moviesToDelete.Add(MOVIE);
                        Console.WriteLine($"{MOVIE.Title}");
                    }
                }
                Console.WriteLine($"Please enter the number of the movie you would like to delete from 1 to {moviesToDelete.Count()}: ");
                int movieToDelete = int.Parse(Console.ReadLine());
                Movie movie = moviesToDelete[movieToDelete-1];
                IEnumerable<MovieGenre> removeMovieGenres =
                    from m in db.MovieGenres
                    where m.Movie == movie
                    select m;
                foreach(MovieGenre mGenre in removeMovieGenres){
                        db.MovieGenres.Remove(mGenre);
                }
                IEnumerable<UserMovie> removeUserMovies =
                    from um in db.UserMovies
                    where um.Movie == movie
                    select um;
                foreach(UserMovie uMovie in removeUserMovies){
                    db.UserMovies.Remove(uMovie);
                }
                IEnumerable<Movie> removeMovie =
                    from m in db.Movies
                    where m == movie
                    select m;
                foreach(Movie m in removeMovie){
                    db.Movies.Remove(m);
                }
                db.SaveChanges();
                
            }
        }
        public static void UpdateMovie(){
            using (var db = new MovieContext()){
                Console.WriteLine($"Please enter the name of the movie you would like to search for: ");
                string title = Console.ReadLine();
                List<Movie> movies = db.Movies.ToList();
                List<Movie> moviesToDelete = new List<Movie>();
                foreach(Movie MOVIE in movies){
                    if(MOVIE.Title.ToLower().Contains(title.ToLower())){
                        moviesToDelete.Add(MOVIE);
                        Console.WriteLine($"{MOVIE.Title}");
                    }
                }
                Console.WriteLine($"Please enter the number of the movie you would like to update from 1 to {moviesToDelete.Count()}: ");
                int movieToDelete = int.Parse(Console.ReadLine());
                Movie movie = moviesToDelete[movieToDelete-1];
                IEnumerable<UserMovie> updateUserMovies =
                    from um in db.UserMovies
                    where um.Movie == movie
                    select um;
                foreach(UserMovie uMovie in updateUserMovies){
                    Console.WriteLine($"Please enter your user id from 1 to {db.Users.Count()}: ");
                    long userId = long.Parse(Console.ReadLine());
                    User tempUser = db.Users.Find(userId);
                    Console.WriteLine($"Please enter the rating you would like to give the movie from 1 to 5: ");
                    long rating = long.Parse(Console.ReadLine());
                    UserMovie mRating = new UserMovie(){
                    Movie = movie,
                    User = tempUser,
                    Rating = rating,
                    RatedAt = DateTime.Now
                };
                db.UserMovies.Add(mRating);
                }
                db.SaveChanges();
                
            }
        }
        public static void SearchMovie(){
            using (var db = new MovieContext()){
                Console.WriteLine($"Please enter the name of the movie you would like to search for: ");
                string title = Console.ReadLine();
                List<Movie> movies = db.Movies.ToList();
                foreach(Movie movie in movies){
                    if(movie.Title.ToLower().Contains(title.ToLower())){
                        long id = movie.Id;
                        Console.WriteLine($"{movie.Title} was released on {movie.ReleaseDate}");
                        Console.WriteLine("Genres are: ");
                        db.Movies.Where(m => m.Id == id).FirstOrDefault().MovieGenres.ToList().ForEach(mg => Console.WriteLine($"{mg.Genre.Name}"));
                    }
                }        
                
            }
    }
}
}
