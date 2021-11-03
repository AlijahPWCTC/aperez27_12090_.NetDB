namespace Abstract_Class___In_Class_work
{
    public class ford : Vehicle
    {
        public string color {get;set;}
        public override string Make { get;set; }

        public override void Drive()
        {
            System.Console.WriteLine("The ford is driving");
        }

        public override void Sell()
        {
            System.Console.WriteLine("You have sold your ford!");
        }

        public override void start(){
            System.Console.WriteLine("The ford is starting");
        }

        public override void buy()
        {
            System.Console.WriteLine("You have bought a ford!");
        }

    }
}