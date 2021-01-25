using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bed : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnAction()
    {
        EventHandler.onDayChanged.Invoke();
        SceneManager.LoadScene("Scenes/Tavern");
    }
}
