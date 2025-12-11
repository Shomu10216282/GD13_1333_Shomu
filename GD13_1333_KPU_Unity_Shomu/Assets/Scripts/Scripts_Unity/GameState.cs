using System;
using UnityEngine;

public static class GameState
{
    public static int HP { get; private set; }
    public static int Score { get; private set; }

    public static event Action<int> OnScoreChanged;
    public static event Action OnGameClear;
    public static event Action OnGameOver;

    private static int clearScore = 10;
    private static int maxHP = 10;

    public static void Initialize(int initialHP = 10, int initialScore = 0, int winScore = 10, int maxHp = 10)
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

        OnScoreChanged?.Invoke(Score);

        if (UIManager.Instance != null)
            UIManager.Instance.UpdateScore(Score);

        if (Score >= clearScore)
        {
            OnGameClear?.Invoke();
            UIManager.Instance.ShowGameClear();
        }
    }

    public static void AddHP(int amount)
    {
        HP += amount;
        HP = Mathf.Clamp(HP, 0, maxHP);

        if (UIManager.Instance != null)
            UIManager.Instance.UpdateHP(HP);

        if (HP <= 0)
        {
            OnGameOver?.Invoke();
            UIManager.Instance.ShowGameOver();
        }
    }

    public static void TakeDamage(int amount)
    {
        AddHP(-Mathf.Abs(amount));
    }

    public static void SetClearScore(int newClearScore)
    {
        clearScore = Mathf.Max(1, newClearScore);
        if (UIManager.Instance != null)
            UIManager.Instance.SetMaxScore(clearScore);
    }
}
