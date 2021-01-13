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

    public UnityEvent onUpgradeChanged;
    
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
        _instance = this;
        onUpgradeChanged = new UnityEvent();
        _upgrades = new Dictionary<string, int>();
    }

    private void Start()
    {
        onUpgradeChanged.AddListener(Save);
        if (ES3.KeyExists("Upgrade"))
        {
            _upgrades = (Dictionary<string, int>)ES3.Load("Upgrade");
        }
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
        if (_upgrades.ContainsKey(id))
        {
            return 0;
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
        if (_upgrades.ContainsKey(id))
        {
            return;
        }

        _upgrades[id]++;
        onUpgradeChanged.Invoke();
    }

    public void Save()
    {   
        ES3.Save("Upgrade", _upgrades);
    }
    


}
