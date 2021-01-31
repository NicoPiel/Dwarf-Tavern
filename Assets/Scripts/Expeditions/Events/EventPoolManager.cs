using System;
using System.Collections.Generic;
using System.Linq;
using Expeditions.Timer;
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
            }
            else
            {
                Destroy(this);
            }
        }

        public SimulationAdapter GetSimulationAdapter()
        {
            return GetComponent<SimulationAdapter>();
        }

        public Event GetRandomEvent(Expedition expedition)
        {
            List<Event> events = eventPool.events.Where(evt =>
                evt.difficulty <= expedition.GetDifficulty() && (expedition.GetKarma() < 0
                    ? evt.neededKarmaLevel >= expedition.GetKarma() && evt.neededKarmaLevel <= 0
                    : evt.neededKarmaLevel <= expedition.GetKarma() && evt.neededKarmaLevel >= 0) &&
                (evt.themeType == Expedition.ThemeType.Default || evt.themeType == expedition.GetThemeType())).ToList();
            Random rand = new Random();
            return events.Count > 0 ? events[rand.Next(events.Count)] : null;
        }
    }
}