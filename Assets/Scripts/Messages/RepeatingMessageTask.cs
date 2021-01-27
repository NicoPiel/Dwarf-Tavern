using System;
using UnityEngine;

namespace Messages
{
    [Serializable]
    [CreateAssetMenu(fileName = "RepeatingTask", menuName = "ScriptableObjects/Messages/Tasks/Repeating", order = 1)]
    public class RepeatingMessageTask : MessageTask
    {
        [Tooltip("The frequency at which this task triggers. On one of the days this task is triggered, the next day will always be (currentDay + period)")]
        [Min(1)]
        public int period;
        [Tooltip("The first day at which this task should trigger.")]
        public int firstDay = 1;
        [Tooltip("The amount of repetitions to trigger. One repetition will cause this task to be triggered a total of two times. Set to -1 for unlimited repetitions.")]
        [Min(-1)]
        public int repetitions = -1;
    }
}