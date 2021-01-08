using System;
using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class SimplePlayerInteractable : PlayerInteractable
    {
        public UnityEvent<GameObject> action;

        public void Awake()
        {
            if (action == null)
                action = new UnityEvent<GameObject>();
        }

        protected override void OnInteract(GameObject source)
        {
            action.Invoke(this.gameObject);
        }
    }
}