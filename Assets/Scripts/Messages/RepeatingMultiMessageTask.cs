using System;
using System.Collections.Generic;
using UnityEngine;

namespace Messages
{
    [Serializable]
    [CreateAssetMenu(fileName = "RepeatingMultiMessageTask", menuName = "ScriptableObjects/Messages/Tasks/RepeatingMultiMessage", order = 1)]
    public class RepeatingMultiMessageTask : RepeatingMessageTask
    {
        [Tooltip("'Random' selects a random message from messages on every iteration. 'Sequential' selects each message in order and loops back to the first.")]
        public MessageSelection messageSelection = MessageSelection.Sequential;
        public List<Message> messages;

        public enum MessageSelection
        {
            Sequential,
            Random
        }
    }
}