using UnityEngine;

public class TreasureRoom : Room
{
    public GameObject treasureObject;
    private bool taken = false;

    protected override void Start()
    {
        roomName = "Treasure Room";
        base.Start();
    }

    public override void TriggerPlayerInteract()
    {
        base.TriggerPlayerInteract(); 

        if (!taken)
        {
            taken = true;
            if (treasureObject != null)
                treasureObject.SetActive(false);

            Debug.Log("Treasure obtained!");
        }
        else
        {
            Debug.Log("Treasure already taken.");
        }
    }
}
