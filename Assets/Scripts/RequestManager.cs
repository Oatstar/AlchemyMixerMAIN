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

        GameMasterManager.instance.ReceiveMoney(potion.potionLevel + 2);
    }

    public void IncorrectPotionReturned()
    {
        Debug.Log("INCORRECT potion received");

    }
}
