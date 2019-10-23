using UnityEngine;
using System.Collections.Generic;

public class DestroyEvent : GameEvent
{
	[SerializeField]
	List<Entity> entitiesToDestroy;

	[SerializeField]
	List<GameObject> gameObjectToDisable;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		foreach(var entity in entitiesToDestroy)
		{
			if (entity.IsAlive) return;
		}
		activate();
		

	}
	public override void activated()
	{
		base.activated();
		foreach (var obj in gameObjectToDisable)
		{
			obj.SetActive(false);
		}
	}
}
