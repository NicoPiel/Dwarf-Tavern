using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlossarStateHolder : MonoBehaviour
{
    private static bool _initialized;
    private static GlossarStateHolder _instance;
    private int _state = 0;
    public UnityEvent glossarStateHolderChanged;

    
    private void OnEnable()
    {
        _instance = this;
        _initialized = true;
        glossarStateHolderChanged = new UnityEvent();
    }
    
    public static GlossarStateHolder GetInstance()
    {
        return _instance;
    }

    public static bool IsInitialized()
    {
        return _initialized;
    }

    
    public void Start()
    {
        glossarStateHolderChanged = new UnityEvent();
    }
    public void SetState(int stateValue)
    {
       _state = stateValue;
       glossarStateHolderChanged.Invoke();
    }

    public int GetState()
    {
        return _state;
    }
}
