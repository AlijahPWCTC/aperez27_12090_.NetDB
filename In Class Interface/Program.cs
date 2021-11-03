using System;

namespace In_Class_Interface
{
    class Program
    {
        static void Main(string[] args)
        {
            ICombatant fighter = new Fighter();

            fighter.Armor.defense = 10;

            fighter.Weapon.power = 10;

            var enemy = new Enemy();

            enemy.Armor.defense = 10;

            enemy.Weapon.power = 10;

            fighter.Attack(enemy);

            fighter.Heal();

            ICivillian merchant = new Merchant();
            
            ICivillian peasant = new Peasant();

            merchant.Gift(fighter);
            peasant.Gift(enemy);
        }
    }
}
