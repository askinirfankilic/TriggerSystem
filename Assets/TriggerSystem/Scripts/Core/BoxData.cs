using System;
using UnityEngine;

namespace TriggerSystem
{
    [Serializable]
    public struct BoxData
    {
#if UNITY_EDITOR
        public Color HandleColor
        {
            get => _handleColor;
            set => _handleColor = value;
        }
#endif

        public Bounds BoxBounds
        {
            get => _boxBounds;
            set => _boxBounds = value;
        }

#if UNITY_EDITOR
        [SerializeField]
        private Color _handleColor;
#endif

        [SerializeField]
        private Bounds _boxBounds;


#if UNITY_EDITOR
        public BoxData(Color handleColor, Bounds boxBounds)
        {
            _handleColor = handleColor;
            _boxBounds = boxBounds;
        }
#else
        public BoxData(Bounds boxBounds)
        {
            _boxBounds = boxBounds;
        }
#endif
    }
}