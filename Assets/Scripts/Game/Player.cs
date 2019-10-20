using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	Dictionary<KeyCode, Vector2> dirPlayerInput = new Dictionary<KeyCode, Vector2>() {
		{ KeyCode.D, Vector2.right },{ KeyCode.A, Vector2.left }
	};

	[SerializeField]
	Rigidbody2D body;
	[SerializeField]
	float movementAcceleration,airControlAceeleration;

	[SerializeField]
	float jumpAceeleration;
	Vector2 playerMovingDirection;
	// Start is called before the first frame update
	void Start()
    {
        
    }
	private void FixedUpdate()
	{
		var test = Physics2D.Raycast(this.transform.position, Vector2.down, 1.8f, LayerMask.GetMask("World"));

		if(test.transform!= null)
		{

			body.AddForce(playerMovingDirection * movementAcceleration * Time.fixedDeltaTime, ForceMode2D.Impulse);
		}
		else
		{
			body.AddForce(playerMovingDirection * airControlAceeleration * Time.fixedDeltaTime, ForceMode2D.Impulse);

		}
		if (playerMovingDirection.x == 0 && playerMovingDirection.y == 0)
		{
			//player is trying to stop
			//stop if there is a ground underneath me
			//Debug.Log(test.transform);
			if (test.transform != null)
			{
				Vector2 velocyThatCanBeCancelled = new Vector2(body.velocity.x, 0);
				body.AddForce(-velocyThatCanBeCancelled * 1/(0.15f) * Time.fixedDeltaTime, ForceMode2D.Impulse);

			}
		}
	}

	// Update is called once per frame
	void Update()
    {
		playerMovingDirection = new Vector2();
		foreach (var playerInputInfo in dirPlayerInput)
		{
			if (Input.GetKey(playerInputInfo.Key)) playerMovingDirection += playerInputInfo.Value;
		}
		playerMovingDirection.Normalize();
		if (Input.GetKeyDown(KeyCode.W))
		{
			body.AddForce(Vector2.up * jumpAceeleration,ForceMode2D.Impulse );
		}
		//Debug.Log(playerMovingDirection);

	}
}
