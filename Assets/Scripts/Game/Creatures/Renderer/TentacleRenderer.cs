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

	public Rigidbody2D tentacleHeadTracker;
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

	[SerializeField]
	float forceAppliedWhenKilled;

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
		tentacleHeadTracker.transform.position = verletRopeRenderer.ropeStart.position;
		tentaclePivotTracker.transform.position = verletRopeRenderer.ropeEnd.position;
		verletRopeRenderer.ropeStart = tentacleHeadTracker.transform;
		verletRopeRenderer.ropeEnd = tentaclePivotTracker.transform;

		tentacleHeadTracker.isKinematic = false;
		aliveRenderer.defaultLineColor = new Color(0.15f,0.15f,0.15f);
		aliveRenderer.highlightLineColor = new Color(0.3f, 0.15f, 0.15f);
		aliveRenderer.timeNeededToReachEnd = 10;

		//add force as a "dead animation"
		tentacleHeadTracker.isKinematic = false;
		tentacleHeadTracker.GetComponent<SpringJoint2D>().distance = tentacle.getTenatcleUnextendedLength();
		tentacleHeadTracker.GetComponent<SpringJoint2D>().enabled = true;
		//tentacleHeadTracker.AddForce((this.tentaclePivotTracker.position - this.tentacleHeadTracker.position).normalized * forceAppliedWhenKilled, ForceMode2D.Impulse);

		//remove all the events from the list
		tentacle.evntKill.Remove(hdlEntityKill);
		tentacle.evntChangedState.Remove(hdlTentacleChangedState);

	}
	
    // Update is called once per frame
    void Update()
    {
		if(tentacle.isActiveAndEnabled) tentacleHeadTracker.transform.position = tentacle.transform.position;
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
