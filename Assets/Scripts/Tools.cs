using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    public static Tools instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public float NormalizeToSlider(float value, float maxValue)
    {
        float normalizedValue = value / maxValue;
        return Mathf.Clamp(normalizedValue, 0.0f, 1.0f);
    }


}
