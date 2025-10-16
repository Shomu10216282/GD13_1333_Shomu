using GD13_1333_Shomu.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace GD13_1333_Shomu.Scripts
{
    internal class Player
    {
        private string v;

        public string Name { get; private set; }
        public int CurrentRoll { get; set; }
        public int Points { get; set; }
        public List<int> Dice { get; private set; }
        public List<int> Rolls { get; private set; }
        public int EvenCount { get; private set; }
        public int OddCount { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; } = 50;
        public int DiceCount { get; private set; } = 1;

        public Player(string name, List<int> diceSet)
        {
            Name = name;
            Points = 0;
            Dice = new List<int>(diceSet);
            Rolls = new List<int>();
            EvenCount = 0;
            OddCount = 0;
        }

        public Player(string v)
        {
            this.v = v;
        }

        public bool HasDice()
        {
            return Dice.Count > 0;
        }

        public bool UseDie(int sides)
        {
            if (Dice.Contains(sides))
            {
                Dice.Remove(sides);
                return true;
            }
            return false;
        }


        public void RecordRoll(int value)
        {
            Rolls.Add(value);
            if (value % 2 == 0) EvenCount++;
            else OddCount++;
        }

        public int TotalRoll()
        {
            int sum = 0;
            foreach (int r in Rolls) sum += r;
            return sum;
        }

        public double AverageRoll()
        {
            if (Rolls.Count == 0) return 0;
            return (double)TotalRoll() / Rolls.Count;
        }


        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;
        }
        public void Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHealth) Health = MaxHealth;
            Console.WriteLine($"{Name} healed {amount} HP! (Now {Health}/{MaxHealth})");
        }
        public void AddDie()
        {
            DiceCount++;
            Console.WriteLine($"{Name} gained a new die! Total dice: {DiceCount}");
        }


        internal class ComputerPlayer : Player
        {
            private System.Random rand = new System.Random();

            public ComputerPlayer(string name, List<int> diceSet) : base(name, diceSet) { }

            public int ChooseDie()
            {
                List<int> dice = Dice;
                int index = rand.Next(dice.Count);
                int choice = dice[index];
                UseDie(choice);
                return choice;
            }

            internal void RecordRoll(int value)
            {
                Rolls.Add(value);
                if (value % 2 == 0) EvenCount++;
                else OddCount++;
            }
        }
    }
}

