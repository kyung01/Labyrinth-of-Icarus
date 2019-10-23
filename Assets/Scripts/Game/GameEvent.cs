using UnityEngine;
using System.Collections;

public class GameEvent : MonoBehaviour
{
	public delegate void DelGameEvent(GameEvent self);

	[SerializeField]
	int remainingActivationChanceCount;
	public DelGameEvent evntActivated;
	
	public virtual void activate()
	{
		if(remainingActivationChanceCount-->0 && evntActivated != null)
		{
			activated();
			evntActivated(this);
		}
	}
	/// <summary>
	/// Use this method if you want to ensure that activation did 100% happen
	/// </summary>
	public virtual void activated()
	{

	}
}
