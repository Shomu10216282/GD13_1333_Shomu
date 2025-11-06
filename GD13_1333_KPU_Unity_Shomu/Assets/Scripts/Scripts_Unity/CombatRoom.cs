using GD13_1333_Shomu.Scripts;
using UnityEngine;

public class CombatRoom : Room
{
    protected override void Start()
    {
        roomName = "Combat Room";
        base.Start();
    }

    protected override void OnPlayerEnter()
    {
        Debug.Log("Enemy appears! (Placeholder battle event)");
    }
}
