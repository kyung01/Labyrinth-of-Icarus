using UnityEngine;
using System.Collections.Generic;

public class KKSNake : MonoBehaviour
{
	public SnakeHead head;
	public List<SnakeBody> bodies = new List<SnakeBody>();
	

	public void init(SnakeHead head, List<SnakeBody> bodies)
	{
		this.head = head;
		this.bodies = bodies;
		float bumperWidth = 0.01f;
		head.transform.parent = this.transform;
		head.transform.localPosition = Vector3.zero;
		head.transform.localRotation = Quaternion.identity;
		float previousWidthOfBody =-( bumperWidth + head.transform.localScale.x);
		for (int i = 0; i < bodies.Count; i++)
		{
			bodies[i].transform.parent = this.transform;
			bodies[i].transform.localRotation = Quaternion.identity;
			bodies[i].transform.localPosition = new Vector3(previousWidthOfBody, 0, 0);
			//Debug.Log("Body " + i + " Position : " + bodies[i].transform.localPosition);
			previousWidthOfBody -= bodies[i].transform.localScale.x+ bumperWidth;
			if (i == 0)
			{

				connect(bodies[i].gameObject,head.GetComponent<Rigidbody2D>());
			}
			else if (i !=0)
			{
				connect(bodies[i].gameObject, bodies[i-1].GetComponent<Rigidbody2D>());

			}
		}
		
	}
	void connect(GameObject connectingFrom, Rigidbody2D connectedTo)
	{
		//return;
		var joint = connectingFrom.AddComponent<HingeJoint2D>();
		joint.useLimits = true;
		joint.useMotor = true; // positive value means moving down
		joint.autoConfigureConnectedAnchor = false;
		joint.enableCollision = true;
		joint.connectedAnchor = new Vector2(-1, 0);
		JointAngleLimits2D newLimit = new JointAngleLimits2D();
		newLimit.min = -90;
		newLimit.max = 00;
		joint.limits = newLimit;
		joint.connectedBody = connectedTo;
	}
	// Use this for initialization
	void Start()
	{

	}
	public float contractionTime = 1.0f;
	// Update is called once per frame
	void FixedUpdate()
	{
		
	}
}
