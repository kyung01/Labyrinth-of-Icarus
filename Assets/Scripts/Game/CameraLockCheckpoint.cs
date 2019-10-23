using UnityEngine;
using System.Collections;

/// <summary>
/// Once player crosses this checkpoint Camera stops moving forward upto certain units
/// CameraLockCheckpoint is disabled once list of entity is destroyed
/// </summary>
public class CameraLockCheckpoint : MonoBehaviour
{
	public delegate void DelCameraLockCheckpoint(CameraLockCheckpoint self);
	[SerializeField]
	Vector2 cameraMaximumTravelDistance;
	protected bool isActivated = false;
	protected DelCameraLockCheckpoint evntActivated;
	protected bool isAllowedToBeActivated = true;
	// Use this for initialization
	void Start()
	{

	}
	void lockCamera()
	{
		if (!isAllowedToBeActivated) return;
		CameraDirector.self.lockPosition(this.transform.position, cameraMaximumTravelDistance);
	}
	// Update is called once per frame
	void Update()
	{
		if(Player.GetPlayerTruePosition().x > this.transform.position.x)
		{
			if (!isActivated)
			{
				if(evntActivated!=null)evntActivated(this);
				lockCamera();
				isActivated = false;
			}
		}
	}
}
