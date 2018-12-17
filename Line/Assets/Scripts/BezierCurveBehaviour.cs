using UnityEngine;

public class BezierCurveBehaviour : MonoBehaviour {
	[Range(1, 200)][SerializeField]private int segment = 50;
	[SerializeField]private Vector3[] controlPoints;
	[HideInInspector][SerializeField]private Vector3[] points;

	#if UNITY_EDITOR
	#pragma warning disable 0414
	[SerializeField]private Color controlColor = Color.yellow;
	[SerializeField]private Color color = Color.cyan;
	#pragma warning restore 0414
	#endif

	public void Intialize(int segment, Vector3[] controlPoints)
	{
		this.segment = segment <= 0 ? 1 : segment;
		this.controlPoints = controlPoints;
		this.points = Bezier.Points(segment, controlPoints);
	}

	public Vector3[] GetPoints()
	{
		return points;
	}

	void Reset()
	{
		segment = 50;
		controlPoints = new Vector3[]{ 
			Vector3.zero,
			Vector3.right * 5 + Vector3.up * 5,
			Vector3.right * 10,
		};
		points = Bezier.Points(segment, controlPoints);
		#if UNITY_EDITOR
		controlColor = Color.yellow;
		color = Color.cyan;
		#endif
	}
}