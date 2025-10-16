using UnityEngine;
using System;

namespace GD13_1333_Shomu.Scripts
{
    internal class DungeonManager
    {
        private Map map;
        private Player player;

        public void Start()
        {
            Console.WriteLine("=== Welcome to Dungeon Dice Adventure ===");
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            player = new Player(name);
            map = new Map();

            Console.WriteLine($"Good luck, {player.Name}!\n");

            bool playing = true;
            while (playing)
            {
                Room currentRoom = map.GetCurrentRoom();
                currentRoom.OnRoomEntered(player);

                Console.WriteLine("\nCommands: [n]orth, [s]outh, [e]ast, [w]est, [search], [exit]");
                Console.Write("> ");
                string input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "n":
                    case "s":
                    case "e":
                    case "w":
                        currentRoom.OnRoomExit(player);
                        map.Move(input);
                        break;
                    case "search":
                        currentRoom.OnRoomSearched(player);
                        break;
                    case "exit":
                        playing = false;
                        Console.WriteLine("You decided to leave the dungeon. Game Over.");
                        break;
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }

                if (player.Health <= 0)
                {
                    Console.WriteLine("You have fallen in battle... Game Over.");
                    playing = false;
                }
            }
        }
    }
}
