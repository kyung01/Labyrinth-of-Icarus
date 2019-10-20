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
	float movementAcceleration;
	[SerializeField]
	float jumpAceeleration;
	Vector2 playerMovingDirection;
	// Start is called before the first frame update
	void Start()
    {
        
    }
	private void FixedUpdate()
	{
		body.AddForce(playerMovingDirection*movementAcceleration * Time.fixedDeltaTime, ForceMode2D.Impulse);
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
			body.AddForce(Vector2.up * jumpAceeleration);
		}
		//Debug.Log(playerMovingDirection);

	}
}
