using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VerletRopeLineRenderer))]
[RequireComponent(typeof(AliveLineRenderer))]
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

	[SerializeField]
	Color colorFoundPlayer;
	[SerializeField]
	Color colorEngaged;

	[SerializeField]
	float foundPlayerTimerBegin;
	[SerializeField]
	float foundPlayerTimerDuration;
	[SerializeField]
	float foundPlayerTimerEnd;
	[SerializeField]
	float foundPlayerLineMin, foundPlayerLineMax;

	[SerializeField]
	float engagedPlayerTimerBegin;
	[SerializeField]
	float engagedPlayerTimerDuration;
	[SerializeField]
	float engagedPlayerTimerEnd;
	[SerializeField]
	float engagedPlayerLineMin, engagedPlayerLineMax;

	// Start is called before the first frame update
	void Start()
    {
		verletRopeRenderer = GetComponent<VerletRopeLineRenderer>();
		aliveRenderer = GetComponent<AliveLineRenderer>();
		var tentacleLength = tentacle.getTentacleLength();
		Debug.Log("TENTACLE LENGTH " + tentacleLength);
		verletRopeRenderer.GENERATED_NODE_COUNT =(int)( tentacleLength*extraLengthScaled + extraLengthAdded);

		verletRopeRenderer.ropeStart = tentacle.transform;
		verletRopeRenderer.ropeEnd = tentacle.pivotObject.transform;
		tentacle.evntKill.Add(hdlEntityKill);
		tentacle.evntChangedState.Add(hdlTentacleChangedState);
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

		//remove all the events from the list
		tentacle.evntKill.Add(hdlEntityKill);
		tentacle.evntChangedState.Add(hdlTentacleChangedState);

	}
	
    // Update is called once per frame
    void Update()
    {
		if(tentacle.isActiveAndEnabled) tentacleHeadAnimation.transform.position = tentacle.transform.position;
		//tentaclePivotTracker.transform.position = tentacle.pivotObject.transform.position;

	}
	void hdlTentacleChangedState(AIEntity entity)
	{
		if(entity.State == AIEntity.EntityState.FOUND_TARGET)
		{
			//turns yellow then strikes the player
			aliveRenderer.timeNeededToStart = foundPlayerTimerBegin;
			aliveRenderer.timeNeededToFinish = foundPlayerTimerEnd;
			aliveRenderer.timeNeededToReachEnd = foundPlayerTimerDuration;
			aliveRenderer.highlightLineColor = colorFoundPlayer;
			aliveRenderer.widthMin = foundPlayerLineMin;
			aliveRenderer.widthMax = foundPlayerLineMax;
		}
		if(entity.State == AIEntity.EntityState.ENGAGED)
		{
			aliveRenderer.timeNeededToStart = engagedPlayerTimerBegin;
			aliveRenderer.timeNeededToFinish = engagedPlayerTimerEnd;
			aliveRenderer.timeNeededToReachEnd = engagedPlayerTimerDuration;
			aliveRenderer.highlightLineColor = colorEngaged;
			aliveRenderer.widthMin = engagedPlayerLineMin;
			aliveRenderer.widthMax = engagedPlayerLineMax;

		}
	}

}
