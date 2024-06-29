using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler {

    [SerializeField] bool slotFull = false;

    void Start()
    {
        CheckSlotState();
    }

    public void CheckSlotState()
    {
        if (this.tag == "PotionHolder")
            slotFull = true;
        else if(this.tag == "Pot")
        {
            if (this.transform.childCount >= 9)
                slotFull = true;
            else
                slotFull = false;
        }
        else
        {
            slotFull = false;
            foreach (Transform child in transform)
            {
                if (child.tag == "Ingredient" || child.tag == "Potion")
                    slotFull = true;
            }
        }
    }

    public bool GetSlotFull()
    {
        return slotFull;
    }

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            if(slotFull)
            {
                eventData.pointerDrag.transform.GetComponent<DragDrop>().ReturnToOriginalSlot();
                Debug.Log("Slot is full. Returning to original slot.");
            }
            else if (this.tag == "ReturnBox" && eventData.pointerDrag.tag != "Potion")
            {
                eventData.pointerDrag.transform.GetComponent<DragDrop>().ReturnToOriginalSlot();
            }
            else if (this.tag == "FlightReturnBox" && eventData.pointerDrag.tag != "Potion")
            {
                eventData.pointerDrag.transform.GetComponent<DragDrop>().ReturnToOriginalSlot();
            }
            else if (this.tag == "Workstation" && eventData.pointerDrag.tag != "Ingredient")
            {
                eventData.pointerDrag.transform.GetComponent<DragDrop>().ReturnToOriginalSlot();
            }
            else if (this.tag == "Pot" && eventData.pointerDrag.tag != "Ingredient")
            {
                eventData.pointerDrag.transform.GetComponent<DragDrop>().ReturnToOriginalSlot();
            }
            else
            {
                DropIntoSlot(eventData.pointerDrag);
            }
        }
    }

    public void DropIntoSlot(GameObject item)
    {
        slotFull = true;
        item.transform.SetParent(this.transform, false);
        item.transform.localPosition = new Vector3(0, 0, 0);

        //RectTransform rect = item.GetComponent<RectTransform>();
        //rect.sizeDelta = new Vector2(40, 40);
        //RectTransform rect = item.transform.GetComponent<RectTransform>();
        //rect.anchoredPosition = new Vector2(25, 25); // posX 0, posY 0
        //rect.sizeDelta = new Vector2(25, 25);    // width 100, height 100

        item.GetComponent<DragDrop>().SetInSlot(true);

        if(this.tag == "Workstation")
        {
            item.transform.SetSiblingIndex(2);
            GetComponent<WorkstationController>().ItemDroppedToWorkstation(item);
        }
        else if (this.tag == "Pot" && item.tag == "Ingredient")
        {
            slotFull = false;
            GetComponent<PotController>().AddIngredient(item);
        }
        else if (this.tag == "ReturnBox")
        {
            GetComponentInParent<RequestCardController>().ReturnOrder(item);
        }
        else if (this.tag == "FlightReturnBox")
        {
            GameMasterManager.instance.PotionOfFlightReady(item);
            slotFull = false;
        }
        else if (this.tag == "TrashCan")
        {
            TrashcanScript.instance.TrashItem(item);
            slotFull = false;
        }

    }


}