using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Expeditions.Events
{
    public class EventPoolManager : MonoBehaviour
    {
        public static EventPoolManager Instance
        {
            get;
            private set;
        }

        public EventPool eventPool;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        public Event GetRandomEvent(Expedition expedition)
        {
            List<Event> events = eventPool.events.Where(evt =>
                evt.difficulty == expedition.GetDifficulty() && (evt.neededKarmaLevel == 0 || (expedition.GetKarma() < 0
                    ? evt.neededKarmaLevel >= expedition.GetKarma()
                    : evt.neededKarmaLevel <= expedition.GetKarma()))).ToList();
            Random rand = new Random();
            return events.Count > 0 ? events[rand.Next(events.Count)] : null;
        }
    }
}