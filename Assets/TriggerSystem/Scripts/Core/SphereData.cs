using System;
using UnityEngine;

namespace TriggerSystem
{
    [Serializable]
    public struct SphereData
    {
#if UNITY_EDITOR
        public Color HandleColor
        {
            get => _handleColor;
            set => _handleColor = value;
        }
#endif

        public float Radius
        {
            get => _radius;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                _radius = value;
            }
        }

        public Vector3 Center
        {
            get => _center;
            set => _center = value;
        }
#if UNITY_EDITOR
        [SerializeField]
        private Color _handleColor;
#endif

        [Min(0)]
        [SerializeField]
        private float _radius;

        [SerializeField]
        private Vector3 _center;

#if UNITY_EDITOR
        private SphereData(Color handleColor, float radius, Vector3 center)
        {
            _handleColor = handleColor;
            _radius = radius;
            _center = center;
        }
#else
    private SphereData(float radius, Vector3 center)
    {
        _radius = radius;
        _center = center;
    }
#endif
    }
}