using UnityEngine;
using System;

public static class GameState
{
    public static int HP = 5;

    private static int score = 0;
    public static int Score
    {
        get => score;
        private set
        {
            score = value;
            OnScoreChanged?.Invoke(score);
        }
    }

    public static event Action<int> OnScoreChanged;

    public static void AddScore(int amount)
    {
        Score += amount;
        Debug.Log("Score: " + Score);
        UIManager.Instance.UpdateScore(Score);
    }

    public static void AddHP(int amount)
    {
        HP += amount;
        HP = Mathf.Clamp(HP, 0, 10);
        Debug.Log("HP: " + HP);
        UIManager.Instance.UpdateHP(HP);
    }

    public static void TakeDamage(int amount)
    {
        AddHP(-amount);
    }
}
