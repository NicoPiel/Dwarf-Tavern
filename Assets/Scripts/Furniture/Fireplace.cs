using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Furniture
{
    public class Fireplace : MonoBehaviour
    {

        private Animator _animator;

        private bool _state = false;

        private static readonly int Burning = Animator.StringToHash("burning");

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
        }
    }

    
}
