using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Utility;

namespace Furniture
{
    public class Fireplace : MonoBehaviour
    {

        private Animator _animator;

        private bool _state = false;

        private static readonly int Burning = Animator.StringToHash("burning");

        public Light2D fireplaceLight;

        public AudioSource audioSource;
        public AudioClip burningSound;

        // Start is called before the first frame update
        private void Start()
        {
            _animator = GetComponent<Animator>();
            Toggle();
        }

        public void Toggle()
        {
            _state = !_state;
            _animator.SetBool(Burning, _state);

            if (_state)
            {
                audioSource.clip = burningSound;
                audioSource.Play();

                fireplaceLight.intensity = 1;
                fireplaceLight.GetComponent<FlickerEffect>().effectEnabled = true;
            }
            else
            {
                audioSource.Stop();
                
                fireplaceLight.intensity = 0;
                fireplaceLight.GetComponent<FlickerEffect>().effectEnabled = false;
            }
        }
    }

    
}
