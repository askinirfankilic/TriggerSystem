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
            BoxBounds = new Bounds()
        };

        public BoxData Data
        {
            get => _data;
            set => _data = value;
        }
    }
}