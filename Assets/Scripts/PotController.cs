using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotController : MonoBehaviour
{
    [SerializeField] PotionManager potionMan;

    [SerializeField] List<GameObject> potIngredients = new List<GameObject> { };
    [SerializeField] GameObject fireBoilingIcon;
    [SerializeField] Image[] potBoilIcons;

    [SerializeField] GameObject potionHolder;
    [SerializeField] GameObject potionPrefab;

    bool boiling;
    //Slider timerSlider;
    float boilTimePerInterval = 5f;
    [SerializeField] float currentBoilAmount = 0f;
    [SerializeField] int boilIntervalCount = 0;

    [SerializeField] GameObject lockIcon;

    bool ingredientsLocked = false;
    

    private void Awake()
    {
        fireBoilingIcon.SetActive(false);
    }

    private void Start()
    {
        RefreshBoilIcons();
    }

    public void AddIngredient(GameObject item)
    {
        if (potIngredients.Count >= 5)
        {
            item.transform.GetComponent<DragDrop>().ReturnToOriginalSlot();
            InfoTextPopupManager.instance.SpawnInfoTextPopup("Cauldron full");
        }
        else
        {
            potIngredients.Add(item);
        }
    }

    public void RemoveIngredient(GameObject item)
    {
        potIngredients.Remove(item);
    }

    public void SetBoilingState(bool state)
    {
        boiling = state;
        fireBoilingIcon.SetActive(state);
        if (state)
        {
            //If setting boiling state to true (active), lock ingredients from being dragged off.
            LockIngredients();
        }
    }

    void LockIngredients()
    {
        //When locking ingredients, if ingredients were already locked continue as normal. If they weren't locked, reset the boilintervalcount
        if(!ingredientsLocked)
            boilIntervalCount = 0;

        ingredientsLocked = true;
        lockIcon.SetActive(true);
        foreach (Transform child in transform)
        {
            child.GetComponent<DragDrop>().enabled = false;
        }
        //Cannot move ingredients. Can only dispose the whole pot
    }

    void UnlockIngredients()
    {
        ingredientsLocked = false;
        lockIcon.SetActive(false);
    }

    void Update()
    {
        if (boiling)
        {
            currentBoilAmount += Time.deltaTime;
            if(currentBoilAmount >= boilTimePerInterval)
            {
                AddBoilTick();
                currentBoilAmount -= boilTimePerInterval;

                RefreshBoilIcons();
            }
        }
    }

    void AddBoilTick()
    {
        boilIntervalCount++;


        //Add 1 flame symbol per each full boilTimePerInterval
    }

    void RefreshBoilIcons()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < boilIntervalCount)
            {
                potBoilIcons[i].color = new Color(1, 1, 1);
            }
            else
            {
                potBoilIcons[i].color = new Color(0.1f, 0.1f, 0.1f);
            }
        }
    }

    public void PullLever()
    {
        if (potionHolder.transform.childCount > 0) //If potionholder already has a potion, lever doesn't do anything
        {
            Debug.Log("Potionholder already has a potion!");
            return;
        }

        PotionRemoved();
        foreach (GameObject ingredient in potIngredients)
        {
            Destroy(ingredient);
        }
        potIngredients.Clear();
        UnlockIngredients();
    }

    void PotionRemoved()
    {
        boiling = false;
        currentBoilAmount = 0f;

        GameObject potionGameObject = Instantiate(potionPrefab, transform.position, Quaternion.identity);
        potionHolder.transform.GetComponent<ItemSlot>().DropIntoSlot(potionGameObject);
        

        BottledPotionController bottledPotionContr = potionGameObject.transform.GetComponent<BottledPotionController>();
        //bottledPotionContr.CacheReadyPotionData();

        RequestedPotion newPotion = new RequestedPotion();

        //newPotion.potionName = "New Potion";
        foreach (GameObject usedIngredient in potIngredients)
        {
            IngredientController ingredientContr = usedIngredient.GetComponent<IngredientController>();
            Herb currentHerb = ingredientContr.GetThisHerb();
            newPotion.herbs.Add(currentHerb);
            //newPotion.herbs.Add(ingredientContr.GetHerbId());
            //newPotion.herbState.Add(ingredientContr.GetWorkState());
        }
        newPotion.boilLevel = boilIntervalCount;
        newPotion.potionName = potionMan.CompareIngredientsAndGetPotionName(newPotion);

        bottledPotionContr.CacheReadyPotionData(newPotion);

        boilIntervalCount = 0;
        RefreshBoilIcons();
    }
}
