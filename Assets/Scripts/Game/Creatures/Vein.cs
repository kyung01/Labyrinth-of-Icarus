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
	[SerializeField]
	float nextConnectionSearchInterval;
	float timeElapsedForSearchingInterval;
	public List<Vector2> topPositions = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
		timeElapsedForSearchingInterval = nextConnectionSearchInterval;


	}

    // Update is called once per frame
    void Update()
    {
		timeElapsedForSearchingInterval -= Time.deltaTime;
		if (timeElapsedForSearchingInterval > 0) return;
		timeElapsedForSearchingInterval = nextConnectionSearchInterval;
		float upwardAngle = Random.Range(45, 45) * (Mathf.PI/180.0f);
		Vector2 dirBranching = new Vector2(Mathf.Cos(upwardAngle), Mathf.Sin(upwardAngle));
		var testResult =Physics2D.Raycast(this.transform.position, dirBranching, 1.0f, LayerMask.GetMask("World"));
		Vector2 nextBranchingSpot = testResult.point;
		Vector2 originPosition = (topPositions.Count == 0) ? (nextBranchingSpot - transform.position.toVec2()) : nextBranchingSpot - topPositions[topPositions.Count - 1];

		//as soon as tree comes in alive, it seeks possible ways to brach upward until it hits a wall
	}
	public override void respawn()
	{
		base.respawn();
	}
}
