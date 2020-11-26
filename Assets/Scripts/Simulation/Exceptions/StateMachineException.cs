using System;
using UnityEngine;

namespace Simulation.Exceptions
{
    public class StateMachineException : Exception
    {
        public StateMachineException()
        {
            base.GetBaseException();
        }

        public StateMachineException(string message)
        {
            base.GetBaseException();
            Debug.LogError(message);
        }
    }
}