using System;
using UnityEngine;
using static UnityEditor.Progress;


namespace GD13_1333_Shomu.Scripts
{
    internal class TreasureRoom : Room
    {
        private Item treasure;
        private bool taken = false;

        public TreasureRoom(Item item)
        {
            treasure = item;
        }

        public override char MapSymbol => 'T';
        public override string Name => "Treasure Room";

        public override void Enter(Player player)
        {
            if (taken)
            {
                Console.WriteLine("You search the chest remnants but find nothing left.");
            }
            else
            {
                Console.WriteLine($"You search the room and find: {treasure.Name}!");
                player.AddItem(treasure);
                taken = true;
                IsCleared = true;
            }
            Visited = true;
        }
    }
}
