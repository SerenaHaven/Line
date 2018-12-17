using UnityEngine;

public class Line : MonoBehaviour {
	[SerializeField]private Vector3 start = Vector3.zero;
	[SerializeField]private Vector3 end = Vector3.zero;

	void Reset () {
		this.start = Vector3.zero;
		this.end = Vector3.zero;
	}

	public void Initialize(Vector3 start, Vector3 end)
	{
		this.start = start;
		this.end = end;
	}

	public Vector3 GetStart()
	{
		return this.start;
	}

	public Vector3 GetEnd()
	{
		return this.end;
	}
}