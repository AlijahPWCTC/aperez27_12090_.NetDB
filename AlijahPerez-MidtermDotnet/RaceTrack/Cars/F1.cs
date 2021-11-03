using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceTrack.RaceTrack.Cars
{
    public class F1 : RaceCar
    {
        public F1()
        {
            Name = "F1";
            TopSpeed = 180;
        }

        public override void StartEngine()
        {
            Console.WriteLine($"The {Name} readys its engine!");
        }
    }
}