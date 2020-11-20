using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float playerSpeed;
        private Vector2 _move;
        private Rigidbody2D _rigidbody;
        private Collider2D _collider;

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
            _rigidbody.MovePosition(_rigidbody.position + _move * (playerSpeed * Time.fixedDeltaTime));
        }
    }
}
