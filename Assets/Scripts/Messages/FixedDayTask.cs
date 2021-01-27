using System;
using UnityEngine;

namespace Messages
{
    [Serializable]
    [CreateAssetMenu(fileName = "FixedDayTask", menuName = "ScriptableObjects/Messages/Tasks/FixedDay", order = 1)]
    public class FixedDayTask : MessageTask
    {
        [Min(1)]
        public int day;
    }
}