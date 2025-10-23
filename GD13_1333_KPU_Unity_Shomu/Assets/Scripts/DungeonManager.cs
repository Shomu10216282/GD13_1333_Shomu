using UnityEngine;
using System;

namespace GD13_1333_Shomu.Scripts
{
    internal class DungeonManager
    {
        private Map map;
        private Player player;

        public void StartDungeon(Player player)
        {
            Map map = new Map();
            Console.WriteLine("\n=== Dungeon Exploration Begins ===");

            while (player.Health > 0)
            {
                map.DrawMap();
                Room? current = map.GetCurrentRoom();
                if (current == null)
                {
                    Console.WriteLine("\nThere is no current room! Ending dungeon.");
                    break;
                }

                Console.WriteLine($"\nYou are at: {current.Name} (Visited: {current.Visited}, Cleared: {current.IsCleared})");

                Console.WriteLine("\nWhat do you want to do?");
                Console.WriteLine("1. Explore the room");
                Console.WriteLine("2. Use an item");
                Console.WriteLine("3. Move (N/S/E/W)");
                Console.WriteLine("4. Show inventory");
                Console.Write("→ ");
                string choice = (Console.ReadLine() ?? "").Trim();

                if (choice == "1")
                {
                    current.Enter(player);
                }
                else if (choice == "2")
                {
                    if (player.Inventory.Count == 0)
                    {
                        Console.WriteLine("You have no items to use.");
                    }
                    else
                    {
                        player.ShowInventory();
                        Console.Write("Enter item number to use (or Enter to cancel): ");
                        string s = Console.ReadLine() ?? "";
                        if (!string.IsNullOrWhiteSpace(s) && int.TryParse(s, out int itemIdx))
                        {
                            player.UseItemByIndex(itemIdx);
                        }
                        else
                        {
                            Console.WriteLine("No item used.");
                        }
                    }
                }
                else if (choice == "3")
                {
                    Console.Write("Enter direction (N/S/E/W): ");
                    string dir = (Console.ReadLine() ?? "").Trim().ToUpper();
                    if (map.TryMove(dir))
                    {
                        Console.WriteLine($"You move {dir}.");
                    }
                    else
                    {
                        Console.WriteLine("You can't move that way.");
                    }
                }
                else if (choice == "4")
                {
                    player.ShowInventory();
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }

                if (player.Health <= 0)
                {
                    Console.WriteLine("\nYou have fallen in the dungeon... Game Over.");
                    break;
                }

                bool anyEnemy = map.AnyUnclearedEncounters();
                if (!anyEnemy)
                {
                    Console.WriteLine("\nAll enemies have been defeated! You cleared the dungeon!");
                    break;
                }
            }

            Console.WriteLine("\n=== Dungeon Exploration Ended ===");
        }
    }
}
