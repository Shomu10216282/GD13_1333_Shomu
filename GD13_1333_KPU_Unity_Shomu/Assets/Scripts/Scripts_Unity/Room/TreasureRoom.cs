using UnityEngine;

public class TreasureRoom : Room
{
    public GameObject treasureObject;
    private bool taken = false;

    [Header("Treasure Settings")]
    public int treasureValue = 1;   

    protected override void Start()
    {
        roomName = "Treasure Room";
        base.Start();
    }

    public override void TriggerPlayerInteract()
    {
        if (!taken)
        {
            TakeTreasure();
            return;
        }

        base.TriggerPlayerInteract();
    }

    private void TakeTreasure()
    {
        taken = true;

        if (treasureObject != null)
            treasureObject.SetActive(false);

        GameState.AddScore(treasureValue);

        Debug.Log($"Treasure obtained! +{treasureValue} point");
    }
}
