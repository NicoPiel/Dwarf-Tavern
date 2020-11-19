using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float playerSpeed;
        private Vector3 move;
        private Rigidbody2D rigidbody;
        private Collider2D collider;

        // Start is called before the first frame update
        private void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            collider = GetComponent<CircleCollider2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        }

        private void FixedUpdate()
        {
            Vector3 pos = gameObject.transform.position;
            rigidbody.MovePosition(pos + move * (Time.fixedDeltaTime * playerSpeed));

            if (move == Vector3.zero)
            {
                rigidbody.velocity = Vector2.zero;
            }
        }
    }
}
