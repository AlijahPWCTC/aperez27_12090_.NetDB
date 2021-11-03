using System;

namespace Abstract_Class___In_Class_work
{
    class Program
    {
        static void Main(string[] args)
        {
            Dodge d = new Dodge();
            ford f = new ford();
            d.Sell();
            f.Sell();
            d.buy();
            f.buy();
            
        }
    }
}
