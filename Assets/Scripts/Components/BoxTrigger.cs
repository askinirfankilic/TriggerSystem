using System;
using UnityEngine;

namespace TriggerSystem
{
    [AddComponentMenu("Trigger System/Box Trigger")]
    public class BoxTrigger : TriggerBase
    {
        [SerializeField]
        private BoxData _data;

        public BoxData Data
        {
            get => _data;
            set => _data = value;
        }
    }
}