using UnityEngine;

public class TreasureRoom : Room
{
    public GameObject treasureObject;
    private bool taken = false;
    private bool playerInside = false;

    [Header("Treasure Settings")]
    public int treasureValue = 1;

    protected override void Start()
    {
        roomName = "Treasure Room";
        base.Start();
    }

    private void Update()
    {
        if (playerInside && !taken)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                TakeTreasure();
            }
        }
    }

    protected override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        playerInside = true;

        if (!taken)
            UIManager.Instance.ShowTreasureUI(true);
    }

    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();
        playerInside = false;

        UIManager.Instance.ShowTreasureUI(false);
    }

    private void TakeTreasure()
    {
        taken = true;

        if (treasureObject != null)
            treasureObject.SetActive(false);

        GameState.AddScore(treasureValue);

        UIManager.Instance.ShowTreasureUI(false);

        Debug.Log($"Treasure obtained! +{treasureValue} point");
    }
}
