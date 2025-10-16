using System;
using UnityEngine;


namespace GD13_1333_Shomu.Scripts
{
    internal class TreasureRoom : Room
    {
        public TreasureRoom() : base("Treasure Room") { }

        public override void OnRoomSearched(Player player)
        {
            if (IsCleared)
            {
                Console.WriteLine("Nothing left here.");
                return;
            }

            Console.WriteLine("You found a treasure chest!");
            System.Random rnd = new System.Random();
            int reward = rnd.Next(1, 3);

            if (reward == 1)
            {
                Console.WriteLine("You found a special die!");
                player.AddDie(); 
            }
            else
            {
                Console.WriteLine("You found a healing potion!");
                player.Heal(10);
            }

            IsCleared = true;
        }
    }
}
