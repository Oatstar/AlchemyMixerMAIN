using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.tag == "Ingredient")
        {
            string text = GetComponent<IngredientController>().GetIngredientName();
            Tooltip.ShowTooltip_Static(text);
        }
        else if (this.tag == "Potion")
        {
            string text = GetComponent<BottledPotionController>().GetRequestedPotionData().potionName;
            Tooltip.ShowTooltip_Static(text);
        }
        else if (this.tag == "HerbCard")
        {
            string text = "Buy a bundle of herbs for 4g";
            Tooltip.ShowTooltip_Static(text);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.HideTooltip_Static();
    }

}
