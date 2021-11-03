namespace Abstract_Class___In_Class_work
{
    public class Dodge : Vehicle
    {
        public override string Make { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override void Drive()
        {
            System.Console.WriteLine("You are driving a dodge.");
        }

        public override void Sell()
        {
            System.Console.WriteLine("You have sold your dodge!");
        }
    }
}