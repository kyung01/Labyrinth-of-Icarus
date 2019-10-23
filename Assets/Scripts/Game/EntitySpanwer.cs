using UnityEngine;
using System.Collections;

/// <summary>
/// Spawns the particular Entity
/// </summary>
public class EntitySpanwer : EntityEnemy
{
	[SerializeField]
	Entity spanwedEntity;

	[SerializeField]
	float spawnInterval;

	float timeRemainingToSpawnEntity = 0;

	public override void Start()
	{
		base.Start();

		timeRemainingToSpawnEntity = spawnInterval;
	}
	
	public override void updateEngaged()
	{
		base.updateEngaged();
		timeRemainingToSpawnEntity -= Time.deltaTime;
		if (timeRemainingToSpawnEntity < 0)
		{
			timeRemainingToSpawnEntity = spawnInterval;
			var entity = Instantiate(spanwedEntity);
			entity.transform.position = this.transform.position;
			entity.transform.rotation = this.transform.rotation;
			var ai = entity.GetComponent<AIEntity>();
			if(ai != null)
			{
				ai.setState(AIEntity.EntityState.ENGAGED);
			}
		}
	}
}
