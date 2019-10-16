using UnityEngine;
using System.Collections;

public class TentacleLink : MonoBehaviour
{
	static float CLOSEUP_ACCELERATION = 10;
	static float AVOID_WORLD_COLLISION_SPEED = 1;
	[SerializeField]
	public TentacleLink before;
	[SerializeField]
	public TentacleLink after;
	// Use this for initialization
	public float maxDistance = 0.5f;

	bool isTryingToAvoidCollision = false;
	Vector3 obstacleDirection = Vector3.zero;
	Vector3 directionIOriginalIntendedToGo = Vector3.zero;
	
	void Start()
	{

	}
	private void FixedUpdate()
	{
		if (isTryingToAvoidCollision)
		{
			updateAvoidWorldCollision();
		}
		else if(before!= null)
		{
			var dir =  before.transform.position-this.transform.position;
			if (dir.magnitude > maxDistance)
			{
				//float the body
				//body.AddForce(body.gravityScale * -Physics2D.gravity * Time.fixedDeltaTime, ForceMode2D.Impulse);
				//float power = Mathf.Abs(body.gravityScale) * CLOSEUP_ACCELERATION;
				//Debug.Log(power);
				if (before.before == null)
					moveBy(dir.normalized * CLOSEUP_ACCELERATION * Time.fixedDeltaTime);
				else
				{
					moveBy(0.5f*dir.normalized * CLOSEUP_ACCELERATION * Time.fixedDeltaTime);
					before.moveBy(-0.5f * dir.normalized * CLOSEUP_ACCELERATION * Time.fixedDeltaTime);

				}
			}

		}
	}
	void moveBy(Vector3 amount)
	{
		MoveTo(this.transform.position + amount);

	}
	void MoveTo(Vector3 newPosition)
	{
		var dir = newPosition - this.transform.position;
		directionIOriginalIntendedToGo = dir.normalized;
		//check if anything is colliding;
		var raycast = Physics2D.Raycast(this.transform.position, dir.normalized,dir.magnitude,LayerMask.GetMask("World") );
		if (raycast.transform == null)
		{
			//nothing is colliding
			this.transform.position = newPosition;
		}
		else
		{
			isTryingToAvoidCollision = true;
			Vector2 perpendicularNormalAxis = new Vector2(raycast.normal.y, -raycast.normal.x);
			Vector3 insertedVector = newPosition - new Vector3(raycast.point.x, raycast.point.y, 0);
			float magnitudeInsertedAlongPlaneSide = Vector2.Dot(new Vector2(insertedVector.x, insertedVector.y), perpendicularNormalAxis);
			Vector2 slidedPositon = raycast.point + perpendicularNormalAxis * magnitudeInsertedAlongPlaneSide;
			this.transform.position = slidedPositon;
			//this.transform.position = new Vector3(slidedPositon.x + raycast.normal.x * 0.1f, slidedPositon.y + raycast.normal.y * 0.3f, 0);

			//var directionToAvoidCollision =  new Vector3(raycast.point.x, raycast.point.y, 0)- this.transform.position ;
			//this.obstacleDirection = directionToAvoidCollision.normalized;
			//updateAvoidWorldCollision();

		}
	}
	void updateAvoidWorldCollision()
	{
		var dir = directionIOriginalIntendedToGo;
		float movedMagnitude = AVOID_WORLD_COLLISION_SPEED * Time.fixedDeltaTime;
		//Vector3 newPosition = this.transform.position + dir * movedMagnitude;
		var raycast = Physics2D.Raycast(this.transform.position, dir, movedMagnitude, LayerMask.GetMask("World"));
		if (raycast.transform == null)
		{
			//nothing is colliding
			this.transform.position = this.transform.position + dir * movedMagnitude;
			isTryingToAvoidCollision = false;
		}
		else
		{
			Debug.Log("Trying to resolve Collision");
			isTryingToAvoidCollision = true;
			Vector2 perpendicularNormalAxis = new Vector2(raycast.normal.y, -raycast.normal.x);
			Vector3 insertedVector = (dir * movedMagnitude) ;
			float magnitudeInsertedAlongPlaneSide = Vector2.Dot(new Vector2(insertedVector.x, insertedVector.y), perpendicularNormalAxis);
			if(magnitudeInsertedAlongPlaneSide< movedMagnitude)
			{
				magnitudeInsertedAlongPlaneSide = movedMagnitude;
			}
			Vector2 slidedPositon = raycast.point + perpendicularNormalAxis * magnitudeInsertedAlongPlaneSide;
			//this.transform.position = new Vector3(slidedPositon.x + raycast.normal.x * 0.1f, slidedPositon.y + raycast.normal.y * 0.3f, 0);

			this.transform.position = slidedPositon;
			//var directionToAvoidCollision =  new Vector3(raycast.point.x, raycast.point.y, 0)- this.transform.position ;
			//this.obstacleDirection = directionToAvoidCollision.normalized;
			//updateAvoidWorldCollision();

		}
	}
	// Update is called once per frame
	void Update()
	{

	}
}
