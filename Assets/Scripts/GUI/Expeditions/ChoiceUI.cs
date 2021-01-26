using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour
{

    public GameObject ChoiceFramePrefab;

    public Sprite Questionmark;
    // Start is called before the first frame update
    private void OnEnable()
    {
        UpdateUI();
        GameManager.GetEventHandler().onExpeditionStarted.AddListener(UpdateUI);
    }

    // Update is called once per frame
    private void UpdateUI()
    {
        DestroyAll();
        if (ExpeditionHolder.GetInstance().IsExpeditionSelected())
        {
            gameObject.transform.localScale = Vector3.one;
            for(int i = 0; i <= ExpeditionHolder.GetInstance().GetSelectedExpedition().GetLength(); i+=1)
            {
                var gameObjectItem = Instantiate(ChoiceFramePrefab, transform);
                gameObjectItem.name = "Slot " + i;
                gameObjectItem.transform.Find("Icon").GetComponent<Image>().sprite = Questionmark;
            }
        }
        else
        {
            gameObject.transform.localScale = Vector3.zero;
        }
    }

    private void DestroyAll()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}