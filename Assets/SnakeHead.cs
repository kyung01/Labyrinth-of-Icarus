using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class SnakeHead : MonoBehaviour
{
	public Rigidbody2D body;

	// Use this for initialization
	void Awake()
	{
		body = GetComponent<Rigidbody2D>();

	}

	// Update is called once per frame
	void Update()
	{

	}
}
