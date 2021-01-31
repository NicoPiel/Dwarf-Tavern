using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bubblingSound;

    private void Start()
    {
        audioSource.clip = bubblingSound;
        audioSource.Play();
    }
}
