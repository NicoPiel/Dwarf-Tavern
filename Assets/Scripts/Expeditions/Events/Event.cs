using System;
using System.Collections.Generic;
using UnityEngine;

namespace Expeditions.Events
{
    [Serializable]
    [CreateAssetMenu(fileName = "Event", menuName = "ScriptableObjects/Expeditions/Event", order = 0)]
    public class Event : ScriptableObject
    {
        [Header("Eventeigenschaften")]
        [TextArea(minLines:1, maxLines:5)]
        public string description;
        [Min(0)]
        public int difficulty;
        
        public int neededKarmaLevel;

        public Expedition.ThemeType themeType;

        [Header("Spielerentscheidungen")]
        public List<EventChoice> choices;
        [Serializable]
        public struct EventChoice
        {
            [Min(1)]
            public int choiceID;
            public string text;
            public Mercenary.MercenaryRole requiredClass;
            public RequirementType requirementType;
            public float reqBonus;
            public int karmaAdjustment;
            public float chance;
            public string reward;
            public string loss;
            [Header("Text bei Erfolg/Verlust")]
            [TextArea(minLines:1, maxLines:5)]
            public string positiveOutcome;
            [TextArea(minLines:1, maxLines:5)]
            public string negativeOutcome;

            public enum RequirementType
            {
                None,
                Soft,
                Hard
            }
            public enum Rewards
            {
                LootDrop,
                MoneyDrop,
                KarmaPlus
            }
            public enum Loss
            {
                Damage,
                Death
            }
            
        }
    }
}