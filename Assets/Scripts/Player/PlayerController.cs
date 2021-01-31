using System;
using UnityEditor.ShaderGraph;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float playerSpeed;
        private Vector2 _move;
        private Vector2 _previous;
        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        private Animator _animator;
        public bool smoothMovement;
        public int smoothness = 5;
        public GameObject selectionTrigger;
        public BeerDisplay beerDisplay;

        public AudioSource footstepAudioSource;
        public List<AudioClip> footstepSounds;

        // Start is called before the first frame update
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CircleCollider2D>();
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            ProcessInput();
            
            _move.x = Input.GetAxisRaw("Horizontal");
            _move.y = Input.GetAxisRaw("Vertical");
            _move.Normalize();
        }

        private void FixedUpdate()
        {
            _animator.SetFloat("Horizontal", _move.x);
            _animator.SetFloat("Vertical", _move.y);
            _animator.SetFloat("Magnitude", _move.magnitude);
            
            if (_move.x < 0 || _move.x > 0)
            {
                _animator.SetFloat("PrevHorizontal", _move.x);
                _animator.SetFloat("PrevVertical", 0);
            }

            if (_move.y < 0 || _move.y > 0)
            {
                _animator.SetFloat("PrevVertical", _move.y);
                _animator.SetFloat("PrevHorizontal", 0);
            }
            
            if (smoothMovement)
            { 
                _move.x = ((_previous.x * (smoothness - 1)) + _move.x) / smoothness;
                _move.y = ((_previous.y * (smoothness - 1)) + _move.y) / smoothness;
                _previous = new Vector2(_move.x, _move.y);
            }
            _rigidbody.MovePosition(_rigidbody.position + _move * (playerSpeed * Time.fixedDeltaTime));
        }

        public Vector2 GetMoveVector2()
        {
            return _move;
        }

        private void ProcessInput()
        {
            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
            {
                if (!beerDisplay.IsShown()) beerDisplay.ShowMenu();
                else beerDisplay.HideMenu();
            }
        }

        public void PlayRandomFootstepSound()
        {
            footstepAudioSource.clip = footstepSounds[Random.Range(0, footstepSounds.Count)];
            footstepAudioSource.Play();
        }
    }
}
