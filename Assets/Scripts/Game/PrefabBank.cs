using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data",menuName ="ScriptableObjects/Prefab Bank",order = 1)]
public class PrefabBank : ScriptableObject
{
	[SerializeField]
	public SimpleBullet simpleBullet;
	[SerializeField]
	public Seed seed;
	[SerializeField]
	public Vein vein;
	[SerializeField]
	public TumorCore tumorCore;
	[SerializeField]
	public Tumor tumor;
	[SerializeField]
	public Flower flower;
	// Use this for initialization
	public T get<T>() where T:Entity
	{
		if (typeof(T) == typeof(SimpleBullet))
		{
			return simpleBullet as T;

		}
		if (typeof(T) == typeof(Seed))
		{
			return seed as T;

		}
		if (typeof(T) == typeof(TumorCore))
		{
			return tumorCore as T;

		}
		if (typeof(T) == typeof(Vein))
		{
			return vein as T;

		}
		if (typeof(T) == typeof(Tumor))
		{
			return tumor as T;

		}
		return null;
	}
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
