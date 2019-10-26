using UnityEngine;
using System.Collections;

//
public class MeatBall : AIEntity
{
	[SerializeField]
	float detectTimeInterval = 1;
	[SerializeField]
	float detectRange = 1;
	[SerializeField]
	float detectJumpForce = 1;
	[SerializeField]
	float reactionTime = 1;
	[SerializeField]
	float rollingTorque = 1;
	[SerializeField]
	float torqueApplyInterval = 1;

	float timeRemainingToReact = 0;

	float timeRemainingToDetect = 0;

	float timeRemainingToApplyTorque = 0;

	public override void Start()
	{
		base.Start();
		timeRemainingToDetect = detectTimeInterval;
		timeRemainingToReact = reactionTime;
		timeRemainingToApplyTorque = torqueApplyInterval;
	}
	/// <summary>
	/// MeatBall jumps every few 2~3 seconds until approached by the player
	/// Then Once player appraoches the meatball, meatball becomes alert
	/// </summary>
	public override void updateIDL()
	{
		base.updateIDL();
		timeRemainingToDetect -= Time.deltaTime;
		if (timeRemainingToDetect < 0)
		{
			timeRemainingToDetect = detectTimeInterval;
			detectPlayer();
		}
	}
	void detectPlayer()
	{
		var dis = Player.GetPlayerPosition() - this.transform.position;
		if(dis.sqrMagnitude < detectRange* detectRange)
		{
			//detected the player
			setState(EntityState.FOUND_TARGET);
			rigidbody.AddForce(this.transform.up * detectJumpForce, ForceMode2D.Impulse);
		}
	}
	/// <summary>
	/// Once MeatBall finds the targe, it waits certain time period, to give the player time to react
	/// </summary>
	public override void updateFoundTarget()
	{
		base.updateFoundTarget();
		timeRemainingToReact -= Time.deltaTime;
		if(timeRemainingToReact < 0)
		{
			timeRemainingToReact = reactionTime;
			State = EntityState.ENGAGED;
		}
	}
	/// <summary>
	/// Meatball rolls to the direction of the player by using torque 
	/// </summary>
	public override void updateEngaged()
	{
		base.updateEngaged();
	}
	private void FixedUpdate()
	{
		if(State == EntityState.ENGAGED)
		{
			timeRemainingToApplyTorque -= Time.fixedDeltaTime;
			if (timeRemainingToApplyTorque < 0)
			{
				timeRemainingToApplyTorque = torqueApplyInterval;
				float x = Player.GetPlayerPosition().x - this.transform.position.x;
				rigidbody.AddTorque(-x / Mathf.Abs(0.01f + x) * rollingTorque , ForceMode2D.Impulse);
			}
			
		}
	}

}
