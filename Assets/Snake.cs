using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour
{
	[SerializeField]
	Rigidbody2D head, body, tail;
	[SerializeField]
	HingeJoint2D bodyJoint, tailJoint;
	bool isContracting = true;
	// Use this for initialization
	void Start()
	{

	}

	private void Update()
	{
		
	}
}
