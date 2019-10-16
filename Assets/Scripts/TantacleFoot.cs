using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TantacleFoot : MonoBehaviour
{
	public TantacleMonster monster;
	public Rigidbody2D monsterRigidBody;
	public Transform joint;
	public float maximumLength;
	public float pushPower = 1.0f;
	public bool holding = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
	public void deAttach()
	{
		holding = false;
		pushPower = 0;

	}
    // Update is called once per frame
    void FixedUpdate()
    {
		var dir = monsterRigidBody.transform.position- this.transform.position;//foot to head
		dir.Normalize();
		if(holding) monsterRigidBody.AddForce(dir * pushPower * Time.fixedDeltaTime, ForceMode2D.Impulse);
		var jointDirection = joint.transform.position - this.transform.position;
		if(jointDirection.magnitude > maximumLength)
		{
			jointDirection.Normalize();
			this.transform.position += jointDirection *10* Time.fixedDeltaTime;
			holding = false;
		}
    }
	public bool canReach(Vector3 point)
	{
		if(( point - joint.transform.position).magnitude > maximumLength)
		{
			//too far cannot reach
			return false;
		}
		return true;
	}
	public bool reach(Vector3 point)
	{
		if (!canReach(point)) return false;
		this.transform.position = point;
		holding = true;
		return true;
	}
}
