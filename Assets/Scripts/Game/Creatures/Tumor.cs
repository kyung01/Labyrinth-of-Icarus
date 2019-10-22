using UnityEngine;
using System.Collections;

public class Tumor : Entity
{
	[SerializeField]
	float forceChasingThePlayer;
	// Use this for initialization
	void Start()
	{

	}

	private void FixedUpdate()
	{
		var dir = (Player.GetPlayerPosition() - this.transform.position).normalized;
		rigidbody.AddForce(dir * forceChasingThePlayer * Time.fixedDeltaTime, ForceMode2D.Impulse);
	}
	// Update is called once per frame
	void Update()
	{
	}
}
