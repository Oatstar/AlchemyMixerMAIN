using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RequestManager : MonoBehaviour
{
    [SerializeField] PotionManager potionMan;
    [SerializeField] GameObject requestCardContainer;
    [SerializeField] GameObject requestCardPrefab;
    public static RequestManager instance;

    int requestBaseReward = 3;
    int requestLevelMultiplier = 4;

    private void Awake()
    {
        instance = this;
    }

    public void RequestPotion()
    {
        GameObject newRequestCard = Instantiate(requestCardPrefab, transform.position, Quaternion.identity);
        newRequestCard.transform.SetParent(requestCardContainer.transform, false);

        RequestedPotion requestedPotion = potionMan.GetNewRecipeRequest();
        newRequestCard.transform.GetComponent<RequestCardController>().SetRequestValues(requestedPotion);
    }

    public void CorrectPotionReturned(RequestedPotion potion)
    {
        Debug.Log("CORRECT potion received");

        float moneyGainedFloat = potion.potionLevel* requestLevelMultiplier + requestBaseReward;
        int moneyGained = Mathf.RoundToInt(moneyGainedFloat);
        GameMasterManager.instance.ReceiveMoney(moneyGained);

        InfoTextPopupManager.instance.SpawnInfoTextPopup("Correct potion! Gained " + moneyGained + "g");

        GameMasterManager.instance.PotionReturned();
    }

    public void IncorrectPotionReturned()
    {
        Debug.Log("INCORRECT potion received");
        InfoTextPopupManager.instance.SpawnInfoTextPopup("Incorrect potion! Lost the deal");
        GameMasterManager.instance.AddStrike(1);

    }

    public void PotionReturnedFinished()
    {
        if(FreeCardSlotsLeft() == 1)
        {
            RequestPotion();
        }
    }

    public int FreeCardSlotsLeft()
    {
        int childs = requestCardContainer.transform.childCount;
        Debug.Log("childs: "+childs);
        return (3 - childs);
    }
}
