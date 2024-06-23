using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryContainer;
    [SerializeField] GameObject herbPrefab;
    [SerializeField] Transform chosenInventorySlot;
    [SerializeField] ItemSlot chosenInventorySlotScript;

    private void Start()
    {
        Invoke("LateStart", 0.01f);
    }
    private void LateStart()
    {
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
        chosenInventorySlotScript.DropIntoSlot(spawnedHerb);
    }
}
