using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class uiManager : MonoBehaviour
{

    [SerializeField] TMP_Text playerMoneyText;
    [SerializeField] TMP_Text strikeText;
    [SerializeField] GameObject featherRecipePanel;

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject startButton;

    private void Awake()
    {
        pausePanel.SetActive(true);
    }

    public void RefreshPlayerMoney()
    {
        playerMoneyText.text = "Money: " + GameMasterManager.instance.GetPlayerMoney().ToString();
    }

    public void RefreshPlayerStrikes(int strikes)
    {
        strikeText.text = "Strikes: " + strikes.ToString();
    }

    public void ToggleFeatherRecipe()
    {
        bool currentState = featherRecipePanel.activeSelf;
        featherRecipePanel.SetActive(!currentState);
    }

    public void ClosePausePanel()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void OpenPausePanel()
    {
        if(GameMasterManager.instance.gameStarted)
        {
            continueButton.SetActive(true);
            startButton.SetActive(false);
        }
        else
        {
            continueButton.SetActive(false);
            startButton.SetActive(true);
        }
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void StartGameWonSequence()
    {

    }


}
