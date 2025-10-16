using System;
using UnityEngine;


namespace GD13_1333_Shomu.Scripts
{
    internal abstract class Room
    {
        public string RoomName { get; protected set; }
        public bool IsCleared { get; protected set; }

        public Room(string name)
        {
            RoomName = name;
            IsCleared = false;
        }

        public virtual void OnRoomEntered(Player player)
        {
            Console.WriteLine($"You entered the {RoomName}.");
        }

        public abstract void OnRoomSearched(Player player);

        public virtual void OnRoomExit(Player player)
        {
            Console.WriteLine($"You left the {RoomName}.\n");
        }
    }
}
