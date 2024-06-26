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
    float flamingDecreaseModifier = 2.5f;
    float nonFlamingDecreaseModifier = 4.5f;

    [SerializeField] Image fireImage;
    [SerializeField] Sprite fireFlaming;
    [SerializeField] Sprite fireStarting;
    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    public void StokeFire()
    {
        if(fireAmount < maxFire)
            fireAmount += firePerClick;

        fireAmount = Mathf.Clamp(fireAmount, 0, maxFire);
        SetFlameGraphicState();

        if (fireAmount >= maxFire)
        {
            flaming = true;
            fireImage.sprite = fireFlaming;
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
            fireAmount = fireAmount - Time.deltaTime * flamingDecreaseModifier;
        }
        else
        {
            fireAmount = fireAmount - Time.deltaTime * nonFlamingDecreaseModifier;
        }

        slider.value = Tools.instance.NormalizeToSlider(fireAmount, maxFire);
    }

    void SetFlameGraphicState()
    {
        ColorBlock colorBlock = slider.colors;
        if (flaming)
        {
            fireImage.gameObject.SetActive(true);
            colorBlock.normalColor = new Color(1f,0.06f, 0f); //Flaming and over 0 heat
            fireImage.sprite = fireFlaming;
        }
        else if(fireAmount > 0)
        {
            fireImage.gameObject.SetActive(true);
            colorBlock.normalColor = new Color(1f, 0.65f, 0.21f); //Over 0 heat but not flaming
            fireImage.sprite = fireStarting;
        }
        else
        {
            fireImage.gameObject.SetActive(false);
            
            colorBlock.normalColor = new Color(0.59f, 0.53f, 0.37f); //Not flaming, cooled down and at 0 heat
        }
        slider.colors = colorBlock;

    }
}
