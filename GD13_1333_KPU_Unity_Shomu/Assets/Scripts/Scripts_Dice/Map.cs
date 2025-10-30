using System;
using UnityEngine;
using static UnityEditor.Progress;


namespace GD13_1333_Shomu.Scripts
{
    internal class Map
    {
        private Room[,] grid;
        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public int PlayerRow { get; private set; }
        public int PlayerCol { get; private set; }

        private System.Random rand = new System.Random();

        public Map()
        {
            Rows = rand.Next(3, 6);
            Cols = rand.Next(3, 6);
            grid = new Room[Rows, Cols];

            GenerateRooms();

            PlayerRow = Rows / 2;
            PlayerCol = Cols / 2;
        }

        private void GenerateRooms()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    int p = rand.Next(100);
                    if (p < 60) grid[r, c] = new NormalRoom();
                    else if (p < 80) grid[r, c] = new TreasureRoom(GenerateRandomItem());
                    else grid[r, c] = new EncounterRoom(rand.Next(8, 26));
                }
            }

            grid[Rows / 2, Cols / 2] = new NormalRoom();
        }

        private Item GenerateRandomItem()
        {
            int p = rand.Next(100);
            if (p < 50) return new Potion("Small Healing Potion", 4, 8);
            else return new Weapon("Rusty Sword", 2);
        }

        public void DrawMap()
        {
            Console.WriteLine($"\nMap ({Rows} x {Cols}):");
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (r == PlayerRow && c == PlayerCol)
                    {
                        Console.Write("[P] ");
                    }
                    else
                    {
                        char ch = grid[r, c].MapSymbol;
                        Console.Write($"[{ch}] ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Legend: P=You, .=Empty, T=Treasure, E=Enemy");
        }

        public Room GetCurrentRoom() => grid[PlayerRow, PlayerCol];

        public bool TryMove(string dir)
        {
            dir = dir.ToUpper();
            int newR = PlayerRow;
            int newC = PlayerCol;
            switch (dir)
            {
                case "N": newR = PlayerRow - 1; break;
                case "S": newR = PlayerRow + 1; break;
                case "E": newC = PlayerCol + 1; break;
                case "W": newC = PlayerCol - 1; break;
                default: return false;
            }
            if (newR < 0 || newR >= Rows || newC < 0 || newC >= Cols)
                return false;

            PlayerRow = newR;
            PlayerCol = newC;
            return true;
        }

        public bool AnyUnclearedEncounters()
        {
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Cols; c++)
                    if (grid[r, c] is EncounterRoom er && !er.IsCleared) return true;
            return false;
        }
    }
}