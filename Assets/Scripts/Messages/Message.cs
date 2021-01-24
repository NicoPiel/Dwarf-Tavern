using System;
using UnityEngine;

namespace Messages
{
    [Serializable]
    public class Message : ScriptableObject
    {
        public string sender;
        public MessageItemReward[] rewardItems;
        public int rewardMoney;

        [Serializable]
        public struct MessageItemReward
        {
            public string itemId;
            public int amount;
        }
    }
}