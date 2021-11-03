namespace In_Class_Interface
{
    public class Merchant : ICivillian
    {
        public string Name {get;set;}
        public int Gold {get;set;}

        public Merchant(){
            Name="Merchant";
            Gold=20;
        }

        public void Gift(ICombatant friendly)
        {
            friendly.Gold+=this.Gold;
            System.Console.WriteLine($"{friendly.Name} was gifted {this.Gold} by {this.Name}");
        }
    }
}