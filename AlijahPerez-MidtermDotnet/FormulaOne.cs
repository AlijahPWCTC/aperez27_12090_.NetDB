using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RaceTrack.RaceTrack.Cars;
using RaceTrack.RaceTrack.Drivers;

namespace RaceTrack
{
    public class FormulaOne
    {
        public Driver F1driver {get;set;}

        public FormulaOne(){
            F1driver= new Sergio(new F1());
        }

        public void formulaLap(){
            Console.WriteLine($"{F1driver.Name} is preparing for the race.");
            F1driver.Car.StartEngine();
            Console.WriteLine($"{F1driver.Name} takes off!");
            F1driver.Drive();
            Console.WriteLine($"{F1driver.Name} approaches the finish line!");
            F1driver.Car.Brake();
            Console.WriteLine($"{F1driver.Name} finishes a lap!");

        }
    }
}