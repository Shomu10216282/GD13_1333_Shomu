using System;
using System.Collections.Generic;
using static GD13_1333_Shomu.Scripts.Player;
using UnityEngine;

namespace GD13_1333_Shomu.Scripts
{
    internal class GameManager : MonoBehaviour
    {
        private Player human;

        private DieRoller dieRoller = new DieRoller();
        private System.Random random = new System.Random();


        //Grid Manager Settings
        [Header("Game Settings")]
        [SerializeField] private GridSetting _gridSetting = null;
        [Header("Environment")]
        //

        private Map gameMap;
        public void Start()
        {
            Debug.Log("GameManager Start");
            gameMap = new Map();
            Debug.Log("GameManager Map Created");

        }


        public void Play()
        {
            Console.WriteLine("=== Welcome to the Dice Dungeon Adventure ===");
            Console.Write("Enter your name: ");
            string name = Console.ReadLine() ?? "Unknown";

            Player player = new Player(name, 20);

            Console.WriteLine($"\nWelcome, {name}! Your adventure begins.");
            DungeonManager dungeon = new DungeonManager();
            dungeon.StartDungeon(player);

            Console.WriteLine("\n=== Game Session Ended ===");
        }
    }
}
