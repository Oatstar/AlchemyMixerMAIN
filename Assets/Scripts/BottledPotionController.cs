using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottledPotionController : MonoBehaviour
{
    public string bottleName = "";
    public List<string> allIngredients = new List<string>();
    public List<Herb> allUsedHerbs= new List<Herb>();

    [SerializeField] RequestedPotion requestedPot; 

    public void CacheReadyPotionData(RequestedPotion newPot)
    {
        requestedPot = newPot;
    }

    public RequestedPotion GetRequestedPotionData()
    {
        return requestedPot;
    }
}
