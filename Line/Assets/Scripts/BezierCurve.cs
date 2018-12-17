using UnityEngine;

[System.Serializable]
public class BezierCurve {
	[SerializeField]private int segment;
	[SerializeField]private Vector3[] controlPoints;
	private Vector3[] points;

	public BezierCurve(int segment, Vector3[] controlPoints)
	{
		this.segment = segment <= 0 ? 1 : segment;
		this.controlPoints = controlPoints;
		this.points = Bezier.Points(this.segment, this.controlPoints);
	}

	public void Reset(int segment, Vector3[] controlPoints)
	{
		this.segment = segment <= 0 ? 1 : segment;
		if (this.segment != segment || this.controlPoints != controlPoints) {
			this.controlPoints = controlPoints;
			this.points = Bezier.Points (this.segment, this.controlPoints);
		}
	}

	public void SetSegment(int segment)
	{
		this.segment = segment <= 0 ? 1 : segment;
		if (this.segment != segment) {
			this.points = Bezier.Points (this.segment, this.controlPoints);
		}
	}

	public void SetControlPoints(Vector3[] controlPoints)
	{
		if (this.controlPoints != controlPoints) {
			this.controlPoints = controlPoints;
			this.points = Bezier.Points (this.segment, this.controlPoints);
		}
	}

	public Vector3[] GetPoints()
	{
		return this.points;
	}
}