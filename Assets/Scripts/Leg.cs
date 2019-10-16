using UnityEngine;
using System.Collections;

public class Leg : MonoBehaviour
{

	public GameObject PinPoint;
	public GameObject foot;
	float MaximumDistance;
	private void Awake()
	{
		MaximumDistance = (foot.transform.position - PinPoint.transform.position).magnitude;
	}
	private void Update()
	{
		var distance = foot.transform.position - PinPoint.transform.position;
		Debug.Log(distance);
		if (distance.magnitude> MaximumDistance) 
		{
			var dir = (foot.transform.position - PinPoint.transform.position).normalized * MaximumDistance;
			foot.transform.position = dir;

		}
	}

}
