using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HerbManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryContainer;
    [SerializeField] GameObject herbPrefab;
    [SerializeField] Transform chosenInventorySlot;
    [SerializeField] ItemSlot chosenInventorySlotScript;

    [SerializeField] Sprite[] Herb0_Images = new Sprite[] { };
    [SerializeField] Sprite[] Herb1_Images = new Sprite[] { };
    [SerializeField] Sprite[] Herb2_Images = new Sprite[] { };
    [SerializeField] Sprite[] Herb3_Images = new Sprite[] { };
    [SerializeField] Sprite[] Herb4_Images = new Sprite[] { };

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
        for (int i = 0; i < 20; i++)
        {
            SpawnHerb();
        }
    }

    public void SpawnMultipleHerbs(List<int> multipleHerbs)
    {
        for (int i = 0; i < multipleHerbs.Count; i++)
        {
            HerbManager.instance.SpawnHerb(multipleHerbs[i]);
        }
    }

    public void SpawnHerb(int spawnHerbId = -1)
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
        spawnedHerb.transform.GetComponent<IngredientController>().SetStartValues(spawnHerbId);
        chosenInventorySlotScript.DropIntoSlot(spawnedHerb);
    }

    public int CountFreeInventorySlots()
    {
        int freeSlotCount = 20;
        foreach (Transform inventorySlot in inventoryContainer.transform)
        {
            chosenInventorySlotScript = inventorySlot.transform.GetComponent<ItemSlot>();
            if (!chosenInventorySlotScript.GetSlotFull())
            {
                freeSlotCount--;
            }
        }

        return freeSlotCount;
    }

    public Sprite GetHerbImage(int herbId, int herbState)
    {
        switch (herbId)
        {
            case 0:
                Debug.Log("returning herb " + herbId + " images at herbstate " + herbState);
                return Herb0_Images[herbState];
            case 1:
                Debug.Log("returning herb " + herbId + " images at herbstate " + herbState);
                return Herb1_Images[herbState];
            case 2:
                Debug.Log("returning herb " + herbId + " images at herbstate " + herbState);
                return Herb2_Images[herbState];
            case 3:
                Debug.Log("returning herb " + herbId + " images at herbstate " + herbState);
                return Herb3_Images[herbState];
            case 4:
                Debug.Log("returning herb " + herbId + " images at herbstate "+herbState);
                return Herb4_Images[herbState];
            default:
                Debug.Log("returning herb " + herbId + " images at herbstate " + herbState);
                return Herb0_Images[0];
        }
    }

    public string GetHerbName(int herbId, int herbState)
    {
        return herbStates[herbState] + " " + herbNames[herbId];
    }
}
