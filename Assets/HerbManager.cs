using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryContainer;
    [SerializeField] GameObject herbPrefab;
    [SerializeField] Transform chosenInventorySlot;
    [SerializeField] ItemSlot chosenInventorySlotScript;

    string[] herbNames = new string[5] { "Mynt", "Saph", "Thym", "Blis", "Neth" };
    string[] herbStates = new string[4] { "Raw", "Crushed", "Chopped", "Dried" };

    public static HerbManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public string GetHerbNames(int id)
    {
        return herbNames[id];
    }

    public string GetHerbStates(int id)
    {
        return herbStates[id];
    }

    private void Start()
    {
        Invoke("LateStart", 0.01f);
    }
    private void LateStart()
    {
        SpawnHerb();
        SpawnHerb();
        SpawnHerb();
        SpawnHerb();
    }

    public void SpawnHerb()
    {
        foreach (Transform inventorySlot in inventoryContainer.transform)
        {
            chosenInventorySlotScript = inventorySlot.transform.GetComponent<ItemSlot>();
            if(!chosenInventorySlotScript.GetSlotFull())
            {
                chosenInventorySlot = inventorySlot;
                break;
            }
        }

        GameObject spawnedHerb = Instantiate(herbPrefab, transform.position, Quaternion.identity);
        spawnedHerb.transform.GetComponent<IngredientController>().SetStartValues();
        chosenInventorySlotScript.DropIntoSlot(spawnedHerb);
    }
}
