using System;
using System.Collections.Generic;
using UnityEngine;

namespace Expeditions.Events
{
    [Serializable]
    [CreateAssetMenu(fileName = "EventPool", menuName = "ScriptableObjects/Expeditions/EventPool")]
    public class EventPool : ScriptableObject
    {
        public List<Event> events;
    }
}