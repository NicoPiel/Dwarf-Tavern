using System;
using UnityEngine;
using Random = System.Random;

namespace Messages
{
    [Serializable]
    public abstract class MessageTask : ScriptableObject
    {
        [SerializeReference] public Message message;
        public bool defaultCancelled = false;

        public int RandomSeed()
        {
            return this.name.GetHashCode();
        }

    }
}