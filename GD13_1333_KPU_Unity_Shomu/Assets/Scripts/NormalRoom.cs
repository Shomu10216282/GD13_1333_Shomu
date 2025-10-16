using System;
using UnityEngine;


namespace GD13_1333_Shomu.Scripts
{
    internal class NormalRoom : Room
    {
        public NormalRoom() : base("Normal Room") { }

        public override void OnRoomSearched(Player player)
        {
            Console.WriteLine("You look around, but nothing happens.");
            IsCleared = true;
        }
    }
}
