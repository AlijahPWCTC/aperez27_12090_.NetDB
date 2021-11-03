namespace In_Class_Interface
{
    public class Peasant : ICivillian
    {
        public string Name {get;set;}
        public int Gold {get;set;}

        public Peasant(){
            Name="Peasant";
            Gold=5;
        }

        public void Gift(ICombatant friendly)
        {
            friendly.Gold+=this.Gold;
            System.Console.WriteLine($"{friendly.Name} was gifted {this.Gold} by {this.Name}");
        }
    }
}