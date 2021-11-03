using System;
using System.Collections.Generic;
using System.IO;

namespace A6___Movie_Library_with_Abstract_Classes
{
    public class shows: media
    {
        int showID{get;set;}
        string title {get;set;}
        int season{get;set;}
        int episode{get;set;}
        string[] writers {get;set;}

        public shows(int id, string Title, int Season, int Episode, string[] array)
        {
            showID=id;
            title=Title;
            season=Season;
            episode=Episode;
            writers=array;
        }
        public static void read(List<shows> showList)
        {
            using(StreamReader sr = new StreamReader("shows.csv")){
                sr.ReadLine();
                while(!sr.EndOfStream){
                    string line = sr.ReadLine();
                    int inx = line.IndexOf('"');            
                    if(inx == -1){
                        string[] temp =line.Split(',');
                        temp[4]=temp[4].Replace("|",",.");
                        string[] tempTwo = temp[4].Split('.');
                        shows test = new shows(int.Parse(temp[0]),temp[1],int.Parse(temp[2]),int.Parse(temp[3]),tempTwo);
                        showList.Add(test);
                    }
                    else{
                        int tempID=(int.Parse(line.Substring(0, inx-1)));
                        line = line.Substring(inx+1);
                        inx = line.IndexOf('"');
                        string tempTitle = (line.Substring(0, inx));
                        line = line.Substring(inx+2);
                        int tempSeason = (int.Parse(line.Substring(0, inx-1)));
                        line = line.Substring(inx+1);
                        int tempEpisode = (int.Parse(line.Substring(0, inx-1)));
                        line = line.Substring(inx+1);
                        string[] tempArray= (line.Replace("|",",")).Split(',');
                        shows test = new shows(tempID,tempTitle,tempSeason,tempEpisode,tempArray);
                        showList.Add(test);
                    }
                }
            }
        }
        public override void display()
        {
            string holder = writers[0];
            for(int i=1; i<writers.Length; i++){
               holder+= $" {writers[i]}";
            }
            Console.WriteLine($"ID: {this.showID} Title: {this.title} Season: {this.season} Episode: {this.episode} Writers: {holder}");
        }
        public static void displayAll(List<shows> showList)
        {
            for (int i=0; i<showList.Count; i++){
                showList[i].display();
            }
        }
        public static void addShow(List<shows> showList){
            String newTitle = "";
            try{
                Console.WriteLine("Enter Show Title: ");
                newTitle = Console.ReadLine();
            }
            catch(Exception e){
                Console.WriteLine($"{e} error please try again.");
                Console.WriteLine("Enter Show Title: ");
                newTitle = Console.ReadLine();
            }
            int newSeason;
            try{
                Console.WriteLine("Enter Season Number: ");
                newSeason = int.Parse(Console.ReadLine());
            }
            catch(Exception e){
                Console.WriteLine($"{e} error please try again.");
                Console.WriteLine("Enter Season Number: ");
                newSeason = int.Parse(Console.ReadLine());
            }
            int newEpisode;
            try{
                Console.WriteLine("Enter Episode Number: ");
                newEpisode = int.Parse(Console.ReadLine());
            }
            catch(Exception e){
                Console.WriteLine($"{e} error please try again.");
                Console.WriteLine("Enter Episode Number: ");
                newEpisode = int.Parse(Console.ReadLine());
            }
            
            for (int i=0; i<showList.Count; i++){
                    if(showList[i].title.Equals(newTitle)==true){
                        if(showList[i].season.Equals(newSeason)==true){
                            if(showList[i].episode.Equals(newEpisode)==true){
                                i=-1;
                                Console.WriteLine("This is not a suitable entry.");
                                try{
                                    Console.WriteLine("Enter Different Show Season: ");
                                    newSeason = int.Parse(Console.ReadLine());
                                }
                                catch(Exception e){
                                    Console.WriteLine($"{e} error please try again.");
                                    Console.WriteLine("Enter Different Show Title: ");
                                    newTitle = Console.ReadLine();
                                }
                                try{
                                    Console.WriteLine("Enter Episode Number: ");
                                    newEpisode = int.Parse(Console.ReadLine());
                                }
                                catch(Exception e){
                                    Console.WriteLine($"{e} error please try again.");
                                    Console.WriteLine("Enter Episode Number: ");
                                    newEpisode = int.Parse(Console.ReadLine());
                                }
                            }
                        }
                        
                    }
                }
            List<String> newWriters= new List<string>();
            String tempHold="";
            for(int j=0; j>-1; j++){
                
                try{
                    Console.WriteLine("Please Enter Writers type END to stop.");
                    tempHold = Console.ReadLine();
                }
                catch(Exception e){
                    Console.WriteLine($"{e} error please try again.");
                    Console.WriteLine("Please Enter Writers type END to stop.");
                    tempHold = Console.ReadLine();
                }
                        
                if (tempHold.Equals("END", StringComparison.OrdinalIgnoreCase)){
                    break;
                }
                else {
                    newWriters.Add(tempHold);
                }
            }
            String writerFile =newWriters[0];
            for(int i=1; i<newWriters.Count; i++){
                writerFile+=$"|{newWriters[i]}";
            }
            string[] writersArray = newWriters.ToArray();
            int newIds = (showList[showList.Count-1].showID+1);
            shows tempShow = new shows(newIds, newTitle, newSeason, newEpisode, writersArray);
            showList.Add(tempShow);
            String newEntry = $"{newIds},{newTitle},{newSeason},{newEpisode},{writerFile}";
            using (StreamWriter sw =File.AppendText("shows.csv")){
                            sw.WriteLine(newEntry);
            }
        }
        public static void showOptions(List<shows> showList){
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
                        Console.WriteLine($"There are a total of {showList.Count} entries.");
                        Console.WriteLine($"Type 0 to display all or type anywhere from 1 to {showList.Count} to display a specific entry: ");
                        choice1= int.Parse(Console.ReadLine());
                    }
                    catch(Exception e){
                        Console.WriteLine($"{e} error please try again.");
                        Console.WriteLine($"There are a total of {showList.Count} entries.");
                        Console.WriteLine($"Type 0 to display all or type anywhere from 1 to {showList.Count} to display a specific entry: ");
                        choice1= int.Parse(Console.ReadLine());
                    }
                    if(choice1==0){
                        shows.displayAll(showList);
                    }
                    else{
                        showList[choice1-1].display();
                    }
                }
                else if (choice==2){
                    shows.addShow(showList);
                }
            }
        }
    }
        
}
