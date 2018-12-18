using UnityEngine;

[System.Serializable]
public class BezierCurve{
	[SerializeField]private int segment;
	[SerializeField]private Vector3[] controlPoints;
	private Vector3[] points;

	public BezierCurve(int segment, Vector3[] controlPoints)
	{
		this.segment = segment <= 0 ? 1 : segment;
		this.controlPoints = controlPoints;
		this.points = Bezier.Points(this.segment, this.controlPoints);
	}

	public BezierCurve(int segment, Vector3 p0, Vector3 p1, Vector3 p2)
	{
		this.segment = segment;
		this.controlPoints = new Vector3[]{ p0, p1, p2 };
		this.points = Bezier.Points (this.segment, p0, p1, p2);
	}

	/// <summary>
	/// 重新计算贝塞尔点
	/// </summary>
	public void Reset()
	{
		this.points = Bezier.Points (this.segment, this.controlPoints);
	}

	/// <summary>
	/// 通过新的参数重新计算贝塞尔点
	/// </summary>
	public void Reset(int segment, Vector3[] controlPoints)
	{
		this.segment = segment <= 0 ? 1 : segment;
		if (this.segment != segment || this.controlPoints != controlPoints) {
			this.controlPoints = controlPoints;
			this.points = Bezier.Points (this.segment, this.controlPoints);
		}
	}

	/// <summary>
	/// 设置段落数并更新贝塞尔点
	/// </summary>
	public void SetSegment(int segment)
	{
		this.segment = segment <= 0 ? 1 : segment;
		if (this.segment != segment) {
			this.points = Bezier.Points (this.segment, this.controlPoints);
		}
	}

	/// <summary>
	/// 设置某个控制点并更新贝塞尔点
	/// </summary>
	public void SetControlPoint(int index, Vector3 newControlPoint)
	{
		if (index < this.controlPoints.Length && this.controlPoints [index] != newControlPoint) {
			this.controlPoints [index] = newControlPoint;
			this.points = Bezier.Points (this.segment, this.controlPoints);
		}
	}

	/// <summary>
	/// 设置所有控制点并更新贝塞尔点
	/// </summary>
	public void SetControlPoints(Vector3[] controlPoints)
	{
		if (this.controlPoints != controlPoints) {
			this.controlPoints = controlPoints;
			this.points = Bezier.Points (this.segment, this.controlPoints);
		}
	}

	/// <summary>
	/// 获取段落数
	/// </summary>
	public int GetSegment()
	{
		return this.segment;
	}

	/// <summary>
	/// 获取所有控制点
	/// </summary>
	public Vector3[] GetControlPoints()
	{
		return this.controlPoints;
	}

	/// <summary>
	/// 获取所有贝塞尔点
	/// </summary>
	public Vector3[] GetPoints()
	{
		return this.points;
	}

	/// <summary>
	/// 获取简单一阶贝塞尔曲线
	/// </summary>
	public static BezierCurve Simple(Vector3 p0, Vector3 p1, Vector3 p2, int segment = 50)
	{
		return new BezierCurve (segment, p0, p1, p2);
	}
}