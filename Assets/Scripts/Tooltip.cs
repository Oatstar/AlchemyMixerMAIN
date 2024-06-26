using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    TMP_Text tooltipText;
    RectTransform backgroundRectTransform;
    RectTransform thisRectTransform;
    Canvas canvas;

    [SerializeField] private Camera uiCamera;

    private static Tooltip instance;

    private void Awake()
    {
        instance = this;

        canvas = GetComponentInParent<Canvas>();
        backgroundRectTransform = transform.Find("TooltipBG").GetComponent<RectTransform>();
        tooltipText = transform.Find("TooltipText").GetComponent<TMP_Text>();
        thisRectTransform = GetComponent<RectTransform>();

        HideToolTip();
    }

    private void Start()
    {
        ShowToolTip("");
        HideToolTip();
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out localPoint);
        Vector2 offset = new Vector2(-15, 35);
        thisRectTransform.anchoredPosition = localPoint + offset;
    }

    private void ShowToolTip(string tooltipString)
    {
        tooltipText.text = tooltipString;
        float padding = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + padding * 2f, tooltipText.preferredHeight + padding * 2f);
        thisRectTransform.sizeDelta = backgroundSize;

        gameObject.SetActive(true);
    }

    private void HideToolTip()
    {
        gameObject.SetActive(false);
        tooltipText.text = "  ";
        float padding = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + padding * 2f, tooltipText.preferredHeight + padding * 2f);
        thisRectTransform.sizeDelta = backgroundSize;
    }


    public static void ShowTooltip_Static(string tooltipString)
    {
        instance.ShowToolTip(tooltipString);
        instance.CancelInvoke("HideOnDelay");
    }

    public static void HideTooltip_Static()
    {
        instance.Invoke("HideOnDelay", 0.05f);
    }

    void HideOnDelay()
    {
        instance.HideToolTip();
    }
}
