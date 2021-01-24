using UnityEngine;

namespace Messages
{
    [CreateAssetMenu(fileName = "RandomDayTask", menuName = "ScriptableObjects/Messages/Tasks/RandomDay", order = 1)]
    public class RandomDayTask : MessageTask
    {
        [Min(1)]
        public int fromDayInclusive;
        [Min(0)]
        public int toDayInclusive;
        [Range(0,1)]
        [Tooltip("If toDayInclusive is 0, enter the daily chance of receiving this Message. Using this feature will make the message be able to appear more than once.")]
        public double chance;
    }
}