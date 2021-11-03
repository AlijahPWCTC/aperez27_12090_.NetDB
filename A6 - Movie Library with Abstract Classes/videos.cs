using System;
using System.Collections.Generic;
using System.IO;

namespace A6___Movie_Library_with_Abstract_Classes
{
    public class videos: media
    {
        int videoID{get;set;}
        string title {get;set;}
        string format{get;set;}
        int length{get;set;}
        int[] region {get;set;}
        public videos(int id, string Title, string Format, int Length, int[] array)
        {
            videoID=id;
            title=Title;
            format=Format;
            length=Length;
            region=array;
        }

        

        public static void read(List<videos> videosList)
        {
            using(StreamReader sr = new StreamReader("videos.csv")){
                sr.ReadLine();
                while(!sr.EndOfStream){
                    string line = sr.ReadLine();
                    int inx = line.IndexOf('"');            
                    if(inx == -1){
                        string[] temp =line.Split(',');
                        temp[4]=temp[4].Replace("|",",");
                        string[] tempTwo = temp[4].Split(',');
                        int[] tempThree = Array.ConvertAll(tempTwo, s => int.Parse(s));
                        videos test = new videos(int.Parse(temp[0]),temp[1],temp[2],int.Parse(temp[3]),tempThree);
                        videosList.Add(test);
                    }
                    else{
                        int tempID=(int.Parse(line.Substring(0, inx-1)));
                        line = line.Substring(inx+1);
                        inx = line.IndexOf('"');
                        string tempTitle = (line.Substring(0, inx));
                        line = line.Substring(inx+2);
                        string tempFormat = (line.Substring(0, inx-1));
                        line = line.Substring(inx+1);
                        int tempLength = (int.Parse(line.Substring(0, inx-1)));
                        line = line.Substring(inx+1);
                        string[] tempArray= (line.Replace("|",",")).Split(',');
                        int[] tempThree = Array.ConvertAll(tempArray, s => int.Parse(s));
                        videos test = new videos(tempID,tempTitle,tempFormat,tempLength,tempThree);
                        videosList.Add(test);
                    }
                }
            }
        }
        public override void display()
                {
                    string holder = $"{(region[0])}";
                    for(int i=1; i<region.Length; i++){
                    holder+= $" {region[i]}";
                    }
                    Console.WriteLine($"ID: {this.videoID} Title: {this.title} Season: {this.format} Episode: {this.length} Region: {holder}");
                }
        public static void displayAll(List<videos> videosList)
        {
            for (int i=0; i<videosList .Count; i++){
                videosList[i].display();
            }
        }
        public static void addVideo(List<videos> videosList){
                String newTitle = "";
            try{
                Console.WriteLine("Enter Video Title: ");
                newTitle = Console.ReadLine();
            }
            catch(Exception e){
                Console.WriteLine($"{e} error please try again.");
                Console.WriteLine("Enter Video Title: ");
                newTitle = Console.ReadLine();
            }
                for (int i=0; i<videosList.Count; i++){
                    if(videosList[i].title.Equals(newTitle)==true){
                        i=-1;
                        Console.WriteLine("This is not a suitable entry.");
                        try{
                            Console.WriteLine("Enter Different Enter Video Title: ");
                            newTitle = Console.ReadLine();
                        }
                        catch(Exception e){
                            Console.WriteLine($"{e} error please try again.");
                            Console.WriteLine("Enter Different Enter Video Title: ");
                            newTitle = Console.ReadLine();
                        }
                        
                    }
                }
                string newformat="";
                string temphold="";
                for(int j=0; j>-1; j++){
                
                try{
                    Console.WriteLine("Please Enter Formats type END to stop.");
                    temphold = Console.ReadLine();
                }
                catch(Exception e){
                    Console.WriteLine($"{e} error please try again.");
                    Console.WriteLine("Please Enter Formats type END to stop.");
                    temphold = Console.ReadLine();
                }
                        
                if (temphold.Equals("END", StringComparison.OrdinalIgnoreCase)){
                    break;
                }
                else if (j==0){
                    newformat=temphold;
                }
                else {
                    newformat+= "|"+temphold;
                }
            }
                int newLength;
                try{
                    Console.WriteLine("Enter Length: ");
                    newLength = int.Parse(Console.ReadLine());
                }
                catch(Exception e){
                    Console.WriteLine($"{e} error please try again.");
                    Console.WriteLine("Enter Length: ");
                    newLength = int.Parse(Console.ReadLine());
                }
                
                List<int> newRegion= new List<int>();
                int tempHold;
                for(int j=0; j>-1; j++){
                    try{
                        Console.WriteLine("Please Enter Video Regions type -1 to stop.");
                        tempHold = int.Parse(Console.ReadLine());
                    }
                    catch(Exception e){
                        Console.WriteLine($"{e} error please try again.");
                        Console.WriteLine("Please Enter Video Regions type -1 to stop.");
                        tempHold = int.Parse(Console.ReadLine());
                    }
                            
                    if (tempHold==-1){
                        break;
                    }
                    else {
                        newRegion.Add(tempHold);
                    }
                }
                string regionFile =$"{newRegion[0]}";
                for(int i=1; i<newRegion.Count; i++){
                    regionFile+=$"|{newRegion[i]}";
                }
                int[] reigionArray = newRegion.ToArray();
                int newIds = (videosList[videosList.Count-1].videoID+1);
                videos tempVideos = new videos(newIds, newTitle, newformat, newLength, reigionArray);
                videosList.Add(tempVideos);
                String newEntry = $"{newIds},{newTitle},{newformat},{newLength},{regionFile}";
                using (StreamWriter sw =File.AppendText("videos.csv")){
                                sw.WriteLine(newEntry);
                }
            }
        public static void videoOptions(List<videos> videosList){
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
                        Console.WriteLine($"There are a total of {videosList.Count} entries.");
                        Console.WriteLine($"Type 0 to display all or type anywhere from 1 to {videosList.Count} to display a specific entry: ");
                        choice1= int.Parse(Console.ReadLine());
                    }
                    catch(Exception e){
                        Console.WriteLine($"{e} error please try again.");
                        Console.WriteLine($"There are a total of {videosList.Count} entries.");
                        Console.WriteLine($"Type 0 to display all or type anywhere from 1 to {videosList.Count} to display a specific entry: ");
                        choice1= int.Parse(Console.ReadLine());
                    }
                    if(choice1==0){
                        videos.displayAll(videosList);
                    }
                    else{
                        videosList[choice1-1].display();
                    }
                }
                else if (choice==2){
                    videos.addVideo(videosList);
                }
            }
        }
    }
}