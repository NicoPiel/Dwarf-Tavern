using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EventHandler : MonoBehaviour
{

    public UnityEvent onGameStarted;
    public UnityEvent onGameLoaded;

    private void Awake()
    {
        onGameStarted = new UnityEvent();
        onGameLoaded = new UnityEvent();
    }
}
