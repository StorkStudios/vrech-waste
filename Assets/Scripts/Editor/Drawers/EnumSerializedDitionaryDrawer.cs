using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.Events;
using System.Linq;
using NUnit.Framework;
using System.Drawing.Printing;

public class EnumSerializedDictionaryDrawer : PropertyDrawer
{
    private Dictionary<string, ReorderableList> lists = new Dictionary<string, ReorderableList>();
    private System.Type enumType => fieldInfo.FieldType.GenericTypeArguments[0];


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        ReorderableList list = lists[property.propertyPath];

        Rect labelRect = position;
        labelRect.height = EditorGUIUtility.singleLineHeight;
        DrawHeaderCallback(list, property.displayName, labelRect);

        position.yMin += EditorGUIUtility.singleLineHeight;

        if (list.serializedProperty.isExpanded)
        {
            list.DoList(position);
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        CreateListIfNecessary(property);

        ReorderableList list = lists[property.propertyPath];

        float result = EditorGUIUtility.singleLineHeight;

        if (list.serializedProperty.isExpanded)
        {
            result += list.GetHeight();
        }

        return result;
    }

    private void CreateListIfNecessary(SerializedProperty property)
    {
        if (!lists.ContainsKey(property.propertyPath) || lists[property.propertyPath].index > lists[property.propertyPath].count - 1)
        {
            SerializedProperty pairsListProperty = property.FindPropertyRelative("pairs");

            lists[property.propertyPath] = new ReorderableList(pairsListProperty.serializedObject, pairsListProperty, false, false, true, true);
            ReorderableList list = lists[property.propertyPath];

            list.drawElementCallback = (rect, index, isActive, isFocused) => DrawElementCallback(list, rect, index, isActive, isFocused);
            list.elementHeightCallback = (index) => ElementHeightCallback(list, index);
            list.onAddDropdownCallback = OnAddDropdownCallback;
            list.onCanAddCallback = CanAddCallback;
        }
    }

    private void DrawHeaderCallback(ReorderableList list, string displayName, Rect rect)
    {
        list.serializedProperty.isExpanded = EditorGUI.Foldout(rect, list.serializedProperty.isExpanded, displayName);
    }

    private void DrawElementCallback(ReorderableList list, Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);

        EditorGUI.PropertyField(rect, element, GUIContent.none);
    }

    private float ElementHeightCallback(ReorderableList list, int index)
    {
        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);

        return EditorGUI.GetPropertyHeight(element);
    }

    private void OnAddDropdownCallback(Rect buttonRect, ReorderableList list)
    {
        HashSet<string> existingValues = new HashSet<string>();
        foreach (SerializedProperty element in list.serializedProperty)
        {
            SerializedProperty keyProperty = element.FindPropertyRelative("Key").FindPropertyRelative("item");
            string valueName = keyProperty.enumNames[keyProperty.enumValueIndex];
            existingValues.Add(valueName);
        }

        GenericMenu menu = new GenericMenu();
        foreach (var enumValue in System.Enum.GetValues(enumType))
        {
            if (existingValues.Contains(enumValue.ToString()))
            {
                continue;
            }

            menu.AddItem(new GUIContent(enumValue.ToString()), false, (value) => AddClickHandler(list, value), enumValue);
        }

        menu.ShowAsContext();
    }

    private void AddClickHandler(ReorderableList list, object value) 
    {
        int index = list.serializedProperty.arraySize;
        list.serializedProperty.arraySize++;
        list.index = index;

        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
        SerializedProperty key = element.FindPropertyRelative("Key").FindPropertyRelative("item");
        key.enumValueIndex = System.Array.IndexOf(System.Enum.GetValues(value.GetType()), value);

        list.serializedProperty.serializedObject.ApplyModifiedProperties();
    }

    private bool CanAddCallback(ReorderableList list)
    {
        return list.serializedProperty.arraySize < System.Enum.GetValues(enumType).Length;
    }
}

