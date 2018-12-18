using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(MonoBehaviour), true)]
public class BezierCurveEditor : Editor {

	private MonoBehaviour monoBehaviour;
	private List<FieldInfo> fieldTargets;

	void OnEnable()
	{
		monoBehaviour = target as MonoBehaviour;
		fieldTargets = new List<FieldInfo> ();
		FieldInfo[] fields = target.GetType ().GetFields (BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
		foreach (var item in fields) {
			if (item.FieldType == typeof(BezierCurve)) {
				fieldTargets.Add (item);
			}
		}
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		if (fieldTargets == null || fieldTargets.Count == 0) {
			return;
		}
		BezierCurveEditorDrawer.DrawColorOnInspectorGUI ();

		for (int i = 0; i < fieldTargets.Count; i++) {
			BezierCurve bezierCurve = fieldTargets [i].GetValue (monoBehaviour) as BezierCurve;
			if (bezierCurve != null) {
				bezierCurve.Reset ();
			}
		}
		serializedObject.ApplyModifiedProperties ();
	}

	void OnSceneGUI () {
		serializedObject.Update ();
		if (fieldTargets == null || fieldTargets.Count == 0) {
			return;
		}

		for (int i = 0; i < fieldTargets.Count; i++) {
			BezierCurveEditorDrawer.DrawOnSceneGUI (fieldTargets [i].GetValue (monoBehaviour) as BezierCurve, monoBehaviour);
		}
		serializedObject.ApplyModifiedProperties ();
	}
}