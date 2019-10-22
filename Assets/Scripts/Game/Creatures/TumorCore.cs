using UnityEngine;
using System.Collections;

public class TumorCore : Entity	
{
	public delegate void DelFinishedGrwoing(TumorCore core);

	[SerializeField]
	float growingSpeed = 0;

	float growingPercentage = 0;
	public float GrwoingPercentage { get { return growingPercentage; } }
	public DelFinishedGrwoing evntFinishedGrowing;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		float maxGrowSize = 3.0f;
		growingPercentage += growingSpeed * Time.deltaTime;
		float ratio = Mathf.Min(1, growingPercentage);
		this.transform.localScale = new Vector3(1 + ratio* maxGrowSize, 1 + ratio* maxGrowSize, 1);
		if (growingPercentage > 1)
		{
			if (evntFinishedGrowing != null) evntFinishedGrowing(this);

		}
	}
}
