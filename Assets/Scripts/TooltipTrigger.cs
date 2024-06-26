using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string optionalText = "";

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(optionalText == "")
        {
            if (this.tag == "Ingredient")
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
            else if (this.transform.parent.tag == "Workstation")
            {
                string text = "";

                if (this.name == "MortarPestleImage")
                {
                    text = "Mortar & Pestle \n Create crushed herb";
                }
                else if (this.name == "CuttingStationImage")
                {
                    text = "Knife \n Create chopped herb";
                }
                else if (this.name == "DryerImage")
                {
                    text = "Drying rack \n Create dried herb";
                }
                else
                {
                    Tooltip.HideTooltip_Static();
                }

                Tooltip.ShowTooltip_Static(text);
            }
        }
        else
        {
            Tooltip.ShowTooltip_Static(optionalText);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.HideTooltip_Static();
    }

}
