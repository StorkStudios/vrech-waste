using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

[CustomPropertyDrawer(typeof(SerializedPair<NullableObject<GameObject>, float>))]
public class SerializedPairDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty value = property.FindPropertyRelative("Value");
        SerializedProperty key = property.FindPropertyRelative("Key");

        EditorGUI.BeginProperty(position, label, property);
        Rect valuePosition = new Rect(position);
        Rect keyPosition = new Rect(position);

        valuePosition.yMin += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        keyPosition.height = EditorGUIUtility.singleLineHeight;

        EditorGUI.PropertyField(valuePosition, value, new GUIContent("Value"), true);
        EditorGUI.PropertyField(keyPosition, key, new GUIContent("Key"));

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float keyHeight = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Key"), label);
        float valueHeight = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Value"), label);
        return keyHeight + valueHeight + EditorGUIUtility.standardVerticalSpacing;
    }
}