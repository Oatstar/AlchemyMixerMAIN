using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RequestCardController : MonoBehaviour
{
    [SerializeField] TMP_Text recipeTextHeader;
    [SerializeField] TMP_Text recipeTextRecipe;

    public void SetRequestValues(RequestedPotion requestedPotion)
    {
        string recipeTextString = "";

        int numberOfHerbs = requestedPotion.herbs.Count;
        for (int i = 0; i < numberOfHerbs; i++)
        {
            string herbStateText = HerbManager.instance.GetHerbStates(requestedPotion.herbState[i]);
            string herbNameText = HerbManager.instance.GetHerbNames(requestedPotion.herbState[i]);

            string tempString = herbStateText + " " + herbNameText;
            if (i != 0)
                recipeTextString += "\n";

            recipeTextString += tempString;
        }

        recipeTextHeader.text = requestedPotion.potionName;
        recipeTextRecipe.text = recipeTextString;
    }

    public void ReturnOrder(GameObject item)
    {
        Debug.Log("Returning newPotion");
    }
}
