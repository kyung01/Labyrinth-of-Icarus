using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CustomCamera : MonoBehaviour
{
	[SerializeField]
	float width;
	Camera camera;
	private void Awake()
	{
		camera = GetComponent<Camera>();
	}
	// Start is called before the first frame update
	void Start()
    {
		adjustCameraSize();

	}

    // Update is called once per frame
    void Update()
    {
		
        
    }

	void adjustCameraSize()
	{
		float orthographicSize = ((float)Screen.height / Screen.width) * (width/2.0f);
		if (orthographicSize < 15.0f) camera.orthographicSize = 15.0f;
		else camera.orthographicSize = orthographicSize;
	}

}
