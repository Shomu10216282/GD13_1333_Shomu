using UnityEngine;

public static class PlayerStats
{
    public static int maxHP = 10;
    public static int currentHP = 10;

    public static void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP < 0) currentHP = 0;

        Debug.Log("Player HP: " + currentHP + "/" + maxHP);
    }
}
