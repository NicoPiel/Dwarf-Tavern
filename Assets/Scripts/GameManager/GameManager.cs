using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
        _instance = this;
    }

    public static GameManager GetGameManager()
    {
        return _instance;
    }
}
