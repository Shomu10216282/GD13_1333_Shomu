using System;
using UnityEngine;


namespace GD13_1333_Shomu.Scripts
{
    internal abstract class Room
    {
        public bool Visited { get; set; } = false;
        public bool IsCleared { get; set; } = false;

        public abstract char MapSymbol { get; }

        public abstract string Name { get; }

        public abstract void Enter(Player player);
    }
}
