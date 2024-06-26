using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] GameObject dragItemContainer;
    [SerializeField] GameObject currentSlot;
    [SerializeField] GameObject originalParent;
    bool inSlot = true;

    private void Awake() {
        canvas = GameObject.Find("Canvas").transform.GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        dragItemContainer = GameObject.Find("DragItemContainer");
    }

    void Start()
    {
        RefreshParentContainer();
        originalParent = currentSlot;
    }   

    public void OnBeginDrag(PointerEventData eventData) {
        //Debug.Log("OnBeginDrag");
        originalParent = this.transform.parent.gameObject;

        CheckWorkStation();

        this.transform.SetParent(dragItemContainer.transform);
        SetInSlot(false);
        originalParent.transform.GetComponent<ItemSlot>().CheckSlotState();

        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

    }

    void CheckWorkStation()
    {
        if(originalParent.tag == "Workstation")
        {
            originalParent.transform.GetComponent<WorkstationController>().ItemRemoved();
        }
        if (originalParent.tag == "Pot")
        {
            originalParent.transform.GetComponent<PotController>().RemoveIngredient(this.gameObject);
        }
    }

    public void OnDrag(PointerEventData eventData) {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        //Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if(!inSlot)
        {
            Debug.Log("Item not in slot. Returning to original slot.");
            ReturnToOriginalSlot();
        }
        else
        {
            transform.parent.GetComponent<ItemSlot>().CheckSlotState();
        }
    }

    public void ReturnToOriginalSlot()
    {
        originalParent.transform.GetComponent<ItemSlot>().DropIntoSlot(this.gameObject);
    }

    public void OnPointerDown(PointerEventData eventData) {
        //Debug.Log("OnPointerDown");
    }


    public void RefreshParentContainer()
    {
        currentSlot = this.transform.parent.gameObject;
    }

    public void SetInSlot(bool state)
    {
        inSlot = state;
        RefreshParentContainer();
    }

}
