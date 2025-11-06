using GD13_1333_Shomu.Scripts;
using UnityEngine;

public class TreasureRoom : Room
{
    protected override void Start()
    {
        roomName = "Treasure Room";
        base.Start();
    }

    protected override void OnPlayerEnter()
    {
        Debug.Log("You found treasure! (Placeholder event)");
    }
}
