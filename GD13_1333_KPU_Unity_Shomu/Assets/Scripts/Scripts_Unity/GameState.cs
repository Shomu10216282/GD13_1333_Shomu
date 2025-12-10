using System;
using UnityEngine;

public static class GameState
{
    public static int HP { get; private set; }
    public static int Score { get; private set; }

    public static event Action<int> OnScoreChanged;
    public static event Action OnGameClear;
    public static event Action OnGameOver;

    private static int clearScore = 20;
    private static int maxHP = 10;

    public static void Initialize(int initialHP, int initialScore, int winScore, int maxHp)
    {
        maxHP = maxHp;
        clearScore = winScore;

        HP = Mathf.Clamp(initialHP, 0, maxHP);
        Score = Mathf.Max(0, initialScore);

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateHP(HP);
            UIManager.Instance.SetMaxScore(clearScore);
            UIManager.Instance.UpdateScore(Score);
        }
    }

    public static void AddScore(int amount)
    {
        Score += amount;
        if (Score < 0) Score = 0;

        UIManager.Instance?.UpdateScore(Score);

        OnScoreChanged?.Invoke(Score);

        if (Score >= clearScore)
        {
            OnGameClear?.Invoke();
        }
    }

    public static void AddHP(int amount)
    {
        HP += amount;
        HP = Mathf.Clamp(HP, 0, maxHP);

        UIManager.Instance?.UpdateHP(HP);

        if (HP <= 0)
        {
            OnGameOver?.Invoke();
        }
    }

    public static void TakeDamage(int amount)
    {
        AddHP(-Mathf.Abs(amount));
    }
}
