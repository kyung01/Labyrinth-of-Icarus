using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
	static Player playerEntity;
	public static Vector3 GetPlayerPosition()
	{
		if(playerEntity== null)
		{
			return Vector3.zero;
		}
		return playerEntity.transform.position;
	}
	Dictionary<KeyCode, Vector2> dirPlayerInput = new Dictionary<KeyCode, Vector2>() {
		{ KeyCode.D, Vector2.right },{ KeyCode.A, Vector2.left }
	};

	[SerializeField]
	Rigidbody2D body;
	[SerializeField]
	Transform weapon;

	[SerializeField]
	float movementAcceleration,airControlAceeleration;

	[SerializeField]
	float jumpInitialAceeleration;
	[SerializeField]
	float jumpAceeleration;
	[SerializeField]
	float jumpDuration;
	[SerializeField]
	float jumpRemainingTime;
	[SerializeField]
	float fallAceeleration;
	Vector2 playerMovingDirection;

	// Start is called before the first frame update
	void Start()
    {
		playerEntity = this;
    }
	private void FixedUpdate()
	{
		var test = Physics2D.Raycast(this.transform.position, Vector2.down, 1.8f, LayerMask.GetMask("World"));
		bool isOnTheGround = test.transform != null;

		if(isOnTheGround)
		{
			body.AddForce(playerMovingDirection * movementAcceleration * Time.fixedDeltaTime, ForceMode2D.Impulse);
			body.drag = 1;
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
		if (jumpRemainingTime > 0)
		{
			jumpRemainingTime -= Time.fixedDeltaTime;

			body.AddForce(Vector2.up * jumpAceeleration*Time.fixedDeltaTime, ForceMode2D.Impulse);
		}
		else
		{
			if (!isOnTheGround)
			{

				body.AddForce(Vector2.down * fallAceeleration * Time.fixedDeltaTime, ForceMode2D.Impulse);
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
			jumpRemainingTime = jumpDuration;
			body.AddForce(Vector2.up * jumpInitialAceeleration, ForceMode2D.Impulse);
		}
		//Debug.Log(playerMovingDirection);
		weapon.transform.LookAt2D(Camera.main.ScreenToWorldPoint(Input.mousePosition));

	}
}
