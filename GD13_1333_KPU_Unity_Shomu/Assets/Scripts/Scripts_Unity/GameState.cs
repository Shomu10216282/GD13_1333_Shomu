using UnityEngine;

public static class GameState
{
    public static int HP = 5;
    public static int Score = 0;

    public static void AddHP(int amount)
    {
        HP += amount;
        HP = Mathf.Clamp(HP, 0, 10);

        Debug.Log("HP: " + HP);
        UIManager.Instance.UpdateHP(HP);
    }

    public static void AddScore(int amount)
    {
        Score += amount;

        Debug.Log("Score: " + Score);
        UIManager.Instance.UpdateScore(Score);
    }
}
