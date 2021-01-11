using System;
using System.Collections.Generic;
using System.Linq;
using Player;
using Unity.Entities;
using UnityEngine;

namespace Interactions
{

    [RequireComponent(typeof(Collider2D))]
    public class PlayerInteractionCursor : MonoBehaviour
    {
        private Collider2D _col;
        private PlayerController _parentPlayer;
        public Material highlight;
        public GameObject _UICanvas;
        private Camera _MainCamera;
        public GameObject PromptPrefab;

        public void Awake()
        {
            _col = GetComponent<Collider2D>();
            _parentPlayer = GetComponentInParent<PlayerController>();
            _MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            PlayerInteractable interactable = other.GetComponent<PlayerInteractable>();
            if (interactable != null)
            {
                GameObject prompt = Instantiate(PromptPrefab, _UICanvas.transform, false);
                InteractionPrompt promptScript = prompt.AddComponent<InteractionPrompt>();
                promptScript.Init(_MainCamera, interactable.transform, 60);
                promptScript.UpdatePos();
                interactable.StartHighlight(highlight, prompt);
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            PlayerInteractable interactable = other.GetComponent<PlayerInteractable>();
            if (interactable != null)
            {
                interactable.StopHighLight();
            }
        }

        public void FixedUpdate()
        {
            Vector2 move = _parentPlayer.GetMoveVector2();
            if (move.x != 0 || move.y != 0)
            {
                Vector2 offset = new Vector2(move.x, move.y).normalized;
                offset.Scale(new Vector2(0.5f, 0.5f));
                if(offset.x != 0 || offset.y != 0)
                    _col.offset = offset;
            }

        }

        public void Update()
        {
            if (Input.GetButtonDown("Interact"))
            {
                List<Collider2D> colliders = new List<Collider2D>();
                _col.OverlapCollider(new ContactFilter2D().NoFilter(), colliders);
                List<PlayerInteractable> interactables = colliders.Select(curCol => curCol.GetComponent<PlayerInteractable>())
                    .Where(curCol => curCol != null).ToList();
                float minDistance = -1;
                PlayerInteractable minInteractable = null;
                foreach (var interact in interactables)
                {
                    float curDist = Vector3.Distance(interact.transform.position, this.transform.position);
                    if (!minInteractable || curDist < minDistance)
                    {
                        minDistance = curDist;
                        minInteractable = interact;
                    }
                }

                if (minInteractable)
                {
                    minInteractable.Interact(_parentPlayer.gameObject);
                }
            }
        }
    }
}