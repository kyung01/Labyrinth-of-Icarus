using UnityEngine;
using System.Collections;

public class PlayerRenderer : MonoBehaviour
{
	[SerializeField]
	float legMovingSpeed;
	[SerializeField]
	Transform leftLegJoint, rightLegJoint;
	[SerializeField]
	Transform leftFoot, rightFoot;
	[SerializeField]
	Transform leftLeg, rightLeg;

	Vector3 previousPosition;
	Vector3 leftFootPosition, rightFootPosition;
	Vector3 leftFootPositionToMove, rightFootPositionToMove;
	// Use this for initialization
	void Start()
	{
		previousPosition = this.transform.position;
		leftFootPosition = leftLegJoint.transform.position;
		rightFootPosition = rightLegJoint.transform.position;
		leftFootPositionToMove = leftFootPosition;
		rightFootPosition = rightFootPositionToMove;
	}
	bool isLegOffGround(Vector3 jointPosition, Vector3 footPosition, float maxLegLength)
	{
		return (jointPosition - footPosition).magnitude > maxLegLength;
	}

	bool legAnimationFlip = false;
	float animationDurationMax = 0.10f;
	float animationDurationElapsedTime = 0;
	// Update is called once per frame
	void Update()
	{
		float LEG_MAX_EXTENDABLE_LENGTH = 1.5f;

		if (legAnimationFlip||(previousPosition - this.transform.position).magnitude > 0.0001f)
		{
			animationDurationElapsedTime += Time.deltaTime;
		}

		if (animationDurationElapsedTime > animationDurationMax)
		{
			legAnimationFlip = !legAnimationFlip;
			animationDurationElapsedTime = 0;
		}
		if (!legAnimationFlip)
		{
			//normal leg movement of standing
			var leftLegHittingTest = Physics2D.Raycast(leftLegJoint.transform.position, Vector2.down, 10.0f, LayerMask.GetMask("World"));
			leftFootPositionToMove = (leftLegHittingTest.transform == null) ? leftFootPosition : new Vector3(leftLegHittingTest.point.x, leftLegHittingTest.point.y, 0);

			var rightLegHittingTest = Physics2D.Raycast(rightLegJoint.transform.position, Vector2.down, 10.0f, LayerMask.GetMask("World"));
			rightFootPositionToMove = (rightLegHittingTest.transform == null) ? rightFootPosition : new Vector3(rightLegHittingTest.point.x, rightLegHittingTest.point.y, 0);


		}
		else
		{
			//try to mimic the animation of crossing leg
			var leftLegHittingTest = Physics2D.Raycast(rightLegJoint.transform.position, Vector2.down, 10.0f, LayerMask.GetMask("World"));
			leftFootPositionToMove = (leftLegHittingTest.transform == null) ? leftFootPosition : new Vector3(leftLegHittingTest.point.x, leftLegHittingTest.point.y, 0);

			var rightLegHittingTest = Physics2D.Raycast(leftLegJoint.transform.position, Vector2.down, 10.0f, LayerMask.GetMask("World"));
			rightFootPositionToMove = (rightLegHittingTest.transform == null) ? rightFootPosition : new Vector3(rightLegHittingTest.point.x, rightLegHittingTest.point.y, 0);

		}

		updateLeg(leftLegJoint.transform.position, LEG_MAX_EXTENDABLE_LENGTH, leftFootPositionToMove, legMovingSpeed, ref leftFootPosition);
		updateLeg(rightLegJoint.transform.position, LEG_MAX_EXTENDABLE_LENGTH, rightFootPositionToMove, legMovingSpeed, ref rightFootPosition);
		renderLeg(leftLeg, leftLegJoint.transform.position, leftFootPosition);
		renderLeg(rightLeg, rightLegJoint.transform.position, rightFootPosition);
		previousPosition = this.transform.position;


	}
	void renderLeg(Transform leg, Vector3 legFrom, Vector3 legTo)
	{
		Vector2 dir = new Vector2(legTo.x, legTo.y) - new Vector2(legFrom.x, legFrom.y);
		Vector3 legNewPosition = legFrom + new Vector3(dir.x, dir.y, 0) * 0.5f;
		float legLength = dir.magnitude;
		var legParent = leg.transform.parent;
		dir.Normalize();
		float angle = 90+Mathf.Atan2(dir.y, dir.x) * (180.0f/Mathf.PI);
		leg.transform.parent = null;
		leg.transform.position = legNewPosition;
		leg.transform.localScale = new Vector3(leg.transform.localScale.x,legLength, leg.transform.localScale.z);
		leg.transform.localRotation = Quaternion.Euler(0, 0, angle);		
		leg.transform.parent = legParent;
		//Debug.Log(angle);
	}
	void updateLeg(Vector3 jointPosition, float legMaxLength, Vector3 newFootLocation, float legMovingSpeed, ref Vector3 footPosition)
	{
		var dirMove = newFootLocation - footPosition;
		footPosition += dirMove * legMovingSpeed * Time.deltaTime;

		var dis = footPosition - jointPosition;
		float mag = dis.magnitude;
		if (mag > legMaxLength)
		{
			footPosition = jointPosition + dis.normalized * legMaxLength;
		}
	}
}
