using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoFloaterScript : MonoBehaviour
{
    TMP_Text infoFloaterText;
    RectTransform backgroundRectTransform;
    RectTransform thisRectTransform;
    Canvas canvas;

    [SerializeField] private Camera uiCamera;
    Vector2 upwardMovement = new Vector2(0, 0.2f);


    private static InfoFloaterScript instance;

    private void Awake()
    {
        instance = this;

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        backgroundRectTransform = transform.Find("InfoFloaterBG").GetComponent<RectTransform>();
        infoFloaterText = transform.Find("InfoFloaterText").GetComponent<TMP_Text>();
        thisRectTransform = GetComponent<RectTransform>();

        HideInfoText();
    }

    
    private void SetPosition()
    {
        Vector2 mousePosition = Input.mousePosition;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out localPoint);
        Vector2 offset = new Vector2(-15, 25);
        thisRectTransform.anchoredPosition = localPoint + offset;
    }

    private void Update()
    {
        thisRectTransform.anchoredPosition += upwardMovement;
    }

    public void ShowInfoText(string infoTextString)
    {
        infoFloaterText.text = infoTextString;
        float padding = 4f;
        Vector2 backgroundSize = new Vector2(infoFloaterText.preferredWidth + padding * 2f, infoFloaterText.preferredHeight + padding * 2f);
        thisRectTransform.sizeDelta = backgroundSize;

        gameObject.SetActive(true);
        SetPosition();
        Invoke("HideOnDelay", 3f);
    }

    private void HideInfoText()
    {
        gameObject.SetActive(false);
    }

    void HideOnDelay()
    {
        Destroy(this.gameObject);
    }

    //public static void ShowInfoText_Static(string infoTextString)
    //{
    //    instance.ShowInfoText(infoTextString);
    //    instance.CancelInvoke("HideOnDelay");
    //}

    //public static void HideInfoText_Static()
    //{
    //    instance.Invoke("HideOnDelay", 0.05f);
    //}

    //void HideOnDelay()
    //{
    //    instance.HideInfoText();
    //}
}
