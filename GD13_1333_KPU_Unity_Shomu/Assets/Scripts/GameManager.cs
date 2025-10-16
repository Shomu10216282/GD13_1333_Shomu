using System;
using System.Collections.Generic;
using static GD13_1333_Shomu.Scripts.Player;
using UnityEngine;

namespace GD13_1333_Shomu.Scripts
{
    internal class GameManager : MonoBehaviour
    {
        private Player human;
        private ComputerPlayer computer;

        private DieRoller dieRoller = new DieRoller();
        private System.Random random = new System.Random();

        private Map gameMap;
        public void Start()
        {
            Debug.Log("GameManager Start");
            gameMap = new Map();
            Debug.Log("GameManager Map Created");

        }


        public void Play()
        {
            Console.WriteLine("WELCOME TO THE DICE BATTLE!");
            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine();
            Console.WriteLine($"\nHello {playerName}, welcome to the game!");
            Console.WriteLine("Rules:");
            Console.WriteLine("- Both players get the same dice set (d6, d8, d12, d20).");
            Console.WriteLine("- Each round, you and the computer pick one die.");
            Console.WriteLine("- Higher roll wins the round. Numbers that have already been used or are not listed cannot be used..");
            Console.WriteLine("- Game ends when no dice remain.\n");


            StartGame(playerName);
        }

        public void PlayBattle(Player player)
        {
            Player enemy = new Player("Enemy");
            Console.WriteLine($"\n Enemy appears!");
            Console.WriteLine($"{player.Name} HP: {player.Health}  |  {enemy.Name} HP: {enemy.Health}");
            Console.WriteLine("Battle Start!\n");
            while (player.Health > 0 && enemy.Health > 0)
            {
                Console.WriteLine("Press Enter to roll dice");
                Console.ReadLine();
                int playerRoll = dieRoller.Roll();
                int enemyRoll = dieRoller.Roll();
                Console.WriteLine($"{player.Name} rolled {playerRoll}");
                Console.WriteLine($"{enemy.Name} rolled {enemyRoll}");
                if (playerRoll > enemyRoll)
                {
                    Console.WriteLine("You hit the enemy!");
                    enemy.TakeDamage(10);
                }
                else if (enemyRoll > playerRoll)
                {
                    Console.WriteLine("The enemy hits you!");
                    player.TakeDamage(10);
                }
                else
                {
                    Console.WriteLine("It's a draw!");
                }
                Console.WriteLine($"{player.Name} HP: {player.Health} | {enemy.Name} HP: {enemy.Health}");
                Console.WriteLine("---------------------------");
            }
            if (player.Health > 0)
            {
                Console.WriteLine("You won the battle!");
            }
            else
            {
                Console.WriteLine("You lost the battle");
            }
        }

        private void StartGame(string playerName)
        {
            List<int> diceSet = new List<int>() { 6, 8, 12, 20 };

            human = new Player(playerName, new List<int>(diceSet));
            computer = new ComputerPlayer("Computer", new List<int>(diceSet));

            int round = 1;
            System.Random rand = new System.Random();
            bool playerFirst = rand.Next(2) == 0;

            Console.WriteLine(playerFirst
                ? $"{human.Name} goes first!"
                : $"{computer.Name} goes first!");
            Console.WriteLine();

            while (human.HasDice() && computer.HasDice())
            {
                Console.WriteLine($"\n--- Round {round} ---");
                if (playerFirst)
                {
                    HumanTurn();
                    ComputerTurn();
                }
                else
                {
                    ComputerTurn();
                    HumanTurn();
                }

                DecideWinner();
                round++;
            }

            PrintResults();

            Console.Write("\nPlay again? (yes/no): ");
            string again = Console.ReadLine().ToLower();
            if (again == "yes")
            {
                Play();
            }
            else
            {
                Console.WriteLine("THANKS FOR PLAYING!");
            }
        }

        private void HumanTurn()
        {
            Console.WriteLine($"\n{human.Name}'s turn!");
            Console.WriteLine($"Available dice: {string.Join(", ", human.Dice)}");

            int sides = 0;
            bool valid = false;
            while (!valid)
            {
                Console.Write("Choose your die (number of sides, e.g., 6, 8, 12, 20): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out sides) && human.UseDie(sides))
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Invalid choice or die already used. Try again.");
                }
            }

            DieRoller die = new DieRoller(sides);
            int roll = die.Roll();
            human.RecordRoll(roll);
            Console.WriteLine($"{human.Name} rolled a d{sides} and got {roll}.");
        }

        private void ComputerTurn()
        {
            Console.WriteLine("\nComputer's turn!");
            int choice = computer.ChooseDie();
            DieRoller die = new DieRoller(choice);
            int roll = die.Roll();
            computer.RecordRoll(roll);
            Console.WriteLine($"Computer rolled a d{choice} and got {roll}.");
        }

        private void DecideWinner()
        {
            int playerRoll = human.Rolls[human.Rolls.Count - 1];
            int compRoll = computer.Rolls[computer.Rolls.Count - 1];

            Console.WriteLine($"\nRound Result:");
            Console.WriteLine($"{human.Name}: {playerRoll}");
            Console.WriteLine($"{computer.Name}: {compRoll}");

            if (playerRoll > compRoll)
            {
                Console.WriteLine($"{human.Name} wins this round!");
                human.Points++;
            }
            else if (compRoll > playerRoll)
            {
                Console.WriteLine("Computer wins this round!");
                computer.Points++;
            }
            else
            {
                Console.WriteLine("It's a tie! No points this round.");
            }

            Console.WriteLine($"Points => {human.Name}: {human.Points}, Computer: {computer.Points}");
        }

        private void PrintResults()
        {
            Console.WriteLine("\n=== GAME OVER ===");
            PrintStats(human);
            PrintStats(computer);

            if (human.Points > computer.Points)
                Console.WriteLine($"\nResult: {human.Name} Wins!");
            else if (computer.Points > human.Points)
                Console.WriteLine("\nResult: Computer Wins!");
            else
                Console.WriteLine("\nResult: It's a Tie!");
        }

        private void PrintStats(Player p)
        {
            Console.WriteLine($"\n--- {p.Name}'s Results ---");
            Console.WriteLine($"Score: {p.Points}");
            Console.WriteLine($"Total of Rolls: {p.TotalRoll()}");
            Console.WriteLine($"Average Roll: {p.AverageRoll():F2}");
            Console.WriteLine($"Even Rolls: {p.EvenCount}, Odd Rolls: {p.OddCount}");
        }
    }
}
