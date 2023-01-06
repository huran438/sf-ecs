using SFramework.Core.Editor;
using SFramework.ECS.Runtime;
using UnityEditor;

namespace SFramework.ECS.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SFComponent<>), true)]
    public class SFComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty _valueSerializedProperty;

        private void OnEnable()
        {
            _valueSerializedProperty = serializedObject.FindProperty("_value");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            foreach (var serializedProperty in _valueSerializedProperty.GetVisibleChildren())
            {
                EditorGUILayout.PropertyField(serializedProperty);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void OnDisable()
        {
            _valueSerializedProperty = null;
        }
    }
}