using UnityEngine;

public class Bezier {
	/// <summary>
	/// N阶插值，N为points的长度
	/// </summary>
	public static Vector3 Lerp(float t, Vector3[] points)
	{
		if (points.Length < 2) {
			return points [0];
		}
		Vector3[] nextPoints = new Vector3[points.Length - 1];
		for (int i = 0; i < points.Length - 1; i++) {
			nextPoints [i] = points [i] * (1 - t) + points [i + 1] * t;
		}
		return Lerp (t, nextPoints);
	}

	/// <summary>
	/// N阶贝塞尔点，N为points的长度
	/// </summary>
	public static Vector3[] Points(int segment, Vector3[] points)
	{
		if (points == null || points.Length == 0 || segment <= 0) {
			return null;
		}

		if (points.Length < 3) {
			return points;
		}

		Vector3[] result = new Vector3[segment + 1];
		float step = 1.0f / segment;
		for (int i = 0; i <= segment; i++) {
			result [i] = Lerp (i * step, points);
		}
		return result;
	}

	/// <summary>
	/// 一阶插值
	/// </summary>
	public static Vector3 Lerp(float t, Vector3 p0, Vector3 p1, Vector3 p2)
	{
		Vector3 p01 = (1 - t) * p0 + t * p1;
		Vector3 p12 = (1 - t) * p1 + t * p2;
		return (1 - t) * p01 + t * p12;
	}

	/// <summary>
	/// 一阶贝塞尔点
	/// </summary>
	public static Vector3[] Points(int segment, Vector3 p0, Vector3 p1, Vector3 p2)
	{
		if (segment <= 0) {
			return null;
		}
		Vector3[] result = new Vector3[segment + 1];
		float step = 1.0f / segment;
		for (int i = 0; i <= segment; i++) {
			result [i] = Lerp (i * step, p0, p1, p2);
		}
		return result;
	}
}