using UnityEngine;
using System.Collections;

public class Flower : EntityEnemy
{
	[SerializeField]
	float timeRequiredToGrow;
	[SerializeField]
	float maximumGrowthSize;
	float timePassed = 0;
	public delegate void DelFlower(Flower flower);
	public DelFlower evntBloom;
	
	public override void respawn()
	{
		base.respawn();
		timePassed = 0;
	}
	public override void fixedUpdateEngaged()
	{
		base.fixedUpdateEngaged();

		timePassed += Time.fixedDeltaTime;
		var ratio = Mathf.Min(1, timePassed / timeRequiredToGrow);
		float growthScale = Mathf.Max(0, maximumGrowthSize - 1);
		this.transform.localScale = new Vector3(1 + ratio * growthScale, 1 + ratio * growthScale, 1);
		if (timePassed > timeRequiredToGrow)
		{
			if (evntBloom != null) evntBloom(this);
			kill();
		}
	}
}
