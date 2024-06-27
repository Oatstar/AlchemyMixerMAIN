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
    [SerializeField] AudioSource flameOn;
    [SerializeField] AudioSource fireplace;
    [SerializeField] AudioSource tickTock;
    [SerializeField] AudioSource boilUp;

    public static SoundManager instance;
    [SerializeField] bool isMuted = false;


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

    public void PlayFlameOn()
    {
        flameOn.Play();
    }

    public void PlayFireplace()
    {
        if(!fireplace.isPlaying)
            fireplace.Play();
    }

    public void StopFireplace()
    {
        if (fireplace.isPlaying)
            fireplace.Stop();
    }

    public void PlayTickTock()
    {
        tickTock.Play();
    }

    public void PlayBoilUp()
    {
        boilUp.Play();
    }

    public void ToggleSounds()
    {
        isMuted = !isMuted;
        SetMuteState(isMuted);
    }

    private void SetMuteState(bool mute)
    {
        basicClick.mute = mute;
        mortar.mute = mute;
        chopping.mute = mute;
        dryer.mute = mute;
        potionReady.mute = mute;
        flameOn.mute = mute;
        fireplace.mute = mute;
        tickTock.mute = mute;
        boilUp.mute = mute;
    }
}
