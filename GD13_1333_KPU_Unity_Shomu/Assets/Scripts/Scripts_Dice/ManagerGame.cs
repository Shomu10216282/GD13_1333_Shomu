using GD13_1333_Shomu.Scripts;
using UnityEngine;
using static GD13_1333_Shomu.Scripts.Player;

public class ManagerGame : MonoBehaviour
{
    private Player human;

    private DieRoller dieRoller = new DieRoller();
    private System.Random random = new System.Random();


    //This the prehab
    [SerializeField] private MapGenerator MapGenerator;

    //Instense of Map
    private MapGenerator gameMap;
    public void Start()
    {
        Debug.Log("GameManager Start");
        gameMap = Instantiate(MapGenerator);
        Debug.Log("GameManager Map Created");
        gameMap.GenerateMap();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        
    }
}
