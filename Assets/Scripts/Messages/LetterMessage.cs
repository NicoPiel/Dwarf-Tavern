using System;
using UnityEngine;

namespace Messages
{
    [CreateAssetMenu(fileName = "LetterMessage", menuName = "ScriptableObjects/Messages/Letter", order = 1)]
    [Serializable]
    public class LetterMessage : Message
    {
        [Multiline]
        public string text;
        public ResponseOption[] responseOptions;
    
        [Serializable]
        public struct ResponseOption
        {
            public string id;
            public string displayText;
        }
    }
}