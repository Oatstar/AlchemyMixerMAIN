using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMasterManager : MonoBehaviour
{
    [SerializeField] uiManager uiMan;
    [SerializeField] PotionManager potionMan;
    [SerializeField] RequestManager requestMan;

    float gameTime = 0f;
    float potionOrderTimer = 0f;
    float potionOrderInterval = 25f;

    public static GameMasterManager instance;
    [SerializeField] Slider slider;

    [SerializeField] int playerMoney = 10;
    [SerializeField] int failedToComply = 0;
    public bool gameStarted = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 0;

        uiMan.RefreshPlayerMoney();
        Invoke("LateStart", 0.5f);
    }

    public void ContinueGame()
    {
        uiMan.ClosePausePanel();
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }

    public void StartGame()
    {
        uiMan.ClosePausePanel();
        gameStarted = true;
    }

    void LateStart()
    {
        StartCoroutine(RequestFirstOrders());
    }

    IEnumerator RequestFirstOrders()
    {
        requestMan.RequestPotion();
        yield return new WaitForSeconds(10f);
        requestMan.RequestPotion();
    }

    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.A))
        //    requestMan.RequestPotion();


        gameTime += Time.deltaTime;
        potionOrderTimer += Time.deltaTime;
        slider.value = Tools.instance.NormalizeToSlider(potionOrderTimer, potionOrderInterval);

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
        uiMan.RefreshPlayerMoney();

    }

    public void AddStrike(int value)
    {
        failedToComply++;
        uiMan.RefreshPlayerStrikes(failedToComply);
    }

    public void PotionOfFlightReady(GameObject returnedPotObject)
    {
        RequestedPotion returnedPot = returnedPotObject.transform.GetComponent<BottledPotionController>().GetRequestedPotionData();
        RequestedPotion reqPot = PotionManager.instance.potionOfFlight;
        bool correctPotion = true;

        if (reqPot.potionName == returnedPot.potionName)
            correctPotion = true;
        else
            correctPotion = false;

        if (reqPot.boilLevel != returnedPot.boilLevel)
        {
            correctPotion = false;
        }

        if(correctPotion)
        {
            Debug.Log("Potion of Flight returned!");
            InfoTextPopupManager.instance.SpawnInfoTextPopup("You created a Potion of Flight!");
            uiMan.OpenPausePanel();
            uiMan.StartGameWonSequence();
        }
        else
        {
            Debug.Log("Wrong potion!");
            InfoTextPopupManager.instance.SpawnInfoTextPopup("Drank the wrong potion!");
            Destroy(returnedPotObject);
        }
    }
}
