using System;
using UnityEngine;

namespace TriggerSystem
{
    public class SphereTrigger : MonoBehaviour
    {
        public Action<SphereTrigger> SphereTriggerEntered;
        public Action<SphereTrigger> SphereTriggerStayed;
        public Action<SphereTrigger> SphereTriggerExited;

        [SerializeField]
        private SphereData _data;

        public SphereData Data
        {
            get => _data;
            set => _data = value;
        }

        public void InvokeEntered(SphereTrigger other)
        {
            SphereTriggerEntered?.Invoke(other);
        }

        public void InvokeStayed(SphereTrigger other)
        {
            SphereTriggerStayed?.Invoke(other);
        }

        public void InvokeExited(SphereTrigger other)
        {
            SphereTriggerExited?.Invoke(other);
        }
    }
}