namespace In_Class_Interface
{
    public interface ICombatant {
        public Armor Armor  {get;set;}
        public Weapon Weapon {get;set;}
        public string Name { get; set; }
        public double Health {get; set;}
        public int Mana {get;set;}
        public int Gold {get;set;}

        void Attack(ICombatant enemy);
        void Defend(ICombatant enemy);
        void Heal();
        }
}