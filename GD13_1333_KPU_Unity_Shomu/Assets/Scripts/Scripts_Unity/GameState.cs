using UnityEngine;
using System;

public static class GameState
{
    public static int HP = 5;
    public static int Score = 0;

    public static event Action<int> OnScoreChanged;
    public static event Action OnGameClear;
    public static event Action OnGameOver;

    private static int clearScore = 5; 

    public static void AddScore(int amount)
    {
        Score += amount;

        OnScoreChanged?.Invoke(Score);
        UIManager.Instance.UpdateScore(Score);

        if (Score >= clearScore)
        {
            OnGameClear?.Invoke();
        }
    }

    public static void AddHP(int amount)
    {
        HP += amount;
        HP = Mathf.Clamp(HP, 0, 10);
        UIManager.Instance.UpdateHP(HP);

        if (HP <= 0)
        {
            OnGameOver?.Invoke();
        }
    }

    public static void TakeDamage(int amount)
    {
        AddHP(-amount);
    }
}
