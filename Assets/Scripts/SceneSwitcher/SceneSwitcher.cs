using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public enum Scenes {
        Tavern = 0,
        AfterHour = 1
    }

    public Scenes sceneToLoad;

    public void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Loading:" + sceneToLoad);
            SceneManager.LoadScene("Scenes/" +sceneToLoad.ToString());
        }
    }
   
}
 

