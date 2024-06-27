using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    [SerializeField] AudioSource mainMusic;

    [SerializeField] bool muted;
    void Start()
    {
        mainMusic.Play();
    }

    public void MuteMusic()
    {
        muted = !muted;
        mainMusic.mute = muted;
    }
}
