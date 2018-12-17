using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Line))]
public class LineEditor : Editor {

	private SerializedProperty propertyStart;
	private SerializedProperty propertyEnd;
	private Line line;
	void OnEnable()
	{
		propertyStart = serializedObject.FindProperty ("start");
		propertyEnd = serializedObject.FindProperty ("end");
		line = target as Line;
	}

	void OnSceneGUI()
	{
		if (line.enabled == false) {
			return;
		}
		Vector3 start = line.transform.TransformPoint (propertyStart.vector3Value);
		Vector3 end = line.transform.TransformPoint (propertyEnd.vector3Value);
		Handles.DrawLine (start, end);
		Quaternion rotation = Tools.pivotRotation == PivotRotation.Local ? line.transform.rotation : Quaternion.identity;
		EditorGUI.BeginChangeCheck ();
		start = Handles.DoPositionHandle (start, rotation);
		if (EditorGUI.EndChangeCheck () == true) {
			Undo.RecordObject (line, "Move Start");
			EditorUtility.SetDirty (line);
			propertyStart.vector3Value = line.transform.InverseTransformPoint (start);
		}

		EditorGUI.BeginChangeCheck ();
		end = Handles.DoPositionHandle (end, rotation);
		if (EditorGUI.EndChangeCheck () == true) {
			Undo.RecordObject (line, "Move End");
			EditorUtility.SetDirty (line);
			propertyEnd.vector3Value = line.transform.InverseTransformPoint (end);
		}
		serializedObject.ApplyModifiedProperties ();
	}
}