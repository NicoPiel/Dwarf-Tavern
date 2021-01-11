using System;
using UnityEngine;

namespace Interactions
{    
    [RequireComponent(typeof(RectTransform))]
    public class InteractionPrompt : MonoBehaviour
    {
        private Camera _camera;
        private Transform _position;
        private int _offsetY;
        private RectTransform _rTransform;
        
        
        public void Init(Camera cam, Transform pos, int offsetY)
        {
            _camera = cam;
            _position = pos;
            _offsetY = offsetY;
            _rTransform = GetComponent<RectTransform>();
        }

        public void UpdatePos()
        {
            Vector3 screenPos = _camera.WorldToScreenPoint(_position.position);
            screenPos.y += _offsetY;
            _rTransform.position = screenPos;
        }

        public void Update()
        {
            UpdatePos();
        }
    }
}