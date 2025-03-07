using UnityEditor;
using UnityEngine;
// ReadOnly 속성을 Inspector에서 처리하는 Drawer
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 기존 GUI 상태를 저장
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label);
        // GUI 상태를 원래대로 복원
        GUI.enabled = true;
    }
}
#endif