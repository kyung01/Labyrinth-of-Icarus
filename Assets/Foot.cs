using UnityEngine;
using System.Collections;



public class Foot : MonoBehaviour
{
	public enum STATUS {IDL, ATTACHED, ATTACHING }
	public STATUS status = STATUS.IDL;


	[SerializeField]
	public Rigidbody2D attachedRigidBody;
	[SerializeField]
	public Transform attachedBody;
	[SerializeField]
	public float maximumLength = 1.0f;
	/// <summary>
	/// Targetting object can lost in case foot losess its connection
	/// </summary>
	Transform targettingObject;
	public Vector3 targetPositionToAttach = Vector3.zero;
	
	public bool IsAvailable { get{ return status == STATUS.IDL; } }

	public void attach(Transform target, Vector2 position, float breakLimit = 1)
	{
		if(((position - new Vector2(attachedBody.transform.position.x, attachedBody.transform.position.y)).magnitude > maximumLength) )
		{
			Debug.Log("invalid attach call refused as unreacable " + (position - new Vector2(attachedBody.transform.position.x, attachedBody.transform.position.y)).magnitude);
			return;
		}

		status = STATUS.ATTACHING;
		targettingObject = target;
		targetPositionToAttach = position;

	}

	public void deAttach()
	{
		Debug.Log("DeAttaching to " + targettingObject);
		status = STATUS.IDL;
		targettingObject = null;
		targetPositionToAttach = Vector3.zero;
	}

	// Use this for initialization
	void Start()
	{

	}

	void phyiscsUpdate()
	{
		switch (status) {
			case STATUS.ATTACHED:
				var dir = attachedBody.transform.position - this.transform.position;
				dir.Normalize();
				attachedRigidBody.AddForce(new Vector2(dir.x, dir.y) * -5 * Time.fixedDeltaTime, ForceMode2D.Impulse);
				break;
		}


	}
	public void pullMe()
	{

	}
	public void pushMe()
	{

	}
	private void FixedUpdate()
	{
		phyiscsUpdate();
		var distance = attachedBody.transform.position - this.transform.position  ;
		var mag = distance.magnitude;
		if(mag > maximumLength)
		{
			//foot cannot hold the object anymore
			deAttach();
			var dir = distance.normalized;
			this.transform.position = attachedBody.transform.position -dir * maximumLength;
		}


		switch (status) {
			case STATUS.ATTACHING:
				var dir = targetPositionToAttach - this.transform.position;
				this.transform.position += dir.normalized * Time.deltaTime;
				if((targetPositionToAttach - this.transform.position).magnitude < 0.01)
				{
					status = STATUS.ATTACHED;
				}
				break;
			case STATUS.ATTACHED:
				break;
		}

	}
	// Update is called once per frame
	void Update()
	{

	}
}
