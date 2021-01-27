using System;
using UnityEngine;

namespace Messages
{
    [Serializable]
    [CreateAssetMenu(fileName = "NewspaperMessage", menuName = "ScriptableObjects/Messages/Newspaper", order = 1)]
    public class NewspaperMessage : Message
    {
        public NewsArticle[] articles;
        [Serializable]
        public struct NewsArticle
        {
            public string headline;
            [Multiline]
            public string articleText;
        }
    }
}