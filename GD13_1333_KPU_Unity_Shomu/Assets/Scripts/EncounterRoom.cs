using UnityEngine;
using System;

namespace GD13_1333_Shomu.Scripts
{
    internal class EncounterRoom : Room
    {
        public EncounterRoom() : base("Encounter Room") { }

        public override void OnRoomSearched(Player player)
        {
            if (IsCleared)
            {
                Console.WriteLine("You already defeated the enemy.");
                return;
            }

            Console.WriteLine("Enemy appears!");
            GameManager gm = new GameManager();
            gm.PlayBattle(player); 

            IsCleared = true;
        }
    }
}
