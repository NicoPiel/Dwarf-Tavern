using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeStateHolder : MonoBehaviour
{
    private static UpgradeStateHolder _instance;
    private Dictionary<string, int> _upgrades;

    public static UpgradeStateHolder GetInstance()
    {
        return _instance;
    }

    private UnityEvent onUpgradeChanged = new UnityEvent();
    
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
        _instance = this;
        
        _upgrades = ES3.KeyExists("Upgrade") ? ES3.Load<Dictionary<string, int>>("Upgrade") : new Dictionary<string, int>();
        onUpgradeChanged.AddListener(Save);
    }

    public bool IsRegistered(string id)
    {
        return _upgrades.ContainsKey(id);
    }

    public void RegisterUpgradeble(string id)
    {
        if (_upgrades.ContainsKey(id))
        {
            return;
        }
        _upgrades.Add(id, 1);
        onUpgradeChanged.Invoke();
    }

    public int GetUpgradeState(string id)
    {
        if (!_upgrades.ContainsKey(id))
        {
            return -1;
        }
        return _upgrades[id];
    }

    public void SetUpgradeState(string id, int state)
    {
        if (_upgrades.ContainsKey(id))
        {
            return;
        }
        _upgrades[id] = state;
        onUpgradeChanged.Invoke();
    }

    public void Upgrade(string id)
    {
        if (!_upgrades.ContainsKey(id))
        {
            return;
        }

        _upgrades[id]++;
        onUpgradeChanged.Invoke();
    }

    private void Save()
    {
        ES3.Save("Upgrade", _upgrades);
        Debug.Log("Saved!");
    }
    


}
