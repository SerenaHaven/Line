using UnityEngine;
using UnityEditor;

public class BezierCurveEditorDrawer {

	private static Color controlColor = Color.yellow;
	private static Color color = Color.cyan;

	public static void DrawColorOnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck ();
		controlColor = EditorGUILayout.ColorField ("Control Color", controlColor);
		color = EditorGUILayout.ColorField ("Color", color);
		if (EditorGUI.EndChangeCheck () == true) {
			SceneView.RepaintAll ();
		}
	}

	public static void DrawOnInspectorGUI(BezierCurve bezierCurve)
	{
		if (bezierCurve == null) {
			return;
		}

		DrawColorOnInspectorGUI ();
			
		EditorGUI.BeginChangeCheck ();
		bezierCurve.SetSegment (EditorGUILayout.IntField ("Segment", bezierCurve.GetSegment ()));
		Vector3[] controlPoints = bezierCurve.GetControlPoints ();

		EditorGUILayout.BeginFadeGroup (EditorGUILayout.Foldout(true,"ControlPoints") == true ? 1 : 0);
		EditorGUI.indentLevel++;
		for (int i = 0; i < controlPoints.Length; i++) {
			bezierCurve.SetControlPoint (i, EditorGUILayout.Vector3Field ("Element " + i, controlPoints [i]));
		}
		EditorGUI.indentLevel--;
		EditorGUILayout.EndFadeGroup ();
	}

	public static void DrawOnSceneGUI(BezierCurve bezierCurve, MonoBehaviour monoBehaviour = null)
	{
		if (bezierCurve == null) {
			return;
		}
		Quaternion rotation = (Tools.pivotRotation == PivotRotation.Local && monoBehaviour != null) ? monoBehaviour.transform.rotation : Quaternion.identity;
		Vector3[] controlPoints = bezierCurve.GetControlPoints ();
		if (controlPoints == null) {
			return;
		}

		for (int i = 0; i < controlPoints.Length; i++) {
			EditorGUI.BeginChangeCheck ();
			Vector3 controlPoint = Handles.DoPositionHandle (monoBehaviour == null ? controlPoints [i] : monoBehaviour.transform.TransformPoint (controlPoints [i]), rotation);
			if (EditorGUI.EndChangeCheck () == true) {
				if (monoBehaviour == null) {
					bezierCurve.SetControlPoint (i, controlPoint);
				} else {
					Undo.RecordObject (monoBehaviour, "Modify Control Point");
					controlPoint = monoBehaviour.transform.InverseTransformPoint (controlPoint);
					bezierCurve.SetControlPoint (i, controlPoint);
					EditorUtility.SetDirty (monoBehaviour);
				}
			}
		}

		Handles.color = controlColor;
		if (monoBehaviour == null) {
			for (int i = 0; i < controlPoints.Length - 1; i++) {
				Handles.DrawLine (controlPoints [i], controlPoints [i + 1]);
			}
		} else {
			for (int i = 0; i < controlPoints.Length - 1; i++) {
				Handles.DrawLine (monoBehaviour.transform.TransformPoint (controlPoints [i]), monoBehaviour.transform.TransformPoint (controlPoints [i + 1]));
			}
		}

		Vector3[] points = bezierCurve.GetPoints ();
		if (points == null) {
			return;
		}
		Handles.color = color;
		if (monoBehaviour == null) {
			for (int i = 0; i < points.Length - 1; i++) {
				Handles.DrawLine (points [i], points [i + 1]);
			}
		} else {
			for (int i = 0; i < points.Length - 1; i++) {
				Handles.DrawLine (monoBehaviour.transform.TransformPoint (points [i]), monoBehaviour.transform.TransformPoint (points [i + 1]));
			}
		}
		Handles.color = Color.white;
	}
}