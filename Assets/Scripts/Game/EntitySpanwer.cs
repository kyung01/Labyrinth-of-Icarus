using UnityEngine;
using System.Collections;

/// <summary>
/// Spawns the particular Entity
/// </summary>
public class EntitySpanwer : Entity
{
	[SerializeField]
	Entity spanwedEntity;

	[SerializeField]
	float spawnInterval;

	float timeRemainingToSpawnEntity = 0;

	// Use this for initialization
	void Start()
	{
		timeRemainingToSpawnEntity = spawnInterval;
	}

	// Update is called once per frame
	void Update()
	{
		timeRemainingToSpawnEntity -= Time.deltaTime;
		if(timeRemainingToSpawnEntity < 0)
		{
			timeRemainingToSpawnEntity = spawnInterval;
			var entity = Instantiate(spanwedEntity);
			entity.transform.position = this.transform.position;
			entity.transform.rotation = this.transform.rotation;
		}

	}
}
