using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class VerletRopeLineRenderer : VerletRope
{
	LineRenderer lineRenderer;
	public override void Start()
	{
		base.Start();
		//conect ropes to linerenderer
		lineRenderer = GetComponent<LineRenderer>();
		
	}
	void Update()
	{
		lineRenderer.positionCount = links.Count;
		for(int i = 0;  i< links.Count; i++)
		{
			lineRenderer.SetPosition(i,new Vector3(links[i].Position.x, links[i].Position.y,0) );
		}
	}
}
