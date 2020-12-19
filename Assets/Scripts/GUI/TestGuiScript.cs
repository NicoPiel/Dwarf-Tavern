using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestGuiScript : MonoBehaviour
{
    private Label counterLabel;
    private Button counterButton;
    private Button closeButton;
    private int counter = 0;
    private void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        counterButton = rootVisualElement.Q<Button>("btn_pressme");
        counterLabel = rootVisualElement.Q<Label>("lbl_pressed");
        closeButton = rootVisualElement.Q<Button>("btn_close");
        
        counterLabel.text = "Pressed: " + counter;
        
        counterButton.RegisterCallback<ClickEvent>(ev => IncrementCounter());
        closeButton.RegisterCallback<ClickEvent>(ev => Close());
    }

    private void IncrementCounter()
    {
        counter++;
        counterLabel.text = "Pressed: " + counter;
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
