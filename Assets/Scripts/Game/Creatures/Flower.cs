using UnityEngine;
using System.Collections;

public class Flower : Entity
{
	[SerializeField]
	float timeRequiredToGrow;
	[SerializeField]
	float maximumGrowthSize;
	float timePassed = 0;
	public delegate void DelFlower(Flower flower);
	public DelFlower evntBloom;

	// Use this for initialization
	void Start()
	{

	}
	public override void respawn()
	{
		base.respawn();
		timePassed = 0;
	}
	// Update is called once per frame
	void Update()
	{
		timePassed += Time.deltaTime;
		var ratio = Mathf.Min(1, timePassed / timeRequiredToGrow);
		float growthScale = Mathf.Max(0, maximumGrowthSize - 1);
		this.transform.localScale = new Vector3(1+ratio * growthScale, 1+ratio * growthScale, 1);
		if(timePassed > timeRequiredToGrow)
		{
			if(evntBloom!=null)evntBloom(this);
			kill();
		}
	}
}
