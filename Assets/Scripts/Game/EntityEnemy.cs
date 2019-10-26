using UnityEngine;
using System.Collections;

/// <summary>
/// Basic class used for enemies.
/// It has a cycle of behaviour that's widely used 
/// </summary>
public class EntityEnemy : AIEntity
{

	[SerializeField]
	float detectTimeInterval = 1;
	[SerializeField]
	float detectRange = 1;
	[SerializeField]
	float reactionTime = 1;

	float timeRemainingToReact = 0;

	float timeRemainingToDetect = 0;

	float timeRemainingToApplyTorque = 0;

	public override void Start()
	{
		base.Start();

		timeRemainingToDetect = detectTimeInterval;
		timeRemainingToReact = reactionTime;
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
		float detectAccuracy = 1.0f;
		var dis = Player.GetPlayerPosition() - this.transform.position;
		if (dis.sqrMagnitude < detectRange * detectRange)
		{
			//range is close enough do the raycast testing
			var rayTest = Physics2D.Raycast(this.transform.position, dis.normalized, 100, Layers.World);
			if((Player.GetPlayerPosition().toVec2() - rayTest.point).sqrMagnitude < detectAccuracy* detectAccuracy)
			{
				evntDetectedPlayer();
				this.State = EntityState.FOUND_TARGET;

			}
			//detected the player
		}
	}
	public virtual void evntDetectedPlayer()
	{

	}
	/// <summary>
	/// Once MeatBall finds the targe, it waits certain time period, to give the player time to react
	/// </summary>
	public override void updateFoundTarget()
	{
		base.updateFoundTarget();
		timeRemainingToReact -= Time.deltaTime;
		if (timeRemainingToReact < 0)
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
}
