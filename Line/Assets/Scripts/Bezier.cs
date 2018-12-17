using UnityEngine;

public class Bezier {
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

	public static Vector3[] Points(int segment, Vector3[] points)
	{
		if (points == null || segment <= 0) {
			return null;
		}
		if (points.Length < 2) {
			return points;
		}

		Vector3[] result = new Vector3[segment + 1];
		float step = 1.0f / segment;
		for (int i = 0; i <= segment; i++) {
			result [i] = Lerp (i * step, points);
		}
		return result;
	}
}
