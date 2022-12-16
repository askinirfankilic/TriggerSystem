using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using TriggerSystem;

namespace TriggerSystemEditor
{
    /// <summary>
    /// Draws a bounding box to SceneView.
    /// </summary>
    [CustomEditor(typeof(BoxTrigger)), CanEditMultipleObjects]
    public class BoxTriggerEditor : UnityEditor.Editor
    {
        private BoxBoundsHandle _boxBoundsHandle = new();

        protected virtual void OnSceneGUI()
        {
            BoxTrigger boxTrigger = target as BoxTrigger;
            Transform boxTransform = boxTrigger.transform;

            BoxData boundsData = boxTrigger.Data;

            Bounds bounds = boundsData.BoxBounds;
            Vector3 center = bounds.center;
            Vector3 size = bounds.size;
            Color color = boundsData.HandleColor;

            _boxBoundsHandle.center = boxTransform.position + center;
            _boxBoundsHandle.size = size;
            _boxBoundsHandle.SetColor(color);

            EditorGUI.BeginChangeCheck();

            _boxBoundsHandle.DrawHandle();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(boxTrigger, "Change Bounds");

                Bounds newBounds = new Bounds();
                newBounds.center = _boxBoundsHandle.center;
                newBounds.size = _boxBoundsHandle.size;

                BoxData newBoxData = new BoxData
                {
#if UNITY_EDITOR
                    HandleColor = color,
#endif
                    BoxBounds = newBounds
                };

                boxTrigger.Data = newBoxData;
            }
        }
    }
}