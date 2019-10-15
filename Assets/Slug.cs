using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye
{
	public RaycastHit2D getSights(Vector2 eyeLocation, Vector2 direction)
	{
		var result = Physics2D.Raycast(eyeLocation, direction,10,LayerMask.GetMask("World"));

		return result;
	}
}
public class Slug : MonoBehaviour
{
	public Vector2 TARGET_POSITION = new Vector2(10, 0);
	public List<Foot> foots;
	// Start is called before the first frame update
	Eye eye = new Eye();
	void Start()
	{

	}
	/// <summary>
	/// Bigger the score means the bigger the distance
	/// </summary>
	/// <param name="calculatedPosition"></param>
	/// <param name="targetPosition"></param>
	/// <returns></returns>
	float hprCalculateScore(Vector2 calculatedPosition, Vector2 targetPosition)
	{
		return (targetPosition - calculatedPosition).magnitude;
	}
	int hprGetFittingPath(List<RaycastHit2D> pathCandidate, Vector2 targetPosition, Vector2 legOrigin, float legMaxDistance)
	{
		//bool foundFittingPath = false;
		int currentBestPath = -1;
		RaycastHit2D currentBestHit = pathCandidate[0];
		for (int i = 0; i < pathCandidate.Count; i++)
		{
			//Debug.Log("Candiate " + i + " " + pathCandidate[i].point);
			
			//Debug.Log("PASS");
			//compare who's better than
			if (currentBestPath != -1)
			{
				if ((currentBestHit.point - targetPosition).magnitude <
					(pathCandidate[i].point - targetPosition).magnitude)
				{
					//Debug.Log("Keeping the current path");
					//preiviously chosen current best bit is better result.
					//do not change 
				}
				else
				{
					//Debug.Log("Found a beetter path");
					if((currentBestHit.point - legOrigin ).magnitude < legMaxDistance)
					{
						Debug.Log("We thought "+(currentBestHit.point - legOrigin).magnitude);
						currentBestPath = i;
						currentBestHit = pathCandidate[i];

					}
				}
			}
			else
			{
				if ((pathCandidate[i].point - legOrigin).magnitude < legMaxDistance)
				{
					currentBestPath = i;
					currentBestHit = pathCandidate[i];
				}
				//Debug.Log("First ");
			}
		}
		return currentBestPath;
	}
	

	// Update is called once per frame
	void Update()
	{
		List<Vector2> eyeSightLines = new List<Vector2>();
		float radiance = Mathf.Atan2(-this.transform.up.y, -this.transform.up.x) - Mathf.PI * 0.5f;
		for (int i = 0; i < 10; i++)
		{
			float angle = radiance + Mathf.PI * (i / 10f);
			eyeSightLines.Add(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
		}
		var lookForward = eye.getSights(this.transform.position, this.transform.right);
		//var lookRight = eye.getSights(this.transform.position, this.transform.right);
		//var lookMiddle = eye.getSights(this.transform.position, (this.transform.up + this.transform.right).normalized);

		List<RaycastHit2D> availablePoints = new List<RaycastHit2D>() { lookForward };
		foreach (var sight in eyeSightLines)
		{
			availablePoints.Add(eye.getSights(this.transform.position, sight));
		}
		bool isALLHandsBusy = true;
		//Debug.Log("isALLHandsBusy" + isALLHandsBusy);
		for (int i = 0; i < foots.Count; i++)
		{
			var foot = foots[i];
			if (!foot.IsAvailable) break; ;
			isALLHandsBusy = false;
			//a foot is availble to use 
			int bestPlaceToPlaceFoot = hprGetFittingPath(availablePoints, TARGET_POSITION,new Vector2(foot.attachedBody.transform.position.x, foot.attachedBody.transform.position.y), foot.maximumLength);
			if (bestPlaceToPlaceFoot == -1)
			{
				Debug.Log("Foot placement fail");
				break; ;
			}
			Debug.Log("Chosen position " + availablePoints[bestPlaceToPlaceFoot].point);
			foot.attach(availablePoints[bestPlaceToPlaceFoot].transform, availablePoints[bestPlaceToPlaceFoot].point);

		}
		if (isALLHandsBusy)
		{
			float minimumScore = -9999;
			int selectedFoot = -1;
			//calculate the least perofrming hand than unlock it
			for (int i = 0; i < foots.Count; i++)
			{
				float tempScore = hprCalculateScore(foots[i].targetPositionToAttach, TARGET_POSITION);
				if (tempScore < minimumScore)
				{
					selectedFoot = i;
					minimumScore = tempScore;
				}
			}
			if (selectedFoot == -1)
			{
				//Debug.Log("Failed to release a foot WHY?");
			}
			else
			{
				Debug.Log("Releasing a foot " + selectedFoot);
				foots[selectedFoot].deAttach();
			}
			//scan the surrounding	
			//use unused foots first 
			//unlink lowest performing foor to locate towards better pefroming situations
		}
	}
}
