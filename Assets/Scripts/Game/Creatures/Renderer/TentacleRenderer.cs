using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VerletRopeLineRenderer))]
public class TentacleRenderer : MonoBehaviour
{
	[SerializeField]
	Tentacle tentacle;
	[SerializeField]
	AliveLineRenderer aliveRenderer;
	[SerializeField]
	float extraLengthAdded;
	[SerializeField]
	float extraLengthScaled;
	VerletRopeLineRenderer verletRopeRenderer;

	public Rigidbody2D tentacleHeadAnimation;
	public Rigidbody2D tentaclePivotTracker;
	// Start is called before the first frame update
	void Start()
    {
		verletRopeRenderer = GetComponent<VerletRopeLineRenderer>();
		var tentacleLength = tentacle.getTentacleLength();
		Debug.Log("TENTACLE LENGTH " + tentacleLength);
		verletRopeRenderer.GENERATED_NODE_COUNT =(int)( tentacleLength*extraLengthScaled + extraLengthAdded);

		verletRopeRenderer.ropeStart = tentacle.transform;
		verletRopeRenderer.ropeEnd = tentacle.pivotObject.transform;
		tentacle.evntKill.Add(hdlEntityKill);
	}

	void hdlEntityKill(Entity entity)
	{
		Debug.Log("entity dead");
		tentacleHeadAnimation.transform.position = verletRopeRenderer.ropeStart.position;
		tentaclePivotTracker.transform.position = verletRopeRenderer.ropeEnd.position;
		verletRopeRenderer.ropeStart = tentacleHeadAnimation.transform;
		verletRopeRenderer.ropeEnd = tentaclePivotTracker.transform;

		tentacleHeadAnimation.isKinematic = false;
		aliveRenderer.defaultLineColor = Color.black;
		aliveRenderer.highlightLineColor = Color.grey;
		aliveRenderer.timeNeededToReachEnd = 10;

		//tentaclePivotTracker.isKinematic = false;

	}
    // Update is called once per frame
    void Update()
    {
		if(tentacle.isActiveAndEnabled) tentacleHeadAnimation.transform.position = tentacle.transform.position;
		//tentaclePivotTracker.transform.position = tentacle.pivotObject.transform.position;

	}
}
