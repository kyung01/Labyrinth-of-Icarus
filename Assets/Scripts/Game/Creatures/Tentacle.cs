using UnityEngine;
using System.Collections;

public class Tentacle : EntityEnemy
{
	[SerializeField]
	float CHASING_FORCE = 10;
	[SerializeField]
	float SWINGING_FORCE = 10;
	[SerializeField]
	float maximumExtendLength;

	float maximumLength;
	public GameObject pivotObject;
	DistanceJoint2D joint;
	public float getTenatcleUnextendedLength()
	{
		return maximumLength;
	}
	public float getTentacleLength()
	{
		return maximumExtendLength + maximumLength;
	}

	public override void Start()
	{
		base.Start();
		var rayTest = Physics2D.Raycast(this.transform.position, this.transform.right, 100, Layers.World);
		pivotObject = new GameObject();
		pivotObject.transform.position = rayTest.point;
		pivotObject.transform.parent = this.transform.parent;
		pivotObject.name = this.gameObject.name + "'s tentacle pivot point";
		var pivotRigidbody = pivotObject.AddComponent<Rigidbody2D>();
		maximumLength = (this.transform.position - pivotObject.transform.position).magnitude;
		pivotRigidbody.isKinematic = true;

		joint = gameObject.AddComponent<DistanceJoint2D>();
		joint.maxDistanceOnly = true;
		joint.connectedBody = pivotRigidbody;
	}
	public override void evntDetectedPlayer()
	{
		base.evntDetectedPlayer();
		joint.distance = maximumLength + maximumExtendLength;
	}
	public override void fixedUpdateIDL()
	{
		base.fixedUpdateIDL();

	}
	public override void fixedUpdateEngaged()
	{
		base.fixedUpdateEngaged();
		rigidbody.AddForce((Player.GetPlayerPosition() - this.transform.position).normalized * CHASING_FORCE * Time.fixedDeltaTime, ForceMode2D.Impulse);

	}

}
