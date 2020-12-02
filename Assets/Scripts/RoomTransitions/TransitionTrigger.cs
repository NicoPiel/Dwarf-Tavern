using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransitionTrigger : MonoBehaviour
{
    public GameObject room;
    public bool startActive = false;

    public void Start()
    {
        if (!startActive)
        {
            UpdateAll(0, 0, 0f);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("enter");
            UpdateAll(0, 1, 0.34f);
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("exit");
            UpdateAll(1, 0, 0.34f);
        }
    }

    private void UpdateAll(float from, float to, float time)
    {
        var objects = room.GetComponentsInChildren<Transform>().Select(tf => tf.gameObject);
        foreach(var obj in objects)
        {
            //Debug.Log(obj.name);
            var tilemap = obj.GetComponent<Tilemap>();
            if(tilemap != null)
            {
                obj.LeanValue(alpha =>
                {
                    tilemap.color = new Color(255, 255, 255, alpha);
                }, from, to, time);
            }
            else
            {
                LeanTween.alpha(obj, to, time);
            }
        }
    }
}
