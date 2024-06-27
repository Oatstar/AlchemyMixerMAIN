using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkstationController : MonoBehaviour
{
    bool working = false;
    bool ready = false;
    [SerializeField] GameObject currentIngredient;
    [SerializeField] float maxTimer = 5f;
    [SerializeField] float currentTimer = 5f;
    Slider timerSlider;

    private void Awake()
    {
        timerSlider = GetComponentInChildren<Slider>();
    }

    public void ItemDroppedToWorkstation(GameObject item)
    {
        currentIngredient = item;

        if (item.transform.GetComponent<IngredientController>().GetWorkState() == 0)
        {
            StartWorking();
        }
        else
        {
            Debug.Log("Ingredient already worked on");
        }
    }

    public void ItemRemoved()
    {
        working = false;
        ready = false;
        currentIngredient = null;
        currentTimer = 5f;
        timerSlider.value = 1f;
    }

    void StartWorking()
    {
        working = true;
        currentTimer = maxTimer;
    }

    
    void Update()
    {
        if(working)
        {
            currentTimer -= Time.deltaTime;
            timerSlider.value = Tools.instance.NormalizeToSlider(currentTimer, maxTimer);

            if(currentTimer <= 0)
            {
                ItemReady();
            }
        }
    }

    void ItemReady()
    {
        working = false;
        ready = true;
        

        if (this.name == "MortarPestle")
        {
            currentIngredient.GetComponent<IngredientController>().SetWorkState(1);
            SoundManager.instance.PlayMortar();
        }
        else if (this.name == "CuttingStation")
        {
            currentIngredient.GetComponent<IngredientController>().SetWorkState(2);
            SoundManager.instance.PlayChopping();
        }
        else if (this.name == "Dryer")
        {
            currentIngredient.GetComponent<IngredientController>().SetWorkState(3);
            SoundManager.instance.PlayDryer();
        }
    }

   public GameObject GetCurrentItem()
    {
        return currentIngredient;
    }
}
