using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientController : MonoBehaviour
{
    [SerializeField] string ingredientName = "Mynt"; //Mynt, Saph, Thym, Blis, Neth
    //[SerializeField] int herbId= 1; //1,2,3,4,5
    //[SerializeField] int state = 0; //0 default, 1 crushed, 2 chopped, 3 dried
    [SerializeField] bool inWork = false; //true if in a workstation or pot and being worked on.
    [SerializeField] Herb thisHerb;

    Image herbImage;
    
    private void Awake()
    {
        //herbImage = transform.Find("HerbImage").GetComponentInChildren<Image>();
        herbImage = GetComponent<Image>();
    }

    public void SetStartValues(int spawnHerbId = -1)
    {
        thisHerb = new Herb();

        if (spawnHerbId == -1)
            thisHerb.herbId = Random.Range(0, 5);
        else
            thisHerb.herbId = spawnHerbId;

        //herbId = 

        RefreshName();
        RefreshHerbGraphics();
    }

    public void RefreshHerbGraphics()
    {
        Sprite herbSprite = HerbManager.instance.GetHerbImage(thisHerb.herbId, thisHerb.herbState);
        herbImage.sprite = herbSprite;
        Debug.Log("Herb image sprite updated");
    }

    public void SetWorkState(int stateId)
    {
        Debug.Log("Herb state change from " + thisHerb.herbState + " to " + stateId);
        thisHerb.herbState = stateId;
        RefreshHerbGraphics();
        RefreshName();
    }

    public int GetWorkState()
    {
        return thisHerb.herbState;
    }

    public int GetHerbId()
    {
        return thisHerb.herbId;
    }

    void RefreshName()
    {
        ingredientName = HerbManager.instance.GetHerbName(thisHerb.herbId, thisHerb.herbState);
    }

    public string GetIngredientName()
    {
        return ingredientName;
    }

    public Herb GetThisHerb()
    {
        return thisHerb;
    }

}

public class Herb
{
    public int herbId;
    public int herbState;
}
