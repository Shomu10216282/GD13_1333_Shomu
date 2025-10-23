using System;

namespace GD13_1333_Shomu.Scripts
{
    internal class Weapon : Item
    {
        public int DamageBonus { get; private set; }

        public Weapon(string name, int bonus) : base(name, $"Weapon: +{bonus} damage")
        {
            DamageBonus = bonus;
        }

        public override void Use(Player player)
        {
            player.EquipWeapon(this);
        }

        public override bool IsConsumable() => false;
    }
}
