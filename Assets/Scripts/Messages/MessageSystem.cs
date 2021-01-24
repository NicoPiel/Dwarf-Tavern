using System;
using System.Collections.Generic;
using UnityEngine;

namespace Messages
{
    [Serializable]
    [CreateAssetMenu(fileName = "MessageSystem", menuName = "ScriptableObjects/Messages/MessageSystem", order = 1)]
    public class MessageSystem : ScriptableObject
    {
        public List<MessageTask> tasks;
    }
}