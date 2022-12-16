using System;
using UnityEngine;

namespace TriggerSystem
{
    [AddComponentMenu("Trigger System/Box Trigger")]
    public class BoxTrigger : TriggerBase
    {
        [SerializeField]
        private BoxData _data = new BoxData
        {
#if UNITY_EDITOR
            HandleColor = Color.white,
#endif
            BoxBounds = new Bounds
            {
                center = Vector3.zero,
                size = Vector3.one
            }
        };

        public BoxData Data
        {
            get => _data;
            set => _data = value;
        }
    }
}