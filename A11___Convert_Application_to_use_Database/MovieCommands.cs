using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using A11___Convert_Application_to_use_Database.DataModels;
using A11___Convert_Application_to_use_Database.Context;
using NLog;
using NLog.Config;

namespace A11___Convert_Application_to_use_Database

{
    public class MovieCommands
    {
        private static Logger movieLog = LogManager.GetCurrentClassLogger();
        public static void Options(){
            bool run=true;
            Console.WriteLine("Welcome to the Movie Database");
            while(run!=false){
                //Has the User select an option.
                Console.WriteLine("Please select an option");
                string choice;
                //Presents the user with the options.
                try{
                    Console.WriteLine("1. Create a new Movie, 2. Update a Movie, 3. Delete a Movie, 4. Search for a Movie, 5 Display all Movies, 6 Add a User, 7 Add a Rating, 8 Exit: ");
                    choice = Console.ReadLine();
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid option");
                    Console.WriteLine("1. Create a new Movie, 2. Update a Movie, 3. Delete a Movie, 4. Search for a Movie, 5 Display all Movies, 6 Add a User, 7 Add a Rating, 8 Exit: ");
                    choice = Console.ReadLine();
                }
                //If the user selects 1, the user is prompted to enter the information for the new movie.
                if(choice=="1"){
                    AddMovie();
                }
                //If the user selects 2, the user is prompted to enter the information for the movie they wish to update.
                else if(choice=="2"){
                    UpdateMovie();
                }
                //If the user selects 3, the user is prompted to enter the information for the movie they wish to delete.
                else if(choice=="3"){
                    DeleteMovie();
                }
                //If the user selects 4, the user is prompted to enter the information for the movie they wish to search for.
                else if(choice=="4"){
                    SearchMovie();
                }
                // If the user selects 5, movies are displayed 100 at a time and then asked to continue.
                else if(choice=="5"){
                    DisplayAll();
                }
                //If the user selects 6, the user is prompted to enter the information for the new user.
                else if(choice=="6"){
                    AddUser();
                }
                //If the user selects 7, the user is prompted to enter the information for the new rating.
                else if(choice=="7"){
                    AddRating();
                }
                //If the user selects 8, the program exits.
                else if(choice=="8"){
                    run = false;
                }
            }
            System.Console.WriteLine("Goodbye!");
        }
        public static void AddMovie(){
            //Prompts the user to enter the information for the new movie.
            string title;
            try{
                Console.WriteLine("Please enter the movie title: ");
                title = Console.ReadLine();
            }
            catch(Exception e){
                movieLog.Error(e.Message+"Please enter a valid title");
                Console.WriteLine("Please enter the movie title: ");
                title = Console.ReadLine();
            }
            int year;
            try{
                Console.WriteLine("Please enter the release year: ");
                year = int.Parse(Console.ReadLine());  
            }
            catch(Exception e){
                movieLog.Error(e.Message+"Please enter a valid year");
                Console.WriteLine("Please enter the release year: ");
                year = int.Parse(Console.ReadLine());    
            }
            int month;
            try{
                Console.WriteLine("Please enter the release month");
                month = int.Parse(Console.ReadLine());   
            }
            catch(Exception e){
                movieLog.Error(e.Message+"Please enter a valid month");
                Console.WriteLine("Please enter the release month");
                month = int.Parse(Console.ReadLine());    
            }
            int day;
            try{
                Console.WriteLine("Please enter the release day");
                day = int.Parse(Console.ReadLine());
            }
            catch(Exception e){
                movieLog.Error(e.Message+"Please enter a valid day");
                Console.WriteLine("Please enter the release day");
                day = int.Parse(Console.ReadLine());    
            }
            int id;
            // Makes new Movie object using user input.
            using (var db = new MovieContext()){
                id = db.Movies.Count()+1;
                Movie movie = new Movie(){
                    Title = title,
                    ReleaseDate = new DateTime(year,month,day)
                };
                //Adds the new movie to the database.
                db.Movies.Add(movie);
                List<Genre>  genres = db.Genres.ToList();
                //Prompts the user to enter the genre of the movie.
                Console.WriteLine("\nPlease enter the genres of the movie");
                // Loops through the list of genres and prompts the user to enter the genre of the movie.
                Console.WriteLine("Genres are: ");
                foreach(Genre genre in genres){
                    Console.Write($"{genre.Id} {genre.Name}, ");
                }
                //Makes a new list of genres.
                List<long> newGenres= new List<long>();
                long tempHold;
                //Loops allowing user to enter multiple genres until stopped by user.
                for(int j=0; j>-1; j++){
                    try{
                        Console.WriteLine("Please Enter Movie Genres type 0 to stop.");
                        tempHold = long.Parse(Console.ReadLine());
                    }
                    catch(Exception e){
                        movieLog.Error(e.Message+"Please enter a valid genre");
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
                //Adds the genres to the movie.
                foreach(long genre in newGenres){
                    Genre GENRES = db.Genres.Find(genre);
                    MovieGenre mGenres = new MovieGenre(){
                        Movie = movie,
                        Genre = GENRES
                    };
                    db.MovieGenres.Add(mGenres);
                }
                db.SaveChanges();
                //Asks what the user id is.
                long userId;
                try{
                    Console.WriteLine($"Please enter your user id from 1 to {db.Users.Count()}: ");
                    userId = long.Parse(Console.ReadLine());
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid id");
                    Console.WriteLine($"Please enter your user id from 1 to {db.Users.Count()}: ");
                    userId = long.Parse(Console.ReadLine());
                }
                User tempUser = db.Users.Find(userId);
                long rating;
                //Asks what they rate the movie.
                try{
                    Console.WriteLine($"Please enter the rating you would like to give the movie from 1 to 5: ");
                    rating = long.Parse(Console.ReadLine());
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid option");
                    Console.WriteLine($"Please enter the rating you would like to give the movie from 1 to 5: ");
                    rating = long.Parse(Console.ReadLine());
                }
                //Adds the rating to the movie.
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
           //Searches for movie by name.
            using (var db = new MovieContext()){
                string title;
                try{
                    Console.WriteLine($"Please enter the name of the movie you would like to search for: ");
                    title = Console.ReadLine();
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid option");
                    Console.WriteLine($"Please enter the name of the movie you would like to search for: ");
                    title = Console.ReadLine();
                }
                List<Movie> movies = db.Movies.ToList();
                List<Movie> moviesToDelete = new List<Movie>();
                foreach(Movie MOVIE in movies){
                    if(MOVIE.Title.ToLower().Contains(title.ToLower())){
                        moviesToDelete.Add(MOVIE);
                        Console.WriteLine($"{MOVIE.Title}");
                    }
                }
                //Returns results that are similar to the search term.
                //Asks the user what movie they would like to delete.
                int movieToDelete;
                try{
                    Console.WriteLine($"Please enter the number of the movie you would like to delete from 1 to {moviesToDelete.Count()}: ");
                    movieToDelete = int.Parse(Console.ReadLine());
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid option");
                    Console.WriteLine($"Please enter the number of the movie you would like to delete from 1 to {moviesToDelete.Count()}: ");
                    movieToDelete = int.Parse(Console.ReadLine());
                }
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
            //Searches for movie by name.
            using (var db = new MovieContext()){
                string title;
                try{
                    Console.WriteLine($"Please enter the name of the movie you would like to search for: ");
                    title = Console.ReadLine();
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid option");
                    Console.WriteLine($"Please enter the name of the movie you would like to search for: ");
                    title = Console.ReadLine();
                }
                List<Movie> movies = db.Movies.ToList();
                List<Movie> moviesToUpdate = new List<Movie>();
                foreach(Movie MOVIE in movies){
                    if(MOVIE.Title.ToLower().Contains(title.ToLower())){
                        moviesToUpdate.Add(MOVIE);
                        Console.WriteLine($"{MOVIE.Title}");
                    }
                }
                //Asks which movie they would like to update
                int movieToUpdate;
                try{
                    Console.WriteLine($"Please enter the number of the movie you would like to update from 1 to {moviesToUpdate.Count()}: ");
                    movieToUpdate = int.Parse(Console.ReadLine());
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid option");
                    Console.WriteLine($"Please enter the number of the movie you would like to update from 1 to {moviesToUpdate.Count()}: ");
                    movieToUpdate = int.Parse(Console.ReadLine());
                }
                Movie movie = moviesToUpdate[movieToUpdate-1];
                //Asks the user what they would like to update.
                string choice;
                try{
                    Console.WriteLine("What would you like to update? 1. Title 2. Release Date 3. Genres 4. All");
                    choice = Console.ReadLine();
                }
                catch(Exception e){
                    Console.WriteLine("Please enter a valid option");
                    Console.WriteLine("What would you like to update? 1. Title 2. Release Date 3. Genres 4. All");
                    choice = Console.ReadLine();
                }
                //If the user selects 1, the user is prompted to enter the new Title.
                if(choice=="1"){
                    string newTitle;
                    try{
                        Console.WriteLine("Please enter the movie title: ");
                        newTitle = Console.ReadLine();
                    }
                    catch(Exception e){
                        movieLog.Error(e.Message+"Please enter a valid title");
                        Console.WriteLine("Please enter the movie title: ");
                        newTitle = Console.ReadLine();
                    }
                    movie.Title = newTitle;
                    db.SaveChanges();
                }
                //If the user selects 2, the user is prompted to enter the new Release Date.
                else if(choice=="2"){
                    int year;
                    try{
                        Console.WriteLine("Please enter the release year: ");
                        year = int.Parse(Console.ReadLine());  
                    }
                    catch(Exception e){
                        movieLog.Error(e.Message+"Please enter a valid year");
                        Console.WriteLine("Please enter the release year: ");
                        year = int.Parse(Console.ReadLine());    
                    }
                    int month;
                    try{
                        Console.WriteLine("Please enter the release month");
                        month = int.Parse(Console.ReadLine());   
                    }
                    catch(Exception e){
                        movieLog.Error(e.Message+"Please enter a valid month");
                        Console.WriteLine("Please enter the release month");
                        month = int.Parse(Console.ReadLine());    
                    }
                    int day;
                    try{
                        Console.WriteLine("Please enter the release day");
                        day = int.Parse(Console.ReadLine());
                    }
                    catch(Exception e){
                        movieLog.Error(e.Message+"Please enter a valid day");
                        Console.WriteLine("Please enter the release day");
                        day = int.Parse(Console.ReadLine());    
                    }
                    movie.ReleaseDate = new DateTime(year, month, day);
                    db.SaveChanges();
                }
                //If the user selects 3, the user is prompted to enter the new Genres.
                else if(choice=="3"){
                    List<Genre>  genres = db.Genres.ToList();
                    //Prompts the user to enter the genre of the movie.
                    Console.WriteLine("\nPlease enter the genres of the movie");
                    // Loops through the list of genres and prompts the user to enter the genre of the movie.
                    Console.WriteLine("Genres are: ");
                    foreach(Genre genre in genres){
                        Console.Write($"{genre.Id} {genre.Name}, ");
                    }
                    //Makes a new list of genres.
                    List<long> newGenres= new List<long>();
                    long tempHold;
                    //Loops allowing user to enter multiple genres until stopped by user.
                    for(int j=0; j>-1; j++){
                        try{
                        Console.WriteLine("Please Enter Movie Genres type 0 to stop.");
                        tempHold = long.Parse(Console.ReadLine());
                        }
                        catch(Exception e){
                            movieLog.Error(e.Message+"Please enter a valid genre");
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
                    //Adds the genres to the movie.
                    foreach(long genre in newGenres){
                        Genre GENRES = db.Genres.Find(genre);
                        MovieGenre mGenres = new MovieGenre(){
                            Movie = movie,
                            Genre = GENRES
                        };
                        db.MovieGenres.Add(mGenres);
                    }
                    db.SaveChanges();
                }
                //If the user selects 4, the user is prompted to enter the new information for the movie.
                else if(choice=="4"){
                    string newTitle;
                    try{
                        Console.WriteLine("Please enter the movie title: ");
                        newTitle = Console.ReadLine();
                    }
                    catch(Exception e){
                        movieLog.Error(e.Message+"Please enter a valid title");
                        Console.WriteLine("Please enter the movie title: ");
                        newTitle = Console.ReadLine();
                    }
                    movie.Title = newTitle;
                    int year;
                    try{
                        Console.WriteLine("Please enter the release year: ");
                        year = int.Parse(Console.ReadLine());  
                    }
                    catch(Exception e){
                        movieLog.Error(e.Message+"Please enter a valid year");
                        Console.WriteLine("Please enter the release year: ");
                        year = int.Parse(Console.ReadLine());    
                    }
                    int month;
                    try{
                        Console.WriteLine("Please enter the release month");
                        month = int.Parse(Console.ReadLine());   
                    }
                    catch(Exception e){
                        movieLog.Error(e.Message+"Please enter a valid month");
                        Console.WriteLine("Please enter the release month");
                        month = int.Parse(Console.ReadLine());    
                    }
                    int day;
                    try{
                        Console.WriteLine("Please enter the release day");
                        day = int.Parse(Console.ReadLine());
                    }
                    catch(Exception e){
                        movieLog.Error(e.Message+"Please enter a valid day");
                        Console.WriteLine("Please enter the release day");
                        day = int.Parse(Console.ReadLine());    
                    }
                    movie.ReleaseDate = new DateTime(year, month, day);
                    List<Genre>  genres = db.Genres.ToList();
                    //Prompts the user to enter the genre of the movie.
                    Console.WriteLine("\nPlease enter the genres of the movie");
                    // Loops through the list of genres and prompts the user to enter the genre of the movie.
                    Console.WriteLine("Genres are: ");
                    foreach(Genre genre in genres){
                        Console.Write($"{genre.Id} {genre.Name}, ");
                    }
                    //Makes a new list of genres.
                    List<long> newGenres= new List<long>();
                    long tempHold;
                    //Loops allowing user to enter multiple genres until stopped by user.
                    for(int j=0; j>-1; j++){
                        try{
                        Console.WriteLine("Please Enter Movie Genres type 0 to stop.");
                        tempHold = long.Parse(Console.ReadLine());
                        }
                        catch(Exception e){
                            movieLog.Error(e.Message+"Please enter a valid genre");
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
                    //Adds the genres to the movie.
                    foreach(long genre in newGenres){
                        Genre GENRES = db.Genres.Find(genre);
                        MovieGenre mGenres = new MovieGenre(){
                            Movie = movie,
                            Genre = GENRES
                        };
                        db.MovieGenres.Add(mGenres);
                    }
                    db.SaveChanges();
                }
            }
        }

        public static void SearchMovie(){
            //Searches for movie by name.
            using (var db = new MovieContext()){
                string title;
                try{
                    Console.WriteLine($"Please enter the name of the movie you would like to search for: ");
                    title = Console.ReadLine();
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid option");
                    Console.WriteLine($"Please enter the name of the movie you would like to search for: ");
                    title = Console.ReadLine();
                }
                List<Movie> movies = db.Movies.ToList();
                //Finds the movies by name.
                //Displays the movie details.
                foreach(Movie movie in movies){
                    if(movie.Title.ToLower().Contains(title.ToLower())){
                        long id = movie.Id;
                        Console.WriteLine($"\n{movie.Title} was released on {movie.ReleaseDate}");
                        Console.WriteLine("Genres are: ");
                        db.Movies.Where(m => m.Id == id).FirstOrDefault().MovieGenres.ToList().ForEach(mg => Console.Write($"{mg.Genre.Name} "));
                    }
                }        
                
            }
        }

        public static void DisplayAll(){
            //Displays all movies.
            using (var db = new MovieContext()){
                List<Movie> movies = db.Movies.ToList();
                //Displays the movie details.
                int counter =0;
                foreach(Movie movie in movies){
                    long id = movie.Id;
                    if(counter==100){
                        //After reaching 100 movies, the user is prompted to continue.
                        string choice;
                        try{
                            Console.WriteLine("\nWould You like to see more? y/n");
                            choice = Console.ReadLine();
                        }
                        catch(Exception e){
                            movieLog.Error(e.Message+"Please enter a valid option");
                            choice = Console.ReadLine();    
                        }
                        if(choice=="y"){
                            counter=0;
                        }
                        else if (choice=="n"){
                            break;
                        }
                    }
                    else{
                        Console.WriteLine($"\n{movie.Title} was released on {movie.ReleaseDate}");
                        Console.WriteLine("Genres are: ");
                        db.Movies.Where(m => m.Id == id).FirstOrDefault().MovieGenres.ToList().ForEach(mg => Console.Write($"{mg.Genre.Name} "));
                    }
                    counter++;
                }
            }
        }

        public static void AddUser(){
            using (var db = new MovieContext()){
                long age;
                try{
                    Console.WriteLine($"Please enter the age of the user you would like to add: ");
                    age = int.Parse(Console.ReadLine());
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid option");
                    Console.WriteLine($"Please enter the age of the user you would like to add: ");
                    age = int.Parse(Console.ReadLine());    
                }
                string gender;
                try{
                    Console.WriteLine($"Please enter the gender of the user you would like to add: ");
                    gender = Console.ReadLine();
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid option");
                    Console.WriteLine($"Please enter the gender of the user you would like to add: ");
                    gender = Console.ReadLine();    
                }
                string zipCode;
                try{
                    Console.WriteLine($"Please enter the Zip Code of the user you would like to add: ");
                    zipCode = Console.ReadLine();
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid option");
                    Console.WriteLine($"Please enter the Zip Code of the user you would like to add: ");
                    zipCode = Console.ReadLine();    
                }
                List<Occupation> occupations = db.Occupations.ToList();
                //Prompts the user to enter their occupation of the movie.
                Console.WriteLine("\nPlease enter the occupation of the movie");
                // Loops through the list of occupations and prompts the user to enter their occupation.
                Console.WriteLine("Occupations are: ");
                foreach(Occupation occupation in occupations){
                    Console.Write($"{occupation.Id} {occupation.Name}, ");
                }
                int tempHold;
                try{
                    Console.WriteLine("\nPlease enter the occupation of the user: ");
                    tempHold = int.Parse(Console.ReadLine());
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid option");
                    Console.WriteLine("Please enter the occupation of the user: ");
                    tempHold = int.Parse(Console.ReadLine());    
                }
                //Adds the user to the database.
                User user = new User(){
                    Age = age,
                    Gender = gender,
                    ZipCode = zipCode,
                    Occupation = occupations[tempHold-1]
                };
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public static void AddRating(){
            using (var db = new MovieContext()){
                string title;
                try{
                    Console.WriteLine($"Please enter the name of the movie you would like to search for: ");
                    title = Console.ReadLine();
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid option");
                    Console.WriteLine($"Please enter the name of the movie you would like to search for: ");
                    title = Console.ReadLine();
                }
                List<Movie> movies = db.Movies.ToList();
                List<Movie> moviesToDelete = new List<Movie>();
                foreach(Movie MOVIE in movies){
                    if(MOVIE.Title.ToLower().Contains(title.ToLower())){
                        moviesToDelete.Add(MOVIE);
                        Console.WriteLine($"{MOVIE.Title}");
                    }
                }
                int movieToDelete;
                try{
                    Console.WriteLine($"Please enter the number of the movie you would like to add a rating for from 1 to {moviesToDelete.Count()}: ");
                    movieToDelete = int.Parse(Console.ReadLine());
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid option");
                    Console.WriteLine($"Please enter the number of the movie you would like to add a rating for from 1 to {moviesToDelete.Count()}: ");
                    movieToDelete = int.Parse(Console.ReadLine());
                }
                Movie movie = moviesToDelete[movieToDelete-1];
                long userId;
                try{
                    Console.WriteLine($"Please enter your user id from 1 to {db.Users.Count()}: ");
                    userId = long.Parse(Console.ReadLine());
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid option");
                    Console.WriteLine($"Please enter your user id from 1 to {db.Users.Count()}: ");
                    userId = long.Parse(Console.ReadLine());
                }
                User tempUser = db.Users.Find(userId);
                long rating;
                try{
                    Console.WriteLine($"Please enter the rating you would like to give the movie from 1 to 5: ");
                    rating = long.Parse(Console.ReadLine());
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid option");
                    Console.WriteLine($"Please enter the rating you would like to give the movie from 1 to 5: ");
                    rating = long.Parse(Console.ReadLine());
                }
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

        public static void ListTopMovie(){
            using (var db = new MovieContext()){
                string choice;
                try{
                    Console.WriteLine("Would you like to list the top rated movies by user age or by occupation? Enter 1 or 2: ");
                    choice = Console.ReadLine();
                }
                catch(Exception e){
                    movieLog.Error(e.Message+"Please enter a valid option");
                    Console.WriteLine("Would you like to list the top rated movies by user age or by occupation? Enter 1 or 2: ");
                    choice = Console.ReadLine();
                }
                if(choice == "1"){
                    int age;
                    try{
                        Console.WriteLine("Please enter the decade of the user you would like to search for (10,20,30,40,... etc): ");
                        age = int.Parse(Console.ReadLine());
                    }
                    catch(Exception e){
                        movieLog.Error(e.Message+"Please enter a valid option");
                        Console.WriteLine("Please enter the decade of the user you would like to search for (10,20,30,40,... etc): ");
                        age = int.Parse(Console.ReadLine());
                    }
                    List<User> users = db.Users.ToList();
                    List<User> ageUsers = new List<User>();
                    foreach(User user in users){
                        if(user.Age>=age && user.Age<age+10){
                            ageUsers.Add(user);
                        }
                    }
                    Console.WriteLine(ageUsers.Count());
                    // List<UserMovie> userMovies = db.UserMovies.ToList();
                    // List<UserMovie> specificReviews = new List<UserMovie>();
                    // foreach(User user in ageUsers){
                    //     foreach(UserMovie userMovie in userMovies){
                    //         if(userMovie.User == user){
                    //             specificReviews.Add(userMovie);
                    //         }
                    //     }
                    // }
                    // List<Movie> movies = db.Movies.ToList();
                    // List<Movie> topRatedMovies = new List<Movie>();
                    // foreach(Movie movie in movies){
                    //     foreach(UserMovie userMovie in specificReviews){
                    //         if(userMovie.Movie == movie){
                    //             topRatedMovies.Add(movie);
                    //             break;
                    //         }
                    //     }
                    // }
                    // List<long> ratings = new List<long>();
                    // int i=0;
                    // foreach(Movie movie in topRatedMovies){
                    //     ratings.Add(0);
                    //     foreach(UserMovie userMovie in specificReviews){
                    //         if(userMovie.Movie == movie){
                    //             ratings[i] += userMovie.Rating;
                    //         }
                    //     }
                    //     i++;
                    // }
                    // long maxNum = ratings.Max(z => z);
                    // Console.WriteLine(maxNum);
                }
                else if(choice == "2"){
                    try{
                    
                    }
                    catch(Exception e){
                        movieLog.Error(e.Message+"Please enter a valid option");
                            
                    }
                }
            }
        }

    }
}