using System;
using UnityEngine;


namespace GD13_1333_Shomu.Scripts
{
    internal class Map
    {
        int mapSize = 3;

        private Room[,] rooms;
        private int playerX;
        private int playerY;
        private System.Random rnd = new System.Random();

        public Map()
        {

            VisualizeMap();

            //rooms = new Room[3, 3];
            //GenerateMap();
            //playerX = 1;
            //playerY = 1; 
        }

        private void VisualizeMap()
        {
            for (int x = 0; x < mapSize; x++)
            {
                for (int z = 0; z < mapSize; z++)
                {
                    var mapRoomRepresentation = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    mapRoomRepresentation.transform.position = new Vector3(x, 0, z);
                }
            }
        }

        private void GenerateMap()
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    int r = rnd.Next(3);
                    if (r == 0)
                        rooms[y, x] = new NormalRoom();
                    else if (r == 1)
                        rooms[y, x] = new TreasureRoom();
                    else
                        rooms[y, x] = new EncounterRoom();
                }
            }
        }

        public Room GetCurrentRoom() => rooms[playerY, playerX];

        public void Move(string direction)
        {
            switch (direction.ToLower())
            {
                case "n":
                    if (playerY > 0) playerY--; else Console.WriteLine("You can't go further north.");
                    break;
                case "s":
                    if (playerY < 2) playerY++; else Console.WriteLine("You can't go further south.");
                    break;
                case "e":
                    if (playerX < 2) playerX++; else Console.WriteLine("You can't go further east.");
                    break;
                case "w":
                    if (playerX > 0) playerX--; else Console.WriteLine("You can't go further west.");
                    break;
                default:
                    Console.WriteLine("Invalid direction.");
                    break;
            }
        }
    }
}
