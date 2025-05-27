using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StatEntry))]
public class StatEntryDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var statTypeProp = property.FindPropertyRelative("StatType");

        string displayName = statTypeProp.enumDisplayNames[statTypeProp.enumValueIndex];
        label = new GUIContent(displayName);

        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}