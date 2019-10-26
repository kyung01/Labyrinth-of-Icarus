using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class VerletLink : MonoBehaviour
{
	public VerletLink init(Vector2 position, Dictionary<VerletLink, float> connectedTo)
	{
		body.MovePosition(position);
		this.connectedTo = connectedTo;
		return this;
	}
	[SerializeField]
	Rigidbody2D body;
	public bool IsKinematic = false;
	public Dictionary<VerletLink, float> connectedTo;
	public Vector2 velocity, acceleration;
	public Vector2 PreviousPosition;
	public Vector2 Position { set {  body.position =value; } get { return body.position; } }
	public List<Vector2> newPositions = new List<Vector2>();

	public bool isConstraintsSatisfied()
	{
		float differenceAllowed = 0.5f;
		foreach(var constraint in connectedTo)
		{
			if((constraint.Key.Position - this.Position).sqrMagnitude > differenceAllowed +constraint.Value* constraint.Value)
			{
				//Debug.Log((constraint.Key.Position - this.Position).magnitude);
				return false;
			}
		}
		return true;
	}

}
