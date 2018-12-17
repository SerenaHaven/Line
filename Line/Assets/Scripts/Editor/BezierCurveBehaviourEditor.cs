using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurveBehaviour))]
public class BezierCurveBehaviourEditor : Editor {
	private SerializedProperty propertySegment;
	private SerializedProperty propertyControlPoints;
	private SerializedProperty propertyPoints;
	private SerializedProperty propertyControlColor;
	private SerializedProperty propertyColor;
	private BezierCurveBehaviour bezierCurveBehaviour;

	void OnEnable () {
		propertySegment = serializedObject.FindProperty ("segment");
		propertyControlPoints = serializedObject.FindProperty ("controlPoints");
		propertyPoints = serializedObject.FindProperty ("points");
		propertyControlColor = serializedObject.FindProperty ("controlColor");
		propertyColor = serializedObject.FindProperty ("color");
		bezierCurveBehaviour = target as BezierCurveBehaviour;
		GetPoints ();
	}

	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();

		EditorGUILayout.PropertyField (propertyControlColor);
		EditorGUILayout.PropertyField (propertyColor);

		EditorGUI.BeginChangeCheck ();
		EditorGUILayout.PropertyField (propertySegment);
		EditorGUILayout.PropertyField (propertyControlPoints);
		int size = EditorGUILayout.IntField ("Size", propertyControlPoints.arraySize);
		propertyControlPoints.arraySize = size < 0 ? 0 : size;

		if (EditorGUILayout.BeginFadeGroup (propertyControlPoints.isExpanded == true ? 1 : 0)) {
			EditorGUI.indentLevel++;
			int length = propertyControlPoints.arraySize;
			for (int i = 0; i < length; i++) {
				EditorGUILayout.PropertyField (propertyControlPoints.GetArrayElementAtIndex (i));
			}
			EditorGUI.indentLevel--;
		}
		EditorGUILayout.EndFadeGroup ();
		if (EditorGUI.EndChangeCheck () == true) {
			Undo.RecordObject (bezierCurveBehaviour, "Modify Control Point");
			EditorUtility.SetDirty (bezierCurveBehaviour);
			GetPoints ();
		}
		serializedObject.ApplyModifiedProperties ();
	}

	void OnSceneGUI () {
		serializedObject.Update ();

		if (propertyControlPoints == null || propertyControlPoints.arraySize < 2 || propertyPoints == null || propertyPoints.arraySize < 2) {
			return;
		}

		Quaternion rotation = Tools.pivotRotation == PivotRotation.Local ? bezierCurveBehaviour.transform.rotation : Quaternion.identity;
		int length = propertyControlPoints.arraySize;
		for (int i = 0; i < length; i++) {
			SerializedProperty propertyPoint = propertyControlPoints.GetArrayElementAtIndex (i);
			Vector3 point = propertyPoint.vector3Value;

			EditorGUI.BeginChangeCheck ();
			point = Handles.PositionHandle (bezierCurveBehaviour.transform.TransformPoint (point), rotation);
			if (EditorGUI.EndChangeCheck () == true) {
				Undo.RecordObject (bezierCurveBehaviour, "Modify Control Point");
				propertyPoint.vector3Value = bezierCurveBehaviour.transform.InverseTransformPoint (point);
				EditorUtility.SetDirty (bezierCurveBehaviour);
				GetPoints ();
			}
		}

		Handles.color = propertyControlColor == null ? Color.yellow : propertyControlColor.colorValue;
		for (int i = 0; i < propertyControlPoints.arraySize - 1; i++) {
			Handles.DrawLine (bezierCurveBehaviour.transform.TransformPoint (propertyControlPoints.GetArrayElementAtIndex (i).vector3Value), 
				bezierCurveBehaviour.transform.TransformPoint (propertyControlPoints.GetArrayElementAtIndex (i + 1).vector3Value));
		}	
	
		Handles.color = propertyColor == null ? Color.cyan : propertyColor.colorValue;
		for (int i = 0; i < propertyPoints.arraySize - 1; i++) {
			Handles.DrawLine (bezierCurveBehaviour.transform.TransformPoint (propertyPoints.GetArrayElementAtIndex (i).vector3Value),
				bezierCurveBehaviour.transform.TransformPoint (propertyPoints.GetArrayElementAtIndex (i + 1).vector3Value));
		}
		Handles.color = Color.white;
		serializedObject.ApplyModifiedProperties ();
	}

	private void GetPoints()
	{
		Vector3[] controlPoints = new Vector3[propertyControlPoints.arraySize];
		for (int i = 0; i < controlPoints.Length; i++) {
			controlPoints [i] = propertyControlPoints.GetArrayElementAtIndex (i).vector3Value;
		}

		Vector3[] points = Bezier.Points (propertySegment.intValue, controlPoints);
		propertyPoints.ClearArray ();
		if (points == null) {
			return;
		}
		for (int i = 0; i < points.Length; i++) {
			propertyPoints.InsertArrayElementAtIndex (i);
			propertyPoints.GetArrayElementAtIndex (i).vector3Value = points [i];
		}
		serializedObject.ApplyModifiedProperties ();
	}
}