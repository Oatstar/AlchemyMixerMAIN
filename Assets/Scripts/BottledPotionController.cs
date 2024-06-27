using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottledPotionController : MonoBehaviour
{
    public string bottleName = "";
    public List<string> allIngredients = new List<string>();
    //public List<int> allUsedHerbs= new List<int>();
    [SerializeField] Sprite[] bottleBaseSprites;
    [SerializeField] Sprite[] bottleCorkSprites;
    [SerializeField] Color[] bottleColors;
    [SerializeField] Image bottleBaseImage;
    [SerializeField] Image bottleCorkImage;

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

        SetPotionGraphics();
    }

    void SetPotionGraphics()
    {
        bottleBaseImage.sprite = bottleBaseSprites[UnityEngine.Random.Range(0, bottleBaseSprites.Length)];
        bottleCorkImage.sprite = bottleCorkSprites[UnityEngine.Random.Range(0, bottleCorkSprites.Length)];
        bottleBaseImage.color = bottleColors[UnityEngine.Random.Range(0, bottleColors.Length)];
    }

    public RequestedPotion GetRequestedPotionData()
    {
        return thisPotionData;
    }
}
