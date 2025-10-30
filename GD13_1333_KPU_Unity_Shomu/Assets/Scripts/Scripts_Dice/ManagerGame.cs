using GD13_1333_Shomu.Scripts;
using UnityEngine;
using static GD13_1333_Shomu.Scripts.Player;

public class ManagerGame : MonoBehaviour
{
    private Player human;

    private DieRoller dieRoller = new DieRoller();
    private System.Random random = new System.Random();

    private Map gameMap;
    public void Start()
    {
        Debug.Log("GameManager Start");
        gameMap = new Map();
        Debug.Log("GameManager Map Created");

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        
    }
}
