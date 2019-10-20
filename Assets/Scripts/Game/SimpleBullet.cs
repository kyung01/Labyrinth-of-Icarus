using UnityEngine;
using System.Collections;

public class SimpleBullet : Bullet
{
	[SerializeField]
	public Rigidbody2D rigidBody;

	[SerializeField]
	int allowedInteractionCount;
	[SerializeField]
	float dealtHPChange;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
	public override void setActive(bool value)
	{
		base.setActive(value);
		if (value)
		{
			rigidBody.velocity = Vector2.zero;
		}
	}
	public override void OnHitTarget(Entity entity)
	{
		base.OnHitTarget(entity);
		entity.changeHpBy(dealtHPChange);
		allowedInteractionCount--;
		if(allowedInteractionCount<= 0)
		{
			kill();
		}
	}
}
