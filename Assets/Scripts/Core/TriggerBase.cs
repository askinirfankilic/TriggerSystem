using System;
using UnityEngine;

namespace TriggerSystem
{
    /// <summary>
    /// Base class for all triggers.
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class TriggerBase : MonoBehaviour
    {
        [ReadOnly]
        public ShapeType Shape;

        [HideInInspector]
        public CollisionType Collision;
        public Action<TriggerBase> TriggerStayed;

        public void InvokeStayed(TriggerBase other)
        {
            TriggerStayed?.Invoke(other);
            OnStayed();
        }

        protected virtual void OnStayed()
        {
            // Override this
        }

        private void OnEnable()
        {
            TriggerBaker.Instance.Add(this);
        }

        private void OnDisable()
        {
            if (TriggerBaker.Instance == null) return;
            TriggerBaker.Instance.Remove(this);
        }
    }
}