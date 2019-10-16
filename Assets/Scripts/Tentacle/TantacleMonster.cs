using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TantacleMonster : MonoBehaviour
{
	public List<TantacleFoot> foots;
	//public TantacleFoot FootL, FootR;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public RaycastHit2D getSights(Vector2 eyeLocation, Vector2 direction)
	{
		var result = Physics2D.Raycast(eyeLocation, direction, 10, LayerMask.GetMask("World"));

		return result;
	}
	void resolveFootDistance(TantacleFoot foot)
	{
		var dir = this.transform.position - foot.transform.position;
		float distanceNeedToBeMoved = dir.magnitude - 1;
		dir.Normalize();
		if (foot.holding)
		{
			Debug.Log("Foot " + foot.transform.position + " " + dir);
			this.GetComponent<Rigidbody2D>().MovePosition(new Vector2(foot.transform.position.x+dir.x, foot.transform.position.y+dir.y)); 

		}
		else
		{
			this.transform.position -= dir * 0.5f;
			foot.transform.position += dir * 0.5f;
		}

	}
	private void FixedUpdate()
	{
		
	}
	int sortMethod(RaycastHit2D a, RaycastHit2D b)
	{
		if (a.point.x < b.point.x) return -1;
		else if (a.point.x > b.point.x) return 1;
		return 0;
	}
	int sortMethod(TantacleFoot a, TantacleFoot b)
	{
		if (a.holding && !b.holding) return -1;
		if (b.holding && !a.holding) return 1;
		if (a.transform.position.x < b.transform.position.x) return -1;
		else if (a.transform.position.x > b.transform.position.x) return 1;
		return 0;
	}
	int waitTIme = 10;
	int noMatchFoundCount = 0;
	
	// Update is called once per frame
	void Update()
    {
		if (waitTIme-- >0)
		{
			return;
		}
		if(noMatchFoundCount > 10)
		{
			Debug.Log("Longer wait");
			waitTIme = 50;
			noMatchFoundCount = 0;
			for (int i = 0; i<foots.Count; i++)
			{
				foots[i].deAttach();
			}
			return;
		}
		waitTIme = 10;
		List<Vector2> eyeSightLines = new List<Vector2>();
		//float radiance = Mathf.Atan2(-this.transform.up.y, -this.transform.up.x) - Mathf.PI * 0.5f;
		for (int i = 0; i < 20; i++)
		{
			float angle = 2 * Mathf.PI * (i / 20.0f);
			eyeSightLines.Add(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
		};

		List<RaycastHit2D> availablePoints = new List<RaycastHit2D>() { };
		foreach (var sight in eyeSightLines)
		{
			availablePoints.Add(getSights(this.transform.position, sight));
			//Debug.Log(sight);
		}
		//sortest lowest value targets to highest value target
		availablePoints.Sort(sortMethod);
		//sorted lowest to highest
		foots.Sort(sortMethod);
		bool nofootmatcahFound = true;
		bool isAllFootsAttahced = true;
		bool aFootWasDeAttached = false;
		foreach(var foot in foots)
		{
			if (!foot.holding) isAllFootsAttahced = false;
		}
		if (isAllFootsAttahced)
		{
			for (int i = 0; i < foots.Count; i++)
			{
				if (!foots[i].holding) continue;
				else
				{
					Debug.Log("deattachged");
					foots[i].deAttach();
					aFootWasDeAttached = true;
					break;
				}
			}
		}
		//Debug.Log(availablePoints.Count + " , " + foots.Count);
		for (int i = availablePoints.Count-1; i >=0; i--)
		{
			//Debug.Log("Testing Point " + availablePoints[i].point);
			for(int j = 0; j <foots.Count ; j++)
			{
				if(!foots[j].holding&& foots[j].reach(availablePoints[i].point))
				{
					//Debug.Log("Using " + i + "/" + availablePoints.Count + " with joint " + j);
					nofootmatcahFound = false;
					noMatchFoundCount++;
					Debug.Log(noMatchFoundCount);
					break;
				}
				
			}
			if (!nofootmatcahFound)
			{
				if(!aFootWasDeAttached)
					noMatchFoundCount =0;
				break;
			}

		}
		float holdingFootsCount = 0;
		for(int i = 0; i< foots.Count; i++)
		{
			if (foots[i].holding) holdingFootsCount++;
		}
		if (holdingFootsCount == 0)
		{
			for(int i = 0; i < foots.Count; i++)
			{
				foots[i].pushPower = 0;
			}
		}
		else
		{
			for(int i = 0; i < foots.Count; i++)
			{
				if (foots[i].holding)
				{
					if(foots[i].transform.position.x > this.transform.position.x)
						foots[i].pushPower = -Physics2D.gravity.magnitude / holdingFootsCount;
					else
						foots[i].pushPower = Physics2D.gravity.magnitude / holdingFootsCount;

				}
					
			}
			
		}

		//TantacleFoot lowerGradeFoot = (FootL.transform.position.x < FootR.transform.position.x) ? FootL : FootR;
		//Vector2 targetPoint = availablePoints[availablePoints.Count - 1].point;
		//lowerGradeFoot.transform.position = new Vector3(targetPoint.x, targetPoint.y, 0);



	}
}
