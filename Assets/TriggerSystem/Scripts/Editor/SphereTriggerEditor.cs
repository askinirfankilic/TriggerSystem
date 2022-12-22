using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using TriggerSystem;

namespace TriggerSystemEditor
{
    /// <summary>
    /// Draws a bounding sphere to SceneView.
    /// </summary>
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

            EditorGUI.BeginChangeCheck();

            _sphereBoundsHandle.DrawHandle();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(sphereTrigger, "Change Bounds");

                SphereData newSphereData = new SphereData
                {
                    HandleColor = color,
                    Radius = radius,
                    Center = center
                };

                sphereTrigger.Data = newSphereData;
            }
        }
    }
}