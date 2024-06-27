using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource basicClick;
    [SerializeField] AudioSource mortar;
    [SerializeField] AudioSource chopping;
    [SerializeField] AudioSource dryer;
    [SerializeField] AudioSource potionReady;

    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlayBasicClick()
    {
        basicClick.Play();
    }

    public void PlayMortar()
    {
        mortar.Play();
    }

    public void PlayChopping()
    {
        chopping.Play();
    }

    public void PlayDryer()
    {
        dryer.Play();
    }

    public void PlayPotionReady()
    {
        potionReady.Play();
    }
}
