using GD13_1333_Shomu.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.Progress;


namespace GD13_1333_Shomu.Scripts
{
    internal class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public List<Item> Inventory { get; private set; }
        public Weapon? EquippedWeapon { get; private set; }

        public Player(string name, int maxHealth = 20)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = MaxHealth;
            Inventory = new List<Item>();
        }

        public void Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHealth) Health = MaxHealth;
            Console.WriteLine($"{Name} healed {amount} HP. (HP: {Health}/{MaxHealth})");
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;
            Console.WriteLine($"{Name} took {amount} damage. (HP: {Health}/{MaxHealth})");
        }

        public void AddItem(Item item)
        {
            Inventory.Add(item);
            Console.WriteLine($"You obtained: {item.Name} — {item.Description}");
        }

        public void EquipWeapon(Weapon weapon)
        {
            EquippedWeapon = weapon;
            Console.WriteLine($"{Name} equipped {weapon.Name}. (+{weapon.DamageBonus} damage)");
        }

        public void ShowInventory()
        {
            if (Inventory.Count == 0)
            {
                Console.WriteLine("Inventory: (empty)");
                return;
            }

            Console.WriteLine("\nInventory:");
            for (int i = 0; i < Inventory.Count; i++)
                Console.WriteLine($"{i + 1}. {Inventory[i].Name} - {Inventory[i].Description}");
        }

        public bool UseItemByIndex(int displayedIndex)
        {
            int index = displayedIndex - 1;
            if (index < 0 || index >= Inventory.Count)
            {
                Console.WriteLine("Invalid item selection.");
                return false;
            }

            Item it = Inventory[index];
            it.Use(this);
            if (it.IsConsumable())
            {
                Inventory.RemoveAt(index);
            }
            return true;
        }
    }
}
