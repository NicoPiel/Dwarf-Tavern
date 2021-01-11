﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public abstract class PlayerInteractable : Interactable
    {
        public UnityEvent onHighlightStart = new UnityEvent();
        public UnityEvent onHighlightStop = new UnityEvent();
        private Material _defaultMat;
        private List<GameObject> _prompts = new List<GameObject>();
        public void StartHighlight(Material highlightMaterial, GameObject prompt)
        {
            SpriteRenderer sprRenderer = GetComponent<SpriteRenderer>();
            if (_defaultMat == null)
            {
                _defaultMat = sprRenderer.material;
            }
            
            sprRenderer.material = highlightMaterial;
            _prompts.Add(prompt);
            onHighlightStart.Invoke();
        }

        public void StopHighLight()
        {
            if (_defaultMat != null)
            {
                SpriteRenderer sprRenderer = GetComponent<SpriteRenderer>();
                sprRenderer.material = _defaultMat;
            }
            _prompts.ForEach(prompt =>
            {
                Destroy(prompt);
            });
            _prompts.Clear();
            onHighlightStop.Invoke();
        }
        
    }
}