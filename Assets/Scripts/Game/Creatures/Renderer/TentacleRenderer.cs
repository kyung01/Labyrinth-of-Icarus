using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VerletRopeLineRenderer))]
public class TentacleRenderer : MonoBehaviour
{
	[SerializeField]
	Tentacle tentacle;
	[SerializeField]
	float extraLengthAdded;
	[SerializeField]
	float extraLengthScaled;
	public GameObject tentacleOriginTracker;
	// Start is called before the first frame update
	void Awake()
    {
		var verletRopeRenderer = GetComponent<VerletRopeLineRenderer>();
		var tentacleLength = (tentacle.transform.position - tentacle.originPosition).magnitude;
		Debug.Log("TENTACLE LENGTH " + tentacleLength);
		verletRopeRenderer.GENERATED_NODE_COUNT =(int)( tentacleLength*extraLengthScaled + extraLengthAdded);
		tentacleOriginTracker = new GameObject();
		tentacleOriginTracker.name = "Tentacle Origin";
		verletRopeRenderer.ropeStart = tentacle.transform;
		verletRopeRenderer.ropeEnd = tentacleOriginTracker.transform;
		tentacleOriginTracker.transform.parent = this.transform;

	}

    // Update is called once per frame
    void Update()
    {
		tentacleOriginTracker.transform.position = tentacle.originPosition;

	}
}
