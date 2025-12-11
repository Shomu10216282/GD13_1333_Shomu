using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHP = 1;
    public int currentHP;

    public System.Action<Enemy> onDeath;

    private void Start()
    {
        currentHP = maxHP;
        UIManager.Instance.UpdateEnemyHP(currentHP);
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;

        UIManager.Instance.UpdateEnemyHP(currentHP);

        if (currentHP <= 0)
        {
            onDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
