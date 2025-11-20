using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Action<GameObject> onDeath;

    public void Die()
    {
        onDeath?.Invoke(gameObject);
        Destroy(gameObject);
    }
}
