using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class SnakeBody : MonoBehaviour
{
	public Rigidbody2D body;

	private void Awake()
	{
		body = GetComponent<Rigidbody2D>();
	}
	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{

	}
}
