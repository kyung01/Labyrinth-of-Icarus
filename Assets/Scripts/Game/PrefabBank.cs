﻿using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data",menuName ="ScriptableObjects/Prefab Bank",order = 1)]
public class PrefabBank : ScriptableObject
{
	[SerializeField]
	public SimpleBullet playerBullet;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}