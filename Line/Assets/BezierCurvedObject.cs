using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurvedObject : MonoBehaviour {
	[SerializeField]private BezierCurve bezierCurve1;
//	[SerializeField]private BezierCurve bezierCurve2;
	void Reset () {
		bezierCurve1 = new BezierCurve (50, new Vector3[]{ Vector3.zero, Vector3.right * 5 + Vector3.up * 5, Vector3.right * 10 });	
//		bezierCurve2 = new BezierCurve (50, new Vector3[]{ -Vector3.zero, -Vector3.right * 5 + Vector3.up * 5, -Vector3.right * 10 });	
	}
}