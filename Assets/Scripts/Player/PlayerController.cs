using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float playerSpeed;
        private Vector2 _move;
        private Vector2 _previous;
        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        public bool smoothMovement;
        public int smoothness = 5;

        // Start is called before the first frame update
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CircleCollider2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            _move.x = Input.GetAxisRaw("Horizontal");
            _move.y = Input.GetAxisRaw("Vertical");
            _move.Normalize();
        }

        private void FixedUpdate()
        {
            if (smoothMovement)
            { 
                _move.x = ((_previous.x * (smoothness - 1)) + _move.x) / smoothness;
                _move.y = ((_previous.y * (smoothness - 1)) + _move.y) / smoothness;
                _previous = new Vector2(_move.x, _move.y);
            }
            _rigidbody.MovePosition(_rigidbody.position + _move * (playerSpeed * Time.fixedDeltaTime));
        }
    }
}
