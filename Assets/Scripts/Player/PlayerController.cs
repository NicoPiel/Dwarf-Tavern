using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float playerSpeed;
        private Vector3 move;
        private CharacterController characterController;

        // Start is called before the first frame update
        private void Start()
        {
            characterController = gameObject.GetComponent<CharacterController>();
        }

        // Update is called once per frame
        private void Update()
        {
            move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
        }

        private void FixedUpdate()
        {
            characterController.Move(move * (Time.fixedDeltaTime * playerSpeed));
        }
    }
}
