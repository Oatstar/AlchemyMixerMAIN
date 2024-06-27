using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    [SerializeField] AudioSource mainMusic;
    void Start()
    {
        mainMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
