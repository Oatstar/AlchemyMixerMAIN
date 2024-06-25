using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottledPotionController : MonoBehaviour
{
    public string bottleName = "";
    public List<string> allIngredients = new List<string>();
    //public List<int> allUsedHerbs= new List<int>();


    [SerializeField] List<int> usedHerbs = new List<int> { };
    [SerializeField] List<int> usedHerbStates = new List<int> { };

    [SerializeField] RequestedPotion thisPotionData; 

    public void CacheReadyPotionData(RequestedPotion newPot)
    {
        thisPotionData = newPot;
        for (int i = 0; i < thisPotionData.herbs.Count; i++)
        {
            usedHerbs.Add(thisPotionData.herbs[i].herbId);
            usedHerbStates.Add(thisPotionData.herbs[i].herbState);
        }
        bottleName = thisPotionData.potionName;
    }

    public RequestedPotion GetRequestedPotionData()
    {
        return thisPotionData;
    }
}
