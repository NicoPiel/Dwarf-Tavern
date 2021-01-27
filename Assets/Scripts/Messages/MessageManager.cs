using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Messages
{
    public class MessageManager : MonoBehaviour
    {

        public static MessageManager Instance { get; private set; }
        
        void Start()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        
    }
}
