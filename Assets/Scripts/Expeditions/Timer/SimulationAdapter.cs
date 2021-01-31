using System;
using Expeditions.Events;
using Simulation.Core;
using UnityEngine;
using Event = Expeditions.Events.Event;

namespace Expeditions.Timer
{
    public class SimulationAdapter : SimulationModule
    {

        private int _ticksToNextEvent = -1;

        public void Start()
        {
            InitModule();
        }
        
        #region Ignored Events
        protected override void OnSimulationStart()
        {
            //Ignore
        }

        protected override void OnSimulationPause()
        {
            //Ignore
        }

        protected override void OnSimulationUnpause()
        {
            //Ignore
        }
        #endregion

        protected override void OnSimulationTick()
        {
            if (ExpeditionHolder.GetInstance() == null) return;
            Debug.Log($"{_ticksToNextEvent} ticks to next event");
            if (_ticksToNextEvent > 0)
            {
                _ticksToNextEvent--;
                return;
            }
            if (ExpeditionHolder.GetInstance().IsExpeditionSelected())
            {
                Expedition expedition = ExpeditionHolder.GetInstance().GetSelectedExpedition();
                if (expedition.IsStarted() && expedition.GetLength() - expedition.GetCompletedEvents() > 0)
                {
                    if (_ticksToNextEvent == 0)
                    {
                        Event evt = EventPoolManager.Instance.GetRandomEvent(expedition);
                        EventHandler.onTriggerExpeditionEvent.Invoke(evt);
                        Debug.Log($"Triggered Event {evt.name} for Expedition {expedition.GetName()}");
                        expedition.AddCompletedEvents(1);
                        ScheduleNextEvent(expedition, false);
                        return;
                    }
                    ScheduleNextEvent(expedition, true);
                }
            }
        }

        private void ScheduleNextEvent(Expedition exp, bool first)
        {
            if (exp.GetLength() - exp.GetCompletedEvents() > 0)
            {
                SimulationManager sim = SimulationManager.GetInstance();
                int bufferDayEnd = (int) Math.Round(10f / SimulationManager.TickDuration);
                int initialBuffer = (int) Math.Round(first ? 10 / SimulationManager.TickDuration : 0);
                int ticksWithoutBuffer = (int) ((sim.endOfDay - sim.timeValue) / SimulationManager.TickDuration) -
                                         bufferDayEnd - initialBuffer;
                int eventsLeft = exp.GetLength() - exp.GetCompletedEvents();
                int fraction = ticksWithoutBuffer / eventsLeft;
                _ticksToNextEvent = first ? initialBuffer : fraction;
            }
        }
    }
}