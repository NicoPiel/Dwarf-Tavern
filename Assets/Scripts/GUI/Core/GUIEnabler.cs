using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIEnabler : MonoBehaviour
{
    public GameObject enableable;
    // Start is called before the first frame update

    public void OnAction()
    {
        enableable.SetActive(!enableable.activeSelf);
    }
}
