using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class AIEntity : Entity
{
	public delegate void DelAIEntity(AIEntity self); 
	public enum EntityState {IDL,PATROLLING,FOUND_TARGET,ENGAGED };
	protected EntityState state = EntityState.IDL;
	public List<DelAIEntity> evntChangedState = new List<DelAIEntity>();
	// Use this for initialization

	public virtual void setState(EntityState state)
	{
		this.state = state;
		for (int i = evntChangedState.Count - 1; i >= 0; i--) evntChangedState[i](this);
	}
	public virtual void Start()
	{

	}
	public virtual void FixedUpdate()
	{

		switch (state)
		{
			case EntityState.IDL:
				fixedUpdateIDL();
				break;
			case EntityState.PATROLLING:
				fixedUpdatePatrolling();
				break;
			case EntityState.ENGAGED:
				fixedUpdateEngaged();
				break;
			case EntityState.FOUND_TARGET:
				fixedUpdateFoundTarget();
				break;
		}
	}
	// Update is called once per frame
	public virtual void Update()
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


	public virtual void fixedUpdateIDL() {

	}
	public virtual void fixedUpdatePatrolling()
	{

	}
	public virtual void fixedUpdateEngaged()
	{

	}
	public virtual void fixedUpdateFoundTarget()
	{

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
