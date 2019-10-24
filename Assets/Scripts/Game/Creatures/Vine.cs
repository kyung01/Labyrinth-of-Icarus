using UnityEngine;
using System.Collections;

/// <summary>
/// At the beginning of the game, Vine roots to the target
/// Then it hangs around. When approached by the player, they
/// </summary>
public class Vine : EntityEnemy
{
	[SerializeField]
	float forceChasing;
	[SerializeField]
	float maximumDistance;
	Vector3 originPosition;
	public override void Start()
	{
		base.Start();
		var hitTest = Physics2D.Raycast(this.transform.position, this.transform.right, 100, Layers.World);
		originPosition = hitTest.point;
		Debug.Log(originPosition);
	}
	public override void fixedUpdateEngaged()
	{
		base.fixedUpdateEngaged();
		var dir = Player.GetPlayerTruePosition() -this.transform.position;
		//rigidbody.AddForce(dir * forceChasing * Time.fixedDeltaTime, ForceMode2D.Impulse);
	}
	public override void Update()
	{
		base.Update();
		var dis = this.transform.position-originPosition;
		if(dis.magnitude > maximumDistance)
		{
			Debug.Log(dis.magnitude  + " " +  originPosition +""+ dis.normalized +""+ maximumDistance);
			rigidbody.position = originPosition + dis.normalized * maximumDistance;
			//rigidbody.velocity = Vector2.zero;
			Debug.Log("Fixed Position " + this.transform.position + " should be " +( originPosition + dis.normalized * maximumDistance) );
		}
	}
}
