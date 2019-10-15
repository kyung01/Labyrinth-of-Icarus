using UnityEngine;
using System.Collections;

public class Foot : MonoBehaviour
{
	[SerializeField]
	Transform attachedBody;
	[SerializeField]
	float maximumLength = 1.0f;
	/// <summary>
	/// Targetting object can lost in case foot losess its connection
	/// </summary>
	Transform targettingObject;

	void attach(Transform target, float breakLimit = 1)
	{

	}

	// Use this for initialization
	void Start()
	{

	}
	public void loseTarget()
	{
		targettingObject = null;
	}
	private void FixedUpdate()
	{
		var distance = attachedBody.transform.position-this.transform.position  ;
		var mag = distance.magnitude;
		if(mag > maximumLength)
		{
			//foot cannot hold the object anymore
			loseTarget();
			var dir = distance.normalized;
			this.transform.position = attachedBody.transform.position+dir * maximumLength;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
