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

    [SerializeField] List<int> neededHerbs = new List<int> { };
    [SerializeField] List<int> neededHerbStates = new List<int> { };

    public void SetRequestValues(RequestedPotion requestedPotion)
    {
        reqPot = requestedPotion;

        string recipeTextString = "";

        int numberOfHerbs = requestedPotion.herbs.Count;

        requestedHerbs.AddRange(reqPot.herbs);
        requestedBoilingLevel = reqPot.boilLevel;

        for (int i = 0; i < numberOfHerbs; i++)
        {
            neededHerbs.Add(reqPot.herbs[i].herbId);
            neededHerbStates.Add(reqPot.herbs[i].herbState);

            string herbStateText = HerbManager.instance.GetHerbStates(reqPot.herbs[i].herbState);
            string herbNameText = HerbManager.instance.GetHerbNames(reqPot.herbs[i].herbId);

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
        {
            RequestManager.instance.CorrectPotionReturned(reqPot);
            Destroy(this.gameObject);
        }
        else
        {
            RequestManager.instance.IncorrectPotionReturned();
            Destroy(this.gameObject);
        }
    }

    bool CheckIfCorrectPotion(GameObject returnedPotion)
    {
        RequestedPotion newPot = returnedPotion.GetComponent<BottledPotionController>().GetRequestedPotionData();
        bool correctPotion = true;

        if(newPot.potionName == reqPot.potionName)
            correctPotion = true;
        else
            correctPotion = false;

        if (newPot.boilLevel != reqPot.boilLevel)
        {
            correctPotion = false;
        }

        return correctPotion;
    }
}
