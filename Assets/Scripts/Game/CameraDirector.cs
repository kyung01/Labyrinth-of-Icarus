using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// X position can be moved forward
/// Y position has no limit
/// </summary>
public class CameraDirector : MonoBehaviour
{
	[SerializeField]
	float MAXIMUM_DISTANCE;
	[SerializeField]
	float IDEAL_CHASING_DISTANCE;
	[SerializeField]
	float IDEAL_VERTICAL_INDENT;
	[SerializeField]
	float CAMERA_FOLLOWUP_SPEED;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		var idealCameraPosition = Player.GetPlayerTruePosition() + new Vector3(IDEAL_CHASING_DISTANCE, IDEAL_VERTICAL_INDENT, 0);
		var disRequiredToMove = idealCameraPosition - this.transform.position ;
		float xDirection =Mathf.Max(0,  disRequiredToMove.x /( 0.001f+Mathf.Abs(disRequiredToMove.x)));
		//Debug.Log(disRequiredToMove.x + " , " + (xDirection * CAMERA_FOLLOWUP_SPEED * Time.deltaTime));
		float xMove = Mathf.Min(disRequiredToMove.x , xDirection * CAMERA_FOLLOWUP_SPEED * Time.deltaTime);

		this.transform.position = new Vector3(this.transform.position.x+xMove, idealCameraPosition.y, idealCameraPosition.z);


		var disBetween = this.transform.position- idealCameraPosition;
		if(disBetween.magnitude > MAXIMUM_DISTANCE)
		{
			//Debug.Log("Distance between was " + disBetween.magnitude);
			this.transform.position = idealCameraPosition+ disBetween.normalized * MAXIMUM_DISTANCE;
			//Debug.Log("Normal was " + disBetween.normalized);
			//Debug.Log("Normal was " + disBetween.normalized);
			//Debug.Log("Distance between is " + (idealCameraPosition - this.transform.position).magnitude);
		}
	}
}
