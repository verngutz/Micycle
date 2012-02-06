﻿using System;
using System.Collections.Generic;

namespace MiUtil
{
    /// <summary>
    /// MiEventQueue allows state changes (encapsulated as MiEvents) to be requested and applied separately in time.
    /// </summary>
    public class MiEventQueue
    {
        private Queue<MiEvent> eventQueue;
        private LinkedList<int> timesliceQueue;

        public MiEventQueue()
        {
            eventQueue = new Queue<MiEvent>();
            eventQueue.Enqueue(null);

            timesliceQueue = new LinkedList<int>();
            timesliceQueue.AddLast(0);
        }

        /// <summary>
        /// Add a new MiEvent to this MiEventQueue.
        /// </summary>
        /// <param name="anEvent">The state change to add</param>
        /// <param name="timeslice">The amount of time between after the specified state change is applied and before the next state change can be applied.</param>
        public void AddEvent(MiEvent anEvent, int timeslice)
        {
            eventQueue.Enqueue(anEvent);
            timesliceQueue.AddLast(timeslice);
        }

        /// <summary>
        /// Get the next state change to apply.
        /// </summary>
        /// <returns>
        /// The next state change, null if there are no such changes 
        /// or this MiEventQueue is waiting for the previously applied state change to finish.
        /// </returns>
        public MiEvent GetNextEvent()
        {
            timesliceQueue.First.Value = timesliceQueue.First.Value - 1;
            if (timesliceQueue.First.Value < 0)
            {
                timesliceQueue.RemoveFirst();
                if (timesliceQueue.Count == 0)
                {
                    timesliceQueue.AddLast(0);
                    eventQueue.Enqueue(null);
                }
                return eventQueue.Dequeue();
            }
            else
            {
                return null;
            }
        }
    }
}