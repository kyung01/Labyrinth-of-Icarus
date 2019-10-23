using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// X position can be moved forward
/// Y position has no limit
/// </summary>
public class CameraDirector : MonoBehaviour
{
	public static CameraDirector self;
	public enum CameraDirectorState {NORMAL,LOCKED,CUSTOM }
	[SerializeField]
	float MAXIMUM_DISTANCE;
	[SerializeField]
	float IDEAL_CHASING_DISTANCE;
	[SerializeField]
	float IDEAL_VERTICAL_INDENT;
	[SerializeField]
	float CAMERA_FOLLOWUP_SPEED;

	CameraDirectorState state = CameraDirectorState.NORMAL;

	Vector3 cameraLockedPosition = Vector3.zero;
	Vector2 cameraLockDistance = Vector3.zero;

	private void Awake()
	{
		self = this;
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

	// Update is called once per frame
	void Update()
	{
		switch (state) {
			case CameraDirectorState.NORMAL:
				updateCameraNormal();
				break;
			case CameraDirectorState.LOCKED:
				updateCameraLocked();
				break;
			}
	}

	void updateCameraNormal()
	{
		Vector3 cameraTargetPosition = Player.GetPlayerTruePosition() + new Vector3(IDEAL_CHASING_DISTANCE, IDEAL_VERTICAL_INDENT, 0);
		UpdateCameraFollowTarget(cameraTargetPosition);
	}
	void updateCameraLocked()
	{
		Vector3 cameraTargetPosition = Player.GetPlayerTruePosition() + new Vector3(IDEAL_CHASING_DISTANCE, IDEAL_VERTICAL_INDENT, 0);
		Vector3 distance = cameraTargetPosition - cameraLockedPosition;
		//float xDir = distance.x / (0.01f + Mathf.Abs(distance.x));
		//float yDir = distance.y / (0.01f + Mathf.Abs(distance.y));
		UpdateCameraFollowTarget(cameraTargetPosition);



		if (Mathf.Abs(distance.x) > Mathf.Abs(cameraLockDistance.x))
		{
			this.transform.position = new Vector3(
				cameraLockedPosition.x + (distance.x / (0.01f + Mathf.Abs(distance.x))) * Mathf.Abs(cameraLockDistance.x), 
				this.transform.position.y, this.transform.position.z);
		}
		if (Mathf.Abs(distance.y) > Mathf.Abs(cameraLockDistance.y))
		{
			this.transform.position = new Vector3(this.transform.position.x, 
				cameraLockedPosition.y + (distance.y / (0.01f + Mathf.Abs(distance.y))) * Mathf.Abs(cameraLockDistance.y), 
				this.transform.position.z);
		}


		if ((cameraLockDistance.x > 0 && this.transform.position.x < cameraLockedPosition.x)||
			(cameraLockDistance.x < 0 && this.transform.position.x > cameraLockedPosition.x)
			)
		{
			//Camera is either too forward or backward, lock the camera
			this.transform.position = new Vector3(cameraLockedPosition.x, this.transform.position.y, this.transform.position.z);
		}
		if ((cameraLockDistance.y > 0 && this.transform.position.y < cameraLockedPosition.y) ||
			(cameraLockDistance.y < 0 && this.transform.position.y > cameraLockedPosition.y)
			)
		{
			//Camera is either too forward or backward, lock the camera
			Debug.Log("Locking Y of Camera");
			this.transform.position = new Vector3( this.transform.position.x,cameraLockedPosition.y, this.transform.position.z);
		}
	}
	

	void UpdateCameraFollowTarget(Vector3 cameraTargetPosition)
    {
		var disRequiredToMove = cameraTargetPosition - this.transform.position ;
		float xDirection =Mathf.Max(0,  disRequiredToMove.x /( 0.001f+Mathf.Abs(disRequiredToMove.x)));
		//Debug.Log(disRequiredToMove.x + " , " + (xDirection * CAMERA_FOLLOWUP_SPEED * Time.deltaTime));
		float xMove = Mathf.Min(disRequiredToMove.x , xDirection * CAMERA_FOLLOWUP_SPEED * Time.deltaTime);

		this.transform.position = new Vector3(this.transform.position.x+xMove, cameraTargetPosition.y, cameraTargetPosition.z);


		var disBetween = this.transform.position- cameraTargetPosition;
		if(disBetween.magnitude > MAXIMUM_DISTANCE)
		{
			//Debug.Log("Distance between was " + disBetween.magnitude);
			this.transform.position = cameraTargetPosition+ disBetween.normalized * MAXIMUM_DISTANCE;
			//Debug.Log("Normal was " + disBetween.normalized);
			//Debug.Log("Normal was " + disBetween.normalized);
			//Debug.Log("Distance between is " + (idealCameraPosition - this.transform.position).magnitude);
		}
	}


	public void lockPosition(Vector3 origin, Vector2 cameraLockDistance)
	{
		this.state = CameraDirectorState.LOCKED;
		cameraLockedPosition = origin;
		this.cameraLockDistance = cameraLockDistance;
	}
	public void releasePosition()
	{
		this.state = CameraDirectorState.NORMAL;
	}
}
