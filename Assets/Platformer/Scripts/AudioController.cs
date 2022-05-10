using System;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public class AudioController : MonoBehaviour
{
    AudioSource song;
    private void Start()
    {
        song = GetComponent<AudioSource>();
        song.loop = true;
        song.Play();
    }
}
