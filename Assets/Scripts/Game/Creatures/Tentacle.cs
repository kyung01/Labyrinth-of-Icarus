using UnityEngine;
using System.Collections;

public class Tentacle : EntityEnemy
{
	public Vector3 originPosition;

	float maximumLength;

	public override void Start()
	{
		base.Start();
		var rayTest = Physics2D.Raycast(this.transform.position, this.transform.right, 100, Layers.World);
		originPosition = rayTest.point;
		maximumLength = (this.transform.position - originPosition).magnitude;
		var pivotObject = new GameObject();
		pivotObject.transform.position = rayTest.point;
		pivotObject.transform.parent = this.transform.parent;
		pivotObject.name = this.gameObject.name + "'s tentacle pivot point";
		var pivotRigidbody = pivotObject.AddComponent<Rigidbody2D>();
		pivotRigidbody.isKinematic = true;

		var joint = gameObject.AddComponent<SpringJoint2D>();
		joint.connectedBody = pivotRigidbody;
	}

	public override void Update()
	{
		base.Update();
		var dis = this.transform.position - originPosition;
		if (dis.sqrMagnitude > maximumLength * maximumLength)
		{
			//this.transform.position = originPosition + dis.normalized * maximumLength;
		}
	}
}
