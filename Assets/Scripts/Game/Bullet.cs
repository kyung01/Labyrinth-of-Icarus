﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// bullets are triggers
/// bullets can hit World and WorldNotSelf
/// </summary>
public class Bullet : Entity
{
	[SerializeField]
	List<OWNER_TYPE> targetOwnerTypes = new List<OWNER_TYPE>();
	[SerializeField]
	List<ENTITY_TYPE> targetEntityTypes = new List<ENTITY_TYPE>();
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
	bool isMyTarget(Entity entity)
	{
		bool isRightOwnerType = false;
		bool isRightEntityType = false;
		
		foreach (var targetOwner in targetOwnerTypes) { 
			if (entity.ownerType == targetOwner)
			{
				isRightOwnerType = true;
				break;
			}
		}
		if (isRightOwnerType)
		{
			foreach (var targetType in targetEntityTypes)
			{
				if (entity.type == targetType)
				{
					isRightEntityType = true;
					break;
				}
			}
		}
		

		return isRightOwnerType && isRightEntityType;
	}
	public virtual void OnHitTarget(Entity entity)
	{

	}
	Entity getEntity(GameObject obj)
	{
		if (obj == null) return null;
		if (obj.gameObject.name == "Collider") return getEntity(obj.transform.parent.gameObject);
		return obj.GetComponent<Entity>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Entity entity;
		if(collision.gameObject.name == "Collider")
		{
			entity = getEntity(collision.gameObject);
		}
		else
		{
			entity = collision.gameObject.GetComponent<Entity>();
		}
		if (entity == null) return;
		if (!isMyTarget(entity)) return;
		//target found
		OnHitTarget(entity);

		
	}
}
