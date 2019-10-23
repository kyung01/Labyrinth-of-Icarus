using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// vein starts from the ground like a tree
/// Their "head flower" is the only attackable are. They head fires bullet to the player.
/// The longer they are allowed to grow, more flowers they can grow that will attack the player
/// </summary>
public class Vein : Entity
{
	static float MAXIMUM_BRANCH_ITERATION = 60;
	static float MINIMUM_BRACNH_LENGTH = 0.1f;
	[SerializeField]
	float nextBranchSearchInterval;
	[SerializeField]
	float nextBranchMaxLength;

	float timeElapsedForSearchingInterval;
	public List<Vector2> extendedRelativePositions = new List<Vector2>();

	bool isReachedWorld = false;
	public bool IsDoneGrowing { get { return isReachedWorld; } }

	float isReachedWorldCountPower = 1;
	float iterationCount = 0;
    // Start is called before the first frame update
    void Start()
    {
		timeElapsedForSearchingInterval = nextBranchSearchInterval;


	}
	
    // Update is called once per frame
    void Update()
	{
		if (isReachedWorld)
		{
			//I have already reached the world
			return;
		}

		timeElapsedForSearchingInterval -= Time.deltaTime;
		if (timeElapsedForSearchingInterval > 0) return;
		if(iterationCount++ > MAXIMUM_BRANCH_ITERATION)
		{
			isReachedWorld = true;
		}


		//Debug.Log("veing activated");
		timeElapsedForSearchingInterval = nextBranchSearchInterval;
		Vector2 dirBranchingForward;
		if (extendedRelativePositions.Count == 0)
		{
			dirBranchingForward = this.transform.right;
		}
		else
		{
			dirBranchingForward = extendedRelativePositions[0].normalized;
		}
		float branchingForwardAngle = Mathf.Atan2(dirBranchingForward.y, dirBranchingForward.x);
		//Debug.Log(dirBranchingForward + " " +branchingForwardAngle * (180f/Mathf.PI));
		float upwardAngle = branchingForwardAngle
			+ Random.Range(-0.3f * Mathf.PI, 0.3f * Mathf.PI);
		Vector2 dirBranching = new Vector2(Mathf.Cos(upwardAngle), Mathf.Sin(upwardAngle));
		//Debug.Log(dirBranching + " " + (upwardAngle * (180f/Mathf.PI))) ;	


		Vector2 lastBranchPosition = this.transform.position;
		foreach (var position in extendedRelativePositions) lastBranchPosition += position;
		var testResult = Physics2D.Raycast(lastBranchPosition, dirBranching, nextBranchMaxLength, LayerMask.GetMask("World"));
		bool didHitAnyTarget = testResult.transform != null;
		Vector2 extendedLength;
		if (didHitAnyTarget)
		{
			isReachedWorld = true;
			extendedLength = testResult.point - lastBranchPosition;
			//Debug.Log("hit the world");
		}
		else
		{
			extendedLength = dirBranching * nextBranchMaxLength;
			//Debug.Log("Not hit the world");
		}
		if (extendedLength.magnitude > MINIMUM_BRACNH_LENGTH)
			extendedRelativePositions.Add(extendedLength);
		else
		{
		}
	}

	void KUpdate(
		Vector3 thisTransformPosition,
		Vector3 thisTransformForward,
		List<Vector2> extendedRelativePositions,
		float nextBranchMaxLength,
		ref bool isReachedWorld,
		ref float nextBranchSearchInterval,
		ref float timeElapsedForSearchingInterval
		)
	{
		timeElapsedForSearchingInterval -= Time.deltaTime;
		if (timeElapsedForSearchingInterval > 0) return;

		if (isReachedWorld)
		{
			//I have already reached the world
			return;
		}

		//Debug.Log("veing activated");
		timeElapsedForSearchingInterval = nextBranchSearchInterval;
		Vector2 dirBranchingForward;
		if (extendedRelativePositions.Count == 0)
		{
			dirBranchingForward = thisTransformForward;
		}
		else
		{
			dirBranchingForward = extendedRelativePositions[0].normalized;
		}
		float branchingForwardAngle = Mathf.Atan2(dirBranchingForward.y, dirBranchingForward.x);
		//Debug.Log(dirBranchingForward + " " +branchingForwardAngle * (180f/Mathf.PI));
		float upwardAngle = branchingForwardAngle
			+ Random.Range(-0.3f * Mathf.PI, 0.3f * Mathf.PI);
		Vector2 dirBranching = new Vector2(Mathf.Cos(upwardAngle), Mathf.Sin(upwardAngle));
		//Debug.Log(dirBranching + " " + (upwardAngle * (180f/Mathf.PI))) ;	


		Vector2 lastBranchPosition = thisTransformPosition;
		foreach (var position in extendedRelativePositions) lastBranchPosition += position;
		var testResult = Physics2D.Raycast(lastBranchPosition, dirBranching, nextBranchMaxLength, LayerMask.GetMask("World"));
		bool didHitAnyTarget = testResult.transform != null;
		Vector2 extendedLength;
		if (didHitAnyTarget)
		{
			isReachedWorld = true;
			extendedLength = testResult.point - lastBranchPosition;
			//Debug.Log("hit the world");
		}
		else
		{
			extendedLength = dirBranching * nextBranchMaxLength;
			//Debug.Log("Not hit the world");
		}
		if (extendedLength.magnitude > MINIMUM_BRACNH_LENGTH)
			extendedRelativePositions.Add(extendedLength);
		else
		{
		}
	}
	public override void respawn()
	{
		base.respawn();
	}
}
