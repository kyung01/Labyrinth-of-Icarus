using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Tentacle : MonoBehaviour
{
	public List<TentacleBody> bodies = new List<TentacleBody>();
	public List<TentacleLip> lips = new List<TentacleLip>();
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
	public void add(TentacleBody body)
	{
		body.transform.parent = this.transform;
		bodies.Add(body);

	}
	public void add(TentacleLip lip)
	{
		lip.transform.parent = this.transform;
		lips.Add(lip);

	}
}
