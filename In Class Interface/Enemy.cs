namespace In_Class_Interface
{
    public class Enemy : ICombatant
    {
        public string Name {get;set;}
        public Armor Armor  {get;set;}
        public Weapon Weapon {get;set;}
        public double Health {get; set;}
        public int Mana {get; set;}
        public int Gold { get; set; }

        public Enemy(){
            Name = "Enemy";
            Armor = new Armor();
            Weapon = new Weapon();
            Mana = 1;
            Health = 15.0;
        }

        public void Attack(ICombatant enemy){
            if(this.Weapon.power > enemy.Armor.defense){
                System.Console.WriteLine($"{this.Name} attacks {enemy.Name} with {this.Weapon.Name}");
            }
            else{
                System.Console.WriteLine($"{enemy.Name} blocks {this.Name} with {enemy.Armor.Name}");
            }
        }

        public void Defend(ICombatant enemy){

        }

        public void Heal()
        {
            this.Health+=this.Mana;
            System.Console.WriteLine($"{this.Name} healed {this.Mana} to a total of {this.Health}");
        }
    }
}