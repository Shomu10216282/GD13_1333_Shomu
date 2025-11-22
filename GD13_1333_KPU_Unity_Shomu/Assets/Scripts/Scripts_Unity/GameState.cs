using UnityEngine;

public static class GameState
{
    public static int score = 0;

    public static void AddScore(int value)
    {
        score += value;
        Debug.Log("Current Score: " + score);
    }
}
