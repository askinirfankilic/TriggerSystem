using System.Collections.Generic;
using UnityEngine;

namespace TriggerSystem
{
    /// <summary>
    /// A singleton used to check the active state of any trigger.
    /// </summary>
    [AddComponentMenu("Trigger System/Trigger Baker")]
    public class TriggerBaker : MonoSingleton<TriggerBaker>
    {
        public List<TriggerBase> Triggers = new();

        public void Add(TriggerBase trigger)
        {
            Triggers.Add(trigger);
        }

        public void Remove(TriggerBase trigger)
        {
            Triggers.Remove(trigger);
        }
    }
}