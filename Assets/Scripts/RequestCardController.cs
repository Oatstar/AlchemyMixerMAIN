using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RequestCardController : MonoBehaviour
{
    [SerializeField] TMP_Text recipeTextHeader;
    [SerializeField] TMP_Text recipeTextRecipe;

    RequestedPotion reqPot;
    [SerializeField] List<Herb> requestedHerbs = new List<Herb> { };
    [SerializeField] int requestedBoilingLevel;

    [SerializeField] List<int> neededHerbs = new List<int> { };
    [SerializeField] List<int> neededHerbStates = new List<int> { };

    [SerializeField] GameObject[] herbContainers= new GameObject[5];
    [SerializeField] TMP_Text[] herbTextObjects = new TMP_Text[5];
    [SerializeField] Image[] herbImageObjects = new Image[5];

    [SerializeField] GameObject[] boilIcons = new GameObject[3];

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

            herbTextObjects[i].text = tempString;
            herbImageObjects[i].sprite = HerbManager.instance.GetHerbImage(reqPot.herbs[i].herbId, reqPot.herbs[i].herbState);
            //if (i != 0)
            //recipeTextString += "\n";

            //recipeTextString += tempString;
        }

        for (int i = numberOfHerbs; i < herbContainers.Length; i++)
        {
            herbContainers[i].SetActive(false);
        }

        recipeTextHeader.text = reqPot.potionName;
        //recipeTextRecipe.text = recipeTextString;

        SetBoilIcons(reqPot);
    }

    void SetBoilIcons(RequestedPotion reqPot)
    {
        boilIcons[0].SetActive(false);
        boilIcons[1].SetActive(false);
        boilIcons[2].SetActive(false);

        for (int i = 0; i < reqPot.boilLevel; i++)
        {
            boilIcons[i].SetActive(true);
        }

        
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
