using System;
using UGF.Types.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.Types.Editor.Tests.IMGUI
{
    [CustomEditor(typeof(TestTypesDropdownScriptableObject))]
    public class TestTypesDropdownScriptableObjectEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyType;
        private TypesDropdown m_dropdown;

        private void OnEnable()
        {
            m_propertyType = serializedObject.FindProperty("m_type");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Rect rect = EditorGUILayout.GetControlRect();
            Rect rectButton = EditorGUI.PrefixLabel(rect, new GUIContent(m_propertyType.displayName));

            if (EditorGUI.DropdownButton(rectButton, new GUIContent(m_propertyType.stringValue), FocusType.Keyboard))
            {
                ShowDropdown(rectButton);
            }
        }

        private bool OnDropdownValidateType(Type type)
        {
            return true;
        }

        private void OnDropdownTypeSelected(Type type)
        {
            m_propertyType.stringValue = type.AssemblyQualifiedName;
            m_propertyType.serializedObject.ApplyModifiedProperties();
        }

        private void ShowDropdown(Rect rect)
        {
            if (m_dropdown == null)
            {
                m_dropdown = TypesEditorGUIUtility.GetTypesDropdown(OnDropdownValidateType);
                m_dropdown.Selected += OnDropdownTypeSelected;
            }

            m_dropdown.Show(rect);
        }
    }
}
