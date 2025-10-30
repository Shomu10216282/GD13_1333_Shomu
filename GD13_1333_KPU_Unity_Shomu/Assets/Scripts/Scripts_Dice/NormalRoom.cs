using System;
using UnityEngine;


namespace GD13_1333_Shomu.Scripts
{
    internal class NormalRoom : Room
    {
        public override char MapSymbol => '.';
        public override string Name => "Empty Chamber";

        public override void Enter(Player player)
        {
            if (!Visited)
            {
                System.Console.WriteLine("This room is quiet. You take a moment to rest.");
                Visited = true;
            }
            else
            {
                System.Console.WriteLine("Nothing new here.");
            }
            // mark as cleared when explored
            IsCleared = true;
        }
    }
}