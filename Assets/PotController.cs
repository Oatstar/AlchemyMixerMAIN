using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour
{
    [SerializeField] List<GameObject> potIngredients = new List<GameObject> { };
    [SerializeField] GameObject boilingIcon;

    [SerializeField] GameObject potionHolder;
    [SerializeField] GameObject potionPrefab;

    bool boiling;
    //Slider timerSlider;
    float boilTimePerInterval = 5f;
    [SerializeField] float currentBoilAmount = 0f;
    [SerializeField] int boilIntervalCount = 0;

    private void Awake()
    {
        boilingIcon = this.gameObject.transform.parent.Find("BoilingIcon").gameObject;
        boilingIcon.SetActive(false);
    }

    public void AddIngredient(GameObject item)
    {
        potIngredients.Add(item);
    }

    public void RemoveIngredient(GameObject item)
    {
        potIngredients.Remove(item);
    }

    public void SetBoilingState(bool state)
    {
        boiling = state;
        boilingIcon.SetActive(state);
        if (state)
        {
            LockIngredients();
        }
    }

    void LockIngredients()
    {
        boilIntervalCount = 0;
        foreach (Transform child in transform)
        {
            child.GetComponent<DragDrop>().enabled = false;
        }
        //Cannot move ingredients. Can only dispose the whole pot
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
            }

        }
    }

    void AddBoilTick()
    {
        boilIntervalCount++;
        //Add 1 flame symbol per each full boilTimePerInterval
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
    }

    void PotionRemoved()
    {
        boiling = false;
        currentBoilAmount = 0f;

        GameObject potionGameObject = Instantiate(potionPrefab, transform.position, Quaternion.identity);
        potionHolder.transform.GetComponent<ItemSlot>().DropIntoSlot(potionGameObject);

        BottledPotionController bottledPotionContr = potionGameObject.transform.GetComponent<BottledPotionController>();
        bottledPotionContr.bottleName = "RandomName";
        foreach (GameObject usedIngredient in potIngredients)
        {
            string ingrName = usedIngredient.transform.GetComponent<IngredientController>().GetIngredientName();
            bottledPotionContr.allIngredients.Add(ingrName);
        }
    }
}
