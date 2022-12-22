using System;
using UnityEngine;

namespace TriggerSystem
{
    /// <summary>
    /// Sphere shaped trigger.
    /// </summary>
    [AddComponentMenu("Trigger System/Sphere Trigger")]
    public class SphereTrigger : TriggerBase
    {
        [SerializeField]
        private SphereData _data;

        public SphereData Data
        {
            get => _data;
            set => _data = value;
        }
    }
}