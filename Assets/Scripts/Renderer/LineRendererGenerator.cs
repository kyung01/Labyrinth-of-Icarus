using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererGenerator : MonoBehaviour
{
	static float LINE_SPACING_DISTANCE = 1.0f;
	LineRenderer lineRenderer;
	// Use this for initialization
	void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
		var posLeft = this.transform.position - this.transform.right * this.transform.lossyScale.x*0.5f;
		var posRight = this.transform.position + this.transform.right * this.transform.lossyScale.x*0.5f;
		float distance = (posRight - posLeft).magnitude;
		Vector3 direction = (posRight - posLeft).normalized;
		List<Vector3> positions = new List<Vector3>();
		float distanceRemaining = distance;
		positions.Add(posLeft);
		while (distanceRemaining > 0.001f)
		{
			var distanceAdvanced = Mathf.Min(distanceRemaining, LINE_SPACING_DISTANCE);
			positions.Add(positions[positions.Count - 1] + direction * distanceAdvanced);
			distanceRemaining -= LINE_SPACING_DISTANCE;
		}
		lineRenderer.positionCount = positions.Count;
		for(int i = 0; i < positions.Count; i++)
		{
			lineRenderer.SetPosition(i, positions[i]);
		}
		lineRenderer.widthMultiplier = this.transform.lossyScale.y;

	}

	// Update is called once per frame
	void Update()
	{

	}
}
