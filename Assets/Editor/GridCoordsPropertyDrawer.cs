using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(GridCoords))]
public class GridCoordsPropertyDrawer : PropertyDrawer {
	

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		EditorGUI.BeginProperty(position,label,property);
		position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), new GUIContent("GCoords (row,col)"));
		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
		Rect rowRect = new Rect(position.x, position.y, 30, position.height);
		Rect colRect = new Rect(position.x + 35, position.y, 30, position.height);
		EditorGUI.PropertyField(rowRect, property.FindPropertyRelative ("row"), GUIContent.none);
		EditorGUI.PropertyField(colRect, property.FindPropertyRelative ("col"), GUIContent.none);

		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}
}
