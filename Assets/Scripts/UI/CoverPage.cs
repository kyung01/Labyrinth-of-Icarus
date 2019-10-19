using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPage : UIPage
{
	[SerializeField]
	GameObject feather;
	Vector3		featherPosition;
	float	featherOrientation;
	private void Awake()
	{
		featherPosition = feather.transform.position;
		featherOrientation = feather.transform.rotation.eulerAngles.z ;
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		float tiltingAngle = 3;
		float floatingHeight = 0.3f;
		float floatingRatio = Mathf.Abs( Mathf.Sin(0.1f*Time.time));
		float tiltingRatio = Mathf.Sin(0.1f*Time.time);
		tiltingRatio = tiltingRatio * tiltingRatio;
		feather.transform.position = featherPosition + new Vector3(0, floatingHeight *floatingRatio, 0);
		feather.transform.rotation = Quaternion.Euler(new Vector3(0,0, featherOrientation + tiltingAngle * tiltingRatio));
    }
}
