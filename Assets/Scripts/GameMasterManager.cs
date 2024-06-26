using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMasterManager : MonoBehaviour
{
    [SerializeField] uiManager uiMan;
    [SerializeField] PotionManager potionMan;
    [SerializeField] RequestManager requestMan;

    float gameTime = 0f;
    float potionOrderTimer = 0f;
    float potionOrderInterval = 30f;

    public static GameMasterManager instance;
    [SerializeField] Slider slider;

    [SerializeField] int playerMoney = 10;
    [SerializeField] int failedToComply = 0;
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        uiMan.RefreshPlayerMoney();
        Invoke("LateStart", 0.5f);
    }

    void LateStart()
    {
        requestMan.RequestPotion();

    }

    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.A))
        //    requestMan.RequestPotion();


        gameTime += Time.deltaTime;
        potionOrderTimer += Time.deltaTime;
        slider.value = Tools.instance.NormalizeToSlider(potionOrderInterval, potionOrderInterval);

        if (potionOrderTimer >= potionOrderInterval)
        {
            potionOrderTimer = 0;
            if(requestMan.FreeCardSlotsLeft() == 0)
            {
                failedToComply++;
            }
            else
            {
                requestMan.RequestPotion();
            }
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
