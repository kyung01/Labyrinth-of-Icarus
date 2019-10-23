using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class AIEntity : Entity
{
	public enum EntityState {IDL,PATROLLING,FOUND_TARGET,ENGAGED };
	protected EntityState state = EntityState.IDL;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		switch (state) {
			case EntityState.IDL:
				updateIDL();
				break;
			case EntityState.PATROLLING:
				updatePatrolling();
				break;
			case EntityState.ENGAGED:
				updateEngaged();
				break;
			case EntityState.FOUND_TARGET:
				updateFoundTarget();
				break;
		}
	}
	public virtual void updateIDL()
	{

	}
	public virtual void updateFoundTarget()
	{

	}
	public virtual void updatePatrolling()
	{

	}
	public virtual void updateEngaged()
	{

	}
}
