using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterManager : MonoBehaviour
{
    [SerializeField] uiManager uiMan;
    [SerializeField] PotionManager potionMan;
    [SerializeField] RequestManager requestMan;

    float gameTime = 0f;
    float potionOrderTimer = 0f;
    float potionOrderInterval = 30f;

    public static GameMasterManager instance;

    [SerializeField] int playerMoney = 10;
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        uiMan.RefreshPlayerMoney();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
            requestMan.RequestPotion();


        gameTime += Time.deltaTime;
        potionOrderTimer += Time.deltaTime;

        if (potionOrderTimer >= potionOrderInterval)
        {
            potionOrderTimer = 0;
            requestMan.RequestPotion();
        }
    }

    public void ReceiveMoney(int receivedMoney)
    {
        playerMoney += receivedMoney;
        uiMan.RefreshPlayerMoney();
    }

    public int GetPlayerMoney()
    {
        return playerMoney;
    }

    public void AddMoney(int moneyAmount)
    {
        playerMoney += moneyAmount;

        if(moneyAmount < 0)
        {
            Debug.Log("Removed money from player: " + moneyAmount);
        }
    }
}
