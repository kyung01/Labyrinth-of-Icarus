using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data",menuName ="ScriptableObjects/Prefab Bank",order = 1)]
public class PrefabBank : ScriptableObject
{
	[SerializeField]
	public SimpleBullet playerBullet;
	[SerializeField]
	public Seed seed;
	[SerializeField]
	public Vein Vein;
	[SerializeField]
	public TumorCore tumorCore;
	[SerializeField]
	public Tumor tumor;
	// Use this for initialization
	public T get<T>() where T:Entity
	{
		if (typeof(T) == typeof(SimpleBullet))
		{
			return playerBullet as T;

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
