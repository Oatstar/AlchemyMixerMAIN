using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireController : MonoBehaviour
{
    [SerializeField] PotController potContr;
    Slider slider;


    bool flaming = false;
    [SerializeField] float fireAmount = 0;
    float maxFire = 20;
    float firePerClick = 2.00f;

    

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    public void StokeFire()
    {
        if(fireAmount < maxFire)
            fireAmount += firePerClick;

        fireAmount = Mathf.Clamp(fireAmount, 0, maxFire);

        if(fireAmount >= maxFire)
        {
            flaming = true;
            SetFlameGraphicState();
            potContr.SetBoilingState(true);
        }
    }

    private void Update()
    {
        if(fireAmount <= 0)
        {
            flaming = false;
            SetFlameGraphicState();
            potContr.SetBoilingState(false);
            return;
        }

        if(flaming)
        {
            fireAmount = fireAmount - Time.deltaTime * 2f;
        }
        else
        {
            fireAmount = fireAmount - Time.deltaTime * 4f;
        }

        slider.value = Tools.instance.NormalizeToSlider(fireAmount, maxFire);
    }

    void SetFlameGraphicState()
    {
        ColorBlock colorBlock = slider.colors;
        if (flaming)
        {
            colorBlock.normalColor = new Color(1f,0.06f, 0f); //Flaming and over 0 heat
        }
        else if(fireAmount > 0)
        {
            colorBlock.normalColor = new Color(1f, 0.65f, 0.21f); //Over 0 heat but not flaming
        }
        else
        {
            colorBlock.normalColor = new Color(0.59f, 0.53f, 0.37f); //Not flaming, cooled down and at 0 heat
        }
        slider.colors = colorBlock;

    }
}
