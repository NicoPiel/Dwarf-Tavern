using System;
using UnityEngine;

namespace Messages
{
    [Serializable]
    [CreateAssetMenu(fileName = "RepeatingLimitedTask", menuName = "ScriptableObjects/Messages/Tasks/RepeatingLimited", order = 1)]
    public class RepeatingLimitedMessageTask : RepeatingMessageTask
    {
        [SerializeReference] public string[] messageIds;
    }
}