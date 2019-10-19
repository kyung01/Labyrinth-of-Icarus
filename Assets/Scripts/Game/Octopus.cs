using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Octopus : MonoBehaviour
{
	[SerializeField]
	public List<Rigidbody2D> joints = new List<Rigidbody2D>();
	// Use this for initialization
	void Start()
	{

	}

	private void FixedUpdate()
	{
		this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10 * Time.deltaTime, ForceMode2D.Impulse);
	}
	// Update is called once per frame
	void Update()
	{

	}
}
