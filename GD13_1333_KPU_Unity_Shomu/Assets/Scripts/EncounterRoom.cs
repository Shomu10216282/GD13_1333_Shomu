using UnityEngine;
using System;

namespace GD13_1333_Shomu.Scripts
{
    internal class EncounterRoom : Room
    {
        private int originalEnemyHP;
        private int enemyHP;
        private bool defeated = false;
        private System.Random rand = new System.Random();

        public EncounterRoom(int enemyHP)
        {
            this.originalEnemyHP = enemyHP;
            this.enemyHP = enemyHP;
        }

        public override char MapSymbol => 'E';
        public override string Name => "Encounter Room";

        public override void Enter(Player player)
        {
            if (defeated)
            {
                Console.WriteLine("This room is quiet; the foe here has already been defeated.");
                Visited = true;
                return;
            }

            Console.WriteLine($"A hostile creature appears! Enemy HP: {enemyHP}");
            DieRoller roller = new DieRoller(6);

            while (enemyHP > 0 && player.Health > 0)
            {
                Console.WriteLine("\nChoose action: 1) Attack  2) Use item");
                Console.Write("> ");
                string action = (Console.ReadLine() ?? "").Trim();

                if (action == "2")
                {
                    if (player.Inventory.Count == 0)
                    {
                        Console.WriteLine("You have no items.");
                    }
                    else
                    {
                        player.ShowInventory();
                        Console.Write("Enter item number to use (or Enter to cancel): ");
                        string inIdx = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(inIdx) && int.TryParse(inIdx, out int idx))
                        {
                            bool used = player.UseItemByIndex(idx);
                            if (used)
                            {

                            }
                        }
                        else
                        {
                            Console.WriteLine("No item used.");
                        }
                    }
                }
                else
                {
                    int dmg = roller.Roll();
                    if (player.EquippedWeapon != null) dmg += player.EquippedWeapon.DamageBonus;
                    Console.WriteLine($"{player.Name} rolls and deals {dmg} damage to the enemy.");
                    enemyHP -= dmg;
                }

                if (enemyHP <= 0)
                {
                    Console.WriteLine("You defeated the enemy!");
                    defeated = true;
                    IsCleared = true;
                    Visited = true;
                    return;
                }

                int enemyAttack = rand.Next(1, 7);
                Console.WriteLine($"Enemy attacks for {enemyAttack} damage.");
                player.TakeDamage(enemyAttack);

                if (player.Health <= 0)
                {
                    Console.WriteLine("You have been slain...");
                    return;
                }
            }
        }
    }
}
