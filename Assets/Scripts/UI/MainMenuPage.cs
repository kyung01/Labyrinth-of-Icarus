using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuPage : UIPage
{
	[SerializeField]
	Transform bttnPlay, bttnLeaderboard, bttnOptions, bttnExit;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (isMouseHoveringOver(bttnPlay))
			{
				SceneManager.LoadScene("Gameplay",LoadSceneMode.Single);
			}
		}

	}
	bool isMouseHoveringOver(Transform transform)
	{
		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float xMin = transform.position.x - transform.lossyScale.x * 0.5f,
				xMax = transform.position.x + transform.lossyScale.x * 0.5f,
				yMin = transform.position.y - transform.lossyScale.y * 0.5f,
				yMax = transform.position.y + transform.lossyScale.y * 0.5f;
		if (mouseWorldPosition.x > xMin && mouseWorldPosition.x < xMax && mouseWorldPosition.y > yMin && mouseWorldPosition.y < yMax)
		{
			return true;
		}
		return false;
	}
}
