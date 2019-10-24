﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VerletRope : MonoBehaviour
{
	[SerializeField]
	VerletLink PREFAB_LINK;
	[SerializeField]
	Vector2 gravity;

	protected List<VerletLink> links = new List<VerletLink>();
	List<GameObject> debugTracers = new List<GameObject>();

	// Use this for initialization
	public virtual void Start()
	{
		int LINK_COUNT = 10;
		for(int i = 0; i < LINK_COUNT; i++)
		{
			Dictionary<VerletLink, float> dicConnectedLinks = new Dictionary<VerletLink, float>();
			if (i != 0)
			{
				dicConnectedLinks.Add(links[i - 1], 1);
			}
			links.Add(Instantiate(PREFAB_LINK).init( this.transform.position, dicConnectedLinks) );
			//links[links.Count-1].transform.parent= this.transform
		}
		for(int i = 0; i < LINK_COUNT-1; i++)
		{
			links[i].connectedTo.Add(links[i + 1],1);
		}
		links[0].IsKinematic = true;
		links[links.Count - 1].IsKinematic = true;
	}

	// Update is called once per frame
	public virtual void FixedUpdate()
	{
		updateGravity();
		updateLinkConstratins();
		updateInnertia();
	}
	void updateGravity()
	{
		for(int i = 0;  i < links.Count; i++)
		{
			if (links[i].IsKinematic) continue;
			links[i].Position = links[i].Position+ gravity * Time.fixedDeltaTime;
		}
	}
	void updateLinkConstratins()
	{
		for (int i = 0; i < links.Count; i++)
		{
			if (links[i].IsKinematic) continue;
			var link = links[i];
			link.newPositions.Clear();
		}
		for (int i = 0; i < links.Count; i++)
		{
			if (links[i].IsKinematic) continue;
			var link = links[i];
			foreach(var constrain in link.connectedTo)
			{
				var otherLink = constrain.Key;
				var maximumDistance = constrain.Value;
				var dis = (link.Position-otherLink.Position);
				if ((link.Position-otherLink.Position ).sqrMagnitude > maximumDistance*maximumDistance)
				{
					//push both nodes 
					var dir = dis.normalized;
					if (otherLink.IsKinematic)
					{

						link.Position = otherLink.Position + (dir * maximumDistance );
					}
					else
					{
						var middleSection = (link.Position + otherLink.Position)*0.5f;
						var dirToLink = (link.Position - middleSection).normalized;
						link.Position = middleSection + (dirToLink * maximumDistance) * 0.5f;
						otherLink.Position = middleSection - (dirToLink * maximumDistance) * 0.5f;

					}
				}
			}

		}
		
	}

	void updateInnertia()
	{
		Vector2 acceleration = Vector2.zero;
		for (int i = 0; i < links.Count; i++)
		{
			if (links[i].IsKinematic) continue;
			var link = links[i];
			var velocity = link.Position - link.PreviousPosition;
			var newPreviousPosition = link.Position;
			link.Position += velocity * Time.fixedDeltaTime;// + acceleration * Time.fixedDeltaTime;
			link.PreviousPosition = newPreviousPosition;
		}
	}

	void KupdateInnertia()
	{
		Vector2 acceleration = Vector2.zero;
		for (int i = 0; i < links.Count; i++)
		{
			if (links[i].IsKinematic) continue;
			if (links[i].newPositions.Count == 0) continue;
			var link = links[i];
			Vector2 velocity = Vector2.zero;
			Vector2 allPositions = Vector2.zero;
			foreach(var newPosition in link.newPositions)
			{
				velocity += newPosition - link.PreviousPosition;
				allPositions += newPosition;
			}
			velocity += allPositions - link.PreviousPosition;

			var newPreviousPosition = link.Position;
			link.Position = allPositions;// allPositions + (velocity+ acceleration * Time.fixedDeltaTime) * Time.fixedDeltaTime;
			//Debug.Log("I WOULD LIKE TO MOVE TO " + (allPositions + (velocity*Time.fixedDeltaTime + acceleration * Time.fixedDeltaTime)));
			link.PreviousPosition = newPreviousPosition;
		}
	}
}
