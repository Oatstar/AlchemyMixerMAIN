using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeatherRecipeManager : MonoBehaviour
{
    [SerializeField] TMP_Text[] herbTexts;
    [SerializeField] Image[] herbImages;

    void Start()
    {
        Invoke("LateStart", 0.05f);
    }

    void LateStart()
    {
        RequestedPotion potOfFlight = PotionManager.instance.potionOfFlight;
        for (int i = 0; i < herbTexts.Length; i++)
        {
            int herbId = potOfFlight.herbs[i].herbId;
            int herbState = potOfFlight.herbs[i].herbState;
            string herbName = HerbManager.instance.GetHerbName(herbId, herbState);
            herbTexts[i].text = herbName;

            herbImages[i].sprite = HerbManager.instance.GetHerbImage(herbId, herbState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

