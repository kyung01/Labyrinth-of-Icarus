using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethods 
{

	public static void LookAt2D(this Transform thisTransform, Vector3 otherTransformPosition)
	{

		var dir = (otherTransformPosition - thisTransform.position).normalized;
		var parent = thisTransform.parent;
		thisTransform.parent = null;
		thisTransform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * (180.0f / Mathf.PI));
		thisTransform.parent = parent;
	}
	public static Vector2 toVec2(this Vector3 v)
	{
		return new Vector2(v.x, v.y);
	}
	public static Vector3 toVec3(this Vector2 v)
	{
		return new Vector3(v.x, v.y, 0);
	}
	public static T GetComponentRecursively<T> (this GameObject obj) 
	{
		T component = obj.GetComponent<T>();
		if(component ==null && obj.transform.parent != null)
		{
			return GetComponentRecursively<T>(obj.transform.parent.gameObject);

		}
		return component;
	}
	public static bool CheckOwnerAndTarget(this Entity entity, List<Entity.OWNER_TYPE> ownerTypes, List<Entity.ENTITY_TYPE> entityTypes)
	{
		bool isCorrectOwner = false;
		bool isCorrectType = false;
		foreach (var ownerT in ownerTypes)
		{
			if (entity.ownerType == ownerT)
			{
				isCorrectOwner = true;
				break;
			}
		}
		if (!isCorrectOwner) return false;
		foreach (var entityT in entityTypes)
		{
			if (entity.type == entityT)
			{
				isCorrectType = true;
				break;
			}
		}
		return true;
		
	}
}
