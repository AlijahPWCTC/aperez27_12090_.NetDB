namespace Abstract_Class___In_Class_work
{
    public abstract class Vehicle
    {
        public abstract string Make {get;set;}

        public string Model {get;set;}

        public abstract void Drive();
       
        public abstract void Sell();

        public virtual void buy(){
            System.Console.WriteLine("You have bought a new vehicle!");
        }

        public virtual void start (){
            System.Console.WriteLine("The Vehicle is starting.");
        }

        public void stop(){
            System.Console.WriteLine("The Vehicle stopped");
        }

        public void drive(){
            System.Console.WriteLine("The vehicle is driving");
        }

    }
}