using UnityEngine;

namespace UGF.Types.Editor.Tests.IMGUI
{
    [CreateAssetMenu(menuName = "Tests/Editor/TestTypesDropdownScriptableObject")]
    public class TestTypesDropdownScriptableObject : ScriptableObject
    {
        [SerializeField] private string m_type;

        public string Type { get { return m_type; } set { m_type = value; } }
    }
}
