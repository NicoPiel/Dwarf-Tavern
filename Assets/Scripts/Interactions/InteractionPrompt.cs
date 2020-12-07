using System;
using UnityEngine;

namespace Interactions
{    
    [RequireComponent(typeof(RectTransform))]
    public class InteractionPrompt : MonoBehaviour
    {
        private Camera _camera;
        private Vector3 _position;
        private int _offsetY;
        private RectTransform _rTransform;
        
        
        public void Init(Camera cam, Vector3 pos, int offsetY)
        {
            _camera = cam;
            _position = pos;
            _offsetY = offsetY;
            _rTransform = GetComponent<RectTransform>();
        }

        public void UpdatePos()
        {
            Vector3 screenPos = _camera.WorldToScreenPoint(_position);
            screenPos.y += _offsetY;
            _rTransform.position = screenPos;
        }

        public void Update()
        {
            UpdatePos();
        }
    }
}