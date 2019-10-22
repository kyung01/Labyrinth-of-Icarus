using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntitySmartList<T> where T : Entity
{
	List<T> list = new List<T>();
	public int Count { get { return list.Count; } }
	int currentIndex = 0;

	public void addEntity(T entity)
	{
		list.Add(entity);
	}
	/// <summary>
	/// returns just next entity in the list, no gurantee that it will be an available instance.
	/// </summary>
	/// <returns></returns>
	public T getNextEntity()
	{
		T entity = list[currentIndex];
		currentIndex = (currentIndex + 1) % list.Count;
		return entity;
	}
	public T getNextDeadEntity()
	{
		for (int i = 0; i < list.Count; i++)
		{
			var entity = getNextEntity();
			if (!entity.IsAlive) return entity;
		}
		return null;
	}
}