using UnityEngine;
using System.Collections;

public class CameraLockCheckpointEventRelease : CameraLockCheckpoint
{
	[SerializeField]
	GameEvent eventToReleaseCamera;

	private void Start()
	{
		eventToReleaseCamera.evntActivated = hdlActivation;
	}
	void hdlActivation(GameEvent gameEvent)
	{
		this.isAllowedToBeActivated = false;
		CameraDirector.self.releasePosition();
	}
}
