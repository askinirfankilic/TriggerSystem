using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace TriggerSystem
{
    [CustomEditor(typeof(SphereTrigger)), CanEditMultipleObjects]
    public class SphereTriggerEditor : Editor
    {
        private SphereBoundsHandle _sphereBoundsHandle = new();

        protected virtual void OnSceneGUI()
        {
            SphereTrigger sphereTrigger = target as SphereTrigger;
            Transform sphereTransform = sphereTrigger.transform;

            SphereData sphereData = sphereTrigger.Data;
            float radius = sphereData.Radius;
            Vector3 center = sphereData.Center;
            Color color = sphereData.HandleColor;

            _sphereBoundsHandle.center = sphereTransform.position + center;
            _sphereBoundsHandle.radius = radius;
            _sphereBoundsHandle.SetColor(color);

            // draw the handle
            EditorGUI.BeginChangeCheck();

            _sphereBoundsHandle.DrawHandle();

            if (EditorGUI.EndChangeCheck())
            {
                // record the target object before setting new values so changes can be undone/redone
                Undo.RecordObject(sphereTrigger, "Change Bounds");

                // copy the handle's updated data back to the target object
                SphereData newSphereData = new SphereData
                {
#if UNITY_EDITOR
                    HandleColor = color,
#endif
                    Radius = radius,
                    Center = center
                };

                sphereTrigger.Data = newSphereData;
            }
        }
    }
}