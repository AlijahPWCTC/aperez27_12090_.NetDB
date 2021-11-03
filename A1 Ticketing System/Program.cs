using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System;
using System.IO;

namespace A1_Ticketing_System
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("Ticket.csv");
            Console.WriteLine("Current Tickets");
            for (int i=1; i<lines.Length; i++){
                Console.WriteLine(lines[i]);
            }
            String nametemp = lines[0];

            String[] firstArr = arrMaker(nametemp);
  
            String[] secondArr = new String[7];
  
            String tempHold;
  
            for (int i=0; i<secondArr.Length; i++){
                if (i==6){
                    for(int j=0; j>-1; j++){
                        Console.WriteLine("Please Enter "+firstArr[6]+" type END to stop.");
                        tempHold = Console.ReadLine();
                        if (tempHold=="END"){
                            break;
                        }
                        else if (j==0){
                            secondArr[6]=tempHold;
                        }
                        else {
                            secondArr[6]+= " | "+tempHold;
                        }
                    }
                }
                else{
                    Console.WriteLine("Please Enter "+firstArr[i]);
                    secondArr[i] = Console.ReadLine();
                }
            }
            String secondLine ="";
            for (int i=0; i<secondArr.Length; i++){
               if (i==6){
                   secondLine+=(secondArr[i]);
               }
               else{
                    secondLine+=(secondArr[i]+",");
               }
            }
            using (StreamWriter sw =File.AppendText("Ticket.csv")){
                sw.WriteLine();
                sw.WriteLine(secondLine);
            }
        }
        
        public static String[] arrMaker (String nametemp){
            String[] arr= new String[7];
            for (int i=0; i<7; i++){
                if (i==6){
                    arr[i]=nametemp.Substring(0);
                }
                else{
                    arr[i]=nametemp.Substring(0,(nametemp.IndexOf(",")));
                    nametemp =nametemp.Substring(nametemp.IndexOf(",")+1);
                }
            }
            return arr;
        }
    }
}
