using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RequestCardController : MonoBehaviour
{
    [SerializeField] TMP_Text recipeTextHeader;
    [SerializeField] TMP_Text recipeTextRecipe;

    RequestedPotion reqPot;
    [SerializeField] List<Herb> requestedHerbs = new List<Herb> { };
    [SerializeField] int requestedBoilingLevel;
    
    public void SetRequestValues(RequestedPotion requestedPotion)
    {
        reqPot = requestedPotion;

        string recipeTextString = "";

        int numberOfHerbs = requestedPotion.herbs.Count;

        requestedHerbs.AddRange(reqPot.herbs);
        requestedBoilingLevel = reqPot.boilLevel;

        for (int i = 0; i < numberOfHerbs; i++)
        {
            string herbStateText = HerbManager.instance.GetHerbStates(reqPot.herbs[i].herbId);
            string herbNameText = HerbManager.instance.GetHerbNames(reqPot.herbs[i].herbState);

            string tempString = herbStateText + " " + herbNameText;
            if (i != 0)
                recipeTextString += "\n";

            recipeTextString += tempString;
        }

        recipeTextHeader.text = reqPot.potionName;
        recipeTextRecipe.text = recipeTextString;


    }

    public void ReturnOrder(GameObject item)
    {
        Debug.Log("Returning a newPotion");

        bool correctPotion = CheckIfCorrectPotion(item);

        if (correctPotion)
            Debug.Log("CORRECT potion received");
        else
            Debug.Log("INCORRECT potion received");
    }

    bool CheckIfCorrectPotion(GameObject returnedPotion)
    {
        RequestedPotion newPot = returnedPotion.GetComponent<BottledPotionController>().GetRequestedPotionData();
        bool correctPotion = true;

        for (int i = 0; i < newPot.herbs.Count; i++)
        {
            if (newPot.herbs[i].herbId != reqPot.herbs[i].herbId || newPot.herbs[i].herbState != reqPot.herbs[i].herbState)
            {
                correctPotion = false;
            }
        }

        if (newPot.boilLevel != reqPot.boilLevel)
        {
            correctPotion = false;
        }

        return correctPotion;
    }
}
