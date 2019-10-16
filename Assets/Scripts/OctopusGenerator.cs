using UnityEngine;
using System.Collections;

/// <summary>
/// Tentacle generator generates 8 legs
/// for each leg attach "sucker" along all the lips
/// </summary>
public class OctopusGenerator : MonoBehaviour
{
	[SerializeField]
	Octopus PREFAB_OCTOPUS;

	[SerializeField]
	TentacleGenerator PREFAB_TENTACLE_GENERATOR;
	// Use this for initialization
	void Start()
	{
		Octopus octopus = Instantiate(PREFAB_OCTOPUS);
		for(int i = 0; i < 3; i++)
		{
			/*

			var tentacle = PREFAB_TENTACLE_GENERATOR.generateTentacleLengthof(5);
			var firstTentacleBody = tentacle.bodies[0];
			var joint = octopus.joints[i];

			firstTentacleBody.transform.position = joint.transform.position;
			firstTentacleBody.transform.rotation = joint.transform.rotation;

			var hingeJoint = tentacle.bodies[0].gameObject.AddComponent<HingeJoint2D>();
			hingeJoint.autoConfigureConnectedAnchor = false;
			hingeJoint.anchor = new Vector2(-0.5f, 0);
			hingeJoint.connectedAnchor = new Vector2(0, 0);
			hingeJoint.useLimits = true;
			var jointLimit = new JointAngleLimits2D();
			jointLimit.min = -90;
			jointLimit.max = 90;
			hingeJoint.limits = jointLimit;
			hingeJoint.connectedBody = octopus.joints[i];
			
			 * */
		}
	}
	// Update is called once per frame
	void Update()
	{

	}
}
